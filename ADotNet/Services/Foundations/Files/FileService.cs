// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using ADotNet.Brokers.IOs;

namespace ADotNet.Services.Foundations.Files
{
    public partial class FileService(IFilesBroker filesBroker) : IFileService
    {
        private readonly IFilesBroker filesBroker = filesBroker;

        public void WriteToFile(string path, string content) =>
        TryCatch(() =>
        {
            ValidateInputs(path, content);

            this.filesBroker.WriteToFile(path, content);
        });
    }
}
