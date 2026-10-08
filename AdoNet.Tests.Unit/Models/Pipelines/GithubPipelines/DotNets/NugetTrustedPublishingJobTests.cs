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
    public class NugetTrustedPublishingJobTests
    {
        [Fact]
        public void ShouldLoginWithTrustedPublishingBeforePushingNugetPackage()
        {
            // given
            string nugetUser = "${{ secrets.NUGET_USER }}";

            var expectedPermissions = new Dictionary<string, string>
            {
                { "id-token", "write" },
                { "contents", "read" }
            };

            // when
            var nugetTrustedPublishingJob = new NugetTrustedPublishingJob(
                runsOn: "ubuntu-latest",
                dependsOn: "add_tag",
                dotNetVersion: "10.0.100",
                nugetUser: nugetUser);

            // then
            nugetTrustedPublishingJob.Permissions.Should().BeEquivalentTo(expectedPermissions);

            GithubTask loginStep =
                nugetTrustedPublishingJob.Steps.Single(step => step.Name == "NuGet Login");

            loginStep.Id.Should().Be("nuget_login");
            loginStep.Uses.Should().Be("NuGet/login@v1");
            loginStep.With["user"].Should().Be(nugetUser);

            GithubTask pushStep =
                nugetTrustedPublishingJob.Steps.Single(step => step.Name == "Push NuGet Package");

            pushStep.Should().BeOfType<NugetPushTask>();
            ((NugetPushTask)pushStep).Run
                .Should().Contain("--api-key ${{ steps.nuget_login.outputs.NUGET_API_KEY }}");

            nugetTrustedPublishingJob.Steps.IndexOf(loginStep)
                .Should().Be(nugetTrustedPublishingJob.Steps.IndexOf(pushStep) - 1);
        }
    }
}
