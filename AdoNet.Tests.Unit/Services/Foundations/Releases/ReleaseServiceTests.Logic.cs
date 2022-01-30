// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using ADotNet.Models.Pipelines.Releases.GithubPipelines;
using Moq;
using Xunit;

namespace AdoNet.Tests.Unit.Services.Foundations.Releases
{
    public partial class ReleaseServiceTests
    {
        [Fact]
        public void ShouldSerializeWriteReleasePipeline()
        {
            // given
            string randomSerializedPipeline = CreateRandomString();
            string serializedGithubPipeline = randomSerializedPipeline;
            string randomPath = CreateRandomString();
            string inputPath = randomPath;

            GithubReleasePipeline randomGithubReleasePipeline =
                CreateRandomReleasePipeline();

            GithubReleasePipeline inputGithubReleasePipeline =
                randomGithubReleasePipeline;

            this.yamlBrokerMock.Setup(broker =>
                broker.SerializeToYaml(inputGithubReleasePipeline))
                    .Returns(serializedGithubPipeline);

            // when
            this.releaseService.SerializeWriteToFile(
                inputPath,
                inputGithubReleasePipeline);

            // then

            this.yamlBrokerMock.Verify(broker =>
                broker.SerializeToYaml(inputGithubReleasePipeline),
                    Times.Once);

            this.filesBrokerMock.Verify(broker =>
                broker.WriteToFile(inputPath, serializedGithubPipeline),
                    Times.Once);

            this.yamlBrokerMock.VerifyNoOtherCalls();
            this.filesBrokerMock.VerifyNoOtherCalls();
        }
    }
}
