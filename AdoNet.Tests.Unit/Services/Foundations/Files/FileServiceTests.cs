// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using ADotNet.Brokers.IOs;
using ADotNet.Services.Foundations.Files;
using Moq;
using Tynamix.ObjectFiller;
using Xunit;

namespace ADotNet.Tests.Unit.Services.Foundations.Files
{
    public partial class FileServiceTests
    {
        private readonly Mock<IFilesBroker> filesBrokerMock;
        private readonly IFileService fileService;

        public FileServiceTests()
        {
            this.filesBrokerMock = new Mock<IFilesBroker>();

            this.fileService = new FileService(
                filesBroker: this.filesBrokerMock.Object);
        }

        public static TheoryData FileDependencyValidationExceptions()
        {
            return new TheoryData<Exception>()
            {
                new ArgumentNullException(),
                new ArgumentOutOfRangeException(),
                new ArgumentException()
            };
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();
    }
}
