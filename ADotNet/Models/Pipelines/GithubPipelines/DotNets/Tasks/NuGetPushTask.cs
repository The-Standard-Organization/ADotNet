// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    public class NuGetPushTask : GithubTask
    {
        [YamlMember(Order = 1)]
        public string Run = "dotnet nuget push";

        internal string SearchPath { get; set; }

        internal string ApiKey { get; set; }

        internal string Destination { get; set; }
    }
}
