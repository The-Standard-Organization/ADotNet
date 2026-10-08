# ExtractProjectPropertyTaskV2

`ExtractProjectPropertyTaskV2` is a step that reads one property, such as `<Version>` or `<PackageReleaseNotes>`, from a `.csproj` file. It makes the value available to later steps in the same job as a step output.

[TagJobV3](../TagJobV3.md) uses it to read the version and release notes, but you can use it in any job.

`ExtractProjectPropertyTaskV2` replaces `ExtractProjectPropertyTask`. On Windows runners, the original task wrote to `$GITHUB_OUTPUT`. PowerShell treats that as an undefined local variable, not the `GITHUB_OUTPUT` environment variable. So the step succeeded, but its output was silently empty. V2 writes to `$env:GITHUB_OUTPUT` instead. On other runners its output is unchanged.

## How it works
The script it generates depends on `runsOn`:

| Runner | Shell | How it reads the property |
|---|---|---|
| Any runner whose name starts with `windows`, e.g. `BuildMachines.WindowsLatest` | `pwsh` | `Select-Xml` |
| Any other runner, e.g. `BuildMachines.UbuntuLatest` | `bash` | `xmlstarlet`, installed with `apt-get` |

Because the non-Windows script uses `apt-get`, use an Ubuntu runner rather than macOS.

The value is written as a multiline step output, so values that span several lines, such as release notes, work. Later steps read it as `${{ steps.<id>.outputs.<stepVariableName> }}`.

## Constructor parameters
| Parameter | Type | Description |
|---|---|---|
| `name` | `string` | The step name shown in the workflow run. |
| `id` | `string` | The step id. Later steps use it to read the output. |
| `projectRelativePath` | `string` | Path to the `.csproj` file, relative to the repository root. |
| `propertyName` | `string` | The project property to read, e.g. `Version`. Use a property that appears only once in the file. |
| `stepVariableName` | `string` | The name of the output, e.g. `version_number`. |
| `runsOn` | `string` | The runner the job uses. Pass the same value as the job's `RunsOn`, so the step generates the right script. |

## Sample
```csharp
var githubPipeline = new GithubPipeline
{
    Name = "Show Version",

    OnEvents = new Events
    {
        Push = new PushEvent { Branches = ["main"] }
    },

    Jobs = new Dictionary<string, Job>
    {
        {
            "show_version",
            new Job
            {
                RunsOn = BuildMachines.UbuntuLatest,

                Steps = new List<GithubTask>
                {
                    new CheckoutTaskV5
                    {
                        Name = "Check out"
                    },

                    new ExtractProjectPropertyTaskV2(
                        name: "Extract Version",
                        id: "extract_version",
                        projectRelativePath: "MyProject/MyProject.csproj",
                        propertyName: "Version",
                        stepVariableName: "version_number",
                        runsOn: BuildMachines.UbuntuLatest),

                    new GithubTask
                    {
                        Name = "Display Version",
                        Run = "echo \"Version number: ${{ steps.extract_version.outputs.version_number }}\""
                    }
                }
            }
        }
    }
};

new ADotNetClient().SerializeAndWriteToFile(
    adoPipeline: githubPipeline,
    path: "../../../../.github/workflows/show-version.yml");
```

### Generated YAML on `ubuntu-latest`
```yaml
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
```

### Generated YAML on `windows-latest`
This is generated when both the job's `RunsOn` and the task's `runsOn` are `BuildMachines.WindowsLatest`:

```yaml
    - name: Extract Version
      id: extract_version
      run: >
        # Running on Windows 

        $version_number=((Select-Xml -Path 'MyProject/MyProject.csproj' -XPath '//Version').Node.InnerXML)

        echo "$version_number"

        echo "version_number<<EOF" >> $env:GITHUB_OUTPUT 

        echo "$version_number" >> $env:GITHUB_OUTPUT 

        echo "EOF" >> $env:GITHUB_OUTPUT 
      shell: pwsh
    - name: Display Version
      run: 'echo "Version number: ${{ steps.extract_version.outputs.version_number }}"'
```

## Upgrading from `ExtractProjectPropertyTask`
The constructor is the same, so replace `new ExtractProjectPropertyTask(` with `new ExtractProjectPropertyTaskV2(`. Ubuntu output doesn't change. Windows output now works.
