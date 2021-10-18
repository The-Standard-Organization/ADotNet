// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using ADotNet.Models.Pipelines;
using Moq;
using Xunit;

namespace AdoNet.Tests.Unit.Services
{
    public partial class AdoServiceTests
    {
        [Theory]
        [MemberData(nameof(Pipelines))]
        public void ShouldSerializeAndWritePipelineModel(IPipeline pipeline)
        {
            // given
            var inputPipeline = pipeline;

            string randomPath = GetRandomFilePath();
            string inputPath = randomPath;
            string serialziedPipeline = GetRandomString();

            this.yamlBrokerMock.Setup(broker =>
                broker.SerializeToYaml(inputPipeline))
                    .Returns(serialziedPipeline);

            // when
            this.adoService.SerializeAndWriteToFile(
                inputPath,
                inputPipeline);

            // then
            this.yamlBrokerMock.Verify(broker =>
                broker.SerializeToYaml(inputPipeline),
                    Times.Once);

            this.filesBrokerMock.Verify(broker =>
                broker.WriteToFile(inputPath, serialziedPipeline),
                    Times.Once);

            this.yamlBrokerMock.VerifyNoOtherCalls();
            this.filesBrokerMock.VerifyNoOtherCalls();
        }
    }
}
