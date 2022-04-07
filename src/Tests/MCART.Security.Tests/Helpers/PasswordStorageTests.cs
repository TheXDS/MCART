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
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using TheXDS.MCART.Security;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Helpers.PasswordStorage;
using static TheXDS.MCART.Types.Extensions.SecureStringExtensions;

internal class PasswordStorageTests
{
    private class DummyPasswordStorage : IPasswordStorage
    {
        public int KeyLength => 16;
        public void ConfigureFrom(BinaryReader reader) => _ = reader.ReadBytes(8);
        public byte[] DumpSettings() => Encoding.UTF8.GetBytes("TESTtest");
        public byte[] Generate(byte[] input) => input.Concat(new byte[16]).ToArray()[0..16];
    }

    [Test]
    public void CreateHash_Test()
    {
        SecureString pw = "password".ToSecureString();
        byte[] expected =
        {
            5,
            (byte)'D', (byte)'U', (byte)'M', (byte)'M', (byte)'Y', (byte)'T', (byte)'E',
            (byte)'S', (byte)'T', (byte)'t', (byte)'e', (byte)'s', (byte)'t', (byte)'p',
            (byte)'a', (byte)'s', (byte)'s', (byte)'w', (byte)'o', (byte)'r', (byte)'d',
            0, 0, 0, 0, 0, 0, 0, 0
        };

        Assert.AreEqual(expected, CreateHash<DummyPasswordStorage>(pw));
    }

    [Test]
    public void VerifyPassword_Test()
    {
        SecureString pw1 = "password".ToSecureString();
        SecureString pw2 = "Test@123".ToSecureString();
        byte[] hash = CreateHash<DummyPasswordStorage>(pw1);
        Assert.IsTrue(VerifyPassword(pw1, hash));
        Assert.IsFalse(VerifyPassword(pw2, hash));

        hash[0] = (byte)'X';
        Assert.IsNull(VerifyPassword(pw1, hash));
    }
}
