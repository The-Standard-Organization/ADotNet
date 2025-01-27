// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    /// <summary>
    /// Represents a task that updates the installed workloads in the .NET SDK.
    /// </summary>
    public class WorkloadUpdateTask : GithubTask
    {
        /// <summary>
        /// Gets or sets the name of the task.
        /// The default value is: "Update installed workloads in the .NET SDK".
        /// </summary>
        public override string Name { get; set; } = "Update installed workloads in the .NET SDK";

        /// <summary>
        /// Gets or sets the command to execute for the task.
        /// The command updates all installed workloads in the .NET SDK to their latest versions.
        /// The default value is: "dotnet workload update".
        /// </summary>
        public override string Run { get; set; } = "dotnet workload update";
    }
}
