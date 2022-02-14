// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.AdoPipelines.AspNets
{
    public class AspNetPipeline
    {
        [YamlMember(Alias = "trigger")]
        public List<string> TriggeringBranches { get; set; }

        [YamlMember(Alias = "pool")]
        public VirtualMachinesPool VirtualMachinesPool { get; set; }

        [YamlMember(Alias = "variables")]
        public ConfigurationVariables ConfigurationVariables { get; set; }

        [YamlMember(Alias = "steps")]
        public List<BuildTask> Tasks { get; set; }
    }
}
