// Copyright (c) 2022 Netified <contact@netified.io>
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.

using System;
using System.Security.Cryptography;

namespace LicenseManager.Core.Security.Cryptography
{
    /// <summary>
    /// Represents a private/public encryption key pair.
    /// </summary>
    public class KeyPair
    {
        private readonly AsymmetricAlgorithm _algorithm;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyPair"/> class
        /// with the provided asymmetric key pair.
        /// </summary>
        /// <param name="algorithm"></param>
        internal KeyPair(AsymmetricAlgorithm algorithm)
        {
            _algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
        }

        /// <summary>
        /// Gets the encrypted and DER encoded private key.
        /// </summary>
        /// <param name="passPhrase">The pass phrase to encrypt the private key.</param>
        /// <returns>The encrypted private key.</returns>
        public string ToEncryptedPrivateKeyString(string passPhrase)
        {
            var data = _algorithm.ExportEncryptedPkcs8PrivateKey(
                password: passPhrase,
                pbeParameters: new PbeParameters(PbeEncryptionAlgorithm.TripleDes3KeyPkcs12, HashAlgorithmName.SHA1, 10));
            return Convert.ToBase64String(data);
        }

        /// <summary>
        /// Gets the DER encoded public key.
        /// </summary>
        /// <returns>The public key.</returns>
        public string ToPublicKeyString()
        {
            var data = _algorithm.ExportSubjectPublicKeyInfo();
            return Convert.ToBase64String(data);
        }
    }
}