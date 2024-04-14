// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Runtime.Serialization;
using ADotNet.Models.Foundations.Files.Exceptions;
using Xeptions;

namespace ADotNet.Services.Foundations.Files
{
    public partial class FileService
    {
        private delegate void ReturningNothingFunction();

        private static void TryCatch(ReturningNothingFunction returningNothingFunction)
        {
            try
            {
                returningNothingFunction();
            }
            catch (InvalidFilePathException invalidFilePathException)
            {
                throw CreateFileValidationException(invalidFilePathException);
            }
            catch (InvalidFileContentException invalidFileContentException)
            {
                throw CreateFileValidationException(invalidFileContentException);
            }
            catch (ArgumentNullException argumentNullException)
            {
                throw CreateFileDependencyValidationException(argumentNullException);
            }
            catch (ArgumentException argumentException)
            {
                throw CreateFileDependencyValidationException(argumentException);
            }
            catch (SerializationException serializationException)
            {
                throw CreateFileDependencyException(serializationException);
            }
            catch (Exception exception)
            {
                throw CreateFileServiceException(exception);
            }
        }

        private static FileValidationException CreateFileValidationException(
            Xeption innerException)
        {
            return new FileValidationException(
                "File validation error occurred, fix the errors and try again.",
                innerException);
        }

        private static FileDependencyValidationException CreateFileDependencyValidationException(
            Exception innerException)
        {
            var invalidFileException =
                new InvalidFileException(
                    "Invalid file error occurred.", innerException);

            throw new FileDependencyValidationException(
                "File dependency validation error occurred, fix the errors and try again.",
                invalidFileException);
        }
        
        private static FileDependencyException CreateFileDependencyException(
            Exception innerException)
        {
            var failedFileSerializationException =
                new FailedFileSerializationException(
                    "Failed file serialization error occurred, contact support.",
                    innerException);

            throw new FileDependencyException(
                "File dependency error occurred, contact support.",
                failedFileSerializationException);
        }
        
        private static FileServiceException CreateFileServiceException(
            Exception innerException)
        {
            var failedFileServiceException =
                new FailedFileServiceException(
                    "Failed file service error occurred, contact support.",
                    innerException);

            throw new FileServiceException(
                "File service error occurred, contact support.",
                failedFileServiceException);
        }
    }
}
