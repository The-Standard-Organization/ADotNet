// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.Linq;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV5s;
using FluentAssertions;
using Xunit;

namespace ADotNet.Tests.Unit.Models.Pipelines.GithubPipelines.DotNets
{
    public class PublishToNugetTests
    {
        [Fact]
        public void ShouldPublishToNugetUsingTrustedPublishing()
        {
            // given / when
            var publishToNuget = new PublishToNuget(
                runsOn: "ubuntu-latest",
                dependsOn: "add_tag",
                dotNetVersion: "10.0.100",
                nugetUser: "the-standard-organization");

            // then
            publishToNuget.Permissions.Should().ContainKey("id-token")
                .WhoseValue.Should().Be("write");

            publishToNuget.Permissions.Should().ContainKey("contents")
                .WhoseValue.Should().Be("read");

            publishToNuget.Steps
                .Single(step => step.Name == "Check out")
                    .Should().BeOfType<CheckoutTaskV5>();

            publishToNuget.Steps
                .Single(step => step.Name == "Setup .Net")
                    .Should().BeOfType<SetupDotNetTaskV5>();

            NuGetLoginTask loginStep =
                publishToNuget.Steps
                    .Single(step => step.Name == "NuGet Login")
                        .Should().BeOfType<NuGetLoginTask>().Subject;

            loginStep.Id.Should().Be("login");
            loginStep.Uses.Should().Be("NuGet/login@v1");
            loginStep.With.Should().Contain("user", "the-standard-organization");

            NugetPushTask pushStep =
                publishToNuget.Steps
                    .Single(step => step.Name == "Push NuGet Package")
                        .Should().BeOfType<NugetPushTask>().Subject;

            pushStep.Run.Should().Contain("${{ steps.login.outputs.NUGET_API_KEY }}");
            pushStep.Run.Should().NotContain("secrets.");
        }

        [Fact]
        public void ShouldDeriveNugetUserFromRepositoryOwnerByDefault()
        {
            // given / when
            var publishToNuget = new PublishToNuget(
                runsOn: "ubuntu-latest",
                dependsOn: "add_tag",
                dotNetVersion: "10.0.100");

            // then
            NuGetLoginTask loginStep =
                publishToNuget.Steps
                    .Single(step => step.Name == "NuGet Login")
                        .Should().BeOfType<NuGetLoginTask>().Subject;

            loginStep.With.Should().Contain("user", "${{ github.repository_owner }}");
        }
    }
}
