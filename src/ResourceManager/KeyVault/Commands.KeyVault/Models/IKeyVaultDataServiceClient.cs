﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Microsoft.KeyVault.WebKey;
using System.Collections.Generic;
using System.Security;

namespace Microsoft.Azure.Commands.KeyVault.Models
{
    public interface IKeyVaultDataServiceClient
    {
        KeyBundle CreateKey(string vaultName, string keyName, KeyAttributes keyAttributes);

        KeyBundle ImportKey(string vaultName, string keyName, KeyAttributes keyAttributes, JsonWebKey webKey, bool? importToHsm);     
       
        KeyBundle SetKey(string vaultName, string keyName, KeyAttributes keyAttributes);

        KeyBundle GetKey(string vaultName, string keyName, string keyVersion);

        IEnumerable<KeyIdentityItem> GetKeys(string vaultName);
        
        KeyBundle DeleteKey(string vaultName, string keyName);

        Secret SetSecret(string vaultName, string secretName, SecureString secretValue);

        Secret GetSecret(string vaultName, string secretName, string secretVersion);

        IEnumerable<SecretIdentityItem> GetSecrets(string vaultName);

        Secret DeleteSecret(string vaultName, string secretName);
    }
}
