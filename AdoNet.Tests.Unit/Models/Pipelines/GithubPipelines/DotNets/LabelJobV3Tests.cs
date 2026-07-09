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
    public class LabelJobV3Tests
    {
        [Fact]
        public void ShouldApplyLabelUsingGithubScriptActionVersionEight()
        {
            // given
            string expectedUses = "actions/github-script@v8";

            // when
            var labelJobV3 = new LabelJobV3(runsOn: "ubuntu-latest");

            // then
            GithubTask applyLabelStep =
                labelJobV3.Steps.Single(step => step.Name == "Apply Label");

            applyLabelStep.Uses.Should().Be(expectedUses);
        }

        [Fact]
        public void ShouldOnlyRunForSameRepositoryPullRequests()
        {
            // given
            string expectedIf =
                "${{ github.event.pull_request.head.repo.full_name == github.repository }}";

            // when
            var labelJobV3 = new LabelJobV3(runsOn: "ubuntu-latest");

            // then
            labelJobV3.If.Should().Be(expectedIf);
        }
    }
}
