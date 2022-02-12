// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class PushEvent
    {
        public string[] Branches { get; set; }

        [YamlMember(Alias = "paths-ignore")]
        public string[] PathsIgnore { get; set; }
    }
}
