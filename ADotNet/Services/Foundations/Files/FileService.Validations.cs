// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

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
                    throw new InvalidFilePathException();

                case { } when IsInvalid(content):
                    throw new InvalidFileContentException();
            }
        }

        private static bool IsInvalid(string @string) =>
            String.IsNullOrWhiteSpace(@string);
    }
}
