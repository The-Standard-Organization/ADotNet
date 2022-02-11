using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class Matrix
    {
        private object _os;

        [YamlMember(Alias = "os", ScalarStyle = ScalarStyle.SingleQuoted)]
        public object Os
        {
            get => $"[{string.Join(", ", (IEnumerable<string>)_os)}]";
            set => _os = (IEnumerable<string>)value;
        }
    }
}
