// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class Matrix
    {
        public Dictionary<string, List<string>> Variables { get; set; }

        [YamlMember(Alias = "include", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public List<Dictionary<string, string>> Include { get; set; }

        [YamlMember(Alias = "exclude", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public List<Dictionary<string, string>> Exclude { get; set; }
    }
}
