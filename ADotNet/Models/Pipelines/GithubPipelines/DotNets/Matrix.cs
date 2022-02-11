using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class Matrix
    {
        [YamlMember(Alias = "os")]
        public IEnumerable<string> Os { get; set; }
    }
}
