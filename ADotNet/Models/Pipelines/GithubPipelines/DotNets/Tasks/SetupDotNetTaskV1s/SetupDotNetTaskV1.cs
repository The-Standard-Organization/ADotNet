// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s
{
    public class SetupDotNetTaskV1 : GithubTask
    {
        [YamlMember(Order = 1)]
        public string Uses = "actions/setup-dotnet@v1";

        [YamlMember(Alias = "with", Order = 2)]
        public TargetDotNetVersion TargetDotNetVersion { get; set; }
    }
}
