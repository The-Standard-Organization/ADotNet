// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using ADotNet.Clients.Builders;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using FluentAssertions;
using Xunit;

namespace ADotNet.Tests.Unit.Clients.Builders
{
    public partial class GitHubPipelineBuilderTests
    {
        [Fact]
        public void ShouldCreateNewPipeline()
        {
            // given..when
            var builder = GitHubPipelineBuilder.CreateNewPipeline();

            // then
            builder.Should().NotBeNull();
        }

        [Fact]
        public void ShouldSetPipelineName()
        {
            // given
            string inputName = "My GitHub Pipeline";
            string expectedName = inputName;

            // when
            var pipelineBuilder = GitHubPipelineBuilder.CreateNewPipeline()
                .SetName(inputName);

            var actualPipeline = GetPipeline(pipelineBuilder);

            // then
            actualPipeline.Should().NotBeNull();
            actualPipeline.Name.Should().BeEquivalentTo(expectedName);
        }

        [Fact]
        public void ShouldAddPushTrigger()
        {
            // given
            string[] inputBranches = { "main", "dev" };

            // when
            var pipelineBuilder = GitHubPipelineBuilder.CreateNewPipeline()
                .OnPush(inputBranches);

            var actualPipeline = GetPipeline(pipelineBuilder);

            // then
            actualPipeline.OnEvents.Push.Should().NotBeNull();
            actualPipeline.OnEvents.Push.Branches.Should().BeEquivalentTo(inputBranches);
        }


        [Fact]
        public void ShouldAddPullRequestTrigger()
        {
            // given
            string[] inputBranches = { "main", "feature/*" };

            // when
            var pipelineBuilder = GitHubPipelineBuilder.CreateNewPipeline()
                .OnPullRequest(inputBranches);

            var actualPipeline = GetPipeline(pipelineBuilder);

            // then
            actualPipeline.OnEvents.PullRequest.Should().NotBeNull();
            actualPipeline.OnEvents.PullRequest.Branches.Should().BeEquivalentTo(inputBranches);
        }

        [Fact]
        public void ShouldAddJobToPipeline()
        {
            // given
            string inputJobName = "build";
            string inputRunsOn = BuildMachines.WindowsLatest;
            string inputTaskName = "Restore";

            string expectedRunsOn = inputRunsOn;
            string expectedTaskName = inputTaskName;

            // when
            var pipelineBuilder = GitHubPipelineBuilder.CreateNewPipeline()
                .AddJob(inputJobName, job =>
                    job.RunsOn(inputRunsOn)
                       .AddRestoreStep(inputTaskName));

            var actualPipeline = GetPipeline(pipelineBuilder);

            // then
            var actualJob = actualPipeline.Jobs[inputJobName];
            actualJob.Should().NotBeNull();
            actualJob.RunsOn.Should().Be(expectedRunsOn);
            actualJob.Steps.Should().HaveCount(1);
            actualJob.Steps[0].Should().BeOfType<RestoreTask>()
                .Which.Name.Should().Be(expectedTaskName);
        }
    }
}
