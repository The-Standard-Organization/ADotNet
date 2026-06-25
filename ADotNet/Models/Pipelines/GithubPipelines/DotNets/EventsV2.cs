// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class EventsV2
    {
        [YamlMember(DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public PushEvent Push { get; set; }

        [YamlMember(Alias = "pull_request", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public PullRequestEvent PullRequest { get; set; }

        public ScheduledEvent[] Schedule { get; set; }

        [YamlMember(Alias = "workflow_dispatch")]
        public WorkflowDispatchEvent WorkflowDispatch { get; set; }
    }
}
