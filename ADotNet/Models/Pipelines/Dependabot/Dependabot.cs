// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.Dependabot
{
    public partial class Dependabot
    {
        [YamlMember(Alias = "version")]
        public int Version { get; set; }

        [YamlMember(Alias = "updates")]
        public List<Update> Updates { get; set; }
    }
}
