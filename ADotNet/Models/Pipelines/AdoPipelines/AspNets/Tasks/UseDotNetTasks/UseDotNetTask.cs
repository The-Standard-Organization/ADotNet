// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.AdoPipelines.AspNets.Tasks.UseDotNetTasks
{
    public class UseDotNetTask : BuildTask
    {
        public string Task { get; set; } = "UseDotNet@2";

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted, Order = 1)]
        public UseDotNetTasksInputs Inputs { get; set; }
    }
}
