// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV3s;

namespace ADotNet.Infrastructure.Build
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var aDotNetClient = new ADotNetClient();

            var githubPipeline = new GithubPipeline
            {
                Name = ".Net",

                OnEvents = new Events
                {
                    Push = new PushEvent
                    {
                        Branches = new string[] { "master" }
                    },

                    PullRequest = new PullRequestEvent
                    {
                        Types = new string[] { "opened", "synchronize", "reopened", "closed" },
                        Branches = new string[] { "master" }
                    }
                },

                EnvironmentVariables = new Dictionary<string, string>
                {
                    { "IS_RELEASE_CANDIDATE", EnvironmentVariables.IsGitHubReleaseCandidate() }
                },

                Jobs = new Jobs
                {
                    Build = new BuildJob
                    {
                        RunsOn = BuildMachines.Windows2019,

                        Steps = new List<GithubTask>
                        {
                            new CheckoutTaskV3
                            {
                                Name = "Check out"
                            },

                            new SetupDotNetTaskV3
                            {
                                Name = "Setup .Net",

                                TargetDotNetVersion = new TargetDotNetVersionV3
                                {
                                    DotNetVersion = "7.0.201"
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
                    },
                    AddTag = new TagJob
                    {
                        RunsOn = BuildMachines.UbuntuLatest,

                        Needs = new string[] { "build" },

                        If =
                        "github.event.pull_request.merged &&\r"
                        + "github.event.pull_request.base.ref == 'master' &&\r"
                        + "startsWith(github.event.pull_request.title, 'RELEASES:') &&\r"
                        + "contains(github.event.pull_request.labels.*.name, 'RELEASES')\r",

                        Steps = new List<GithubTask>
                        {
                            new CheckoutTaskV3
                            {
                                Name = "Checkout code"
                            },

                            new ShellScriptTask
                            {
                                Name = "Extract Version Number",
                                Id = "extract_version",
                                Run =
                                    "echo \"version_number=$(grep -oP '(?<=<Version>)[^<]+' ADotNet/ADotNet.csproj)\" >> $GITHUB_OUTPUT\r"
                                    + "echo \"package_release_notes=$(grep -oP '(?<=<PackageReleaseNotes>)[^<]+' ADotNet/ADotNet.csproj)\" >> $GITHUB_OUTPUT"
                            },

                            new ShellScriptTask
                            {
                                Name = "Print Version Number",
                                Run =
                                    "echo \"Version number - v${{ steps.extract_version.outputs.version_number }}\"\r"
                                    + "echo \"Release Notes - ${{ steps.extract_version.outputs.package_release_notes }}\""
                            },

                            new ShellScriptTask
                            {
                                Name = "Configure Git",
                                Run =
                                    "git config user.name \"GitHub Action\""
                                    + "\r"
                                    + "git config user.email \"action@github.com\""
                            },

                            new CheckoutTaskV3
                            {
                                Name = "Authenticate with GitHub",
                                With = new Dictionary<string, string>
                                {
                                    { "token", "${{ secrets.PAT_FOR_TAGGING }}" }
                                }
                            },

                            new ShellScriptTask
                            {
                                Name = "Add Release Tag",
                                Run =
                                    "git tag -a \"v${{ steps.extract_version.outputs.version_number }}\" -m \"Release - v${{ steps.extract_version.outputs.version_number }}\"\r"
                                    + "git push origin --tags"
                            },

                            new ReleaseTaskV1
                            {
                                Name = "Create Release",
                                Uses = "actions/create-release@v1",

                                EnvironmentVariables = new Dictionary<string, string>
                                {
                                    { "GITHUB_TOKEN", "${{ secrets.PAT_FOR_TAGGING }}" }
                                },

                                With = new Dictionary<string, string>
                                {
                                    { "tag_name", "v${{ steps.extract_version.outputs.version_number }}" },
                                    { "release_name", "Release - v${{ steps.extract_version.outputs.version_number }}" },

                                    { "body",
                                        "### Release - v${{ steps.extract_version.outputs.version_number }}\r"
                                        + "\r"
                                        + "#### Release Notes\r"
                                        + "${{ steps.extract_version.outputs.package_release_notes }}"
                                    },
                                }
                            }
                        }
                    }
                }
            };

            string buildScriptPath = "../../../../.github/workflows/dotnet.yml";
            string directoryPath = Path.GetDirectoryName(buildScriptPath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            aDotNetClient.SerializeAndWriteToFile(githubPipeline, path: buildScriptPath);
        }
    }
}
