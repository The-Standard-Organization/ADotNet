// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using ADotNet.Models.Pipelines.Releases.GithubPipelines.Exceptions;

namespace ADotNet.Services.Releases
{
    public partial class ReleaseService
    {
        private static void ValidateInputs(string path)
        {
            if (path is null)
            {
                throw new NullReleasePathException();
            }
        }
    }
}
