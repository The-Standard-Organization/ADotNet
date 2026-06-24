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
    public sealed class SetAuthorAsPrAssigneeJobV2 : Job
    {
        public SetAuthorAsPrAssigneeJobV2(string runsOn)
        {
            RunsOn = runsOn;
            If = "${{ github.event.pull_request.head.repo.full_name == github.repository }}";

            Permissions = new Dictionary<string, string>
            {
                { "contents", "read" },
                { "issues", "write" },
                { "pull-requests", "write" }
            };

            Steps = new List<GithubTask>
                {
                    new GithubTask()
                    {
                        Name = "Set Author As PR Assignee",
                        Uses = "actions/github-script@v8",
                        With = new Dictionary<string, string>
                        {
                            { "github-token", "${{ secrets.GITHUB_TOKEN }}" },
                            { "script",
                                "const pr = context.payload.pull_request;\n" +
                                "if (!pr) {\n" +
                                "  console.log('No pull request context available.');\n" +
                                "  return;\n" +
                                "}\n\n" +
                                "const author = pr.user.login;\n" +
                                "if (author.endsWith('[bot]')) {\n" +
                                "  console.log(`Skipping bot author: ${author}`);\n" +
                                "  return;\n" +
                                "}\n\n" +
                                "console.log(`Assigning PR to author: ${author}`);\n\n" +
                                "await github.rest.issues.addAssignees({\n" +
                                "  owner: context.repo.owner,\n" +
                                "  repo: context.repo.repo,\n" +
                                "  issue_number: pr.number,\n" +
                                "  assignees: [author]\n" +
                                "});\n"
                            }
                        }
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

        [DefaultValue(false)]
        [YamlMember(Order = 11, Alias = "continue-on-error", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new bool ContinueOnError { get; set; }

        [YamlMember(Order = 12, Alias = "permissions", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new Dictionary<string, string> Permissions { get; set; }
    }
}
