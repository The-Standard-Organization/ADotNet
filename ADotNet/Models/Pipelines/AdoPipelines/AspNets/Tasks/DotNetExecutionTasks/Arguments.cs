// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

namespace ADotNet.Models.Pipelines.AdoPipelines.AspNets.Tasks.DotNetExecutionTasks
{
    public struct Arguments
    {
        public const string DefaultBuildAndPublishConfigurations =
            "--configuration $(BuildConfiguration) --output $(build.artifactstagingdirectory)";
    }
}
