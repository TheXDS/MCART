/*
Pbkdf2Storage.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
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

using System.Security.Cryptography;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Security;

/// <summary>
/// Derives keys from passwords using the PBKDF2 algorithm.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Pbkdf2Storage"/> class,
/// specifying the key‑derivation algorithm configuration to use.
/// </remarks>
/// <param name="settings">
/// The key‑derivation algorithm configuration to use.
/// </param>
public class Pbkdf2Storage(Pbkdf2Settings settings) : IPasswordStorage<Pbkdf2Settings>
{
    private const string DEFAULT_HASH_ALG = "SHA512";

    /// <summary>
    /// Gets a <see cref="Pbkdf2Settings"/> representing the recommended
    /// default configuration for key derivation in storage.
    /// </summary>
    /// <returns>
    /// A <see cref="Pbkdf2Settings"/> that represents the recommended
    /// default configuration.
    /// </returns>
    public static Pbkdf2Settings GetDefaultSettings()
    {
        return new()
        {
            Salt = RandomNumberGenerator.GetBytes(128),
            Iterations = 1024,
            HashFunction = DEFAULT_HASH_ALG,
            DerivedKeyLength = 64,
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Pbkdf2Storage"/> class,
    /// setting the key‑derivation algorithm configuration to the default
    /// values.
    /// </summary>
    public Pbkdf2Storage() : this(GetDefaultSettings())
    {
    }

    /// <inheritdoc/>
    public Pbkdf2Settings Settings { get; set; } = settings;

    int IPasswordStorage.KeyLength => Settings.DerivedKeyLength;

    byte[] IPasswordStorage.Generate(byte[] input)
    {
        using Rfc2898DeriveBytes pbkdf2 = GetPbkdf2(input);
        return pbkdf2.GetBytes(Settings.DerivedKeyLength);
    }

    private Rfc2898DeriveBytes GetPbkdf2(byte[] input)
    {
        return new(input, Settings.Salt, Settings.Iterations, new HashAlgorithmName(Settings.HashFunction.IsEmpty() ? DEFAULT_HASH_ALG : Settings.HashFunction));
    }

    /// <inheritdoc/>
    public void ConfigureFrom(BinaryReader reader)
    {
        Settings = new Pbkdf2Settings
        {
            Salt = reader.ReadBytes(reader.ReadInt32()),
            Iterations = reader.ReadInt32(),
            HashFunction = reader.ReadString(),
            DerivedKeyLength = reader.ReadInt32(),
        };
    }

    /// <inheritdoc/>
    public byte[] DumpSettings()
    {
        using var ms = new MemoryStream();
        using var writer = new BinaryWriter(ms);
        writer.Write(Settings.Salt.Length);
        writer.Write(Settings.Salt);
        writer.Write(Settings.Iterations);
        writer.Write(Settings.HashFunction ?? string.Empty);
        writer.Write(Settings.DerivedKeyLength);
        writer.Flush();
        return ms.ToArray();
    }
}
