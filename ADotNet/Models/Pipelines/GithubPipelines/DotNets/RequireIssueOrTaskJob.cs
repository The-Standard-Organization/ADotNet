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
    public sealed class RequireIssueOrTaskJob : Job
    {
        public RequireIssueOrTaskJob()
        {
            RunsOn = "ubuntu-latest";

            Permissions = new Dictionary<string, string>
            {
                { "contents", "read" },
                { "pull-requests", "read" }
            };

            Steps = new List<GithubTask>
                {
                    new CheckoutTaskV3
                    {
                        Name = "Check out"
                    },

                    new GithubTask()
                    {
                        Name = "Get PR Information",
                        Id = "get_pr_info",
                        Uses = "actions/github-script@v6",
                        With = new Dictionary<string, string>
                        {
                            { 
                                "script", 
                                """
                                    const pr = await github.rest.pulls.get({
                                      owner: context.repo.owner,
                                      repo: context.repo.repo,
                                      pull_number: context.payload.pull_request.number
                                    });

                                    const prOwner = pr.data.user.login || "";
                                    const prBody = pr.data.body || "";
                                    core.setOutput("prOwner", prOwner);
                                    core.setOutput("description", prBody);
                                    console.log(`PR Owner: ${prOwner}`);
                                    console.log(`PR Body: ${prBody}`);
                                """
                            }
                        }
                    },

                    new GithubTask()
                    {
                        Name = "Check For Associated Issues Or Tasks",
                        If = "${{ steps.get-pr.outputs.prOwner != 'dependabot[bot]' }}",
                        Id = "check_for_issues_or_tasks",
                        Shell = "bash",
                        Run =
                            """
                                PR_BODY="${{ steps.get-pr.outputs.description }}"
                                echo "::notice::Raw PR Body: $PR_BODY"

                                if [[ -z "$PR_BODY" ]]; then
                                  echo "Error: PR description does not contain any links to issue(s)/task(s) (e.g., 'closes #123' / 'closes AB#123' / 'fixes #123' / 'fixes AB#123')."
                                  exit 1
                                fi

                                PR_BODY=$(echo "$PR_BODY" | tr -s '\r\n' ' ' | tr '\n' ' ' | xargs)
                                echo "::notice::Normalized PR Body: $PR_BODY"

                                if echo "$PR_BODY" | grep -Piq "((close|closes|closed|fix|fixes|fixed|resolve|resolves|resolved)\s*(\[#\d+\]|\#\d+)|(?:close|closes|closed|fix|fixes|fixed|resolve|resolves|resolved)\s*(\[AB#\d+\]|AB#\d+))"; then
                                  echo "Valid PR description."
                                else
                                  echo "Error: PR description does not contain any links to issue(s)/task(s) (e.g., 'closes #123' / 'closes AB#123' / 'fixes #123' / 'fixes AB#123')."
                                  exit 1
                                fi
                            """,
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
