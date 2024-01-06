// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Xeptions;

namespace ADotNet.Models.Foundations.Files.Exceptions
{
    public class FileServiceException : Xeption
    {
        public FileServiceException(Xeption innerException)
            : base(message: "File service error occurred, contact support.",
                innerException: innerException)
        { }
        
        public FileServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}