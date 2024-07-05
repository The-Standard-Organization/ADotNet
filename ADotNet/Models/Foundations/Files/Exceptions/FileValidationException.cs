// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Xeptions;

namespace ADotNet.Models.Foundations.Files.Exceptions
{
    public class FileValidationException(string message, Xeption innerException) 
        : Xeption(message, innerException)
    {
    }
}