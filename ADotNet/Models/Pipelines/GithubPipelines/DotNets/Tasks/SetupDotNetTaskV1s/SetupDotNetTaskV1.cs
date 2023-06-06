// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s
{
    public class SetupDotNetTaskV1 : GithubTask
    {
        public SetupDotNetTaskV1()
        {
            Uses = "actions/setup-dotnet@v1";
        }

        /// <summary>
        /// Represents the usage of an external action or a specific version of an action in a GitHub Actions job step.
        /// Default value: actions/setup-dotnet@v1
        /// </summary>
        [YamlMember(Order = 4, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new string Uses { get; private set; }

        [YamlMember(Order = 5, Alias = "with", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public TargetDotNetVersion TargetDotNetVersion { get; set; }
    }
}
