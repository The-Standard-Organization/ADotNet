// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    public class Credentials
    {
        [YamlMember(Order = 0, Alias = "username", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public string Username { get; set; }

        [YamlMember(Order = 1, Alias = "password", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public string Password { get; set; }
    }
}
