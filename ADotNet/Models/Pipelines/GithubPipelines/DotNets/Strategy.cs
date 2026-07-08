// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class Strategy
    {
        [DefaultValue(true)]
        [YamlMember(Order = 0, Alias = "fail-fast", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public bool? FailFast { get; set; }

        [YamlMember(Order = 1, Alias = "max-parallel", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public int? MaxParallel { get; set; }

        [YamlMember(Order = 2, Alias = "matrix", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public Matrix Matrix { get; set; }
    }
}
