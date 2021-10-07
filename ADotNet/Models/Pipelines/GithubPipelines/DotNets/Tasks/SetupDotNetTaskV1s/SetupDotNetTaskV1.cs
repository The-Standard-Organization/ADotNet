using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s
{
    public class SetupDotNetTaskV1 : GithubTask
    {
        [YamlMember(Order = 1)]
        public string Uses = "actions/setup-dotnet@v1";

        [YamlMember(Alias ="with", Order = 2)]
        public TargetDotNetVersion TargetDotNetVersion { get; set; }
    }
}
