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
    public class GitHubPipelineBuilder
    {
        private readonly GithubPipeline githubPipeline;
        private readonly IADotNetClient aDotNetClient;

        internal GitHubPipelineBuilder(IADotNetClient aDotNetClient)
        {
            this.githubPipeline = new GithubPipeline
            {
                OnEvents = new Events(),
                Jobs = new Dictionary<string, Job>()
            };

            this.aDotNetClient = aDotNetClient;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="GitHubPipelineBuilder"/> class 
        /// with a default <see cref="ADotNetClient"/>.
        /// </summary>
        /// <returns>A new instance of <see cref="GitHubPipelineBuilder"/>.</returns>
        public static GitHubPipelineBuilder CreateNewPipeline()
        {
            var aDotNetClient = new ADotNetClient();

            return new GitHubPipelineBuilder(aDotNetClient);
        }

        /// <summary>
        /// Sets the name of the GitHub pipeline.
        /// </summary>
        /// <param name="name">The name of the pipeline.</param>
        /// <returns>The current instance of <see cref="GitHubPipelineBuilder"/>.</returns>
        public GitHubPipelineBuilder SetName(string name)
        {
            this.githubPipeline.Name = name;

            return this;
        }

        /// <summary>
        /// Configures the pipeline to trigger on push events for specified branches.
        /// </summary>
        /// <param name="branches">The branches to trigger on push events.</param>
        /// <returns>The current instance of <see cref="GitHubPipelineBuilder"/>.</returns>
        public GitHubPipelineBuilder OnPush(params string[] branches)
        {
            this.githubPipeline.OnEvents.Push = new PushEvent
            {
                Branches = branches
            };

            return this;
        }

        /// <summary>
        /// Configures the pipeline to trigger on pull request events for specified branches.
        /// </summary>
        /// <param name="branches">The branches to trigger on pull request events.</param>
        /// <returns>The current instance of <see cref="GitHubPipelineBuilder"/>.</returns>
        public GitHubPipelineBuilder OnPullRequest(params string[] branches)
        {
            this.githubPipeline.OnEvents.PullRequest = new PullRequestEvent
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
        /// <returns>The current instance of <see cref="GitHubPipelineBuilder"/>.</returns>
        public GitHubPipelineBuilder AddJob(string jobIdentifier, Action<JobBuilder> configureJob)
        {
            var jobBuilder = new JobBuilder();
            configureJob(jobBuilder);
            this.githubPipeline.Jobs[jobIdentifier] = jobBuilder.Build();

            return this;
        }

        /// <summary>
        /// Saves the configured pipeline (yml) to the specified file path.
        /// </summary>
        /// <param name="path">The file path where the pipeline will be saved.</param>
        public void SaveToFile(string path) =>
            this.aDotNetClient.SerializeAndWriteToFile(this.githubPipeline, path);
    }
}
