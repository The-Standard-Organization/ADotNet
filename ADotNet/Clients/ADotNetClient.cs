// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using ADotNet.Brokers.IOs;
using ADotNet.Brokers.Serializers;
using ADotNet.Services.Builds;

namespace ADotNet.Clients
{
    public class ADotNetClient
    {
        private readonly IBuildService buildService;

        public ADotNetClient()
        {
            var yamlBroker = new YamlBroker();
            var filesBroker = new FilesBroker();

            this.buildService = new BuildService(
                yamlBroker,
                filesBroker);
        }

        public void SerializeAndWriteToFile(object adoPipeline, string path) =>
            this.buildService.SerializeAndWriteToFile(path, adoPipeline);
    }
}
