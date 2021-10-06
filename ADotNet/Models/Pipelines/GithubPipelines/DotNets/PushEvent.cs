using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class PushEvent
    {
        public string[] Branches { get; set; }
    }
}
