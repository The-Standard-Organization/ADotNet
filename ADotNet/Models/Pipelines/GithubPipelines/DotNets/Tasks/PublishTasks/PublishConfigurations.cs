// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.PublishTasks
{
    public class PublishConfigurations
    {
        public PublishConfigurations(string projectName)
        {
            ProjectName = projectName;
        }

        public static string ProjectName { get; set; }

        [YamlMember(Alias = "args", Order = 0)]
        public string Arguments { get; set; } = $"push {ProjectName}/bin/Release/*.nupkg -Source https://api.nuget.org/v3/index.json -ApiKey ${{ secrets.NUGET_API_KEY }}";
    }
}