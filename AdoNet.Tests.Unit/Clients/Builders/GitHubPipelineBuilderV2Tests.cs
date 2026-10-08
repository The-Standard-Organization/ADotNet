// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using ADotNet.Clients;
using ADotNet.Clients.Builders;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using Moq;
using Tynamix.ObjectFiller;

namespace ADotNet.Tests.Unit.Clients.Builders
{
    public partial class GitHubPipelineBuilderV2Tests
    {
        private readonly Mock<IADotNetClient> aDotNetClientMock;
        private readonly GitHubPipelineBuilderV2 gitHubPipelineBuilderV2;

        public GitHubPipelineBuilderV2Tests()
        {
            this.aDotNetClientMock = new Mock<IADotNetClient>();

            this.gitHubPipelineBuilderV2 = new GitHubPipelineBuilderV2(
                aDotNetClient: aDotNetClientMock.Object);
        }

        private static GithubPipelineV2 GetPipeline(GitHubPipelineBuilderV2 builder)
        {
            var privateField = typeof(GitHubPipelineBuilderV2)
                .GetField(
                    name: "githubPipelineV2",
                    bindingAttr: System.Reflection.BindingFlags.NonPublic
                        | System.Reflection.BindingFlags.Instance);

            return (GithubPipelineV2)privateField.GetValue(builder);
        }

        private static string GetRandomString() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static int GetRandomNumber() =>
           new IntRange(min: 2, max: 10).GetValue();

        private static string GetRandomFileName() =>
           Path.GetRandomFileName();

        private static GithubPipelineV2 CreateRandomGithubPipelineV2(string name) =>
            CreateGithubPipelineV2Filler(name).Create();

        private static GithubPipelineV2 CreateRandomGithubPipelineV2() =>
            CreateGithubPipelineV2Filler(name: GetRandomString()).Create();

        private static Filler<GithubPipelineV2> CreateGithubPipelineV2Filler(string name)
        {
            var filler = new Filler<GithubPipelineV2>();

            filler.Setup()
                .OnProperty(p => p.EnvironmentVariables)
                .Use(() => new Dictionary<string, string>
                {
                    { GetRandomString(), GetRandomString() },
                    { GetRandomString(), GetRandomString() }
                })
                .OnType<object>().Use(() => GetRandomString());

            return filler;
        }
    }
}
