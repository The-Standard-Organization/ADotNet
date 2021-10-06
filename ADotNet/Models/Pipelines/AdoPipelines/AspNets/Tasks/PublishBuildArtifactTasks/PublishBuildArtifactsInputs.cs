// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.AdoPipelines.AspNets.Tasks.PublishBuildArtifactTasks
{
    public class PublishBuildArtifactsInputs
    {
        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string PathToPublish { get; set; }
    }
}
