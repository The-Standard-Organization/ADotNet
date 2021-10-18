﻿// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using ADotNet.Models.Pipelines.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace AdoNet.Tests.Unit.Services
{
    public partial class AdoServiceTests
    {
        [Theory]
        [MemberData(nameof(FileValidationExceptions))]
        public void ShouldThrowDependencyValidationOnSerializeIfDependencyValidationErrorOccurs(
            Exception dependencyValidationException)
        {
            // given
            var somePipeline = Mock.Of<object>();
            string somePath = GetRandomFilePath();

            this.yamlBrokerMock.Setup(broker =>
                broker.SerializeToYaml(It.IsAny<object>()))
                    .Throws(dependencyValidationException);

            // when
            Action serializeAndWriteToFileAction = () =>
                this.adoService.SerializeAndWriteToFile(
                    somePath,
                    somePipeline);

            // then
            AdoDependencyValidationException actualAdoDependencyValidationException =
                Assert.Throws<AdoDependencyValidationException>(
                    serializeAndWriteToFileAction);

            actualAdoDependencyValidationException.InnerException.Message.Should()
                .BeEquivalentTo(dependencyValidationException.Message);

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
        public void ShouldThrowDependencyExceptionOnSerializeIfDependencyValidationErrorOccurs(
            Exception dependencyException)
        {
            // given
            var somePipeline = Mock.Of<object>();
            string somePath = GetRandomFilePath();

            this.yamlBrokerMock.Setup(broker =>
                broker.SerializeToYaml(It.IsAny<object>()))
                    .Throws(dependencyException);

            // when
            Action serializeAndWriteToFileAction = () =>
                this.adoService.SerializeAndWriteToFile(
                    somePath,
                    somePipeline);

            // then
            AdoDependencyException actualAdoDependencyException =
                Assert.Throws<AdoDependencyException>(
                    serializeAndWriteToFileAction);

            actualAdoDependencyException.InnerException.Message.Should()
                .BeEquivalentTo(dependencyException.Message);

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
        public void ShouldThrowServiceExceptionOnSerializeIfSystemErrorOccurs()
        {
            // given
            var somePipeline = Mock.Of<object>();
            string somePath = GetRandomFilePath();
            string innerExceptionMessage = GetRandomString();
            var serviceException = new Exception(innerExceptionMessage);

            this.yamlBrokerMock.Setup(broker =>
                broker.SerializeToYaml(It.IsAny<object>()))
                    .Throws(serviceException);

            // when
            Action serializeAndWriteToFileAction = () =>
                this.adoService.SerializeAndWriteToFile(
                    somePath,
                    somePipeline);

            // then
            AdoServiceException actualAdoServiceException =
                Assert.Throws<AdoServiceException>(
                    serializeAndWriteToFileAction);

            actualAdoServiceException.InnerException.Message.Should()
                .BeEquivalentTo(serviceException.Message);

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
