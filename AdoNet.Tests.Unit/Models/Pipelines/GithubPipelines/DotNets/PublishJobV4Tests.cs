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
    public class PublishJobV4Tests
    {
        [Fact]
        public void ShouldUseVersionFiveCheckoutAndSetupDotNetTasks()
        {
            // given / when
            var publishJobV4 = new PublishJobV4(
                runsOn: "ubuntu-latest",
                dependsOn: "build",
                dotNetVersion: "10.0.100",
                nugetApiKey: "${{ secrets.NUGET_API_KEY }}");

            // then
            publishJobV4.Steps
                .Single(step => step.Name == "Check out")
                    .Should().BeOfType<CheckoutTaskV5>();

            publishJobV4.Steps
                .Single(step => step.Name == "Setup .Net")
                    .Should().BeOfType<SetupDotNetTaskV5>();
        }
    }
}
