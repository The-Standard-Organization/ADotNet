// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

namespace ADotNet.Brokers.Serializers
{
    public interface IYamlBroker
    {
        string SerializeToYaml(object @object);
    }
}
