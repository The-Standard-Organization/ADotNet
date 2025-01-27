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
    /// <summary>
    /// A builder to create a job for a GitHub Actions workflow.
    /// </summary>
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

        /// <summary>
        /// Sets the name of the job.
        /// </summary>
        /// <param name="name">The name of the job.</param>
        /// <returns>The current instance of <see cref="JobBuilder"/>.</returns>
        public JobBuilder WithName(string name)
        {
            this.job.Name = name;

            return this;
        }

        /// <summary>
        /// Specifies the machine on which the job will run.
        /// </summary>
        /// <param name="machine">The machine or environment to run the job on.</param>
        /// <returns>The current instance of <see cref="JobBuilder"/>.</returns>
        public JobBuilder RunsOn(string machine)
        {
            this.job.RunsOn = machine;

            return this;
        }

        /// <summary>
        /// Adds an environment variable to the job.
        /// </summary>
        /// <param name="key">The key of the environment variable.</param>
        /// <param name="value">The value of the environment variable.</param>
        /// <returns>The current instance of <see cref="JobBuilder"/>.</returns>
        public JobBuilder AddEnvironmentVariable(string key, string value)
        {
            this.job.EnvironmentVariables ??= new Dictionary<string, string>();

            this.job.EnvironmentVariables[key] = value;

            return this;
        }

        /// <summary>
        /// Adds multiple environment variables to the job.
        /// </summary>
        /// <param name="variables">A dictionary of environment variables to add.</param>
        /// <returns>The current instance of <see cref="JobBuilder"/>.</returns>
        public JobBuilder AddEnvironmentVariables(Dictionary<string, string> variables)
        {
            this.job.EnvironmentVariables ??= new Dictionary<string, string>();

            foreach (var variable in variables)
            {
                this.job.EnvironmentVariables[variable.Key] = variable.Value;
            }

            return this;
        }

        /// <summary>
        /// Adds a checkout step to the job.
        /// </summary>
        /// <param name="name">The name of the checkout step (default: "Check out").</param>
        /// <returns>The current instance of <see cref="JobBuilder"/>.</returns>
        public JobBuilder AddCheckoutStep(string name = "Check out")
        {
            this.job.Steps.Add(new CheckoutTaskV2 { Name = name });

            return this;
        }

        /// <summary>
        /// Adds a setup step for a specific .NET version to the job.
        /// </summary>
        /// <param name="version">The version of .NET to set up.</param>
        /// <param name="stepName">The name of the setup step (default: "Setup Dot Net Version").</param>
        /// <param name="includePrerelease">Specifies whether to include prerelease versions.</param>
        /// <returns>The current instance of <see cref="JobBuilder"/>.</returns>
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

        /// <summary>
        /// Adds a restore step to the job.
        /// </summary>
        /// <param name="name">The name of the restore step (default: "Restore").</param>
        /// <returns>The current instance of <see cref="JobBuilder"/>.</returns>
        public JobBuilder AddRestoreStep(string name = "Restore")
        {
            this.job.Steps.Add(new RestoreTask { Name = name });

            return this;
        }

        /// <summary>
        /// Adds a build step to the job.
        /// </summary>
        /// <param name="name">The name of the build step (default: "Build").</param>
        /// <returns>The current instance of <see cref="JobBuilder"/>.</returns>
        public JobBuilder AddBuildStep(string name = "Build")
        {
            this.job.Steps.Add(new DotNetBuildTask { Name = name });

            return this;
        }

        /// <summary>
        /// Adds a test step to the job.
        /// </summary>
        /// <param name="name">The name of the test step (default: "Test").</param>
        /// <param name="command">The command to execute the test 
        /// (default: "dotnet test --no-build --verbosity normal").</param>
        /// <returns>The current instance of <see cref="JobBuilder"/>.</returns>
        public JobBuilder AddTestStep(string name = "Test", string command = null)
        {
            this.job.Steps.Add(new TestTask
            {
                Name = name,
                Run = command ?? "dotnet test --no-build --verbosity normal"
            });

            return this;
        }

        /// <summary>
        /// Adds a generic step to the job with a custom command.
        /// </summary>
        /// <param name="name">The name of the step.</param>
        /// <param name="runCommand">The command to execute for this step.</param>
        /// <returns>The current instance of <see cref="JobBuilder"/>.</returns>
        public JobBuilder AddGenericStep(string name, string runCommand)
        {
            this.job.Steps.Add(new GithubTask
            {
                Name = name,
                Run = runCommand
            });

            return this;
        }

        /// <summary>
        /// Builds and returns the configured job.
        /// </summary>
        /// <returns>The configured <see cref="Job"/> instance.</returns>
        public Job Build() => this.job;
    }
}