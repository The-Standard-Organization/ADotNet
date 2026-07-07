using ADotNet.Clients.Builders;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using FluentAssertions;
using Xunit;

namespace ADotNet.Tests.Unit.Clients.Builders
{
    public partial class ServiceBuilderTests
    {
        [Fact]
        public void ShouldSetImage()
        {
            // given
            string expectedImage = GetRandomString();

            // when
            Service actualService = serviceBuilder
                .WithImage(expectedImage)
                .Build();

            // then
            actualService.Image.Should().Be(expectedImage);
        }

        [Fact]
        public void ShouldAddEnvironmentVariable()
        {
            // given
            string key = GetRandomString();
            string value = GetRandomString();

            // when
            Service actualService = serviceBuilder
                .AddEnvironmentVariable(key, value)
                .Build();

            // then
            actualService.Environment.Should().ContainKey(key);
            actualService.Environment[key].Should().Be(value);
        }
    }
}