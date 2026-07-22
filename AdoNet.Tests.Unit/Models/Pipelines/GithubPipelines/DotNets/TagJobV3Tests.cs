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
    public class TagJobV3Tests
    {
        [Fact]
        public void ShouldTagAndReleaseUsingDefaultGithubTokenWithContentsWritePermission()
        {
            // given / when
            var tagJobV3 = new TagJobV3(
                runsOn: "ubuntu-latest",
                dependsOn: "build",
                projectRelativePath: "Project/Project.csproj",
                branchName: "main");

            // then
            tagJobV3.Permissions.Should().ContainKey("contents")
                .WhoseValue.Should().Be("write");

            CheckoutTaskV5 checkoutStep =
                tagJobV3.Steps
                    .Single(step => step.Name == "Checkout code")
                        .Should().BeOfType<CheckoutTaskV5>().Subject;

            checkoutStep.With.Should().BeNull();

            CreateGitHubReleaseTask releaseStep =
                tagJobV3.Steps
                    .Single(step => step.Name == "Create GitHub Release")
                        .Should().BeOfType<CreateGitHubReleaseTask>().Subject;

            releaseStep.Run.Should().BeNull();
            releaseStep.Uses.Should().Be("actions/create-release@v1");

            releaseStep.EnvironmentVariables.Should()
                .Contain("GITHUB_TOKEN", "${{ secrets.GITHUB_TOKEN }}");
        }
    }
}
