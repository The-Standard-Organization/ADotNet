// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;

namespace ADotNet.Models.Pipelines.Exceptions
{
    public class BuildServiceException : Exception
    {
        public BuildServiceException(Exception innerException)
            : base("Build service exception occurred, contact support.", innerException)
        {

        }
    }
}
