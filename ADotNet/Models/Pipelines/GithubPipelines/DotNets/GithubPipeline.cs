// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class GithubPipeline
    {
        public string Name { get; set; }

        [YamlMember(Alias = "on")]
        public Events OnEvents { get; set; }

        [YamlMember(Alias = "env", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public Dictionary<string, string> EnvironmentVariables { get; set; }

        public Jobs Jobs { get; set; }
    }
}
