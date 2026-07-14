// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using ADotNet.Models.Foundations.Files.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace ADotNet.Tests.Unit.Services.Foundations.Files
{
    public partial class FileServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        private void ShouldThrowValidationExceptionOnWriteIfPathIsInvalid(
            string invalidPath)
        {
            // given
            string someContent = GetRandomString();

            var invalidFilePathException =
                new InvalidFilePathException(
                    message: "Invalid file path, fix the errors and try again.");

            var expectedFileValidationException =
                new FileValidationException(
                    message: "File validation error occurred, fix the errors and try again.",
                    innerException: invalidFilePathException);

            // when
            Action writeToFileAction = () =>
                this.fileService.WriteToFile(
                    invalidPath,
                    someContent);

            // then
            FileValidationException actualFileValidationException =
                Assert.Throws<FileValidationException>(writeToFileAction);

            actualFileValidationException.Should().BeEquivalentTo(
                expectedFileValidationException);

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
        private void ShouldThrowValidationExceptionOnWriteIfContentIsInvalid(
            string invalidContent)
        {
            // given
            string somePath = GetRandomString();

            var invalidFileContentException =
                new InvalidFileContentException(
                    message: "Invalid file content, fix errors and try again.");

            var expectedFileValidationException =
                new FileValidationException(
                    message: "File validation error occurred, fix the errors and try again.",
                    innerException: invalidFileContentException);

            // when
            Action writeToFileAction = () =>
                this.fileService.WriteToFile(
                    somePath,
                    invalidContent);

            // then
            FileValidationException actualFileValidationException =
                Assert.Throws<FileValidationException>(writeToFileAction);

            actualFileValidationException.Should().BeEquivalentTo(
                expectedFileValidationException);

            this.filesBrokerMock.Verify(broker =>
                broker.WriteToFile(
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                        Times.Never);

            this.filesBrokerMock.VerifyNoOtherCalls();
        }
    }
}
