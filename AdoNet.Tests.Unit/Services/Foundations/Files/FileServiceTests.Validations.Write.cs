// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using ADotNet.Models.Foundations.Files.Exceptions;
using Moq;
using Xunit;

namespace AdoNet.Tests.Unit.Services.Foundations.Files
{
    public partial class FileServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void ShouldThrowValidationExceptionOnWriteIfPathIsInvalid(
            string invalidPath)
        {
            // given
            string someContent = GetRandomString();

            var invalidFilePathException =
                new InvalidFilePathException();

            var expectedFileValidationException =
                new FileValidationException(
                    invalidFilePathException);

            // when
            Action writeToFileAction = () =>
                this.fileService.WriteToFile(
                    invalidPath,
                    someContent);

            // then
            Assert.Throws<FileValidationException>(
                writeToFileAction);

            this.filesBrokerMock.Verify(broker =>
                broker.WriteToFile(
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                        Times.Never);

            this.filesBrokerMock.VerifyNoOtherCalls();
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void ShouldThrowValidationExceptionOnWriteIfContentIsInvalid(
            string invalidContent)
        {
            // given
            string somePath = GetRandomString();

            var invalidFileContentException =
                new InvalidFileContentException();

            var expectedFileValidationException =
                new FileValidationException(
                    invalidFileContentException);

            // when
            Action writeToFileAction = () =>
                this.fileService.WriteToFile(
                    somePath,
                    invalidContent);

            // then
            Assert.Throws<FileValidationException>(
                writeToFileAction);

            this.filesBrokerMock.Verify(broker =>
                broker.WriteToFile(
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                        Times.Never);

            this.filesBrokerMock.VerifyNoOtherCalls();
        }
    }
}
