using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    public class CheckoutTask : GithubTask
    {
        [YamlMember(Order = 1)]
        public string Uses = "actions/checkout@v2";
    }
}
