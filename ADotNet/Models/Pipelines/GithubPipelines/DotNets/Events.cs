using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class Events
    {
        public PushEvent Push { get; set; }

        [YamlMember(Alias = "pull_request")]
        public PullRequestEvent PullRequest { get; set; }
    }
}
