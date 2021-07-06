// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.AspNets.Tasks.DotNetExecutionTasks
{
    public class DotNetExecutionTask : BuildTask
    {
        public string Task { get; set; } = "DotNetCoreCLI@2";

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted, Order = 1)]
        public DotNetExecutionTasksInputs Inputs { get; set; }
    }
}
