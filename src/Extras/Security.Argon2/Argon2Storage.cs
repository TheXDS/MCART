/*
Argon2Storage.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
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

#pragma warning disable IDE0130

using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Math;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Security;

/// <summary>
/// Derives keys from passwords using the Argon2 algorithm.
/// </summary>
/// <param name="settings">
/// Configuration of the key derivation algorithm to be used.
/// </param>
public class Argon2Storage(Argon2Settings settings) : IPasswordStorage<Argon2Settings>
{
    static Argon2Storage()
    {
        PasswordStorage.RegisterAlgorithm<Argon2Storage>();
    }

    /// <summary>
    /// Gets a <see cref="Argon2Settings"/> that represents the recommended
    /// default configuration for deriving storage keys.
    /// </summary>
    /// <returns>
    /// A <see cref="Argon2Settings"/> that represents the recommended
    /// default configuration.
    /// </returns>
    public static Argon2Settings GetDefaultSettings()
    {
        return new()
        {
            Salt = RandomNumberGenerator.GetBytes(16),
            Iterations = 4,
            KbMemSize = 262144,
            Parallelism = (short)Environment.ProcessorCount.Clamp(0, short.MaxValue),
            Type = Argon2Type.Argon2id,
            KeyLength = 16,
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Argon2Storage"/> class,
    /// setting the key derivation algorithm configuration to the default values.
    /// </summary>
    public Argon2Storage() : this(GetDefaultSettings())
    {
    }

    /// <inheritdoc/>
    public Argon2Settings Settings { get; set; } = settings;

    int IPasswordStorage.KeyLength => Settings.KeyLength;

    byte[] IPasswordStorage.Generate(byte[] input)
    {
        using Argon2 a = GetArgon2(input);
        ConfigureArgon2(a);
        return a.GetBytes(Settings.KeyLength);
    }

    private void ConfigureArgon2(Argon2 a)
    {
        a.Salt = Settings.Salt;
        a.Iterations = Settings.Iterations;
        a.MemorySize = Settings.KbMemSize;
        a.DegreeOfParallelism = Settings.Parallelism;
    }

    private Argon2 GetArgon2(byte[] input)
    {
        try
        {
            return Settings.Type switch
            {
                Argon2Type.Argon2d => new Argon2d(input),
                Argon2Type.Argon2i => new Argon2i(input),
                Argon2Type.Argon2id => new Argon2id(input),
                _ => throw Errors.UndefinedEnum(nameof(Settings), Settings.Type)
            };
        }
        catch (Exception ex)
        {
            throw new InvalidArgon2SettingsException(ex.Message, ex);
        }
    }

    /// <inheritdoc/>
    public void ConfigureFrom(BinaryReader reader)
    {
        Settings = new Argon2Settings
        {
            Salt = reader.ReadBytes(reader.ReadInt32()),
            Iterations = reader.ReadInt32(),
            KbMemSize = reader.ReadInt32(),
            Parallelism = reader.ReadInt16(),
            Type = (Argon2Type)reader.ReadByte(),
            KeyLength = reader.ReadInt32(),
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
        writer.Write(Settings.KbMemSize);
        writer.Write(Settings.Parallelism);
        writer.Write((byte)Settings.Type);
        writer.Write(Settings.KeyLength);
        writer.Flush();
        return ms.ToArray();
    }
}
