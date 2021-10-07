// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization.TypeInspectors;

namespace ADotNet.Brokers.Serializers
{
    public class YamlBroker : IYamlBroker
    {
        private readonly ISerializer serializer;

        public YamlBroker()
        {
            this.serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .WithTypeInspector(inner => inner, s => s.InsteadOf<YamlAttributesTypeInspector>())
                        .WithTypeInspector(inner => new YamlAttributesTypeInspector(inner), s => s.Before<NamingConventionTypeInspector>())
                            .Build();
        }

        public string SerializeToYaml(object @object) =>
            this.serializer.Serialize(@object);
    }
}
