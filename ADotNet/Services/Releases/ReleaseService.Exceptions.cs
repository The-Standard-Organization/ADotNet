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
        private delegate void ReturningNothingFunction();

        private void TryCatch(ReturningNothingFunction returningNothingFunction)
        {
            try
            {
                returningNothingFunction();
            }
            catch (NullReleasePathException nullReleasePathException)
            {
                throw new ReleaseValidationException(nullReleasePathException);
            }
            catch (NullReleasePipelineException nullReleasePipelineException)
            {
                throw new ReleaseValidationException(nullReleasePipelineException);
            }
        }
    }
}
