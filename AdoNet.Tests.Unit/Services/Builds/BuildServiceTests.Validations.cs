// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System;
using ADotNet.Models.Pipelines.AdoPipelines.AspNets;
using ADotNet.Models.Pipelines.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace ADotNet.Tests.Unit.Services
{
    public partial class BuildServiceTests
    {
        [Fact]
        private void ShouldThrowValidationExceptionOnSerializeIfPipelineIsNull()
        {
            // given
            AspNetPipeline invalidPipeline = null;
            string somePath = GetRandomFilePath();

            var nullPiplineException =
                new NullPipelineException(
                    message: "Pipeline is null");

            // when
            Action serializeAndWriteToFileAction = () =>
                this.buildService.SerializeAndWriteToFile(somePath, invalidPipeline);

            // then
            AdoValidationException actualAdoValidationException =
                Assert.Throws<AdoValidationException>(
                    serializeAndWriteToFileAction);

            actualAdoValidationException.InnerException.Message.Should()
                .BeEquivalentTo(nullPiplineException.Message);

            this.yamlBrokerMock.Verify(broker =>
                broker.SerializeToYaml(It.IsAny<object>()),
                    Times.Never);

            this.filesBrokerMock.Verify(broker =>
                broker.WriteToFile(It.IsAny<string>(), It.IsAny<string>()),
                    Times.Never);

            this.yamlBrokerMock.VerifyNoOtherCalls();
            this.filesBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private void ShouldThrowValidationExceptionOnSerializeIfPathIsNull()
        {
            // given
            string invalidPath = null;

            AspNetPipeline someAspNetPipeline =
                CreateRandomAspNetPipeline();

            var nullPathException =
                new NullPathException(
                    message: "Path is null");

            // when
            Action serializeAndWriteToFileAction = () =>
                this.buildService.SerializeAndWriteToFile(
                    invalidPath,
                    someAspNetPipeline);

            // then
            AdoValidationException actualAdoValidationException =
                Assert.Throws<AdoValidationException>(
                    serializeAndWriteToFileAction);

            actualAdoValidationException.InnerException.Message.Should()
                .BeEquivalentTo(nullPathException.Message);

            this.yamlBrokerMock.Verify(broker =>
                broker.SerializeToYaml(It.IsAny<object>()),
                    Times.Never);

            this.filesBrokerMock.Verify(broker =>
                broker.WriteToFile(It.IsAny<string>(), It.IsAny<string>()),
                    Times.Never);

            this.yamlBrokerMock.VerifyNoOtherCalls();
            this.filesBrokerMock.VerifyNoOtherCalls();
        }
    }
}
