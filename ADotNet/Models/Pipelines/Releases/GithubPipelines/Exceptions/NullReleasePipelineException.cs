// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Xeptions;

namespace ADotNet.Models.Pipelines.Releases.GithubPipelines.Exceptions
{
    public class NullReleasePipelineException : Xeption
    {
        public NullReleasePipelineException()
            : base(message: "Release pipeline is null.")
        { }
    }
}
