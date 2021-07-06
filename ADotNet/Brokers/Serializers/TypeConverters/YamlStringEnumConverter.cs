// ---------------------------------------------------------------
// This piece of code belong to Mr. Michael Yanni (https://github.com/MiYanni)
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace ADotNet.Brokers.Serializers.TypeConverters
{
    public class YamlStringEnumConverter : IYamlTypeConverter
    {
        private object m;

        public bool Accepts(Type type) => type.IsEnum;

        public object ReadYaml(IParser parser, Type type)
        {
            Scalar parsedEnum = parser.Consume<Scalar>();

            Dictionary<string, MemberInfo?> serializableValues = type.GetMembers()
                .Select(member => new KeyValuePair<string, MemberInfo>(
                    member.GetCustomAttributes<EnumMemberAttribute>(true)
                    .Select(enumMemberAttribute => enumMemberAttribute.Value).FirstOrDefault(), member))
                        .Where(enumMemberAttribute => !String.IsNullOrEmpty(enumMemberAttribute.Key))
                            .ToDictionary(pa => pa.Key, pa => pa.Value);

            if (!serializableValues.ContainsKey(parsedEnum.Value))
            {
                throw new YamlException(
                    parsedEnum.Start,
                    parsedEnum.End,
                    $"Value '{parsedEnum.Value}' not found in enum '{type.Name}'");
            }

            return Enum.Parse(type, serializableValues[parsedEnum.Value].Name);
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            MemberInfo? enumMember = type.GetMember(value.ToString()).FirstOrDefault();

            string yamlValue = enumMember?.GetCustomAttributes<EnumMemberAttribute>(true)
                .Select(ema => ema.Value)
                    .FirstOrDefault() ?? value.ToString();


            emitter.Emit(new Scalar(yamlValue));
        }
    }
}
