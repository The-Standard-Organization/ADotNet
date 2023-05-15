// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV3s
{
    public class SetupDotNetTaskV3 : GithubTask
    {
        [YamlMember(Order = 1)]
        public string Uses = "actions/setup-dotnet@v3";

        [YamlMember(Alias = "with", Order = 2)]
        public TargetDotNetVersionV3 TargetDotNetVersion { get; set; }
    }
}
