// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;

namespace ADotNet.Clients.Builders
{
    public class JobBuilder
    {
        private readonly Job job;

        internal JobBuilder()
        {
            this.job = new Job
            {
                Steps = new List<GithubTask>(),
                EnvironmentVariables = null
            };
        }

        public JobBuilder WithName(string name)
        {
            this.job.Name = name;
            return this;
        }

        public JobBuilder RunsOn(string machine)
        {
            this.job.RunsOn = machine;
            return this;
        }

        public JobBuilder AddEnvironmentVariable(string key, string value)
        {
            this.job.EnvironmentVariables ??=
                new Dictionary<string, string>();

            this.job.EnvironmentVariables[key] = value;
            return this;
        }

        public JobBuilder AddEnvironmentVariables(Dictionary<string, string> variables)
        {
            this.job.EnvironmentVariables ??=
                new Dictionary<string, string>();

            foreach (var variable in variables)
            {
                this.job.EnvironmentVariables[variable.Key] = variable.Value;
            }
            return this;
        }

        public JobBuilder AddCheckoutStep(string name = "Check out")
        {
            this.job.Steps.Add(new CheckoutTaskV2 { Name = name });
            return this;
        }

        public JobBuilder AddSetupDotNetStep(
            string version,
            string stepName = "Setup Dot Net Version",
            bool includePrerelease = false)
        {
            this.job.Steps.Add(new SetupDotNetTaskV1
            {
                Name = stepName,
                TargetDotNetVersion = new TargetDotNetVersion
                {
                    DotNetVersion = version,
                    IncludePrerelease = includePrerelease
                }
            });
            return this;
        }

        public JobBuilder AddRestoreStep(string name = "Restore")
        {
            this.job.Steps.Add(new RestoreTask { Name = name });
            return this;
        }

        public JobBuilder AddBuildStep(string name = "Build")
        {
            this.job.Steps.Add(new DotNetBuildTask { Name = name });
            return this;
        }

        public JobBuilder AddTestStep(string name = "Test", string command = null)
        {
            this.job.Steps.Add(new TestTask
            {
                Name = name,
                Run = command ?? "dotnet test --no-build --verbosity normal"
            });
            return this;
        }

        public JobBuilder AddGenericStep(string name, string runCommand)
        {
            this.job.Steps.Add(new GithubTask
            {
                Name = name,
                Run = runCommand
            });
            return this;
        }

        public Job Build() => this.job;
    }

}
