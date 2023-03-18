// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.Dependabot
{
    public partial class Update
    {
        [YamlMember(Alias = "package-ecosystem")]
        public string PackageEcosystem { get; set; }

        [YamlMember(Alias = "directory")]
        public string Directory { get; set; }

        [YamlMember(Alias = "schedule")]
        public Schedule Schedule { get; set; }

        [YamlMember(Alias = "open-pull-requests-limit")]
        public int OpenPullRequestsLimit { get; set; }
    }
}
