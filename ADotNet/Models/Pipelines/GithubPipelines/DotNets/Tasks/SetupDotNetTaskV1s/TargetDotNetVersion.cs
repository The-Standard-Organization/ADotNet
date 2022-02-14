// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s
{
    public class TargetDotNetVersion
    {
        [YamlMember(Alias = "dotnet-version")]
        public string DotNetVersion { get; set; }

        [YamlMember(Alias = "include-prerelease")]
        public bool IncludePrerelease { get; set; }
    }
}
