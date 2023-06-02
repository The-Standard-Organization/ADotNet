// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    /// <summary>
    /// A task to run tests.
    /// </summary>
    public sealed class TestTask : GithubTask
    {
        /// <summary>
        /// Gets or sets the command to execute for the task.
        /// Default value: "dotnet test --no-build --verbosity normal".
        /// </summary>
        public override string Run { get; set; } = "dotnet test --no-build --verbosity normal";
    }
}
