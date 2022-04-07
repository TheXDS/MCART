/*
PasswordStorageTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     Taylor Hornby (Original implementation)
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
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
