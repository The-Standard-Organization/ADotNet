// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

namespace ADotNet.Services.Builds
{
    public interface IBuildService
    {
        void SerializeAndWriteToFile(string path, object adoPipeline);
    }
}
