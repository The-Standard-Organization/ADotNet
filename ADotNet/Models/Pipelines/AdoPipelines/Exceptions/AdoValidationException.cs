// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System;

namespace ADotNet.Models.Pipelines.AdoPipelines.Exceptions
{
    public class AdoValidationException : Exception
    {
        public AdoValidationException(string message,Exception innerException)
            : base(message, innerException)
        { }
    }
}