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

        public static GitHubPipelineBuilder CreateNewPipeline()
        {
            var aDotNetClient = new ADotNetClient();

            return new GitHubPipelineBuilder(aDotNetClient);
        }

        public GitHubPipelineBuilder SetName(string name)
        {
            this.githubPipeline.Name = name;

            return this;
        }

        public GitHubPipelineBuilder OnPush(params string[] branches)
        {
            this.githubPipeline.OnEvents.Push = new PushEvent
            {
                Branches = branches
            };

            return this;
        }

        public GitHubPipelineBuilder OnPullRequest(params string[] branches)
        {
            this.githubPipeline.OnEvents.PullRequest = new PullRequestEvent
            {
                Branches = branches
            };

            return this;
        }

        public GitHubPipelineBuilder AddJob(string jobIdentifier, Action<JobBuilder> configureJob)
        {
            var jobBuilder = new JobBuilder();

            configureJob(jobBuilder);

            this.githubPipeline.Jobs[jobIdentifier] = jobBuilder.Build();

            return this;
        }

        public void SaveToFile(string path) =>
            this.aDotNetClient.SerializeAndWriteToFile(this.githubPipeline, path);
    }
}
