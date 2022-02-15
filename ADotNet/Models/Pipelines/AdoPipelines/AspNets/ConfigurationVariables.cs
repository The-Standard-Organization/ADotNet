﻿// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.AdoPipelines.AspNets
{
    public class ConfigurationVariables
    {
        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public BuildConfiguration BuildConfiguration { get; set; }
    }
}
