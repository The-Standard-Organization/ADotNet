// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.AdoPipelines.AspNets.Tasks.PublishBuildArtifactTasks
{
    public class PublishBuildArtifactsTask : BuildTask
    {
        public string Task { get; set; } = "PublishBuildArtifacts@1";

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted, Order = 1)]
        public PublishBuildArtifactsInputs Inputs { get; set; }

        [YamlMember(DefaultValuesHandling = DefaultValuesHandling.OmitNull, Order = 2)]
        public string? Condition { get; set; }
    }
}
