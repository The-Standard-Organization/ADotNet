// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.AspNets
{
    public class VirtualMachinesPool
    {
        [YamlMember(Alias = "vmImage", ScalarStyle = ScalarStyle.SingleQuoted)]
        public string VirtualMachineImage { get; set; }
    }
}
