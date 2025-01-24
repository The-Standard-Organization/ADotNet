// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using ADotNet.Clients.Builders;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;

namespace ADotNet.Tests.Unit.Clients.Builders
{
    public partial class GitHubPipelineBuilderTests
    {
        private static GithubPipeline GetPipeline(GitHubPipelineBuilder builder)
        {
            var privateField = typeof(GitHubPipelineBuilder)
                .GetField("githubPipeline", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            return (GithubPipeline)privateField.GetValue(builder);
        }
    }
}
