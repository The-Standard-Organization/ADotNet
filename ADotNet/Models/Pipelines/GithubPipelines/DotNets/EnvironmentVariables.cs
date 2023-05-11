// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public sealed class EnvironmentVariables
    {
        public static string IsGitHubReleaseCandidate() =>
            "${{\n"
            + "  (\n"
            + "    github.event_name == 'pull_request' &&\n"
            + "    startsWith(github.event.pull_request.title, 'RELEASES:') &&\n"
            + "    contains(github.event.pull_request.labels.*.name, 'RELEASES')\n"
            + "  )\n"
            + "  ||\n"
            + "  (\n"
            + "    github.event_name == 'push' &&\n"
            + "    startsWith(github.event.head_commit.message, 'RELEASES:') &&\n"
            + "    github.ref_name == 'RELEASE'\n"
            + "  )\n"
            + "}}";
    }
}
