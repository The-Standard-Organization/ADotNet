// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.IO;
using System.Security;
using ADotNet.Brokers.IOs;
using ADotNet.Brokers.Serializers;
using ADotNet.Models.Pipelines.AspNets;
using ADotNet.Services;
using Moq;
using Tynamix.ObjectFiller;
using Xunit;

namespace AdoNet.Tests.Unit.Services
{
    public partial class AdoServiceTests
    {
        private readonly Mock<IYamlBroker> yamlBrokerMock;
        private readonly Mock<IFilesBroker> filesBrokerMock;
        private readonly IAdoService adoService;

        public AdoServiceTests()
        {
            this.yamlBrokerMock = new Mock<IYamlBroker>();
            this.filesBrokerMock = new Mock<IFilesBroker>();

            this.adoService = new AdoService(
                yamlBroker: this.yamlBrokerMock.Object,
                filesBroker: this.filesBrokerMock.Object);
        }

        private static AspNetPipeline CreateRandomAspNetPipeline() =>
            CreateAspNetPipelineFiller().Create();

        private static string GetRandomFilePath() =>
            new MnemonicString().GetValue();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        public static TheoryData FileValidationExceptions()
        {
            return new TheoryData<Exception>
            {
                new ArgumentException(),
                new ArgumentNullException(),
                new PathTooLongException(),
                new DirectoryNotFoundException()
            };
        }

        public static TheoryData FileDependencyExceptions()
        {
            return new TheoryData<Exception>
            {
                new IOException(),
                new UnauthorizedAccessException(),
                new NotSupportedException(),
                new SecurityException()
            };
        }

        private static Filler<AspNetPipeline> CreateAspNetPipelineFiller() =>
            new Filler<AspNetPipeline>();
    }
}
