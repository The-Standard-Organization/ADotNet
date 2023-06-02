// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV3s;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class PublishJob
    {
        public PublishJob(
            string runsOn,
            string dependsOn,
            string nugetApiKey)
        {
            RunsOn = runsOn;
            Needs = new string[] { dependsOn };

            If = $"needs.{dependsOn}.result == 'success'";

            Steps = new List<GithubTask> {
                new CheckoutTaskV3
                {
                    Name = "Check out"
                },

                new SetupDotNetTaskV3
                {
                    Name = "Setup .Net",

                    With = new TargetDotNetVersionV3
                    {
                        DotNetVersion = "7.0.201"
                    }
                },

                new RestoreTask
                {
                    Name = "Restore"
                },

                new DotNetBuildReleaseTask
                {
                    Name = "Build",
                },

                new PackNugetTaskWithSymbols
                {
                    Name = "Pack NuGet Package",
                },

                new NugetPushTask(nugetApiKey)
                {
                    Name = "Push NuGet Package",
                }
            };

        }

        [YamlMember(Order = 0, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new string Name { get; private set; }

        [YamlMember(Order = 1, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new string Environment { get; private set; }

        [YamlMember(Order = 2, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new DefaultValues Defaults { get; private set; }

        [YamlMember(Order = 3, Alias = "runs-on")]
        public new string RunsOn { get; private set; }

        [YamlMember(Order = 4)]
        public new List<GithubTask> Steps { get; private set; }

        [DefaultValue(0)]
        [YamlMember(Order = 5, Alias = "timeout-minutes", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new int TimeoutInMinutes { get; private set; }

        [YamlMember(Order = 6, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new Strategy Strategy { get; private set; }

        [YamlMember(Order = 7, Alias = "env", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new Dictionary<string, string> EnvironmentVariables { get; private set; }

        [YamlMember(Order = 8, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new Dictionary<string, string> Outputs { get; private set; }

        [YamlMember(Order = 9, Alias = "needs", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new string[] Needs { get; private set; }

        [YamlMember(Order = 10, Alias = "if", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new string If { get; private set; }
    }
}

