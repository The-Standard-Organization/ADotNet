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
                var invalidFileException =
                    new InvalidFileException(
                        message: "Invalid file error occurred.",
                        innerException: argumentNullException);

                throw CreateFileDependencyValidationException(invalidFileException);
            }
            catch (ArgumentException argumentException)
            {
                var invalidFileException =
                    new InvalidFileException(
                        message: "Invalid file error occurred.",
                        innerException: argumentException);

                throw CreateFileDependencyValidationException(invalidFileException);
            }
            catch (SerializationException serializationException)
            {
                var failedFileSerializationException =
                    new FailedFileSerializationException(
                        message: "Failed file serialization error occurred, contact support.",
                        innerException: serializationException);

                throw CreateFileDependencyException(failedFileSerializationException);
            }
            catch (Exception exception)
            {
                var failedFileServiceException =
                    new FailedFileServiceException(
                        message: "Failed file service error occurred, contact support.",
                        innerException: exception);

                throw CreateFileServiceException(failedFileServiceException);
            }
        }

        private static FileValidationException CreateFileValidationException(
            Xeption innerException)
        {
            return new FileValidationException(
                message: "File validation error occurred, fix the errors and try again.",
                innerException: innerException);
        }
        
        private static FileDependencyValidationException CreateFileDependencyValidationException(
            Exception innerException)
        {
            return new FileDependencyValidationException(
                message: "File dependency validation error occurred, fix the errors and try again.",
                innerException: innerException);
        }
        
        private static FileDependencyException CreateFileDependencyException(
            Xeption innerException)
        {
            return new FileDependencyException(
                message: "File dependency error occurred, contact support.",
                innerException);
        }
        
        private static FileServiceException CreateFileServiceException(
            Xeption innerException)
        {
            return new FileServiceException(
                message: "File service error occurred, contact support.",
                innerException: innerException);
        }
    }
}