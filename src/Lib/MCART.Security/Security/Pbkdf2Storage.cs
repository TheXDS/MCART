﻿/*
Pbkdf2Storage.cs

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

using System.Security.Cryptography;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Security;

/// <summary>
/// Deriva claves a partir de contraseñas utilizando el algoritmo PBKDF2.
/// </summary>
/// <remarks>
/// Inicializa una nueva instancia de la clase <see cref="Pbkdf2Storage"/>,
/// especificando la configuración del algoritmo de derivación de claves a
/// utilizar.
/// </remarks>
/// <param name="settings">
/// Configuración del algoritmo de derivación de claves a utilizar.
/// </param>
public class Pbkdf2Storage(Pbkdf2Settings settings) : IPasswordStorage<Pbkdf2Settings>
{
    private const string DEFAULT_HASH_ALG = "SHA512";

    /// <summary>
    /// Obtiene un <see cref="Pbkdf2Settings"/> que representa la configuración
    /// predeterminada recomendada para derivar claves de almacenamiento.
    /// </summary>
    /// <returns>
    /// Un <see cref="Pbkdf2Settings"/> que representa la configuración
    /// predeterminada recomendada.
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
    /// Inicializa una nueva instancia de la clase <see cref="Pbkdf2Storage"/>,
    /// estableciendo la configuración del algoritmo de derivación de claves a
    /// los valores predeterminados.
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
