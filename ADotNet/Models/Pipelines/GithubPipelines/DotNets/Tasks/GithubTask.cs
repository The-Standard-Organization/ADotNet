// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    public class GithubTask
    {
        [YamlMember(Order = 0, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public virtual string Name { get; set; }

        [YamlMember(Order = 1, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public virtual string Id { get; set; }

        [YamlMember(Order = 2, Alias = "needs", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public virtual string[] Needs { get; set; }

        [YamlMember(Order = 3, Alias = "if", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public virtual string If { get; set; }

        [YamlMember(Order = 4, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public virtual string Uses { get; set; }

        [YamlMember(Order = 5, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public virtual Dictionary<string, string> With { get; set; }

        [YamlMember(Order = 6, Alias = "env", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public virtual Dictionary<string, string> EnvironmentVariables { get; set; }

        [YamlMember(Order = 7, Alias = "run", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public virtual string Run { get; set; }

        [DefaultValue(0)]
        [YamlMember(Order = 8, Alias = "timeout-minutes", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public virtual int TimeoutInMinutes { get; set; }

        [YamlMember(Order = 9, Alias = "shell", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public virtual string Shell { get; set; }
    }
}
