// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System;

namespace ADotNet.Models.Pipelines.Exceptions
{
    public class NullPipelineException : Exception
    {
        public NullPipelineException()
            : base(message: "Pipeline is null")
        { }
        
        public NullPipelineException(string message)
            : base(message)
        { }
    }
}