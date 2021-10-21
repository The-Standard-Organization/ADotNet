﻿// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.IO;
using System.Security;
using ADotNet.Brokers.IOs;
using ADotNet.Brokers.Serializers;
using ADotNet.Models.Pipelines;
using ADotNet.Models.Pipelines.AdoPipelines.AspNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Services;
using Moq;
using Tynamix.ObjectFiller;
using Xunit;

namespace AdoNet.Tests.Unit.Services
{
    public partial class BuildServiceTests
    {
        private readonly Mock<IYamlBroker> yamlBrokerMock;
        private readonly Mock<IFilesBroker> filesBrokerMock;
        private readonly IBuildService buildService;

        public BuildServiceTests()
        {
            this.yamlBrokerMock = new Mock<IYamlBroker>();
            this.filesBrokerMock = new Mock<IFilesBroker>();

            this.buildService = new BuildService(
                yamlBroker: this.yamlBrokerMock.Object,
                filesBroker: this.filesBrokerMock.Object);
        }

        private static T CreateRandomPipeline<T>() where T : class =>
            CreatePipelineFiller<T>().Create();

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

        public static TheoryData Pipelines()
        {
            return new TheoryData<IPipeline>
            {
                CreateRandomPipeline<AspNetPipeline>(),
                CreateRandomPipeline<GithubPipeline>()
            };
        }

        private static Filler<T> CreatePipelineFiller<T>() where T : class =>
            new();
    }
}
