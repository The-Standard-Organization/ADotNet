// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV5s;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public sealed class PublishToNuget : Job
    {
        private const string NugetApiKey = "${{ steps.login.outputs.NUGET_API_KEY }}";

        public PublishToNuget(
            string runsOn,
            string dependsOn,
            string dotNetVersion,
            string nugetUser)
        {
            RunsOn = runsOn;
            Needs = new string[] { dependsOn };

            If = $"needs.{dependsOn}.result == 'success'";

            Permissions = new Dictionary<string, string>
            {
                { "id-token", "write" },
                { "contents", "read" }
            };

            Steps = new List<GithubTask>
            {
                new CheckoutTaskV5
                {
                    Name = "Check out"
                },

                new SetupDotNetTaskV5
                {
                    Name = "Setup .Net",

                    With = new TargetDotNetVersionV5
                    {
                        DotNetVersion = dotNetVersion
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

                new NuGetLoginTask(nugetUser)
                {
                    Name = "NuGet Login",
                },

                new NugetPushTask(NugetApiKey)
                {
                    Name = "Push NuGet Package",
                }
            };
        }

        [YamlMember(Order = 0, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new string Name { get; set; }

        [YamlMember(Order = 1, Alias = "runs-on")]
        public new string RunsOn { get; set; }

        [YamlMember(Order = 2, Alias = "permissions", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new Dictionary<string, string> Permissions { get; set; }

        [YamlMember(Order = 3, Alias = "needs", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new string[] Needs { get; set; }

        [YamlMember(Order = 4, Alias = "if", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new string If { get; set; }

        [YamlMember(Order = 5, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new string Environment { get; set; }

        [YamlMember(Order = 6, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new DefaultValues Defaults { get; set; }

        [YamlMember(Order = 7)]
        public new List<GithubTask> Steps { get; set; }

        [DefaultValue(0)]
        [YamlMember(Order = 8, Alias = "timeout-minutes", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new int TimeoutInMinutes { get; set; }

        [YamlMember(Order = 9, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new Strategy Strategy { get; set; }

        [YamlMember(Order = 10, Alias = "env", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new Dictionary<string, string> EnvironmentVariables { get; set; }

        [YamlMember(Order = 11, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new Dictionary<string, string> Outputs { get; set; }
    }
}
