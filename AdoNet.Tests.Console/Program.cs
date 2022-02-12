// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Collections.Generic;
using ADotNet.Clients;
using ADotNet.Models.Pipelines.AdoPipelines.AspNets;
using ADotNet.Models.Pipelines.AdoPipelines.AspNets.Tasks.DotNetExecutionTasks;
using ADotNet.Models.Pipelines.AdoPipelines.AspNets.Tasks.PublishBuildArtifactTasks;
using ADotNet.Models.Pipelines.AdoPipelines.AspNets.Tasks.UseDotNetTasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;

namespace AdoNet.Tests.Console
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
                        Branches = new string[] { "master" },
                        PathsIgnore = new string[] { "*.md" }
                    },
                    PullRequest = new PullRequestEvent
                    {
                        Branches = new string[] { "master" }
                    }
                },

                Jobs = new Jobs
                {
                    Build = new BuildJob
                    {
                        RunsOn = BuildMachines.Windows2019,
                        TimeoutInMinutes = 10,

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

                            new TestTask
                            {
                                Name = "Test"
                            }
                        }
                    }
                }
            };

            adoClient.SerializeAndWriteToFile(githubPipeline, "github-pipelines.yaml");
        }
    }
}
