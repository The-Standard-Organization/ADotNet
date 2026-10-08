// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
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
        public void ShouldTagAndReleaseWithGithubTokenAndContentsWritePermission()
        {
            // given
            var expectedPermissions = new Dictionary<string, string>
            {
                { "contents", "write" }
            };

            // when
            var tagJobV3 = new TagJobV3(
                runsOn: "ubuntu-latest",
                dependsOn: "build",
                projectRelativePath: "Project/Project.csproj",
                branchName: "main");

            // then
            tagJobV3.Permissions.Should().BeEquivalentTo(expectedPermissions);

            GithubTask checkoutStep =
                tagJobV3.Steps.Single(step => step.Name == "Checkout code");

            checkoutStep.Should().BeOfType<CheckoutTaskV5>();
            checkoutStep.With.Should().BeNull();

            GithubTask releaseStep =
                tagJobV3.Steps.Single(step => step.Name == "Create GitHub Release");

            releaseStep.Should().BeOfType<CreateGitHubReleaseTask>();
            releaseStep.Uses.Should().Be("actions/create-release@v1");

            ((CreateGitHubReleaseTask)releaseStep).EnvironmentVariables["GITHUB_TOKEN"]
                .Should().Be("${{ secrets.GITHUB_TOKEN }}");
        }

        [Fact]
        public void ShouldExtractProjectPropertiesWithVersionTwoTaskOnWindows()
        {
            // given / when
            var tagJobV3 = new TagJobV3(
                runsOn: BuildMachines.WindowsLatest,
                dependsOn: "build",
                projectRelativePath: "Project/Project.csproj",
                branchName: "main");

            // then
            tagJobV3.Steps.Should().NotContain(step => step is ExtractProjectPropertyTask);

            List<ExtractProjectPropertyTaskV2> extractSteps =
                tagJobV3.Steps.OfType<ExtractProjectPropertyTaskV2>().ToList();

            extractSteps.Select(step => step.Id)
                .Should().Equal("extract_version", "extract_package_release_notes");

            extractSteps.Should().OnlyContain(step => step.Run.Contains(">> $env:GITHUB_OUTPUT"));
        }
    }
}
