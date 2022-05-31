/*
PasswordStorageTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     Taylor Hornby (Original implementation)
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace TheXDS.MCART.Security.Tests.Helpers;
using NUnit.Framework;
using System;
using System.IO;
using TheXDS.MCART.Security;
using TheXDS.MCART.Types.Extensions;

internal class Pbkdf2StorageTests
{
    [Test]
    public void Generate_SHA1_Test()
    {
        IPasswordStorage pbkdf2 = new Pbkdf2Storage()
        {
            Settings = new Pbkdf2Settings() {
                Salt = Convert.FromBase64String("WFAM4zPKWXHYcalDt4koaw=="),
                Iterations = 1000,
                DerivedKeyLength = 128 / 8
            }
        };
        byte[] expected = Convert.FromBase64String("R1WBjyfhVe02qhe7YyY8Wg==");
        Assert.AreEqual(expected, pbkdf2.Generate("password".ToSecureString()));
    }

    [Test]
    public void Generate_SHA512_Test()
    {
        IPasswordStorage pbkdf2 = new Pbkdf2Storage()
        {
            Settings = new Pbkdf2Settings()
            {
                Salt = Convert.FromBase64String("WFAM4zPKWXHYcalDt4koaw=="),
                Iterations = 1000,
                DerivedKeyLength = 128 / 8,
                HashFunction = "SHA512"
            }
        };
        byte[] expected = Convert.FromBase64String("fqg8ZoPMzmLiOVqZtdlB2g==");
        Assert.AreEqual(expected, pbkdf2.Generate("password".ToSecureString()));
    }

    [Test]
    public void Settings_parsing_test()
    {
        IPasswordStorage pbkdf2 = new Pbkdf2Storage()
        {
            Settings = new Pbkdf2Settings()
            {
                Salt = Convert.FromBase64String("WFAM4zPKWXHYcalDt4koaw=="),
                Iterations = 768,
                DerivedKeyLength = 24,
                HashFunction = "SHA384"
            }
        };
        byte[] settings = pbkdf2.DumpSettings();        
        pbkdf2 = new Pbkdf2Storage();
        Assert.AreNotEqual(settings, pbkdf2.DumpSettings());
        using var ms = new MemoryStream(settings);
        using var br = new BinaryReader(ms);
        pbkdf2.ConfigureFrom(br);
        Assert.AreEqual(settings, pbkdf2.DumpSettings());
    }
}
