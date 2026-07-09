// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    /// <summary>
    /// A task to install Microsoft.Playwright.CLI.
    /// </summary>
    public class InstallPlaywrightTask : GithubTask
    {
        /// <summary>  
        /// Gets or sets the name of task.  
        /// </summary>  
        public override string Name { get; set; } = "Install Microsoft.Playwright.CLI.";  

        /// <summary>
        /// Gets or sets the command to execute for the task.
        /// The default value is: "dotnet tool install --global Microsoft.Playwright.CLI".
        /// </summary>
        public override string Run { get; set; } = "dotnet tool install --global Microsoft.Playwright.CLI";
    }
}
