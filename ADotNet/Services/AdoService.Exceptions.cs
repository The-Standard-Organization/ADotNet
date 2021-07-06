// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.IO;
using ADotNet.Models.Pipelines.Exceptions;

namespace ADotNet.Services
{
    public partial class AdoService
    {
        private delegate void ReturningNothingFunction();

        private static void TryCatch(ReturningNothingFunction returningNothingFunction)
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
            catch (ArgumentNullException argumentNullException)
            {
                throw new AdoDependencyValidationException(argumentNullException);
            }
            catch (ArgumentException argumentException)
            {
                throw new AdoDependencyValidationException(argumentException);
            }
            catch (PathTooLongException pathTooLongException)
            {
                throw new AdoDependencyValidationException(pathTooLongException);
            }
            catch (DirectoryNotFoundException directoryNotFoundException)
            {
                throw new AdoDependencyValidationException(
                    directoryNotFoundException);
            }
            catch (Exception exception)
            {
                throw new AdoServiceException(exception);
            }
        }
    }
}
