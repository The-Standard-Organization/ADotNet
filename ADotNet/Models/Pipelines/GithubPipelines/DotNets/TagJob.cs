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
    public partial class TagJob
    {
        [YamlMember(Alias = "if")]
        public string TagTrait = "github.event_name == 'pull_request' && github.event.action == 'closed'" +
                " && github.event.pull_request.merged && startsWith(github.event.pull_request.title, 'RELEASES:')" + "" +
                    " && contains(github.event.pull_request.labels.*.name, 'RELEASES')";

        [YamlMember(Alias = "needs")]
        public string[] Needs = { "build", "test" };

        [YamlMember(Alias = "runs-on")]
        public string RunsOn { get; set; }

        public List<GithubTask> Steps { get; set; }
    }
}