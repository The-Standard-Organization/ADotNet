// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV5s;

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
            this.job.Steps.Add(new CheckoutTaskV5 { Name = name });

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
            this.job.Steps.Add(new SetupDotNetTaskV5
            {
                Name = stepName,
                With = new TargetDotNetVersionV5
                {
                    DotNetVersion = version,
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
        /// Specifies the jobs that this job depends on.
        /// </summary>
        /// <param name="jobNames">The names of the jobs that this job depends on.</param>
        /// <returns>The current instance of <see cref="JobBuilder"/>.</returns>
        public JobBuilder DependsOn(params string[] jobNames)
        {
            this.job.Needs = jobNames;

            return this;
        }

        /// <summary>
        /// Sets a conditional expression that determines whether the job runs
        /// </summary>
        /// <param name="condition">The condition for the step.</param>
        /// <returns>The current instance of <see cref="JobBuilder"/>.</returns>
        public JobBuilder WithCondition(string condition)
        {
            this.job.If = condition;

            return this;
        }

        /// <summary>
        /// Adds an axis variable (e.g. "provider": ["sqlserver", "postgres"]) to the job's matrix strategy.
        /// </summary>
        /// <param name="variable">The name of the matrix axis variable.</param>
        /// <param name="values">The values for the matrix axis.</param>
        /// <returns>The current instance of <see cref="JobBuilder"/>.</returns>
        public JobBuilder AddMatrix(
            string variable,
            params string[] values)
        {
            this.job.Strategy ??= new Strategy();
            this.job.Strategy.Matrix ??= new Matrix();
            this.job.Strategy.Matrix.Variables ??= new Dictionary<string, List<string>>();
            this.job.Strategy.Matrix.Variables[variable] = new List<string>(values);

            return this;
        }

        /// <summary>
        /// Adds a matrix "include" combination, adding a new configuration or extending an existing one.
        /// </summary>
        /// <param name="include">The key/value pairs describing the combination to include.</param>
        /// <returns>The current instance of <see cref="JobBuilder"/>.</returns>
        public JobBuilder AddMatrixInclude(Dictionary<string, string> include)
        {
            this.job.Strategy ??= new Strategy();
            this.job.Strategy.Matrix ??= new Matrix();
            this.job.Strategy.Matrix.Include ??= new List<Dictionary<string, string>>();
            this.job.Strategy.Matrix.Include.Add(include);

            return this;
        }

        /// <summary>
        /// Adds a matrix "exclude" combination, removing a matching configuration.
        /// </summary>
        /// <param name="exclude">The key/value pairs describing the combination to exclude.</param>
        /// <returns>The current instance of <see cref="JobBuilder"/>.</returns>
        public JobBuilder AddMatrixExclude(Dictionary<string, string> exclude)
        {
            this.job.Strategy ??= new Strategy();
            this.job.Strategy.Matrix ??= new Matrix();
            this.job.Strategy.Matrix.Exclude ??= new List<Dictionary<string, string>>();
            this.job.Strategy.Matrix.Exclude.Add(exclude);

            return this;
        }

        /// <summary>
        /// Sets whether the job's matrix strategy cancels all in-progress jobs if any matrix job fails.
        /// GitHub Actions defaults to true; set this explicitly to override.
        /// </summary>
        /// <param name="failFast">Whether to fail fast.</param>
        /// <returns>The current instance of <see cref="JobBuilder"/>.</returns>
        public JobBuilder WithFailFast(bool failFast)
        {
            this.job.Strategy ??= new Strategy();
            this.job.Strategy.FailFast = failFast;

            return this;
        }

        /// <summary>
        /// Sets the maximum number of jobs that can run simultaneously from the matrix strategy.
        /// </summary>
        /// <param name="maxParallel">The maximum number of parallel jobs.</param>
        /// <returns>The current instance of <see cref="JobBuilder"/>.</returns>
        public JobBuilder WithMaxParallel(int maxParallel)
        {
            this.job.Strategy ??= new Strategy();
            this.job.Strategy.MaxParallel = maxParallel;

            return this;
        }

        /// <summary>
        /// Adds a generic run-based step, optionally with an id so later steps can reference its outputs.
        /// </summary>
        /// <param name="name">The name of the step.</param>
        /// <param name="runCommand">The command to execute for this step.</param>
        /// <param name="id">The id of the step.</param>
        /// <param name="shell">The shell to use for the step.</param>
        /// <returns>The current instance of <see cref="JobBuilder"/>.</returns>
        public JobBuilder AddGenericStep(
            string name,
            string runCommand,
            string id = null,
            string shell = null)
        {
            this.job.Steps.Add(new GithubTask
            {
                Id = id,
                Name = name,
                Run = runCommand,
                Shell = shell
            });

            return this;
        }

        /// <summary>
        /// Adds a step to the job that uses a specific action (e.g. "actions/checkout@v3"), 
        /// optionally with an id, input parameters, and environment variables.
        /// </summary>
        /// <param name="name">The name of the step.</param>
        /// <param name="uses">The GitHub Action to use.</param>
        /// <param name="id">The id of the step.</param>
        /// <param name="with">The input parameters for the action.</param>
        /// <param name="environmentVariables">The environment variables for the step.</param>
        /// <returns>The current instance of <see cref="JobBuilder"/>.</returns>
        public JobBuilder AddActionStep(
            string name,
            string uses,
            string id = null,
            Dictionary<string, string> with = null,
            Dictionary<string, string> environmentVariables = null)
        {
            this.job.Steps.Add(new GithubTask
            {
                Id = id,
                Name = name,
                Uses = uses,
                With = with,
                EnvironmentVariables = environmentVariables
            });

            return this;
        }

        /// <summary>
        /// Attaches a service container to the job.
        /// </summary>
        /// <param name="id">The service id, used as the key under the job's "services" map.</param>
        /// <param name="service">The service container definition.</param>
        /// <returns>The current instance of <see cref="JobBuilder"/>.</returns>
        public JobBuilder AddService(string id, Service service)
        {
            this.job.Services ??= new Dictionary<string, Service>();
            this.job.Services[id] = service;

            return this;
        }

        /// <summary>
        /// Builds and returns the configured job.
        /// </summary>
        /// <returns>The configured <see cref="Job"/> instance.</returns>
        public Job Build() => this.job;
    }
}