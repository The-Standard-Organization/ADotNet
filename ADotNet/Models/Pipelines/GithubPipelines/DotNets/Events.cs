﻿// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class Events
    {
        public PushEvent Push { get; set; }

        [YamlMember(Alias = "pull_request")]
        public PullRequestEvent PullRequest { get; set; }
    }
}
