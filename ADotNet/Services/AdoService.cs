// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using ADotNet.Brokers.IOs;
using ADotNet.Brokers.Serializers;

namespace ADotNet.Services
{
    public class AdoService : IAdoService
    {
        private readonly IYamlBroker yamlBroker;
        private readonly IFilesBroker filesBroker;

        public AdoService(
            IYamlBroker yamlBroker,
            IFilesBroker filesBroker)
        {
            this.yamlBroker = yamlBroker;
            this.filesBroker = filesBroker;
        }

        public void SerializeAndWriteToFile(string path, object adoPipeline)
        {
            string serializedPipeline = 
                this.yamlBroker.SerializeToYaml(adoPipeline);

            this.filesBroker.WriteToFile(path, serializedPipeline);
        }
    }
}
