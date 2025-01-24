// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using ADotNet.Clients.Builders;
using FluentAssertions;
using Xunit;

namespace ADotNet.Tests.Unit.Clients.Builders
{
    public partial class GitHubPipelineBuilderTests
    {
        [Fact]
        public void ShouldCreateNewPipeline()
        {
            // given..when
            var builder = GitHubPipelineBuilder.CreateNewPipeline();

            // then
            builder.Should().NotBeNull();
        }

        [Fact]
        public void ShouldSetPipelineName()
        {
            // given
            string inputName = "My GitHub Pipeline";
            string expectedName = inputName;

            // when
            var pipelineBuilder = GitHubPipelineBuilder.CreateNewPipeline()
                .SetName(inputName);

            var actualPipeline = GetPipeline(pipelineBuilder);

            // then
            actualPipeline.Should().NotBeNull();
            actualPipeline.Name.Should().BeEquivalentTo(expectedName);
        }
    }
}
