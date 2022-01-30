// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using ADotNet.Models.Pipelines.Releases.GithubPipelines;
using ADotNet.Models.Pipelines.Releases.GithubPipelines.Exceptions;
using Moq;
using Xunit;

namespace AdoNet.Tests.Unit.Services.Foundations.Releases
{
    public partial class ReleaseServiceTests
    {
        [Fact]
        public void ShouldThrowValidationExceptionOnSerializeWriteIfPathIsNull()
        {
            // given
            string invalidPath = null;
            
            GithubReleasePipeline someReleasePipeline =
                CreateRandomReleasePipeline();

            var nullReleasePathException =
                new NullReleasePathException();

            var expectedReleaseValidationException =
                new ReleaseValidationException(
                    nullReleasePathException);

            // when
            Action serializeWriteAction = () =>
                this.releaseService.SerializeWriteToFile(invalidPath, someReleasePipeline);

            // then
            Assert.Throws<ReleaseValidationException>(serializeWriteAction);

            this.yamlBrokerMock.Verify(broker =>
                broker.SerializeToYaml(It.IsAny<Object>()),
                    Times.Never);

            this.filesBrokerMock.Verify(broker =>
                broker.WriteToFile(It.IsAny<string>(), It.IsAny<string>()),
                    Times.Never);

            this.yamlBrokerMock.VerifyNoOtherCalls();
            this.filesBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowValidationExceptionOnSerializeWriteIfPipelineIsNull()
        {
            // given
            GithubReleasePipeline invalidGithubReleasePipeline = null;
            string somePath = CreateRandomString();

            var nullReleasePipelineException =
                new NullReleasePipelineException();

            var expectedReleaseValidationException =
                new ReleaseValidationException(
                    nullReleasePipelineException);

            // when
            Action serializeWriteAction = () =>
                this.releaseService.SerializeWriteToFile(
                    somePath,
                    invalidGithubReleasePipeline);

            // then
            Assert.Throws<ReleaseValidationException>(serializeWriteAction);

            this.yamlBrokerMock.Verify(broker =>
                broker.SerializeToYaml(It.IsAny<Object>()),
                    Times.Never);

            this.filesBrokerMock.Verify(broker =>
                broker.WriteToFile(It.IsAny<string>(), It.IsAny<string>()),
                    Times.Never);

            this.yamlBrokerMock.VerifyNoOtherCalls();
            this.filesBrokerMock.VerifyNoOtherCalls();
        }
    }
}
