// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public sealed class CheckPrTitleJob : Job
    {
        public CheckPrTitleJob(
            string runsOn)
        {
            RunsOn = runsOn;

            Steps = new List<GithubTask>
                {
                    new CheckoutTaskV3
                    {
                        Name = "Checkout code",
                    },

                    new GithubTask()
                    {
                        Name = "Check PR Title",
                        Run = @"PR_TITLE=""${{ github.event.pull_request.title }}""

# Define the list of prefixes
PREFIXES=(
    ""ACCEPTANCE""
    ""AGGREGATION""
    ""BASE""
    ""BROKER""
    ""BUSINESS""
    ""CODE RUB""
    ""COMPONENT""
    ""CONFIG""
    ""CONTROLLER""
    ""COORDINATION""
    ""CLIENT""
    ""DATA""
    ""DESIGN""
    ""DOCUMENTATION""
    ""EXPOSER""
    ""FOUNDATION""
    ""INFRA""
    ""INTEGRATION""
    ""MAJORFIX""
    ""MANAGEMENT""
    ""MEDIUMFIX""
    ""MINORFIX""
    ""ORCHESTRATION""
    ""PAGE""
    ""PROCESSING""
    ""PROVISION""
    ""RELEASE""
    ""STANDARD""
    ""VIEW""
)

# Check if the PR title starts with any of the prefixes
PREFIX_MATCH=false
for PREFIX in ""${PREFIXES[@]}""
do
    if [[ ""${PR_TITLE}"" == ""${PREFIX}""* ]]; then
        PREFIX_MATCH=true
        break
    fi
done

# Fail the workflow if no prefix match is found
if [ ""${PREFIX_MATCH}"" == false ]; then
    echo ""error: The PR title must start with one of THE STANDARD specified prefixes.  Current PR Title: '${{ github.event.pull_request.title }}'""
    echo ""See https://github.com/hassanhabib/The-Standard-Team/blob/main/4%20Practices/4%20Practices.md#4113-category-list for the complete category list""
    exit 1
fi"
                    },
                };
        }

        [YamlMember(Order = 0, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new string Name { get; set; }

        [YamlMember(Order = 1, Alias = "runs-on")]
        public new string RunsOn { get; set; }

        [YamlMember(Order = 2, Alias = "needs", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new string[] Needs { get; set; }

        [YamlMember(Order = 3, Alias = "if", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new string If { get; set; }

        [YamlMember(Order = 4, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new string Environment { get; set; }

        [YamlMember(Order = 5, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new DefaultValues Defaults { get; set; }

        [YamlMember(Order = 6)]
        public new List<GithubTask> Steps { get; set; }

        [DefaultValue(0)]
        [YamlMember(Order = 7, Alias = "timeout-minutes", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new int TimeoutInMinutes { get; set; }

        [YamlMember(Order = 8, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new Strategy Strategy { get; set; }

        [YamlMember(Order = 9, Alias = "env", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new Dictionary<string, string> EnvironmentVariables { get; set; }

        [YamlMember(Order = 10, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new Dictionary<string, string> Outputs { get; set; }
    }
}
