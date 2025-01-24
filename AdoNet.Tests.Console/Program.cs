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
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;

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
                            new CheckoutTaskV2
                            {
                                Name = "Check Out"
                            },

                            new SetupDotNetTaskV1
                            {
                                Name = "Setup Dot Net Version",

                                TargetDotNetVersion = new TargetDotNetVersion
                                {
                                    DotNetVersion = "6.0.101",
                                    IncludePrerelease = true
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
                    .AddEnvironmentVariable("AzureTenantId", "${{ secrets.AZURETENANTID }}")
                    .AddEnvironmentVariable("AzureClientSecret", "${{ secrets.AZURECLIENTSECRET }}")
                    .AddEnvironmentVariable("AzureAdminName", "${{ secrets.AZUREADMINNAME }}")
                    .AddEnvironmentVariable("AzureAdminAccess", "${{ secrets.AZUREADMINACCESS }}")
                    //.AddEnvironmentVariables(new Dictionary<string, string>
                    //{
                    //    { "AzureClientId", "${{ secrets.AZURECLIENTID }}" },
                    //    { "AzureTenantId", "${{ secrets.AZURETENANTID }}" },
                    //    { "AzureClientSecret", "${{ secrets.AZURECLIENTSECRET }}" },
                    //    { "AzureAdminName", "${{ secrets.AZUREADMINNAME }}" },
                    //    { "AzureAdminAccess", "${{ secrets.AZUREADMINACCESS }}" }
                    //})
                    .AddCheckoutStep("Check Out")
                    .AddSetupDotNetStep(
                        version: "6.0.101",
                        includePrerelease: true)
                    .AddRestoreStep()
                    .AddBuildStep()
                    .AddGenericStep(
                        name: "Provision",
                        runCommand: "dotnet run --project .\\OtripleS.Api.Infrastructure.Provision\\OtripleS.Web.Api.Infrastructure.Provision.csproj"))
                .SaveToFile("github-pipelines-2.yaml");

        }
    }
}
