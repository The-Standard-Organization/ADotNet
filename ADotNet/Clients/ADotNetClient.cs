// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using ADotNet.Brokers.IOs;
using ADotNet.Brokers.Serializers;
using ADotNet.Services;

namespace ADotNet.Clients
{
    public class ADotNetClient
    {
        private readonly IAdoService adoService;

        public ADotNetClient()
        {
            var yamlBroker = new YamlBroker();
            var filesBroker = new FilesBroker();

            this.adoService = new AdoService(
                yamlBroker,
                filesBroker);
        }

        public void SerializeAndWriteToFile(object adoPipeline, string path) =>
            this.adoService.SerializeAndWriteToFile(path, adoPipeline);
    }
}
