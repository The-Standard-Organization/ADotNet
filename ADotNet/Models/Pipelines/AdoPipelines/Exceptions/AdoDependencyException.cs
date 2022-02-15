// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System;

namespace ADotNet.Models.Pipelines.Exceptions
{
    public class AdoDependencyException : Exception
    {
        public AdoDependencyException(Exception innerException)
            : base("Ado dependency error occurred, contact support.", innerException)
        {

        }
    }
}
