// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    /// <summary>
    /// A task that allows you to automate the creation of releases in a GitHub repository.
    /// </summary>
    public class ReleaseTaskV1 : GithubTask
    {
        /// <summary>
        /// Represents the usage of an external action or a specific version of an action in a GitHub Actions job step.
        /// Default value: actions/create-release@v1
        /// </summary>
        public override string Uses { get; set; } = "actions/create-release@v1";
    }
}
