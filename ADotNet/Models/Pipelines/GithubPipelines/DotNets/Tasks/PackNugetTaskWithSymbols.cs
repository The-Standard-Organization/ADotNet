// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    /// <summary>
    /// A task that packs the project into a nuget package including the symbols.
    /// </summary>
    public sealed class PackNugetTaskWithSymbols : GithubTask
    {
        /// <summary>
        /// Gets or sets the command to execute for the task.
        /// Default value: "dotnet pack --configuration Release --include-symbols".
        /// </summary>
        public override string Run { get; set; } = "dotnet pack --configuration Release --include-symbols";
    }
}
