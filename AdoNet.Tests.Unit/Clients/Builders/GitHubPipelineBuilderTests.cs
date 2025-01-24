// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.IO;
using ADotNet.Clients;
using ADotNet.Clients.Builders;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using Moq;
using Tynamix.ObjectFiller;

namespace ADotNet.Tests.Unit.Clients.Builders
{
    public partial class GitHubPipelineBuilderTests
    {
        private readonly Mock<ADotNetClient> aDotNetClientMock;
        private readonly GitHubPipelineBuilder gitHubPipelineBuilder;

        public GitHubPipelineBuilderTests()
        {
            this.aDotNetClientMock = new Mock<ADotNetClient>();

            this.gitHubPipelineBuilder = new GitHubPipelineBuilder(
                aDotNetClient: aDotNetClientMock.Object);
        }

        private static GithubPipeline GetPipeline(GitHubPipelineBuilder builder)
        {
            var privateField = typeof(GitHubPipelineBuilder)
                .GetField("githubPipeline", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            return (GithubPipeline)privateField.GetValue(builder);
        }

        private static string GetRandomString() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static int GetRandomNumber() =>
           new IntRange(min: 2, max: 10).GetValue();

        private static string GetRandomFileName() =>
           Path.GetRandomFileName();

        private static GithubPipeline CreateRandomGithubPipeline(string name) =>
            CreateGithubPipelineFiller(name).Create();

        private static GithubPipeline CreateRandomGithubPipeline() =>
            CreateGithubPipelineFiller(name: GetRandomString()).Create();

        private static Filler<GithubPipeline> CreateGithubPipelineFiller(string name) =>
            new Filler<GithubPipeline>();
    }
}
