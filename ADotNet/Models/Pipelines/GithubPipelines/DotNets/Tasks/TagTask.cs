// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    public partial class TagTask : GithubTask
    {
        [YamlMember(Order = 1, ScalarStyle = YamlDotNet.Core.ScalarStyle.Literal)]
        public string Run = "git tag -a \"release-${{ github.run_number }}\" -m \"Release ${{ github.run_number }}\"\r\ngit push origin --tags";
    }
}