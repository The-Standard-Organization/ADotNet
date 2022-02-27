// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Moq;
using Xunit;

namespace AdoNet.Tests.Unit.Services.Foundations.Files
{
    public partial class FileServiceTests
    {
        [Fact]
        public void ShouldWriteToFile()
        {
            // given
            string randomFilePath = GetRandomString();
            string inputFilePath = randomFilePath;
            string randomFileContent = GetRandomString();
            string inputFileContent = randomFileContent;

            // when
            this.fileService.WriteToFile(
                inputFilePath,
                inputFileContent);

            // then
            this.filesBrokerMock.Verify(broker =>
                broker.WriteToFile(inputFilePath, inputFileContent),
                    Times.Once);

            this.filesBrokerMock.VerifyNoOtherCalls();
        }
    }
}
