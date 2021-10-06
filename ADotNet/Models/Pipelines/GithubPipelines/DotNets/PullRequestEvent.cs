using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class PullRequestEvent
    {
        [YamlMember(ScalarStyle = ScalarStyle.Folded)]
        public string[] Branches { get; set; }
    }
}
