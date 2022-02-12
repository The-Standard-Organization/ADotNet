// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using ADotNet.Brokers.IOs;

namespace ADotNet.Services.Foundations.Files
{
    public partial class FileService : IFileService
    {
        private readonly IFilesBroker filesBroker;

        public FileService(IFilesBroker filesBroker) =>
            this.filesBroker = filesBroker;

        public void WriteToFile(string path, string content) =>
        TryCatch(() =>
        {
            ValidateFilePath(path);

            this.filesBroker.WriteToFile(path, content);
        });
    }
}
