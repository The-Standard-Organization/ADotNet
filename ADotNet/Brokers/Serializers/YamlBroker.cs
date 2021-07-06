// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using ADotNet.Brokers.Serializers.TypeConverters;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ADotNet.Brokers.Serializers
{
    public class YamlBroker : IYamlBroker
    {
        private readonly ISerializer serializer;

        public YamlBroker()
        {
            this.serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
        }

        public string SerializeToYaml(object @object) =>
            this.serializer.Serialize(@object);
    }
}
