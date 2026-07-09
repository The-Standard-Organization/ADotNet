// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV5s;
using FluentAssertions;
using Xunit;

namespace ADotNet.Tests.Unit.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    public class SetupDotNetTaskV5Tests
    {
        [Fact]
        public void ShouldUseSetupDotNetActionVersionFive()
        {
            // given
            string expectedUses = "actions/setup-dotnet@v5";

            // when
            var setupDotNetTaskV5 = new SetupDotNetTaskV5();

            // then
            setupDotNetTaskV5.Uses.Should().Be(expectedUses);
        }

        [Fact]
        public void ShouldCarryTheTargetDotNetVersion()
        {
            // given
            string inputDotNetVersion = "10.0.100";

            // when
            var setupDotNetTaskV5 = new SetupDotNetTaskV5
            {
                With = new TargetDotNetVersionV5 { DotNetVersion = inputDotNetVersion }
            };

            // then
            setupDotNetTaskV5.With.DotNetVersion.Should().Be(inputDotNetVersion);
        }
    }
}
