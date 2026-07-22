// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    public class RunUntilFailureTask : GithubTask
    {
        [YamlMember(Order = 1, Alias = "shell", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public override string Shell => "bash";

        [YamlMember(Order = 2, Alias = "run", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public override string Run => "for i in {1..1000};" +
            @" do echo ""Run #$i""; dotnet test --no-build " +
            "--verbosity normal --filter 'FullyQualifiedName!~Integrations' || exit 1; done";
    }
}
