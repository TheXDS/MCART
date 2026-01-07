/*
PasswordStorageTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     Taylor Hornby (Original implementation)
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

using System.Security;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Security.Tests.Security;

internal class Pbkdf2StorageTests
{
    [TestCase("SHA1")]
    [TestCase("SHA256")]
    [TestCase("SHA512")]
    public void Use_PBKDF2_for_password_storage(string hashFunction)
    {
        IPasswordStorage pbkdf2 = new Pbkdf2Storage()
        {
            Settings = Pbkdf2Storage.GetDefaultSettings() with
            {
                HashFunction = hashFunction
            }
        };
        SecureString pw1 = "password".ToSecureString();
        SecureString pw2 = "Test@123".ToSecureString();
        byte[] hash = PasswordStorage.CreateHash(pbkdf2, pw1);
        Assert.That(PasswordStorage.VerifyPassword(pw1, hash), Is.True);
        Assert.That(PasswordStorage.VerifyPassword(pw2, hash), Is.False);
    }

    [Test]
    public void Generate_SHA1_Test()
    {
        IPasswordStorage pbkdf2 = new Pbkdf2Storage()
        {
            Settings = new Pbkdf2Settings()
            {
                Salt = Convert.FromBase64String("WFAM4zPKWXHYcalDt4koaw=="),
                Iterations = 1000,
                DerivedKeyLength = 128 / 8,
                HashFunction = "SHA1"
            }
        };
        byte[] expected = Convert.FromBase64String("R1WBjyfhVe02qhe7YyY8Wg==");
        Assert.That(expected, Is.EqualTo(pbkdf2.Generate("password".ToSecureString())));
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
        Assert.That(expected, Is.EqualTo(pbkdf2.Generate("password".ToSecureString())));
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
        Assert.That(settings, Is.Not.EqualTo(pbkdf2.DumpSettings()));
        using var ms = new MemoryStream(settings);
        using var br = new BinaryReader(ms);
        pbkdf2.ConfigureFrom(br);
        Assert.That(settings, Is.EqualTo(pbkdf2.DumpSettings()));
    }

    [Test]
    public void Pbkdf2_default_algorithm_is_SHA512()
    {
        IPasswordStorage pbkdf2 = new Pbkdf2Storage()
        {
            Settings = new Pbkdf2Settings()
            {
                Salt = Convert.FromBase64String("WFAM4zPKWXHYcalDt4koaw=="),
                Iterations = 1000,
                DerivedKeyLength = 16,
                HashFunction = null
            }
        };
        byte[] expected = Convert.FromBase64String("fqg8ZoPMzmLiOVqZtdlB2g==");
        Assert.That(expected, Is.EqualTo(pbkdf2.Generate("password".ToSecureString())));
    }

    [Test]
    public void Pbkdf2_gets_key_length_from_settings()
    {
        IPasswordStorage pbkdf2 = new Pbkdf2Storage()
        {
            Settings = new Pbkdf2Settings()
            {
                DerivedKeyLength = 16
            }
        };
        Assert.That(pbkdf2.KeyLength, Is.EqualTo(16));
    }
}
