// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    [Obsolete(message: "Use Job instead to define your jobs on the GitHubPipeline class")]
    public class BuildJob
    {
        [YamlMember(Alias = "runs-on")]
        public string RunsOn { get; set; }

        [DefaultValue(0)]
        [YamlMember(Alias = "timeout-minutes", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public int TimeoutInMinutes { get; set; }

        [YamlMember(Alias = "env", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public Dictionary<string, string> EnvironmentVariables { get; set; }

        public List<GithubTask> Steps { get; set; }
    }
}
