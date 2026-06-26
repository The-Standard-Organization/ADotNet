// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.Linq;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using FluentAssertions;
using Xunit;

namespace ADotNet.Tests.Unit.Models.Pipelines.GithubPipelines.DotNets
{
    public class TagJobV2Tests
    {
        [Fact]
        public void ShouldCheckoutWithVersionFiveAndCreateReleaseViaGithubCli()
        {
            // given / when
            var tagJobV2 = new TagJobV2(
                runsOn: "ubuntu-latest",
                dependsOn: "build",
                projectRelativePath: "Project/Project.csproj",
                githubToken: "${{ secrets.PAT_FOR_TAGGING }}",
                branchName: "main");

            // then
            tagJobV2.Steps
                .Single(step => step.Name == "Checkout code")
                    .Should().BeOfType<CheckoutTaskV5>();

            GithubTask releaseStep =
                tagJobV2.Steps.Single(step => step.Name == "Create GitHub Release");

            releaseStep.Run.Should().BeNull();
            releaseStep.Uses.Should().Be("actions/create-release@v1");
        }
    }
}
