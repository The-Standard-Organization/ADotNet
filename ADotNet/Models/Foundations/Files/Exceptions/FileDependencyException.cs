// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using Xeptions;

namespace ADotNet.Models.Foundations.Files.Exceptions
{
    public class FileDependencyException : Xeption
    {
        public FileDependencyException(Xeption innerException)
            : base(message: "File dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
