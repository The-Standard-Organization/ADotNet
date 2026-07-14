// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class StrategyV2
    {
        [YamlMember(Order = 0, Alias = "fail-fast", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public bool? FailFast { get; set; }

        [YamlMember(Order = 1, Alias = "max-parallel", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public int? MaxParallel { get; set; }

        [YamlMember(Order = 3, Alias = "matrix", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public Dictionary<string, object> Matrix { get; set; }

        [YamlMember(Order = 4, Alias = "include", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public List<Dictionary<string, string>> Include { get; set; }

        [YamlMember(Order = 5, Alias = "exclude", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public List<Dictionary<string, string>> Exclude { get; set; }
    }
}
