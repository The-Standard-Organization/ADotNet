// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class TagJob
    {
        [YamlMember(Order = 0, Alias = "if", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public string If { get; set; }

        [YamlMember(Order = 1, Alias = "runs-on")]
        public string RunsOn { get; set; }

        [YamlMember(Order = 2, Alias = "env", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public Dictionary<string, string> EnvironmentVariables { get; set; }

        [YamlMember(Order = 3)]
        public List<GithubTask> Steps { get; set; }
    }
}
