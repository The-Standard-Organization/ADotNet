// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.AdoPipelines.AspNets.Tasks.UseDotNetTasks
{
    public class UseDotNetTasksInputs
    {
        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public PackageType PackageType { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string Version { get; set; }

        public bool IncludePreviewVersions { get; set; }
    }
}
