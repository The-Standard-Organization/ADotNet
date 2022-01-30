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
    public class ReleaseValidationException : Xeption
    {
        public ReleaseValidationException(Xeption innerException)
            : base(message: "Release validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
