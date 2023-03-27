// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.Builders
{
    public class DotNetBuildTaskBuilder
    {
        private DotNetBuildTask dotnetBuildTask;

        public DotNetBuildTaskBuilder()
        {
            dotnetBuildTask = new DotNetBuildTask
            {
                Run = "dotnet build"
            };
        }

        public DotNetBuildTaskBuilder WithName(string name)
        {
            dotnetBuildTask.Name = name;
            return this;
        }

        public DotNetBuildTaskBuilder WithRestore(bool restore = true)
        {
            dotnetBuildTask.Restore = restore;
            return this;
        }

        public DotNetBuildTask Build()
        {
            if (!dotnetBuildTask.Restore)
                dotnetBuildTask.Run += " --no-restore";

            return dotnetBuildTask;
        }
    }
}
