// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using FluentAssertions;
using Xunit;

namespace ADotNet.Tests.Unit.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    public class CheckoutTaskV5Tests
    {
        [Fact]
        public void ShouldUseCheckoutActionVersionFive()
        {
            // given
            string expectedUses = "actions/checkout@v5";

            // when
            var checkoutTaskV5 = new CheckoutTaskV5();

            // then
            checkoutTaskV5.Uses.Should().Be(expectedUses);
        }
    }
}
