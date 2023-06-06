// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    /// <summary>
    /// A task to build .NET project.
    /// </summary>
    public sealed class CreateGitHubReleaseTask : GithubTask
    {
        public CreateGitHubReleaseTask(string releaseName, string tagName, string releaseNotes, string githubToken)
        {
            EnvironmentVariables = new Dictionary<string, string> { { "GITHUB_TOKEN", githubToken } };

            With = new Dictionary<string, string>
                {
                    { "tag_name", tagName },
                    { "release_name", releaseName },

                    { "body",
                        $"## {releaseName}\r"
                        + "\r"
                        + "### Release Notes\r"
                        + releaseNotes
                    },
                };
        }

        [YamlMember(Order = 5, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new Dictionary<string, string> With { get; private set; }

        /// <summary>
        /// The environment variables to set for the step.
        /// </summary>
        [YamlMember(Order = 6, Alias = "env", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new Dictionary<string, string> EnvironmentVariables { get; private set; }
    }
}
