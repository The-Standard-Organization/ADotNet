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
            Run = "$" + environmentVariableName + "=((Select-Xml -Path '" + projectRelativePath + "' -XPath '//" + propertyName + "').Node.InnerXML)\n"
                + "'" + environmentVariableName + "<<EOF' >> $env:GITHUB_ENV\n"
                + "'$" + environmentVariableName + "' >> $env:GITHUB_ENV\n"
                + "'EOF' >> $env:GITHUB_ENV\n";
        }

        /// <summary>
        /// Gets the command to execute for the task.
        /// </summary>
        [YamlMember(Order = 7, Alias = "run")]
        public new string Run { get; private set; }

        /// <summary>
        /// Gets the shell on which the task is executed.
        /// </summary>
        [YamlMember(Order = 9, Alias = "shell")]
        public new string Shell { get; private set; } = ShellEnvironments.PowerShellCore;
    }
}
