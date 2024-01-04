// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System;

namespace ADotNet.Models.Pipelines.Exceptions
{
    public class AdoValidationException : Exception
    {
        public AdoValidationException(Exception innerException)
            : base(message: "Ado validation exception occurred, try again",
                innerException: innerException)
        { }
        
        public AdoValidationException(string message,Exception innerException)
            : base(message, innerException)
        { }
    }
}