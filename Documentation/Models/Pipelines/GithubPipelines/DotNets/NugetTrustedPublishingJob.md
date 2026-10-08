# NugetTrustedPublishingJob

`NugetTrustedPublishingJob` builds, packs and publishes NuGet packages to nuget.org using [NuGet Trusted Publishing](https://learn.microsoft.com/en-us/nuget/nuget-org/trusted-publishing).

You don't store a long-lived NuGet API key. Instead, the job asks GitHub for a short-lived OIDC token, and the `NuGet/login@v1` action exchanges that token for a NuGet API key. That key is valid for one hour and is used straight away to push the package.

`NugetTrustedPublishingJob` replaces `PublishJobV4`, which needed a NuGet API key passed in as `nugetApiKey`.

## Before you use it
You must set up both of these first, or the `NuGet Login` step will fail.

### 1. Create a Trusted Publishing policy on nuget.org
Sign in to nuget.org as the user or organisation that owns the package. Select your username, choose **Trusted Publishing**, and add a policy with these values:

| Field | Value |
|---|---|
| Repository Owner | The GitHub user or organisation, e.g. `contoso` |
| Repository | The repository name, e.g. `contoso-sdk` |
| Workflow File | The workflow file name only, e.g. `build.yml`. This is the file you pass to `SerializeAndWriteToFile`, without the `.github/workflows/` path. |
| Environment | Leave empty. This job doesn't set a GitHub Actions environment. |

Choose the scopes and package glob patterns that fit your packages.

For private repositories, nuget.org activates the policy temporarily for 7 days. A successful publish in that time makes it permanent. If the 7 days pass first, reactivate the policy on nuget.org.

For full details, see the NuGet documentation: [Trusted Publishing](https://learn.microsoft.com/en-us/nuget/nuget-org/trusted-publishing).

### 2. Add the `NUGET_USER` secret in GitHub
Create a GitHub Actions secret named `NUGET_USER` at either level:
- **Organisation level:** **Organisation → Settings → Secrets and variables → Actions**. Make sure the secret's **Repository access** includes the repository that publishes. By default, organisation secrets are available to private repositories only.
- **Repository level:** **Repository → Settings → Secrets and variables → Actions**.

Set its value to the nuget.org **profile name** of the policy owner, not an email address.

See [Using secrets in GitHub Actions](https://docs.github.com/en/actions/how-tos/write-workflows/choose-what-workflows-do/use-secrets) for details.

## What it does
1. Checks out the repository.
2. Sets up .NET.
3. Restores packages.
4. Builds in `Release` configuration.
5. Packs the NuGet package with symbols.
6. Calls `NuGet/login@v1` to get a short-lived API key.
7. Pushes the package to nuget.org with that key. Versions that already exist are skipped.

The job gets these permissions:
- `id-token: write`, so it can request the OIDC token;
- `contents: read`, so it can check out the code.

## Constructor parameters
| Parameter | Type | Description |
|---|---|---|
| `runsOn` | `string` | The runner to use, e.g. `BuildMachines.UbuntuLatest`. |
| `dependsOn` | `string` | The job that must succeed first. The publish runs only if it does. |
| `dotNetVersion` | `string` | The .NET SDK version to install, e.g. `10.x`. |
| `nugetUser` | `string` | The nuget.org profile name. Pass the secret expression `${{ secrets.NUGET_USER }}`. |

## Sample
This sample publishes after [TagJobV3](TagJobV3.md) has tagged and released.

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
        },
        {
            "publish",
            new NugetTrustedPublishingJob(
                runsOn: BuildMachines.UbuntuLatest,
                dependsOn: "add_tag",
                dotNetVersion: "10.x",
                nugetUser: "${{ secrets.NUGET_USER }}")
        }
    }
};

new ADotNetClient().SerializeAndWriteToFile(
    adoPipeline: githubPipeline,
    path: "../../../../.github/workflows/build.yml");
```

### Generated YAML (`publish` job)
```yaml
  publish:
    runs-on: ubuntu-latest
    needs:
    - add_tag
    if: needs.add_tag.result == 'success'
    steps:
    - name: Check out
      uses: actions/checkout@v5
    - name: Setup .Net
      uses: actions/setup-dotnet@v5
      with:
        dotnet-version: 10.x
    - name: Restore
      run: dotnet restore
    - name: Build
      run: dotnet build --no-restore --configuration Release
    - name: Pack NuGet Package
      run: dotnet pack --configuration Release --include-symbols
    - name: NuGet Login
      id: nuget_login
      uses: NuGet/login@v1
      with:
        user: ${{ secrets.NUGET_USER }}
    - name: Push NuGet Package
      run: dotnet nuget push **/bin/Release/**/*.nupkg --source https://api.nuget.org/v3/index.json --api-key ${{ steps.nuget_login.outputs.NUGET_API_KEY }} --skip-duplicate
    permissions:
      id-token: write
      contents: read
```

## Things to know
- **Allowed actions:** if your organisation restricts which actions can run, allow `NuGet/login@v1`.
- **Pull requests from forks:** these can't get an OIDC token, so publishing only works for pull requests raised from a branch in the same repository.
- **Removing old credentials:** once publishing works, delete any old NuGet API key secret from GitHub and revoke the key on nuget.org.
