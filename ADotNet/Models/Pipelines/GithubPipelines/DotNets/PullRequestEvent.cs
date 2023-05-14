// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class PullRequestEvent
    {
        [YamlMember(Order = 0, ScalarStyle = ScalarStyle.Folded, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public string[] Types { get; set; }

        [YamlMember(Order = 1, ScalarStyle = ScalarStyle.Folded)]
        public string[] Branches { get; set; }
    }
}
