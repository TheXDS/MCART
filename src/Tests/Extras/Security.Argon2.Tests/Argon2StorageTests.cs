// Argon2StorageTests.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2025 César Andrés Morgan
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the “Software”), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Security;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Security.Argon2.Tests;

public class Argon2StorageTests
{
    [TestCase(Argon2Type.Argon2i)]
    [TestCase(Argon2Type.Argon2d)]
    [TestCase(Argon2Type.Argon2id)]
    public void Use_Argon2_for_password_storage(Argon2Type type)
    {
        IPasswordStorage argon2 = new Argon2Storage()
        {
            Settings = Argon2Storage.GetDefaultSettings() with
            {
                Type = type
            }
        };
        SecureString pw1 = "password".ToSecureString();
        SecureString pw2 = "Test@123".ToSecureString();
        byte[] hash = PasswordStorage.CreateHash(argon2, pw1);
        Assert.That(PasswordStorage.VerifyPassword(pw1, hash), Is.True);
        Assert.That(PasswordStorage.VerifyPassword(pw2, hash), Is.False);
    }

    [Test]
    public void Argon2_fails_if_settings_are_incorrect()
    {
        Argon2Settings brokenSettings = Argon2Storage.GetDefaultSettings() with { Type = (Argon2Type)128 };
        Assert.That(()=> _ = ((IPasswordStorage)new Argon2Storage(brokenSettings)).Generate([]), Throws.TypeOf<InvalidArgon2SettingsException>());
    }

    [Test]
    public void Argon2_exposes_key_length_property_on_IPasswordStorage_interface()
    {
        var settings = Argon2Storage.GetDefaultSettings();
        Assert.That(((IPasswordStorage)new Argon2Storage(settings)).KeyLength, Is.EqualTo(settings.KeyLength));
    }
}
