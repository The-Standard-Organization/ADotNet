// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace ADotNet.Models.Pipelines.Releases.GithubPipelines.Exceptions
{
    public class NullReleasePathException : Xeption
    {
        public NullReleasePathException()
            : base(message: "Release path is null.")
        { }
    }
}
