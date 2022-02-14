// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using ADotNet.Models.Pipelines.AdoPipelines.AspNets;
using Moq;
using Xunit;

namespace ADotNet.Tests.Unit.Services
{
    public partial class BuildServiceTests
    {
        [Fact]
        public void ShouldSerializeAndWriteAdoPipelineModel()
        {
            // given
            AspNetPipeline randomAspNetPipeline =
                CreateRandomAspNetPipeline();

            AspNetPipeline inputAspNetPipeline =
                randomAspNetPipeline;

            string randomPath = GetRandomFilePath();
            string inputPath = randomPath;
            string serialziedPipeline = GetRandomString();

            this.yamlBrokerMock.Setup(broker =>
                broker.SerializeToYaml(inputAspNetPipeline))
                    .Returns(serialziedPipeline);

            // when
            this.buildService.SerializeAndWriteToFile(
                inputPath,
                inputAspNetPipeline);

            // then
            this.yamlBrokerMock.Verify(broker =>
                broker.SerializeToYaml(inputAspNetPipeline),
                    Times.Once);

            this.filesBrokerMock.Verify(broker =>
                broker.WriteToFile(inputPath, serialziedPipeline),
                    Times.Once);

            this.yamlBrokerMock.VerifyNoOtherCalls();
            this.filesBrokerMock.VerifyNoOtherCalls();
        }
    }
}
