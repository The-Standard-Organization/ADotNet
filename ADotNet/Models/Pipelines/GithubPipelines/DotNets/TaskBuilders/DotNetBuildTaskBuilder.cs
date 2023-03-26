// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.TaskBuilders
{
    public class DotNetBuildTaskBuilder
    {
        private DotNetBuildTask dotnetBuildTask;

        public DotNetBuildTaskBuilder()
        {
            this.dotnetBuildTask = new DotNetBuildTask
            {
                Run = "dotnet build"
            };
        }

        public DotNetBuildTaskBuilder WithName(string name)
        {
            this.dotnetBuildTask.Name = name;
            return this;
        }

        public DotNetBuildTaskBuilder WithRestore(bool restore)
        {
            this.dotnetBuildTask.Restore = restore;
            return this;
        }

        public DotNetBuildTask Build()
        {
            if (!this.dotnetBuildTask.Restore)
            {
                this.dotnetBuildTask.Run += " --no-restore";
            }

            return this.dotnetBuildTask;
        }
    }
}
