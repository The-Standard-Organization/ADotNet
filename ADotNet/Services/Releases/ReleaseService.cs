// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using ADotNet.Brokers.IOs;
using ADotNet.Brokers.Serializers;

namespace ADotNet.Services.Releases
{
    public partial class ReleaseService : IReleaseService
    {
        private readonly IYamlBroker yamlBroker;
        private readonly IFilesBroker filesBroker;

        public ReleaseService(
            IYamlBroker yamlBroker,
            IFilesBroker filesBroker)
        {
            this.yamlBroker = yamlBroker;
            this.filesBroker = filesBroker;
        }

        public void SerializeWriteToFile(string path, object releasePipeline) =>
        TryCatch(() =>
        {
            ValidateInputs(path, releasePipeline);

            string serializedPipeline =
                this.yamlBroker.SerializeToYaml(releasePipeline);

            this.filesBroker.WriteToFile(
                path,
                data: serializedPipeline);
        });
    }
}
