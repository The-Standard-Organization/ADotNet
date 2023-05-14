// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    public class ShellScriptTask : GithubTask
    {
        [YamlMember(Order = 1, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public string Id { get; set; }

        [YamlMember(Order = 2, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public string Run { get; set; }
    }
}
