/*
Argon2Storage.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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

#pragma warning disable IDE0130

using Konscious.Security.Cryptography;
using System.Security.Cryptography;

namespace TheXDS.MCART.Security;

/// <summary>
/// Derives a hash from a given input using the BLAKE2b algorithm.
/// </summary>
/// <param name="settings"></param>
public class Blake2Storage(Blake2Settings settings) : IPasswordStorage<Blake2Settings>
{
    /// <summary>
    /// Gets a <see cref="Blake2Settings"/> that represents the recommended
    /// default configuration for deriving storage keys.
    /// </summary>
    /// <returns>
    /// A <see cref="Blake2Settings"/> that represents the recommended
    /// default configuration.
    /// </returns>
    public static Blake2Settings GetDefaultSettings()
    {
        return new()
        {
            Key = RandomNumberGenerator.GetBytes(32),
            HashSize = 32,
        };
    }

    /// <inheritdoc/>
    public Blake2Settings Settings { get; set; } = settings;

    /// <inheritdoc/>
    public void ConfigureFrom(BinaryReader reader)
    {
        Settings = new Blake2Settings
        {
            Key = reader.ReadBytes(reader.ReadInt32()),
            HashSize = reader.ReadInt32(),
        };
    }

    int IPasswordStorage.KeyLength => Settings.HashSize;

    byte[] IPasswordStorage.DumpSettings()
    {
        using var ms = new MemoryStream();
        using var writer = new BinaryWriter(ms);
        writer.Write(Settings.Key.Length);
        writer.Write(Settings.Key);
        writer.Write(Settings.HashSize);
        writer.Flush();
        return ms.ToArray();
    }

    byte[] IPasswordStorage.Generate(byte[] input)
    {
        using HMACBlake2B blake = new(Settings.Key, Settings.HashSize);
        return blake.ComputeHash(input);
    }
}