// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    /// <summary>
    /// Represents a task that installs Playwright browsers for a given project.
    /// </summary>
    public class InstallPlaywrightBrowsersTask : GithubTask
    {
        private readonly string projectName;
        private readonly string dotnetVersion;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstallPlaywrightBrowsersTask"/> class.
        /// </summary>
        /// <param name="projectName">The name of the project for which Playwright browsers should be installed.</param>
        public InstallPlaywrightBrowsersTask(
            string projectName,
            string dotnetVersion = "net9.0")
        {
            this.projectName = projectName;
            this.dotnetVersion = dotnetVersion;
        }
        /// <summary>
        /// Gets or sets the name of the task.
        /// The default value is: "Install Microsoft.Playwright.CLI".
        /// </summary>
        public override string Name { get; set; } = "Install Microsoft.Playwright.CLI.";

        /// <summary>
        /// Gets the command to execute for the task.
        /// The command installs Playwright browsers by running the Playwright CLI script.
        /// The default value is: "pwsh ./{projectName}/bin/Debug/{dotnetVersion}/playwright.ps1 install".
        /// </summary>
        public override string Run => $"pwsh ./{projectName}/bin/Debug/{dotnetVersion}/playwright.ps1 install";
    }
}
