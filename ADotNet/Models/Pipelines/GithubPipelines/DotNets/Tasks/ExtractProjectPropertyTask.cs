// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    /// <summary>
    /// A task to build .NET project.
    /// </summary>
    public sealed class ExtractProjectPropertyTask : GithubTask
    {
        public ExtractProjectPropertyTask(
            string projectRelativePath,
            string propertyName,
            string environmentVariableName)
        {
            Run = environmentVariableName + "=$(awk -v RS='' -F'</?" + propertyName + ">' 'NF>1{print $2}' "
                + $"{projectRelativePath} | sed -e 's/^[[:space:]]*//')\n"
                + "echo '" + environmentVariableName + "<<EOF' >> $GITHUB_ENV\n"
                + "echo -e \"$" + environmentVariableName + "\" >> $GITHUB_ENV\n"
                + "echo 'EOF' >> $GITHUB_ENV\n"
                + "echo \"" + propertyName + " - ${{ env." + environmentVariableName + " }}\"";
        }

        /// <summary>
        /// Gets the command to execute for the task.
        /// </summary>
        [YamlMember(Order = 7, Alias = "run")]
        public new string Run { get; private set; }
    }
}
