// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV4s
{
    public class SetupDotNetTaskV4 : GithubTask
    {
        [Obsolete("Use latest version instead.")]
        public SetupDotNetTaskV4()
        {
            this.Uses = "actions/setup-dotnet@v4";
        }

        /// <summary>
        /// Represents the usage of an external action or a specific version of an action in a GitHub Actions job step.
        /// Default value: actions/setup-dotnet@v4
        /// </summary>
        [YamlMember(Order = 4, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new string Uses { get; private set; }

        /// <summary>
        /// Used to provide additional configuration or parameters for a specific step in the workflow.
        /// </summary>
        [YamlMember(Order = 5, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new TargetDotNetVersionV4 With { get; set; }
    }
}
