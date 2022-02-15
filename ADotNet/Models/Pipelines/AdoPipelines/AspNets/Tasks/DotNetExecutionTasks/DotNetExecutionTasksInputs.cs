﻿// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.AdoPipelines.AspNets.Tasks.DotNetExecutionTasks
{
    public class DotNetExecutionTasksInputs
    {
        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public Command Command { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted, DefaultValuesHandling = DefaultValuesHandling.OmitNull)]
        public Feeds? FeedsToUse { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted, DefaultValuesHandling = DefaultValuesHandling.OmitNull)]
        public string? Projects { get; set; }

        [YamlMember(DefaultValuesHandling = DefaultValuesHandling.OmitNull)]
        public bool? PublishWebProjects { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted, DefaultValuesHandling = DefaultValuesHandling.OmitNull)]
        public string Arguments { get; set; }

        [YamlMember(DefaultValuesHandling = DefaultValuesHandling.OmitNull)]
        public bool? ZipAfterPublish { get; set; }
    }
}
