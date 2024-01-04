// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System;

namespace ADotNet.Models.Pipelines.Exceptions
{
    public class AdoDependencyValidationException : Exception
    {
        public AdoDependencyValidationException(Exception innerException)
            : base(message: "Ado dependency validation error occured, try again.",
                innerException: innerException)
        { }
        
        public AdoDependencyValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}