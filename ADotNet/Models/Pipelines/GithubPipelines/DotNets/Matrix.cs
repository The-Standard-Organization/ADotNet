using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class Matrix
    {
        private object _os;

        [YamlMember(Alias = "os")]
        public object Os
        {
            get => $"BRACKET[{string.Join(", ", (IEnumerable<string>)_os)}]BRACKET";
            set => _os = (IEnumerable<string>)value;
        }
    }
}
