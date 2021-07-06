// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using ADotNet.Models.Pipelines.AspNets;
using ADotNet.Models.Pipelines.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace AdoNet.Tests.Unit.Services
{
    public partial class AdoServiceTests
    {
        [Fact]
        public void ShouldThrowValidationExceptionOnSerializeIfPipelineIsNull()
        {
            // given
            AspNetPipeline invalidPipeline = null;
            string somePath = GetRandomFilePath();

            var nullPiplineException =
                new NullPipelineException();

            // when
            Action serializeAndWriteToFileAction = () =>
                this.adoService.SerializeAndWriteToFile(somePath, invalidPipeline);

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
        public void ShouldThrowValidationExceptionOnSerializeIfPathIsNull()
        {
            // given
            string invalidPath = null;
            
            AspNetPipeline someAspNetPipeline = 
                CreateRandomAspNetPipeline();

            var nullPathException =
                new NullPathException();

            // when
            Action serializeAndWriteToFileAction = () =>
                this.adoService.SerializeAndWriteToFile(
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
