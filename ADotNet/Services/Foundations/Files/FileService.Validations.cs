// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using ADotNet.Models.Foundations.Files.Exceptions;

namespace ADotNet.Services.Foundations.Files
{
    public partial class FileService
    {
        private static void ValidateInputs(string path, string content)
        {
            switch (path, content)
            {
                case { } when IsInvalid(path):
                    throw new InvalidFilePathException(
                        message: "Invalid file path, fix the errors and try again.");

                case { } when IsInvalid(content):
                    throw new InvalidFileContentException(
                        message: "Invalid file content, fix errors and try again.");
            }
        }
       
        private static bool IsInvalid(string @string) =>
            String.IsNullOrWhiteSpace(@string);
    }
}
