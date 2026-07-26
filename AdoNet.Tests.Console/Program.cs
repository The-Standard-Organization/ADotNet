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


            var githubPipelineV2 = new GithubPipelineV2
            {
                Name = "test-strategy-services-surface",

                OnEvents = new Events
                {
                    Push = new PushEvent
                    {
                        Branches = new[] { "main" }
                    },

                    PullRequest = new PullRequestEvent
                    {
                        Branches = new[] { "main" }
                    }
                },

                Jobs = new Dictionary<string, JobV2>
                {
                    ["full_surface"] = new JobV2
                    {
                        Name = "Full Strategy + Services Surface",
                        RunsOn = BuildMachines.UbuntuLatest,

                        Strategy = new StrategyV2
                        {
                            FailFast = false,
                            MaxParallel = 2,

                            Matrix = new Dictionary<string, object>
                            {
                                ["provider"] = new List<string>
                                    {
                                        "sqlserver",
                                        "postgres"
                                    },

                                ["dotnet-version"] = new List<string>
                                    {
                                        "8.0.x",
                                        "10.0.100"
                                    }
                            },

                            Include = new List<Dictionary<string, string>>
                                {
                                    new()
                                    {
                                        ["provider"] = "sqlserver",
                                        ["connection_string"] =
                                            "Server=localhost;Database=TestDb;User Id=sa;" +
                                            "Password=Your_password123!;TrustServerCertificate=True;" +
                                            "MultipleActiveResultSets=true;Pooling=false"
                                    },

                                    new()
                                    {
                                        ["provider"] = "postgres",
                                        ["connection_string"] =
                                            "Host=localhost;Database=TestDb;" +
                                            "Username=postgres;Password=postgres;Pooling=false"
                                    },

                                    new()
                                    {
                                        ["provider"] = "postgres",
                                        ["dotnet-version"] = "9.0.x",
                                        ["connection_string"] =
                                            "Host=localhost;Database=TestDb;" +
                                            "Username=postgres;Password=postgres;Pooling=false"
                                    }
                                },

                            Exclude = new List<Dictionary<string, string>>
                            {
                                new()
                                {
                                    ["provider"] = "sqlserver",
                                    ["dotnet-version"] = "8.0.x"
                                }
                            }
                        },

                        Services = new Dictionary<string, Service>
                        {
                            ["postgres"] = new Service
                            {
                                Image = "postgres:17",

                                Environment = new Dictionary<string, string>
                                {
                                    ["POSTGRES_DB"] = "TestDb",
                                    ["POSTGRES_USER"] = "postgres",
                                    ["POSTGRES_PASSWORD"] = "postgres"
                                },

                                Ports = new List<string>
                                {
                                    "5432:5432"
                                },

                                Options =
                                    "--health-cmd pg_isready " +
                                    "--health-interval 10s " +
                                    "--health-timeout 5s " +
                                    "--health-retries 5"
                            }
                        },

                        Steps = new List<GithubTask>
                        {
                            new CheckoutTaskV5
                            {
                                Name = "Check out"
                            },

                            new SetupDotNetTaskV5
                            {
                                Name = "Setup .NET",

                                With = new TargetDotNetVersionV5
                                {
                                    DotNetVersion = "${{ matrix.dotnet-version }}"
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
                                Name = "Test",
                                Run =
                                    "dotnet test Test.Core.Tests.Acceptance " +
                                    "--no-build --verbosity normal"
                            }
                        }
                    }
                }
            };

            adoClient.SerializeAndWriteToFile(githubPipeline, "github-pipelines-strategy-services.yaml");
        }
    }
}
