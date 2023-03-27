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

        public NuGetPushTaskBuilder() =>
            this.nugetPushTask = new NuGetPushTask();

        public NuGetPushTaskBuilder WithName(string name)
        {
            this.nugetPushTask.Name = name;

            return this;
        }

        public NuGetPushTaskBuilder WithSearchPath(string searchPath)
        {
            this.nugetPushTask.SearchPath = searchPath;

            return this;
        }

        public NuGetPushTaskBuilder WithApiKey(string apiKey)
        {
            this.nugetPushTask.ApiKey = apiKey;

            return this;
        }

        public NuGetPushTaskBuilder WithDestination(string destination)
        {
            this.nugetPushTask.Destination = destination;

            return this;
        }

        public NuGetPushTask Build()
        {
            if (!string.IsNullOrEmpty(this.nugetPushTask.SearchPath))
            {
                this.nugetPushTask.Run += $" \"{this.nugetPushTask.SearchPath}\"";
            }

            if (!string.IsNullOrEmpty(this.nugetPushTask.ApiKey))
            {
                this.nugetPushTask.Run += $" --api-key {this.nugetPushTask.ApiKey}";
            }

            if (!string.IsNullOrEmpty(this.nugetPushTask.Destination))
            {
                this.nugetPushTask.Run += $" --source \"{this.nugetPushTask.Destination}\"";
            }

            return this.nugetPushTask;
        }
    }
}
