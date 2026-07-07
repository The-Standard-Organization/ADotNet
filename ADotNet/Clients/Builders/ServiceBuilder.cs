// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;

namespace ADotNet.Clients.Builders
{
    /// <summary>
    /// A builder to create a service for a GitHub Actions workflow.
    /// </summary>
    public class ServiceBuilder
    {
        private readonly Service service;

        internal ServiceBuilder()
        {
            this.service = new Service
            {
                Environment = new Dictionary<string, string>(),
                Ports = new List<string>()
            };
        }

        /// <summary>
        /// Sets the image for the service.
        /// </summary>
        /// <param name="image">The image for the service.</param>
        /// <returns>The current instance of <see cref="ServiceBuilder"/>.</returns>
        public ServiceBuilder WithImage(string image)
        {
            this.service.Image = image;
            return this;
        }

        /// <summary>
        /// Sets the environment variables for the service.
        /// </summary>
        /// <param name="key">The key for the environment variable.</param>
        /// <param name="value">The value for the environment variable.</param>
        /// <returns>The current instance of <see cref="ServiceBuilder"/>.</returns>
        public ServiceBuilder AddEnvironmentVariable(
            string key,
            string value)
        {
            this.service.Environment ??=
                new Dictionary<string, string>();

            this.service.Environment[key] = value;

            return this;
        }

        /// <summary>
        /// Sets the ports for the service.
        /// </summary>
        /// <param name="hostPort">The host port to add.</param>
        /// <param name="containerPort">The container port to add.</param>
        /// <returns>The current instance of <see cref="ServiceBuilder"/>.</returns>
        public ServiceBuilder AddPort(
            int hostPort,
            int containerPort)
        {
            this.service.Ports ??= new();

            this.service.Ports.Add($"{hostPort}:{containerPort}");

            return this;
        }

        /// <summary>
        /// Builds and returns the configured service.
        /// </summary>
        /// <returns>The configured <see cref="Service"/> instance.</returns>
        internal Service Build() => this.service;
    }
}