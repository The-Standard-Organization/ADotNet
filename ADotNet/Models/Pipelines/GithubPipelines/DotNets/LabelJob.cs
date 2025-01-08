// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    [Obsolete("This job is now obsolete. Please migrate to LabelJobV2.")]
    public sealed class LabelJob : Job
    {
        public LabelJob(
            string runsOn,
            string githubToken)
        {
            RunsOn = runsOn;

            Steps = new List<GithubTask>
                {
                    new GithubTask()
                    {
                        Name = "Apply Label",
                        Uses = "actions/github-script@v6",
                        With = new Dictionary<string, string>
                        {
                            { "github-token", githubToken },
                            { "script", @"
const prefixes = [
  'INFRA:',
  'PROVISIONS:',
  'RELEASES:',
  'DATA:',
  'BROKERS:',
  'FOUNDATIONS:',
  'PROCESSINGS:',
  'ORCHESTRATIONS:',
  'COORDINATIONS:',
  'MANAGEMENTS:',
  'AGGREGATIONS:',
  'CONTROLLERS:',
  'CLIENTS:',
  'EXPOSERS:',
  'PROVIDERS:',
  'BASE:',
  'COMPONENTS:',
  'VIEWS:',
  'PAGES:',
  'ACCEPTANCE:',
  'INTEGRATIONS:',
  'CODE RUB:',
  'MINOR FIX:',
  'MEDIUM FIX:',
  'MAJOR FIX:',
  'DOCUMENTATION:',
  'CONFIG:',
  'STANDARD:',
  'DESIGN:',
  'BUSINESS:'
];

const pullRequest = context.payload.pull_request;

if (!pullRequest) {
  console.log('No pull request context available.');
  return;
}

const title = context.payload.pull_request.title;
const existingLabels = context.payload.pull_request.labels.map(label => label.name);

for (const prefix of prefixes) {
  if (title.startsWith(prefix)) {
    const label = prefix.slice(0, -1);
    if (!existingLabels.includes(label)) {
      console.log(`Applying label: ${label}`);
      await github.rest.issues.addLabels({
        owner: context.repo.owner,
        repo: context.repo.repo,
        issue_number: context.payload.pull_request.number,
        labels: [label]
      });
    }
    break;
  }
}
" }
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
    }
}
