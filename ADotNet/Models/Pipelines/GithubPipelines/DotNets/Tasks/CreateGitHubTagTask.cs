// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    /// <summary>
    /// A task to build .NET project.
    /// </summary>
    public sealed class CreateGitHubTagTask(string tagName, string tagMessage) : GithubTask
    {

        /// <summary>
        /// Gets the command to execute for the task.
        /// </summary>
        [YamlMember(Order = 7, Alias = "run")]
        public new string Run { get; private set; } = $"git tag -a \"{tagName}\" -m \"{tagMessage}\"\r"
                + "git push origin --tags";
    }
}
