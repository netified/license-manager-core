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
    public class Signer
    {
        /// <summary>
        /// Signs the specified document to sign.
        /// </summary>
        /// <param name="documentToSign">The document to sign.</param>
        /// <param name="privateKey">The private key.</param>
        /// <param name="passPhrase">The pass phrase.</param>
        /// <returns></returns>
        public byte[] Sign(byte[] documentToSign, string privateKey, string passPhrase)
        {
            var ecdsa = ECDsa.Create();
            ecdsa.ImportEncryptedPkcs8PrivateKey(passPhrase, Convert.FromBase64String(privateKey), out int _);
            return ecdsa.SignData(documentToSign, HashAlgorithmName.SHA512, DSASignatureFormat.Rfc3279DerSequence);
        }

        /// <summary>
        /// Verifies the signature.
        /// </summary>
        /// <param name="documentToSign">The document to sign.</param>
        /// <param name="signature">The signature.</param>
        /// <param name="publicKey">The public key.</param>
        /// <returns></returns>
        public bool VerifySignature(byte[] documentToSign, byte[] signature, string publicKey)
        {
            var ecdsa = ECDsa.Create();
            ecdsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(publicKey), out int read);
            return ecdsa.VerifyData(documentToSign, signature, HashAlgorithmName.SHA512, DSASignatureFormat.Rfc3279DerSequence);
        }
    }
}