// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.Dependabot
{
    public partial class Schedule
    {
        [YamlMember(Alias = "interval")]
        public string Interval { get; set; }
    }
}
