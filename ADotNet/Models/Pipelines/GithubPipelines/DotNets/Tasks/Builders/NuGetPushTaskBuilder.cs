// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.Builders
{
    public class NuGetPushTaskBuilder
    {
        private NuGetPushTask nugetPushTask;

        public NuGetPushTaskBuilder()
        {
            nugetPushTask = new NuGetPushTask();
        }

        public NuGetPushTaskBuilder WithName(string name)
        {
            nugetPushTask.Name = name;
            return this;
        }

        public NuGetPushTaskBuilder WithSearchPath(string searchPath)
        {
            nugetPushTask.SearchPath = searchPath;
            return this;
        }

        public NuGetPushTaskBuilder WithApiKey(string apiKey)
        {
            nugetPushTask.ApiKey = apiKey;
            return this;
        }

        public NuGetPushTaskBuilder WithDestination(string destination)
        {
            nugetPushTask.Destination = destination;
            return this;
        }

        public NuGetPushTask Build()
        {
            if (!string.IsNullOrEmpty(nugetPushTask.SearchPath))
                nugetPushTask.Run += $" \"{nugetPushTask.SearchPath}\"";

            if (!string.IsNullOrEmpty(nugetPushTask.ApiKey))
                nugetPushTask.Run += $" --api-key {nugetPushTask.ApiKey}";

            if (!string.IsNullOrEmpty(nugetPushTask.Destination))
                nugetPushTask.Run += $" --source \"{nugetPushTask.Destination}\"";

            return nugetPushTask;
        }
    }
}
