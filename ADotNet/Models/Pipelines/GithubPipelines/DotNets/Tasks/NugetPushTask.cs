// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    public class NugetPushTask : GithubTask
    {
        [YamlMember(Order = 1)]
        public string Run = "dotnet nuget push **/bin/Release/**/*.nupkg --source https://api.nuget.org/v3/index.json --api-key ${{ secrets.NUGET_ACCESS }}";
    }
}
