# TagJobV3

`TagJobV3` creates a Git tag and a GitHub release for a project. It runs after a release pull request is merged, and it reads the version and release notes from the project's `.csproj` file.

It uses the workflow's built-in `GITHUB_TOKEN` and gives the job `contents: write` permission. You don't need to create a personal access token (PAT) or store one as a secret.

`TagJobV3` replaces `TagJobV2`, which needed a PAT passed in as `githubToken`.

## When it runs
The job runs only when all of these are true:
- every job listed in `dependsOn` succeeded;
- the pull request was merged into `branchName`;
- the pull request title starts with `RELEASES:`;
- the pull request has the `RELEASES` label.

## What it does
1. Checks out the repository.
2. Configures the Git user.
3. Reads `<Version>` and `<PackageReleaseNotes>` from the project file, using [ExtractProjectPropertyTaskV2](Tasks/ExtractProjectPropertyTaskV2.md). On Windows runners it uses `Select-Xml` in PowerShell. On other runners it installs `xmlstarlet` with `apt-get`, so use an Ubuntu runner rather than macOS.
4. Creates and pushes an annotated tag `v<Version>`.
5. Creates a GitHub release called `Release - v<Version>`, with the package release notes as its body.

## Constructor parameters
| Parameter | Type | Description |
|---|---|---|
| `runsOn` | `string` | The runner to use, e.g. `BuildMachines.UbuntuLatest` or `BuildMachines.WindowsLatest`. |
| `dependsOn` | `string` or `string[]` | The job or jobs that must succeed first. |
| `projectRelativePath` | `string` | Path to the `.csproj` file that holds `<Version>` and `<PackageReleaseNotes>`, relative to the repository root. |
| `branchName` | `string` | The branch that release pull requests merge into, e.g. `main`. |

## Sample
```csharp
var githubPipeline = new GithubPipeline
{
    Name = "Build",

    OnEvents = new Events
    {
        Push = new PushEvent { Branches = ["main"] },

        PullRequest = new PullRequestEvent
        {
            Types = ["opened", "synchronize", "reopened", "closed"]
        }
    },

    Jobs = new Dictionary<string, Job>
    {
        {
            "build",
            new Job
            {
                // build and test steps
            }
        },
        {
            "add_tag",
            new TagJobV3(
                runsOn: BuildMachines.UbuntuLatest,
                dependsOn: "build",
                projectRelativePath: "MyProject/MyProject.csproj",
                branchName: "main")
            {
                Name = "Tag and Release"
            }
        }
    }
};

new ADotNetClient().SerializeAndWriteToFile(
    adoPipeline: githubPipeline,
    path: "../../../../.github/workflows/build.yml");
```

The pull request trigger must include the `closed` type, otherwise the job never sees the merge.

### Generated YAML (`add_tag` job)
```yaml
  add_tag:
    name: Tag and Release
    runs-on: ubuntu-latest
    needs:
    - build
    if: >-
      needs.build.result == 'success' && 

      github.event.pull_request.merged && 

      github.event.pull_request.base.ref == 'main' && 

      startsWith(github.event.pull_request.title, 'RELEASES:') && 

      contains(github.event.pull_request.labels.*.name, 'RELEASES')
    steps:
    - name: Checkout code
      uses: actions/checkout@v5
    - name: Configure Git
      run: >-
        git config user.name "GitHub Action"

        git config user.email "action@github.com"
    - name: Extract Version
      id: extract_version
      run: >
        # Running on Linux/Unix 

        sudo apt-get install xmlstarlet

        version_number=$(xmlstarlet sel -t -v "//Version" -n MyProject/MyProject.csproj)

        echo "$version_number"

        echo "version_number<<EOF" >> $GITHUB_OUTPUT 

        echo "$version_number" >> $GITHUB_OUTPUT 

        echo "EOF" >> $GITHUB_OUTPUT 
      shell: bash
    - name: Display Version
      run: 'echo "Version number: ${{ steps.extract_version.outputs.version_number }}"'
    - name: Extract Package Release Notes
      id: extract_package_release_notes
      run: >
        # Running on Linux/Unix 

        sudo apt-get install xmlstarlet

        package_release_notes=$(xmlstarlet sel -t -v "//PackageReleaseNotes" -n MyProject/MyProject.csproj)

        echo "$package_release_notes"

        echo "package_release_notes<<EOF" >> $GITHUB_OUTPUT 

        echo "$package_release_notes" >> $GITHUB_OUTPUT 

        echo "EOF" >> $GITHUB_OUTPUT 
      shell: bash
    - name: Display Package Release Notes
      run: 'echo "Package Release Notes: ${{ steps.extract_package_release_notes.outputs.package_release_notes }}"'
    - name: Create GitHub Tag
      run: >-
        git tag -a "v${{ steps.extract_version.outputs.version_number }}" -m "Release - v${{ steps.extract_version.outputs.version_number }}"

        git push origin --tags
    - name: Create GitHub Release
      uses: actions/create-release@v1
      with:
        tag_name: v${{ steps.extract_version.outputs.version_number }}
        release_name: Release - v${{ steps.extract_version.outputs.version_number }}
        body: >-
          ## Release - v${{ steps.extract_version.outputs.version_number }}


          ### Release Notes

          ${{ steps.extract_package_release_notes.outputs.package_release_notes }}
      env:
        GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
    permissions:
      contents: write
```

## Things to know
- **Protected tags:** if a tag ruleset protects `v*` tags, add GitHub Actions to its bypass list. Otherwise the tag push is rejected.
- **Organisation limits:** if your organisation or enterprise limits workflow token permissions, make sure `contents: write` is allowed.
- **Pull requests from forks:** these get a read-only `GITHUB_TOKEN`. Raise release pull requests from a branch in the same repository.
- **Other workflows aren't triggered:** tags pushed with `GITHUB_TOKEN` don't start other workflows. To publish after tagging, chain the next job with `dependsOn` in the same workflow, as [NugetTrustedPublishingJob](NugetTrustedPublishingJob.md) does.
