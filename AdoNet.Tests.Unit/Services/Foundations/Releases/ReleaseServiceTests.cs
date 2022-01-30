// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using ADotNet.Brokers.IOs;
using ADotNet.Brokers.Serializers;
using ADotNet.Models.Pipelines.Releases.GithubPipelines;
using ADotNet.Services.Releases;
using Moq;
using Tynamix.ObjectFiller;

namespace AdoNet.Tests.Unit.Services.Foundations.Releases
{
    public partial class ReleaseServiceTests
    {
        private readonly Mock<IYamlBroker> yamlBrokerMock;
        private readonly Mock<IFilesBroker> filesBrokerMock;
        private readonly IReleaseService releaseService;

        public ReleaseServiceTests()
        {
            this.yamlBrokerMock = new Mock<IYamlBroker>();
            this.filesBrokerMock = new Mock<IFilesBroker>();

            this.releaseService = new ReleaseService(
                yamlBroker: this.yamlBrokerMock.Object,
                filesBroker: this.filesBrokerMock.Object);
        }

        private static string CreateRandomString() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static GithubReleasePipeline CreateRandomReleasePipeline() =>
            CreateGithubReleasePipelineFiller().Create();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static Filler<GithubReleasePipeline> CreateGithubReleasePipelineFiller() =>
            new Filler<GithubReleasePipeline>();
    }
}
