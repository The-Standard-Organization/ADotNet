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

        [Fact]
        public void ShouldAddPort()
        {
            // given
            int hostPort = GetRandomNumber();
            int containerPort = GetRandomNumber();

            // when
            Service actualService = serviceBuilder
                .AddPort(hostPort, containerPort)
                .Build();

            // then
            actualService.Ports.Should().Contain($"{hostPort}:{containerPort}");
        }

        [Fact]
        public void ShouldSetOptions()
        {
            // given
            string expectedOptions = GetRandomString();

            // when
            Service actualService = serviceBuilder
                .WithOptions(expectedOptions)
                .Build();

            // then
            actualService.Options.Should().Be(expectedOptions);
        }
    }
}