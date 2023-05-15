// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class PushEvent
    {
        [YamlMember(Order = 0, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public string[] Tags { get; set; }

        [YamlMember(Order = 1)]
        public string[] Branches { get; set; }
    }
}
