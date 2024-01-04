// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Xeptions;

namespace ADotNet.Models.Foundations.Files.Exceptions
{
    public class FileDependencyValidationException : Xeption
    {
        public FileDependencyValidationException(Xeption innerException)
            : base(message: "File dependency validation error occurred, fix the errors and try again.",
                innerException: innerException)
        { }
        
        public FileDependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}