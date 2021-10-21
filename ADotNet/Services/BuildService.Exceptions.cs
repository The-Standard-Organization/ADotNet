﻿// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.IO;
using System.Security;
using ADotNet.Models.Pipelines.Exceptions;

namespace ADotNet.Services
{
    public partial class BuildService
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
            catch (IOException ioException)
            {
                throw new AdoDependencyException(ioException);
            }
            catch (SecurityException securityException)
            {
                throw new AdoDependencyException(securityException);
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                throw new AdoDependencyException(unauthorizedAccessException);
            }
            catch (NotSupportedException notSupportedException)
            {
                throw new AdoDependencyException(notSupportedException);
            }
            catch (Exception exception)
            {
                throw new BuildServiceException(exception);
            }
        }
    }
}
