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
    public class BuildJob
    {
        [YamlMember(Alias = "runs-on")]
        public string RunsOn { get; set; }

        [YamlMember(Alias = "timeout-minutes")]
        public int TimeoutInMinutes { get; set; }

        public List<GithubTask> Steps { get; set; }
    }
}
