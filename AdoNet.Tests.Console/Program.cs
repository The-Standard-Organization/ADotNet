// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using ADotNet.Clients;
using ADotNet.Clients.Builders;
using ADotNet.Models.Pipelines.AdoPipelines.AspNets;
using ADotNet.Models.Pipelines.AdoPipelines.AspNets.Tasks.DotNetExecutionTasks;
using ADotNet.Models.Pipelines.AdoPipelines.AspNets.Tasks.PublishBuildArtifactTasks;
using ADotNet.Models.Pipelines.AdoPipelines.AspNets.Tasks.UseDotNetTasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV5s;

namespace ADotNet.Tests.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var adoClient = new ADotNetClient();

            var aspNetPipeline = new AspNetPipeline
            {
                TriggeringBranches = new List<string>
                {
                    "master"
                },

                VirtualMachinesPool = new VirtualMachinesPool
                {
                    VirtualMachineImage = VirtualMachineImages.Windows2019
                },

                ConfigurationVariables = new ConfigurationVariables
                {
                    BuildConfiguration = BuildConfiguration.Release
                },

                Tasks = new List<BuildTask>
                {
                    new UseDotNetTask
                    {
                        DisplayName = "Use DotNet 6.0",

                        Inputs = new UseDotNetTasksInputs
                        {
                            Version = "6.0.100-preview.6.21202.5",
                            IncludePreviewVersions = true,
                            PackageType = PackageType.sdk
                        }
                    },

                    new DotNetExecutionTask
                    {
                        DisplayName = "Restore",

                        Inputs = new DotNetExecutionTasksInputs
                        {
                            Command = Command.restore,
                            FeedsToUse = Feeds.select
                        }
                    },

                    new DotNetExecutionTask
                    {
                        DisplayName = "Build",

                        Inputs = new DotNetExecutionTasksInputs
                        {
                            Command = Command.build,
                        }
                    },

                    new DotNetExecutionTask
                    {
                        DisplayName = "Test",

                        Inputs = new DotNetExecutionTasksInputs
                        {
                            Command = Command.test,
                            Projects = "**/*Unit*.csproj"
                        }
                    },

                    new DotNetExecutionTask
                    {
                        DisplayName = "Publish",

                        Inputs = new DotNetExecutionTasksInputs
                        {
                            Command = Command.publish,
                            PublishWebProjects = true,
                            ZipAfterPublish = true,
                            Arguments = Arguments.DefaultBuildAndPublishConfigurations
                        }
                    },

                    new PublishBuildArtifactsTask
                    {
                        DisplayName = "Publish Artifacts",

                        Inputs = new PublishBuildArtifactsInputs
                        {
                            PathToPublish = PublishPaths.DefaultPathToPublish
                        },

                        Condition = Conditions.SucceededOrFailed
                    }
                }
            };

            var githubPipeline = new GithubPipeline
            {
                Name = "Github",

                OnEvents = new Events
                {
                    Push = new PushEvent
                    {
                        Branches = new string[] { "master" }
                    },
                    PullRequest = new PullRequestEvent
                    {
                        Branches = new string[] { "master" }
                    }
                },

                Jobs = new Dictionary<string, Job>
                {
                    {
                        "build",
                        new Job
                    {
                        Name = "Build",
                        RunsOn = BuildMachines.WindowsLatest,
                        EnvironmentVariables = new Dictionary<string, string>
                        {
                            { "AzureClientId", "${{ secrets.AZURECLIENTID }}" },
                            { "AzureTenantId", "${{ secrets.AZURETENANTID }}"},
                            { "AzureClientSecret", "${{ secrets.AZURECLIENTSECRET }}"},
                            { "AzureAdminName", "${{ secrets.AZUREADMINNAME }}"},
                            { "AzureAdminAccess", "${{ secrets.AZUREADMINACCESS }}"}
                        },

                        Steps = new List<GithubTask>
                        {
                            new CheckoutTaskV5
                            {
                                Name = "Check Out"
                            },

                            new SetupDotNetTaskV5
                            {
                                Name = "Setup Dot Net Version",

                                With = new TargetDotNetVersionV5
                                {
                                    DotNetVersion = "10.x",
                                }
                            },

                            new RestoreTask
                            {
                                Name = "Restore"
                            },

                            new DotNetBuildTask
                            {
                                Name = "Build"
                            },

                            new GithubTask
                            {
                                Name = "Provision",
                                Run = "dotnet run --project .\\OtripleS.Api.Infrastructure.Provision\\OtripleS.Web.Api.Infrastructure.Provision.csproj"
                            }
                        }
                    }
                    }
                }
            };

            adoClient.SerializeAndWriteToFile(githubPipeline, "github-pipelines.yaml");


            Job matrixJob = new JobBuilder()
                .WithName("Build & Test (DB matrix)")
                .RunsOn("ubuntu-latest")
                .AddCheckoutStep()
                .AddSetupDotNetStep("10.0.100")
                .AddRestoreStep()
                .AddBuildStep()
                .AddTestStep()
                .AddMatrixV2("provider", "sqlserver", "postgres")
                .AddMatrixInclude(new Dictionary<string, string>
                {
                    ["provider"] = "sqlserver",
                    ["connection_string"] = "Server=localhost,1433;..."
                })
                .AddService("sqlserver", new Service { Image = "mcr.microsoft.com/mssql/server:2019-latest" })
                .Build();

            GitHubPipelineBuilder.CreateNewPipeline()
                .SetName("Github")
                .OnPush("master")
                .OnPullRequest("master")

                .AddJob("build", job => job
                    .WithName("Build")
                    .RunsOn(BuildMachines.WindowsLatest)
                    .AddEnvironmentVariable("AzureClientId", "${{ secrets.AZURECLIENTID }}")

                    .AddEnvironmentVariables(new Dictionary<string, string>
                    {
                        { "AzureTenantId", "${{ secrets.AZURETENANTID }}" },
                        { "AzureClientSecret", "${{ secrets.AZURECLIENTSECRET }}" },
                        { "AzureAdminName", "${{ secrets.AZUREADMINNAME }}" },
                        { "AzureAdminAccess", "${{ secrets.AZUREADMINACCESS }}" }
                    })

                    .AddCheckoutStep("Check Out")

                    .AddSetupDotNetStep(
                        version: "6.0.101",
                        includePrerelease: true)

                    .AddRestoreStep()
                    .AddBuildStep()

                    .AddGenericStep(
                        name: "Provision",
                        runCommand:
                            "dotnet run --project .\\{projectName}\\{projectName}.csproj"))

                .SaveToFile("github-pipelines-fluent.yaml");

            GitHubPipelineBuilder.CreateNewPipeline()
                   .SetName("Build")
                   .OnPush("main")
                   .OnPullRequest("main")

                   .AddJob("build-windows", job => job
                       .WithName("Build (Windows)")
                       .RunsOn(BuildMachines.WindowsLatest)
                       .AddCheckoutStep("Check out")
                       .AddSetupDotNetStep(version: "10.0.100")
                       .AddRestoreStep()
                       .AddBuildStep()
                       .AddTestStep())

                   .AddJob("build-integration", job =>
                   {
                       const string databaseName = "EventHighwayDb";
                       const string sqlServerPassword = "Your_password123!";

                       job
                           .WithName("Build & Test (DB matrix)")
                           .RunsOn(BuildMachines.UbuntuLatest)
                           .WithFailFast(false)
                           .AddMatrixInclude(new()
                           {
                               ["provider"] = "sqlserver",
                               ["connection_string"] =
                                   $"Server=localhost;Database={databaseName};User Id=sa;Password={sqlServerPassword};"
                           })
                           .AddMatrixInclude(new()
                           {
                               ["provider"] = "postgres",
                               ["connection_string"] =
                                   $"Host=localhost;Database={databaseName};Username=postgres;Password=postgres"
                           })
                           .AddService("sqlserver", new Service
                           {
                               Image = "mcr.microsoft.com/mssql/server:2019-latest",
                               Environment = new()
                               {
                                   ["ACCEPT_EULA"] = "Y",
                                   ["SA_PASSWORD"] = sqlServerPassword
                               },
                               Ports = new() { "1433:1433" },
                               Options =
                                   "--health-cmd \"/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -Q 'SELECT 1' || exit 1\" " +
                                   "--health-interval 10s --health-timeout 5s --health-retries 10"
                           })
                           .AddService("postgres", new Service
                           {
                               Image = "postgres:17",
                               Environment = new()
                               {
                                   ["POSTGRES_DB"] = databaseName,
                                   ["POSTGRES_USER"] = "postgres",
                                   ["POSTGRES_PASSWORD"] = "postgres"
                               },
                               Ports = new() { "5432:5432" },
                               Options = "--health-cmd pg_isready --health-interval 10s --health-timeout 5s --health-retries 5"
                           })
                           .AddEnvironmentVariable("PROVIDER", "${{ matrix.provider }}")
                           .AddEnvironmentVariable("CONNECTION_STRING", "${{ matrix.connection_string }}")
                           .AddCheckoutStep()
                           .AddSetupDotNetStep("10.0.100")
                           .AddRestoreStep()
                           .AddBuildStep()
                           .AddGenericStep(
                               name: "Apply Migrations",
                               runCommand: "dotnet ef database update")
                           .AddTestStep();
                   })

                   .AddJob("tag-release", job => job
                       .WithName("Tag and Release")
                       .RunsOn(BuildMachines.UbuntuLatest)
                       .DependsOn("build-windows", "build-integration")
                       .WithCondition(
                           "needs.build-windows.result == 'success' && " +
                           "needs.build-integration.result == 'success' && " +
                           "github.event.pull_request.merged && " +
                           "github.event.pull_request.base.ref == 'main' && " +
                           "startsWith(github.event.pull_request.title, 'RELEASES:') && " +
                           "contains(github.event.pull_request.labels.*.name, 'RELEASES')")
                       .AddActionStep(
                           name: "Checkout code",
                           uses: "actions/checkout@v3",
                           with: new Dictionary<string, string>
                           {
                               ["token"] = "${{ secrets.PAT_FOR_TAGGING }}"
                           })
                       .AddGenericStep(
                           name: "Configure Git",
                           runCommand:
                               "git config user.name \"GitHub Action\"\n" +
                               "git config user.email \"action@github.com\"")
                       .AddGenericStep(
                           id: "extract_version",
                           name: "Extract Version",
                           shell: "bash",
                           runCommand:
                               "sudo apt-get install xmlstarlet\n" +
                               "version_number=$(xmlstarlet sel -t -v \"//Version\" -n EventHighway.Core/EventHighway.Core.csproj)\n" +
                               "echo \"$version_number\"\n" +
                               "echo \"version_number<<EOF\" >> $GITHUB_OUTPUT\n" +
                               "echo \"$version_number\" >> $GITHUB_OUTPUT\n" +
                               "echo \"EOF\" >> $GITHUB_OUTPUT")
                       .AddGenericStep(
                           name: "Display Version",
                           runCommand: "echo \"Version number: ${{ steps.extract_version.outputs.version_number }}\"")
                       .AddGenericStep(
                           id: "extract_package_release_notes",
                           name: "Extract Package Release Notes",
                           shell: "bash",
                           runCommand:
                               "sudo apt-get install xmlstarlet\n" +
                               "package_release_notes=$(xmlstarlet sel -t -v \"//PackageReleaseNotes\" -n EventHighway.Core/EventHighway.Core.csproj)\n" +
                               "echo \"$package_release_notes\"\n" +
                               "echo \"package_release_notes<<EOF\" >> $GITHUB_OUTPUT\n" +
                               "echo \"$package_release_notes\" >> $GITHUB_OUTPUT\n" +
                               "echo \"EOF\" >> $GITHUB_OUTPUT")
                       .AddGenericStep(
                           name: "Display Package Release Notes",
                           runCommand: "echo \"Package Release Notes: ${{ steps.extract_package_release_notes.outputs.package_release_notes }}\"")
                       .AddGenericStep(
                           name: "Create GitHub Tag",
                           runCommand:
                               "git tag -a \"v${{ steps.extract_version.outputs.version_number }}\" -m \"Release - v${{ steps.extract_version.outputs.version_number }}\"\n" +
                               "git push origin --tags")
                       .AddActionStep(
                           name: "Create GitHub Release",
                           uses: "actions/create-release@v1",
                           with: new Dictionary<string, string>
                           {
                               ["tag_name"] = "v${{ steps.extract_version.outputs.version_number }}",
                               ["release_name"] = "Release - v${{ steps.extract_version.outputs.version_number }}",
                               ["body"] =
                                   "## Release - v${{ steps.extract_version.outputs.version_number }}\n\n" +
                                   "### Release Notes\n" +
                                   "${{ steps.extract_package_release_notes.outputs.package_release_notes }}"
                           },
                           environmentVariables: new Dictionary<string, string>
                           {
                               ["GITHUB_TOKEN"] = "${{ secrets.PAT_FOR_TAGGING }}"
                           }))

                   .AddJob("publish", job => job
                       .WithName("Publish to NuGet")
                       .RunsOn(BuildMachines.UbuntuLatest)
                       .DependsOn("tag-release")
                       .WithCondition("needs.tag-release.result == 'success'")
                       .AddCheckoutStep("Check out")
                       .AddSetupDotNetStep(version: "10.0.100")
                       .AddRestoreStep()
                       .AddGenericStep(
                           name: "Build",
                           runCommand: "dotnet build --no-restore --configuration Release")
                       .AddGenericStep(
                           name: "Pack NuGet Package",
                           runCommand: "dotnet pack --configuration Release --include-symbols")
                       .AddGenericStep(
                           name: "Push NuGet Package",
                           runCommand:
                               "dotnet nuget push **/bin/Release/**/*.nupkg " +
                               "--source https://api.nuget.org/v3/index.json " +
                               "--api-key ${{ secrets.NUGET_ACCESS }} --skip-duplicate"))

                   .SaveToFile("C:\\Users\\slima\\Desktop\\New folder\\github-pipelines-fluent2.yaml");
        }
    }
}
