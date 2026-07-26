// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;

namespace ADotNet.Clients.Builders
{
    /// <summary>
    /// Builder for creating a GitHub pipeline.
    /// </summary>
    public class GitHubPipelineBuilderV2
    {
        private readonly GithubPipelineV2 githubPipelineV2;
        private readonly IADotNetClient aDotNetClient;

        internal GitHubPipelineBuilderV2(IADotNetClient aDotNetClient)
        {
            this.githubPipelineV2 = new GithubPipelineV2
            {
                OnEvents = new Events(),
                Jobs = new Dictionary<string, JobV2>()
            };

            this.aDotNetClient = aDotNetClient;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="GitHubPipelineBuilderV2"/> class 
        /// with a default <see cref="ADotNetClient"/>.
        /// </summary>
        /// <returns>A new instance of <see cref="GitHubPipelineBuilderV2"/>.</returns>
        public static GitHubPipelineBuilderV2 CreateNewPipeline()
        {
            var aDotNetClient = new ADotNetClient();

            return new GitHubPipelineBuilderV2(aDotNetClient);
        }

        /// <summary>
        /// Sets the name of the GitHub pipeline.
        /// </summary>
        /// <param name="name">The name of the pipeline.</param>
        /// <returns>The current instance of <see cref="GitHubPipelineBuilderV2"/>.</returns>
        public GitHubPipelineBuilderV2 SetName(string name)
        {
            this.githubPipelineV2.Name = name;

            return this;
        }

        /// <summary>
        /// Configures the pipeline to trigger on push events for specified branches.
        /// </summary>
        /// <param name="branches">The branches to trigger on push events.</param>
        /// <returns>The current instance of <see cref="GitHubPipelineBuilderV2"/>.</returns>
        public GitHubPipelineBuilderV2 OnPush(params string[] branches)
        {
            this.githubPipelineV2.OnEvents.Push = new PushEvent
            {
                Branches = branches
            };

            return this;
        }

        /// <summary>
        /// Configures the pipeline to trigger on pull request events for specified branches.
        /// </summary>
        /// <param name="branches">The branches to trigger on pull request events.</param>
        /// <returns>The current instance of <see cref="GitHubPipelineBuilderV2"/>.</returns>
        public GitHubPipelineBuilderV2 OnPullRequest(params string[] branches)
        {
            this.githubPipelineV2.OnEvents.PullRequest = new PullRequestEvent
            {
                Branches = branches
            };

            return this;
        }

        /// <summary>
        /// Adds a job to the GitHub pipeline.
        /// </summary>
        /// <param name="jobIdentifier">The unique identifier for the job.</param>
        /// <param name="configureJob">The action to configure the job.</param>
        /// <returns>The current instance of <see cref="GitHubPipelineBuilderV2"/>.</returns>
        public GitHubPipelineBuilderV2 AddJob(string jobIdentifier, Action<JobBuilderV2> configureJob)
        {
            var jobBuilder = new JobBuilderV2();
            configureJob(jobBuilder);
            this.githubPipelineV2.Jobs[jobIdentifier] = jobBuilder.Build();

            return this;
        }

        /// <summary>
        /// Saves the configured pipeline (yml) to the specified file path.
        /// </summary>
        /// <param name="path">The file path where the pipeline will be saved.</param>
        public void SaveToFile(string path) =>
            this.aDotNetClient.SerializeAndWriteToFile(this.githubPipelineV2, path);
    }
}
