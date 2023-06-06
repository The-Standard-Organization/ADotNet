// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    /// <summary>
    /// A task that packs the project into a nuget package.
    /// </summary>
    public sealed class PackTask : GithubTask
    {
        /// <summary>
        /// Gets or sets the command push a NuGet package. If a value is specified, 
        /// Default value: "dotnet pack --configuration Release".
        /// </summary>
        public override string Run { get; set; } = "dotnet pack --configuration Release";
    }
}
