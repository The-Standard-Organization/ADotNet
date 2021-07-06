// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

namespace ADotNet.Clients
{
    public interface IADotNetClient
    {
        void SerializeAndWriteToFile(object adoPipeline, string path);
    }
}
