// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    /// <summary>
    /// A task to build .NET project with Release configuration.
    /// </summary>
    public sealed class DotNetBuildReleaseTask : GithubTask
    {
        /// <summary>
        /// Gets or sets the command to execute for the task.
        /// The default value is: "dotnet build --no-restore --configuration Release".
        /// </summary>
        public override string Run { get; set; } = "dotnet build --no-restore --configuration Release";
    }
}
