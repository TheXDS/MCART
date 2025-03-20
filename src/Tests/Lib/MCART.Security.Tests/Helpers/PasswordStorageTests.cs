/*
PasswordStorageTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     Taylor Hornby (Original implementation)
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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

using System.Diagnostics.CodeAnalysis;
using System.Security;
using System.Text;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Helpers.PasswordStorage;

namespace TheXDS.MCART.Security.Tests.Helpers;

internal class PasswordStorageTests
{
    [ExcludeFromCodeCoverage]
    private class DummyPasswordStorage : IPasswordStorage
    {
        static DummyPasswordStorage()
        {
            RegisterAlgorithm<DummyPasswordStorage>();
        }

        private string settings = "TESTtest";
        public int KeyLength => 16;
        public void ConfigureFrom(BinaryReader reader) => settings = Encoding.UTF8.GetString(reader.ReadBytes(8));
        public byte[] DumpSettings() => Encoding.UTF8.GetBytes(settings);
        public byte[] Generate(byte[] input) => input.Concat(new byte[16]).ToArray()[0..16];
    }

    [ExcludeFromCodeCoverage]
    private class Dummy2PasswordStorage : IPasswordStorage<int>
    {
        public byte[] Generate(byte[] input) => input.Concat(new byte[KeyLength]).ToArray()[0..KeyLength];
        public void ConfigureFrom(BinaryReader reader) => Settings = reader.ReadInt32();
        public byte[] DumpSettings() => BitConverter.GetBytes(Settings);

        public int KeyLength { get; set; }
        public int Settings { get; set; }
    }

    [Test]
    public void DumpSettings_default_implementation_Test()
    {
        IPasswordStorage p = new Dummy2PasswordStorage
        {
            Settings = 1234,
            KeyLength = 2
        };
        var s = p.DumpSettings();
        Assert.That(BitConverter.GetBytes(1234), Is.EqualTo(s));
    }

    [Test]
    public void ConfigureFrom_default_implementation_Test()
    {
        IPasswordStorage<int> p = new Dummy2PasswordStorage
        {
            Settings = 1234,
            KeyLength = 2
        };
        var s = BitConverter.GetBytes(5678);
        p.ConfigureFrom(s);
        Assert.That(5678, Is.EqualTo(p.Settings));
    }

    [Test]
    public void CreateHash_Test()
    {
        SecureString pw = "password".ToSecureString();
        byte[] expected = [ 5, ..("DUMMYTESTtestpassword"u8.ToArray()), 0, 0, 0, 0, 0, 0, 0, 0 ];

        Assert.That(expected, Is.EqualTo(CreateHash<DummyPasswordStorage>(pw)));
    }

    [Test]
    public void VerifyPassword_Test()
    {
        SecureString pw1 = "password".ToSecureString();
        SecureString pw2 = "Test@123".ToSecureString();
        byte[] hash = CreateHash<DummyPasswordStorage>(pw1);

        Assert.That(VerifyPassword(pw1, hash), Is.True);
        Assert.That(VerifyPassword(pw2, hash), Is.False);

        hash[0] = (byte)'X';
        Assert.That(VerifyPassword(pw1, hash), Is.Null);
    }

    [Test]
    public void VerifyPassword_returns_null_with_no_algorithm()
    {
        byte[] testData = [7, .. ("UNKNOWN"u8.ToArray())];
        Assert.That(VerifyPassword("Test@123".ToSecureString(), testData), Is.Null);
    }
}
