// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using ADotNet.Models.Pipelines.Releases.GithubPipelines;
using ADotNet.Models.Pipelines.Releases.GithubPipelines.Exceptions;

namespace ADotNet.Services.Releases
{
    public partial class ReleaseService
    {
        private static void ValidateInputs(string path, object pipeline)
        {
            switch(path, pipeline)
            {
                case (null, _):
                    throw new NullReleasePathException();

                case (_, null):
                    throw new NullReleasePipelineException();
            }
        }
    }
}
