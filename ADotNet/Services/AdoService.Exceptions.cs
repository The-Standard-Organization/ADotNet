// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using ADotNet.Models.Pipelines.Exceptions;

namespace ADotNet.Services
{
    public partial class AdoService
    {
        private delegate void ReturningNothingFunction();

        private void TryCatch(ReturningNothingFunction returningNothingFunction)
        {
            try
            {
                returningNothingFunction();
            }
            catch (NullPipelineException nullPipelineException)
            {
                throw new AdoValidationException(nullPipelineException);
            }
            catch (NullPathException nullPathException)
            {
                throw new AdoValidationException(nullPathException);
            }
            catch (Exception exception)
            {
                throw new AdoServiceException(exception);
            }
        }
    }
}
