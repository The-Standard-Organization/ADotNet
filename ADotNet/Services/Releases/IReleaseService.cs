// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

namespace ADotNet.Services.Releases
{
    public interface IReleaseService
    {
        void SerializeWriteToFile(string path, object releasePipeline);
    }
}
