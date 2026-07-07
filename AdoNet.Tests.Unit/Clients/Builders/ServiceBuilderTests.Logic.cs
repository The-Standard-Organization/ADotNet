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

        [Fact]
        public void ShouldBuildServiceWithMultipleConfigurations()
        {
            // given
            string image = GetRandomString();
            string options = GetRandomString();
            int hostPort = GetRandomNumber();
            int containerPort = GetRandomNumber();

            // when
            Service actualService = serviceBuilder
                .WithImage(image)
                .AddEnvironmentVariable("POSTGRES_USER", "postgres")
                .AddEnvironmentVariable("POSTGRES_PASSWORD", "postgres")
                .AddPort(hostPort,containerPort)
                .WithOptions(options)
                .Build();

            // then
            actualService.Image.Should().Be(image);
            actualService.Options.Should().Be(options);
            actualService.Ports.Should().ContainSingle()
                .Which.Should().Be($"{hostPort}:{containerPort}");

            actualService.Environment.Should().ContainKey("POSTGRES_USER");
            actualService.Environment.Should().ContainKey("POSTGRES_PASSWORD");
        }
    }
}