// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    /// <summary>
    /// A task that allows you to push a package file to NuGet.
    /// </summary>
    public sealed class NugetPushTask(string nugetApiKey) : GithubTask
    {

        /// <summary>
        /// Gets the command to execute for the task.
        /// </summary>
        [YamlMember(Order = 7, Alias = "run")]
        public new string Run { get; private set; } = "dotnet nuget push **/bin/Release/**/*.nupkg --source "
                + $"https://api.nuget.org/v3/index.json --api-key {nugetApiKey} --skip-duplicate";
    }
}
