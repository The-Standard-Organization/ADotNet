// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class Service
    {
        [YamlMember(Order = 0, Alias = "image", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public string Image { get; set; }

        [YamlMember(Order = 1, Alias = "credentials", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public Credentials Credentials { get; set; }

        [YamlMember(Order = 2, Alias = "env", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public Dictionary<string, string> Environment { get; set; }

        [YamlMember(Order = 3, Alias = "ports", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public List<string> Ports { get; set; }

        [YamlMember(Order = 4, Alias = "volumes", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public List<string> Volumes { get; set; }

        [YamlMember(Order = 5, Alias = "options", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public string Options { get; set; }
    }
}
