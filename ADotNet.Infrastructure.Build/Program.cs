// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using ISL.ReIdentification.Infrastructure.Services;

namespace ADotNet.Infrastructure.Build
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var scriptGenerationService = new ScriptGenerationService();

            scriptGenerationService.GenerateBuildScript(
                branchName: "main",
                projectName: "ADotNet",
                dotNetVersion: "9.0.100");

            scriptGenerationService.GeneratePrLintScript(branchName: "main");
        }
    }
}