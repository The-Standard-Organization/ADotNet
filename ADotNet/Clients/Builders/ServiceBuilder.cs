// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;

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
        /// Builds and returns the configured service.
        /// </summary>
        /// <returns>The configured <see cref="Service"/> instance.</returns>
        internal Service Build() => this.service;
    }
}