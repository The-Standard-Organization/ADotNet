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
    public class SetAuthorAsPrAssigneeJobV2Tests
    {
        [Fact]
        public void ShouldAssignAuthorUsingGithubScriptActionVersionEight()
        {
            // given
            string expectedUses = "actions/github-script@v8";

            // when
            var setAuthorAsPrAssigneeJobV2 = new SetAuthorAsPrAssigneeJobV2(runsOn: "ubuntu-latest");

            // then
            GithubTask assignStep =
                setAuthorAsPrAssigneeJobV2.Steps.Single(step => step.Name == "Set Author As PR Assignee");

            assignStep.Uses.Should().Be(expectedUses);
        }

        [Fact]
        public void ShouldOnlyRunForSameRepositoryPullRequests()
        {
            // given
            string expectedIf =
                "${{ github.event.pull_request.head.repo.full_name == github.repository }}";

            // when
            var setAuthorAsPrAssigneeJobV2 = new SetAuthorAsPrAssigneeJobV2(runsOn: "ubuntu-latest");

            // then
            setAuthorAsPrAssigneeJobV2.If.Should().Be(expectedIf);
        }
    }
}
