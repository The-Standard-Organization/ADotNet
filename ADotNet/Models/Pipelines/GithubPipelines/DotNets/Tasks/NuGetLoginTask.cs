// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.Collections.Generic;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    /// <summary>
    /// A task that exchanges the GitHub Actions OIDC token for a short-lived NuGet API key
    /// using NuGet Trusted Publishing. Exposes the key through the step output NUGET_API_KEY.
    /// Requires the job to grant the 'id-token: write' permission.
    /// </summary>
    public sealed class NuGetLoginTask : GithubTask
    {
        public NuGetLoginTask(string nugetUser)
        {
            Id = "login";
            Uses = "NuGet/login@v1";

            With = new Dictionary<string, string>
            {
                { "user", nugetUser }
            };
        }
    }
}
