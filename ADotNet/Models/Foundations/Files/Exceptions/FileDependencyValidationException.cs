// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System;
using Xeptions;

namespace ADotNet.Models.Foundations.Files.Exceptions
{
    public class FileDependencyValidationException : Xeption
    {
        public FileDependencyValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}