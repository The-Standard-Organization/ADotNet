// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    /// <summary>
    /// A task that that checks out your source code repository into the runner environment 
    /// where your GitHub Actions workflows are executed.
    /// </summary>
    public class CheckoutTaskV4 : GithubTask
    {
        /// <summary>
        /// Represents the usage of an external action or a specific version of an action in a GitHub Actions job step.
        /// Default value: actions/checkout@v4
        /// </summary>
        public override string Uses { get; set; } = "actions/checkout@v4";
    }
}
