// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using ADotNet.Brokers.IOs;
using ADotNet.Brokers.Serializers;

namespace ADotNet.Services
{
    public partial class BuildService : IBuildService
    {
        private readonly IYamlBroker yamlBroker;
        private readonly IFilesBroker filesBroker;

        public BuildService(
            IYamlBroker yamlBroker,
            IFilesBroker filesBroker)
        {
            this.yamlBroker = yamlBroker;
            this.filesBroker = filesBroker;
        }

        public void SerializeAndWriteToFile(string path, object adoPipeline) =>
        TryCatch(() =>
        {
            ValidateInputs(path, adoPipeline);

            string serializedPipeline =
                this.yamlBroker.SerializeToYaml(adoPipeline);

            // todo: with out this line, the serialization will create single quote for Brackets and that breaks the yaml compilation.
            // the question is there any better idea or way on how to generate Brackets with out single quote?
            // working on a solution :)
            serializedPipeline = serializedPipeline.Replace("'[", "[").Replace("]'", "]");

            this.filesBroker.WriteToFile(path, serializedPipeline);
        });
    }
}
