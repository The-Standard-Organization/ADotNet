// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using ADotNet.Models.Services.Foundations.Files.Exceptions;

namespace ADotNet.Services.Foundations.Files
{
    public partial class FileService
    {
        private static void ValidateFilePath(string path)
        {
            if (IsInvalid(path))
            {
                throw new InvalidFilePathException();
            }
        }

        private static bool IsInvalid(string @string) =>
            String.IsNullOrWhiteSpace(@string);
    }
}
