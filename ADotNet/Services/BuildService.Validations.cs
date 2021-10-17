// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using ADotNet.Models.Pipelines.Exceptions;

namespace ADotNet.Services
{
    public partial class BuildService
    {
        public static void ValidateInputs(string path, object pipeline)
        {
            switch (path, pipeline)
            {
                case (_, null):
                    throw new NullPipelineException();

                case (null, _):
                    throw new NullPathException();
            }
        }
    }
}
