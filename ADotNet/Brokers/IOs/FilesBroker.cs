﻿// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System.IO;

namespace ADotNet.Brokers.IOs
{
    public class FilesBroker : IFilesBroker
    {
        public void WriteToFile(string path, string data) =>
            File.WriteAllText(path, data);
    }
}
