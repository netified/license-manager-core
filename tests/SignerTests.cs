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

using LicenseManager.Core.Security.Cryptography;
using NUnit.Framework;
using System;
using System.Text;

namespace LicenseManager.Core.Tests
{
    public class SignerTests
    {
        private const string passphrase = "passphrase";
        private const string privateKey = "MIIBMTAbBgoqhkiG9w0BDAEDMA0ECCKZdIbxFNZ+AgEKBIIBEEhnXR+1mC3+SntH2Uiiyb1B7ivbK1nx1jOCEYvquvoUmVm22MI20sw3YTMPzTalrY8ogR3WBJzJRIn7lRKtQFqJjdlhh3G+5Qf8YBLjiDZbgeLsnL5K4DG+s0cDs8/Wx36a8/OnbhvJdIM3ualRo/az9lkbBt5BPD+Mx8MWHmXpyVVBPoeosZiuYRNmGdB7SY5NaslOpMM7SfV0X961uQp2xCcclhDuExv5XAuOcccONCSf1wfH1MhI1IumQfCMdWOnCdJUhDCxL/IVVjkb7bBLpQiMXLm2rCZoWtGrniQmpL4iQexa4J9SqCupZ+TP4zGpHM5clGuuefY4cKo+E0hH8Ftj55hplRNiNAtBDhG/";
        private const string publicKey = "MIGbMBAGByqGSM49AgEGBSuBBAAjA4GGAAQBMjD7TcXgSMbjDDpkOtNe68prK21mPv3c4q8+CSUZKSz9mO8YB0oXmXKCeKORp2v4bDhx6xqNsXCMX07GmgSm7n0A6q71AkjSGDz7iNrW2TSByFql38c6wdtCKneBu4R29u9z7VE/dfGuwDjmo0Fwpo4zaZSrubwCjqkMoU0fr/j7DtQ=";

        [Theory]
        [TestCase("MIGHAkIB1OypetzT5Dml4axBDyp2wi6kroAEAtR6PbewRMBNJy4CTDyxTdThk++WmvUiB4mT8VbhvRl4+XZZUdlU1rhmrhwCQUb3qclZf9aHr63WGU8ojs9zxZHvwC8+vKjakUBuzLmZ/6N+R38NNu6Uqxdt83DTS5/nAyKScgp08BgRCPHxWJWO")]
        public void VerifySignature_Works(string signature)
        {
            var data = Encoding.UTF8.GetBytes("Hello, World!");

            var signer = new Signer();
            Assert.True(signer.VerifySignature(data, Convert.FromBase64String(signature), publicKey));
        }
    }
}