// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;

namespace ADotNet.Models.Pipelines.Exceptions
{
    public class AdoDependencyValidationException : Exception
    {
        public AdoDependencyValidationException(Exception innerException)
            : base("Ado dependency validation error occurred, try again.", innerException)
        {

        }
    }
}
