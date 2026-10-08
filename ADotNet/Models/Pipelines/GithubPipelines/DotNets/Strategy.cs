// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    [Obsolete("No longer in use. Please migrate to StrategyV2.")]
    public class Strategy
    {
        [Obsolete("This property is now obsolete. Please migrate to MatrixV2.")]
        [YamlMember(Order = 0, Alias = "matrix", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public Dictionary<string, string> Matrix { get; set; }
    }
}
