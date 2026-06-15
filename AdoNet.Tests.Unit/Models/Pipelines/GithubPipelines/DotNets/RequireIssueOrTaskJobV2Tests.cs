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
    public class RequireIssueOrTaskJobV2Tests
    {
        [Fact]
        public void ShouldComposeSingleExcludedAuthorIntoSkipCondition()
        {
            // given
            string excludedAuthors = "dependabot[bot]";

            string expectedCondition =
                "${{ !contains(',dependabot[bot],', " +
                    "format(',{0},', steps.get_pr_info.outputs.prOwner)) }}";

            // when
            var requireIssueOrTaskJobV2 = new RequireIssueOrTaskJobV2(excludedAuthors);

            // then
            GithubTask checkStep = requireIssueOrTaskJobV2.Steps
                .Single(step => step.Id == "check_for_issues_or_tasks");

            checkStep.If.Should().Be(expectedCondition);
        }

        [Fact]
        public void ShouldComposeMultipleExcludedAuthorsAndStripSpaces()
        {
            // given
            string excludedAuthors = "app, dependabot[bot]";

            string expectedCondition =
                "${{ !contains(',app,dependabot[bot],', " +
                    "format(',{0},', steps.get_pr_info.outputs.prOwner)) }}";

            // when
            var requireIssueOrTaskJobV2 = new RequireIssueOrTaskJobV2(excludedAuthors);

            // then
            GithubTask checkStep = requireIssueOrTaskJobV2.Steps
                .Single(step => step.Id == "check_for_issues_or_tasks");

            checkStep.If.Should().Be(expectedCondition);
        }
    }
}
