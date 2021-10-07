// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
