// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV3s
{
    public class SetupDotNetTaskV3 : GithubTask
    {
        public SetupDotNetTaskV3() => this.Uses = "actions/setup-dotnet@v3";

        /// <summary>
        /// Represents the usage of an external action or a specific version of an action in a GitHub Actions job step.
        /// Default value: actions/setup-dotnet@v3
        /// </summary>
        [YamlMember(Order = 4, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new string Uses { get; private set; }

        /// <summary>
        /// Used to provide additional configuration or parameters for a specific step in the workflow.
        /// </summary>
        [YamlMember(Order = 5, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new TargetDotNetVersionV3 With { get; set; }
    }
}
