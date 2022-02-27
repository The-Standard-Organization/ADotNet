// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Runtime.Serialization;
using ADotNet.Models.Foundations.Files.Exceptions;

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
                throw new FileValidationException(invalidFilePathException);
            }
            catch (InvalidFileContentException invalidFileContentException)
            {
                throw new FileValidationException(invalidFileContentException);
            }
            catch (ArgumentNullException argumentNullException)
            {
                var invalidFileException =
                    new InvalidFileException(argumentNullException);

                throw new FileDependencyValidationException(invalidFileException);
            }
            catch (ArgumentException argumentException)
            {
                var invalidFileException =
                    new InvalidFileException(argumentException);

                throw new FileDependencyValidationException(invalidFileException);
            }
            catch (SerializationException serializationException)
            {
                var failedFileSerializationException =
                    new FailedFileSerializationException(serializationException);

                throw new FileDependencyException(failedFileSerializationException);
            }
        }
    }
}
