// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;
using ADotNet.Models.Foundations.Files.Exceptions;
using Moq;
using Xunit;

namespace AdoNet.Tests.Unit.Services.Foundations.Files
{
    public partial class FileServiceTests
    {
        [Theory]
        [MemberData(nameof(FileDependencyValidationExceptions))]
        public void ShouldThrowDependencyValidationExceptionOnWriteIfDependencyValidationErrorOccurs(
            Exception dependencyValidationException)
        {
            // given
            string somePath = GetRandomString();
            string someContent = GetRandomString();

            var invalidFileException =
                new InvalidFileException(
                    dependencyValidationException);

            var fileDependencyValidationException =
                new FileDependencyValidationException(
                    invalidFileException);

            this.filesBrokerMock.Setup(broker =>
                broker.WriteToFile(It.IsAny<string>(), It.IsAny<string>()))
                    .Throws(dependencyValidationException);

            // when
            Action writeToFileAction = () =>
                this.fileService.WriteToFile(somePath, someContent);

            // then
            Assert.Throws<FileDependencyValidationException>(
                writeToFileAction);

            this.filesBrokerMock.Verify(broker =>
                broker.WriteToFile(It.IsAny<string>(), It.IsAny<string>()),
                    Times.Once);

            this.filesBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowDependencyExceptionOnWriteIfSerializationErrorOccurs()
        {
            // given
            string somePath = GetRandomString();
            string someContent = GetRandomString();

            var serializationException =
                new SerializationException();

            var failedFileSerializationException =
                new FailedFileSerializationException(
                    serializationException);

            var fileDependencyException =
                new FileDependencyException(
                    failedFileSerializationException);

            this.filesBrokerMock.Setup(broker =>
                broker.WriteToFile(It.IsAny<string>(), It.IsAny<string>()))
                    .Throws(serializationException);

            // when
            Action writeToFileAction = () =>
                this.fileService.WriteToFile(somePath, someContent);

            // then
            Assert.Throws<FileDependencyException>(
                writeToFileAction);

            this.filesBrokerMock.Verify(broker =>
                broker.WriteToFile(It.IsAny<string>(), It.IsAny<string>()),
                    Times.Once);

            this.filesBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShoudThrowServiceExceptionOnWriteIfServiceErrorOccurs()
        {
            // given
            string somePath = GetRandomString();
            string someContent = GetRandomString();
            var serviceException = new Exception();

            var failedFileServiceException =
                new FailedFileServiceException(serviceException);

            var fileServiceException =
                new FileServiceException(failedFileServiceException);

            this.filesBrokerMock.Setup(broker =>
                broker.WriteToFile(It.IsAny<string>(), It.IsAny<string>()))
                    .Throws(serviceException);

            // when
            Action writeToFileAction = () =>
                this.fileService.WriteToFile(somePath, someContent);

            // then
            Assert.Throws<FileServiceException>(writeToFileAction);

            this.filesBrokerMock.Verify(broker =>
                broker.WriteToFile(It.IsAny<string>(), It.IsAny<string>()),
                    Times.Once);

            this.filesBrokerMock.VerifyNoOtherCalls();
        }
    }
}
