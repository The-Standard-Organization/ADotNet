// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Runtime.Serialization;
using ADotNet.Models.Foundations.Files.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace ADotNet.Tests.Unit.Services.Foundations.Files
{
    public partial class FileServiceTests
    {
        [Theory]
        [MemberData(nameof(FileDependencyValidationExceptions))]
        private void ShouldThrowDependencyValidationExceptionOnWriteIfDependencyValidationErrorOccurs(
            Exception dependencyValidationException)
        {
            // given
            string somePath = GetRandomString();
            string someContent = GetRandomString();

            var invalidFileException =
                new InvalidFileException(
                    message: "Invalid file error occurred.",
                    innerException: dependencyValidationException);

            var expectedFileDependencyValidationException =
                new FileDependencyValidationException(
                    message: "File dependency validation error occurred, fix the errors and try again.",
                    innerException: invalidFileException);

            this.filesBrokerMock.Setup(broker =>
                broker.WriteToFile(It.IsAny<string>(), It.IsAny<string>()))
                    .Throws(dependencyValidationException);

            // when
            Action writeToFileAction = () =>
                this.fileService.WriteToFile(somePath, someContent);

            // then
            FileDependencyValidationException actualFileDependencyValidationException =
                Assert.Throws<FileDependencyValidationException>(writeToFileAction);

            actualFileDependencyValidationException.Should().BeEquivalentTo(
                expectedFileDependencyValidationException);

            this.filesBrokerMock.Verify(broker =>
                broker.WriteToFile(It.IsAny<string>(), It.IsAny<string>()),
                    Times.Once);

            this.filesBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private void ShouldThrowDependencyExceptionOnWriteIfSerializationErrorOccurs()
        {
            // given
            string somePath = GetRandomString();
            string someContent = GetRandomString();

            var serializationException =
                new SerializationException();

            var failedFileSerializationException =
                new FailedFileSerializationException(
                    message: "Failed file serialization error occurred, contact support.",
                    innerException: serializationException);

            var fileDependencyException =
                new FileDependencyException(
                    message: "File dependency error occurred, contact support.",
                    innerException: failedFileSerializationException);

            this.filesBrokerMock.Setup(broker =>
                broker.WriteToFile(It.IsAny<string>(), It.IsAny<string>()))
                    .Throws(serializationException);

            // when
            Action writeToFileAction = () =>
                this.fileService.WriteToFile(somePath, someContent);

            // then
            FileDependencyException actualFileDependencyException =
                Assert.Throws<FileDependencyException>(writeToFileAction);

            actualFileDependencyException.Should().BeEquivalentTo(
                fileDependencyException);

            this.filesBrokerMock.Verify(broker =>
                broker.WriteToFile(It.IsAny<string>(), It.IsAny<string>()),
                    Times.Once);

            this.filesBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private void ShoudThrowServiceExceptionOnWriteIfServiceErrorOccurs()
        {
            // given
            string somePath = GetRandomString();
            string someContent = GetRandomString();
            var serviceException = new Exception();

            var failedFileServiceException =
                new FailedFileServiceException(
                    message: "Failed file service error occurred, contact support.",
                    innerException: serviceException);

            var fileServiceException =
                new FileServiceException(
                    message: "File service error occurred, contact support.",
                    innerException: failedFileServiceException);

            this.filesBrokerMock.Setup(broker =>
                broker.WriteToFile(It.IsAny<string>(), It.IsAny<string>()))
                    .Throws(serviceException);

            // when
            Action writeToFileAction = () =>
                this.fileService.WriteToFile(somePath, someContent);

            // then
            FileServiceException actualFileServiceException =
                Assert.Throws<FileServiceException>(writeToFileAction);

            actualFileServiceException.Should().BeEquivalentTo(
                fileServiceException);

            this.filesBrokerMock.Verify(broker =>
                broker.WriteToFile(It.IsAny<string>(), It.IsAny<string>()),
                    Times.Once);

            this.filesBrokerMock.VerifyNoOtherCalls();
        }
    }
}
