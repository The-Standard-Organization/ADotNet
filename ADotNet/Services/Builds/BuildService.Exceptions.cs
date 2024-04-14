// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System;
using System.IO;
using System.Security;
using ADotNet.Models.Pipelines.AdoPipelines.Exceptions;
using Xeptions;

namespace ADotNet.Services.Builds
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
                throw CreateAdoValidationException(nullPipelineException);
            }
            catch (NullPathException nullPathException)
            {
                throw CreateAdoValidationException(nullPathException);
            }
            catch (ArgumentNullException argumentNullException)
            {
                throw CreateAdoDependencyValidationException(argumentNullException);
            }
            catch (ArgumentException argumentException)
            {
                throw CreateAdoDependencyValidationException(argumentException);
            }
            catch (PathTooLongException pathTooLongException)
            {
                throw CreateAdoDependencyValidationException(pathTooLongException);
            }
            catch (DirectoryNotFoundException directoryNotFoundException)
            {
                throw CreateAdoDependencyValidationException(directoryNotFoundException);
            }
            catch (IOException ioException)
            {
                throw CreateAdoDependencyException(ioException);
            }
            catch (SecurityException securityException)
            {
                throw CreateAdoDependencyException(securityException);
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                throw CreateAdoDependencyException(unauthorizedAccessException);
            }
            catch (NotSupportedException notSupportedException)
            {
                throw CreateAdoDependencyException(notSupportedException);
            }
            catch (Exception exception)
            {
                throw CreateBuildServiceException(exception);
            }
        }
        
        private static AdoValidationException CreateAdoValidationException(Xeption innerException)
        {
            return new AdoValidationException(
                message: "Ado validation exception occurred, try again",
                innerException: innerException);
        }
        
        private static AdoDependencyValidationException CreateAdoDependencyValidationException(
            Exception innerException)
        {
            return new AdoDependencyValidationException(
                message: "Ado dependency validation error occurs, try again.",
                innerException: innerException);
        }
        
        private static AdoDependencyException CreateAdoDependencyException(
            Exception innerException)
        {
            return new AdoDependencyException(
                message: "Ado dependency error occured, contact support.",
                innerException: innerException);
        }
        
        private static Exception CreateBuildServiceException(Exception innerException)
        {
            return new BuildServiceException(
                message: "Build service exception occured, contact support.",
                innerException: innerException);
        }
    }
}