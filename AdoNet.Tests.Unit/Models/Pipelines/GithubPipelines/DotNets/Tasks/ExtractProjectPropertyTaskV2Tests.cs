// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using FluentAssertions;
using Xunit;

namespace ADotNet.Tests.Unit.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    public class ExtractProjectPropertyTaskV2Tests
    {
        [Fact]
        public void ShouldWriteToGithubOutputEnvironmentVariableOnWindows()
        {
            // given / when
            var extractProjectPropertyTaskV2 = new ExtractProjectPropertyTaskV2(
                name: "Extract Version",
                id: "extract_version",
                projectRelativePath: "MyProject/MyProject.csproj",
                propertyName: "Version",
                stepVariableName: "version_number",
                runsOn: BuildMachines.WindowsLatest);

            // then
            extractProjectPropertyTaskV2.Shell.Should().Be(ShellEnvironments.PowerShellCore);

            extractProjectPropertyTaskV2.Run
                .Should().Contain("echo \"version_number<<EOF\" >> $env:GITHUB_OUTPUT");

            extractProjectPropertyTaskV2.Run
                .Should().Contain("echo \"$version_number\" >> $env:GITHUB_OUTPUT");

            extractProjectPropertyTaskV2.Run
                .Should().Contain("echo \"EOF\" >> $env:GITHUB_OUTPUT");

            extractProjectPropertyTaskV2.Run
                .Should().NotContain(">> $GITHUB_OUTPUT");
        }

        [Fact]
        public void ShouldWriteToGithubOutputEnvironmentVariableOnLinux()
        {
            // given / when
            var extractProjectPropertyTaskV2 = new ExtractProjectPropertyTaskV2(
                name: "Extract Version",
                id: "extract_version",
                projectRelativePath: "MyProject/MyProject.csproj",
                propertyName: "Version",
                stepVariableName: "version_number",
                runsOn: BuildMachines.UbuntuLatest);

            // then
            extractProjectPropertyTaskV2.Shell.Should().Be(ShellEnvironments.Bash);

            extractProjectPropertyTaskV2.Run
                .Should().Contain("echo \"version_number<<EOF\" >> $GITHUB_OUTPUT");

            extractProjectPropertyTaskV2.Run
                .Should().Contain("echo \"$version_number\" >> $GITHUB_OUTPUT");

            extractProjectPropertyTaskV2.Run
                .Should().Contain("echo \"EOF\" >> $GITHUB_OUTPUT");
        }
    }
}
