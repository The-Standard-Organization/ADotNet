// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Collections.Generic;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class BuildJob
    {
        private string _runsOn;

        [YamlMember(Alias = "runs-on")]
        public string RunsOn
        {
            get => Strategy.Matrix.Os != null ? BuildMachines.Matrix : _runsOn;
            set => _runsOn = value;
        }

        [YamlMember(Alias = "timeout-minutes")]
        public int TimeoutInMinutes { get; set; }

        [YamlMember(Alias = "strategy")]
        public Strategy Strategy { get; set; }

        public List<GithubTask> Steps { get; set; }
    }
}
