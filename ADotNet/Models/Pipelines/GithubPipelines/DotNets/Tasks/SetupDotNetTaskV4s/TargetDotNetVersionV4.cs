// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV4s
{
    [Obsolete("Use latest version instead.")]
    public class TargetDotNetVersionV4
    {
        [YamlMember(Alias = "dotnet-version")]
        public string DotNetVersion { get; set; }
    }
}
