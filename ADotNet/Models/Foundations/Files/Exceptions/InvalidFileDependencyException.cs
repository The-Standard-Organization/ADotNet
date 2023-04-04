// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System;
using Xeptions;

namespace ADotNet.Models.Foundations.Files.Exceptions
{
    public class InvalidFileException : Xeption
    {
        public InvalidFileException(Exception innerException)
            : base(message: "Invalid file error occurred.",
                  innerException)
        { }
    }
}
