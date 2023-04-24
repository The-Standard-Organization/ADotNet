// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    public class PackTask : GithubTask
    {
        [YamlMember(Order = 1)]
        public string Run = "dotnet pack --configuration Release";

        [YamlMember(Alias = "working-directory", Order = 2)]
        public string WorkingDirectory { get; set; }

        [YamlMember(Alias = "env", Order = 3, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public Dictionary<string, string> EnvironmentVariables { get; set; }
    }
}
