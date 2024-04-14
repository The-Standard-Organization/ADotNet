// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System;
using ADotNet.Models.Pipelines.AdoPipelines.AspNets;
using ADotNet.Models.Pipelines.AdoPipelines.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace ADotNet.Tests.Unit.Services.Builds
{
    public partial class BuildServiceTests
    {
        [Theory]
        [MemberData(nameof(FileValidationExceptions))]
        private void ShouldThrowDependencyValidationOnSerializeIfDependencyValidationErrorOccurs(
            Exception dependencyValidationException)
        {
            // given
            AspNetPipeline somePipeline = CreateRandomAspNetPipeline();
            string somePath = GetRandomFilePath();

            var expectedAdoDependencyValidationException = new AdoDependencyValidationException(
                message: "Ado dependency validation error occurs, try again.",
                innerException: dependencyValidationException);

            this.yamlBrokerMock.Setup(broker =>
                broker.SerializeToYaml(It.IsAny<object>()))
                    .Throws(dependencyValidationException);

            // when
            Action serializeAndWriteToFileAction = () =>
                this.buildService.SerializeAndWriteToFile(
                    somePath,
                    somePipeline);

            // then
            AdoDependencyValidationException actualAdoDependencyValidationException =
                Assert.Throws<AdoDependencyValidationException>(
                    serializeAndWriteToFileAction);

            actualAdoDependencyValidationException.Should()
                .BeEquivalentTo(expectedAdoDependencyValidationException);

            this.yamlBrokerMock.Verify(broker =>
                broker.SerializeToYaml(It.IsAny<object>()),
                    Times.Once);

            this.filesBrokerMock.Verify(broker =>
                broker.WriteToFile(It.IsAny<string>(), It.IsAny<string>()),
                    Times.Never);

            this.yamlBrokerMock.VerifyNoOtherCalls();
            this.filesBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(FileDependencyExceptions))]
        private void ShouldThrowDependencyExceptionOnSerializeIfDependencyValidationErrorOccurs(
            Exception dependencyException)
        {
            // given
            AspNetPipeline somePipeline = CreateRandomAspNetPipeline();
            string somePath = GetRandomFilePath();

            var expectedAdoDependencyException = new AdoDependencyException(
                message: "Ado dependency error occured, contact support.",
                innerException: dependencyException);

            this.yamlBrokerMock.Setup(broker =>
                broker.SerializeToYaml(It.IsAny<object>()))
                    .Throws(dependencyException);

            // when
            Action serializeAndWriteToFileAction = () =>
                this.buildService.SerializeAndWriteToFile(
                    somePath,
                    somePipeline);

            // then
            AdoDependencyException actualAdoDependencyException =
                Assert.Throws<AdoDependencyException>(
                    serializeAndWriteToFileAction);

            actualAdoDependencyException.Should()
                .BeEquivalentTo(expectedAdoDependencyException);

            this.yamlBrokerMock.Verify(broker =>
                broker.SerializeToYaml(It.IsAny<object>()),
                    Times.Once);

            this.filesBrokerMock.Verify(broker =>
                broker.WriteToFile(It.IsAny<string>(), It.IsAny<string>()),
                    Times.Never);

            this.yamlBrokerMock.VerifyNoOtherCalls();
            this.filesBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private void ShouldThrowServiceExceptionOnSerializeIfSystemErrorOccurs()
        {
            // given
            AspNetPipeline somePipeline = CreateRandomAspNetPipeline();
            string somePath = GetRandomFilePath();
            string innerExceptionMessage = GetRandomString();
            var serviceException = new Exception(innerExceptionMessage);

            var expectedBuildServiceException = new BuildServiceException(
                message: "Build service exception occured, contact support.",
                innerException: serviceException);

            this.yamlBrokerMock.Setup(broker =>
                broker.SerializeToYaml(It.IsAny<object>()))
                    .Throws(serviceException);

            // when
            Action serializeAndWriteToFileAction = () =>
                this.buildService.SerializeAndWriteToFile(
                    somePath,
                    somePipeline);

            // then
            BuildServiceException actualBuildServiceException =
                Assert.Throws<BuildServiceException>(
                    serializeAndWriteToFileAction);

            actualBuildServiceException.Should()
                .BeEquivalentTo(expectedBuildServiceException);

            this.yamlBrokerMock.Verify(broker =>
                broker.SerializeToYaml(It.IsAny<object>()),
                    Times.Once);

            this.filesBrokerMock.Verify(broker =>
                broker.WriteToFile(It.IsAny<string>(), It.IsAny<string>()),
                    Times.Never);

            this.yamlBrokerMock.VerifyNoOtherCalls();
            this.filesBrokerMock.VerifyNoOtherCalls();
        }
    }
}
