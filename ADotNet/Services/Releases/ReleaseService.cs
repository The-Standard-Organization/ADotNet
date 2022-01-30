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
    public class ReleaseService : IReleaseService
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
            throw new NotImplementedException();
    }
}
