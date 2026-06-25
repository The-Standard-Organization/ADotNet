// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    public class InstallPlaywrightTask : GithubTask
    {
        public override string Run { get; set; } = "dotnet tool install --global Microsoft.Playwright.CLI";
    }
}
