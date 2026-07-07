// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using ADotNet.Clients;
using ADotNet.Clients.Builders;
using Moq;
using Tynamix.ObjectFiller;

namespace ADotNet.Tests.Unit.Clients.Builders
{
    public partial class ServiceBuilderTests
    {
        private readonly Mock<IADotNetClient> aDotNetClientMock;
        private readonly ServiceBuilder serviceBuilder;

        public ServiceBuilderTests() =>
            this.serviceBuilder = new ServiceBuilder();

        private static string GetRandomString() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static int GetRandomNumber() =>
           new IntRange(min: 2, max: 10).GetValue();
    }
}
