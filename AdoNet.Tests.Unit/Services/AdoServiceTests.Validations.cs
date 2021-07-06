// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using ADotNet.Models.Pipelines.AspNets;
using ADotNet.Models.Pipelines.Exceptions;
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

            var expectedAdoValidationException = 
                new AdoValidationException(nullPiplineException);

            // when
            Action serializeAndWriteToFileAction = () =>
                this.adoService.SerializeAndWriteToFile(somePath, invalidPipeline);

            // then
            Assert.Throws<AdoValidationException>(
                serializeAndWriteToFileAction);

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
