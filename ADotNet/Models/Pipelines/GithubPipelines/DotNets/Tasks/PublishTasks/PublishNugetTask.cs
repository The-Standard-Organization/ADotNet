// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.PublishTasks
{
    public class PublishNugetTask : GithubTask
    {
        [YamlMember(Order = 1)]
        public string Uses = "nuget/nuget-client@v2";

        [YamlMember(Alias = "with", Order = 2)]
        public PublishConfigurations PublishConfigurations { get; set; }
    }
}
