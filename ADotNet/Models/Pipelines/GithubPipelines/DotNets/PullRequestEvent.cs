// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class PullRequestEvent
    {
        [YamlMember(ScalarStyle = ScalarStyle.Folded)]
        public string[] Branches { get; set; }
    }
}
