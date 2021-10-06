using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class GithubPipeline
    {
        public string Name { get; set; }

        [YamlMember(Alias = "on")]
        public Events OnEvents { get; set; }
    }
}
