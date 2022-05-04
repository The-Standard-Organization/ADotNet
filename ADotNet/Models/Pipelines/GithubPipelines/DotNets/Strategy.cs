using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class Strategy
    {
        [YamlMember(Alias = "fail-fast")]
        public bool FailFast { get; set; }

        [YamlMember(Alias = "matrix")]
        public Matrix Matrix { get; set; }
    }
}
