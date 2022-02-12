// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Xeptions;

namespace ADotNet.Models.Services.Foundations.Files.Exceptions
{
    public class InvalidFilePathException : Xeption
    {
        public InvalidFilePathException()
            : base(message: "Invalid file path, fix the errors and try again.")
        { }
    }
}
