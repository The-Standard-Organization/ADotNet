// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    public class InstallPlaywrightBrowsersTask : GithubTask
    {
        [YamlIgnore]
        public string ProjectName { get; set; }
        public override string Run => $"pwsh ./{ProjectName}/bin/Debug/net9.0/playwright.ps1 install";
    }
}
