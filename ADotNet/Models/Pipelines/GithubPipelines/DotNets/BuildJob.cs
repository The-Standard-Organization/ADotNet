// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class BuildJob
    {
        private string runsOn;

        [YamlMember(Alias = "runs-on")]
        public string RunsOn
        {
            get => Strategy?.Matrix?.Os != null ? BuildMachines.Matrix : this.runsOn;
            set => this.runsOn = value;
        }

        [DefaultValue(0)]
        [YamlMember(Alias = "timeout-minutes", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public int TimeoutInMinutes { get; set; }

        [YamlMember(Alias = "strategy")]
        public Strategy Strategy { get; set; }

        public List<GithubTask> Steps { get; set; }
    }
}
