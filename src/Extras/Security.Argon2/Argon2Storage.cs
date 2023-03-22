/*
Argon2Storage.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using TheXDS.MCART.Math;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Security;

/// <summary>
/// Deriva claves a partir de contraseñas utilizando el algoritmo Argon2.
/// </summary>
public class Argon2Storage : IPasswordStorage<Argon2Settings>
{
    /// <summary>
    /// Obtiene un <see cref="Argon2Settings"/> que representa la configuración
    /// predeterminada recomendada para derivar claves de almacenamiento.
    /// </summary>
    /// <returns>
    /// Un <see cref="Argon2Settings"/> que representa la configuración
    /// predeterminada recomendada.
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
    /// Inicializa una nueva instancia de la clase <see cref="Argon2Storage"/>,
    /// estableciendo la configuración del algoritmo de derivación de claves a
    /// los valores predeterminados.
    /// </summary>
    public Argon2Storage() : this(GetDefaultSettings())
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="Argon2Storage"/>,
    /// especificando la configuración del algoritmo de derivación de claves a
    /// utilizar.
    /// </summary>
    /// <param name="settings">
    /// Configuración del algoritmo de derivación de claves a utilizar.
    /// </param>
    public Argon2Storage(Argon2Settings settings)
    {
        Settings = settings;
    }

    /// <inheritdoc/>
    public Argon2Settings Settings { get; set; }

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
        return Settings.Type switch
        {
            Argon2Type.Argon2d => new Argon2d(input),
            Argon2Type.Argon2i => new Argon2i(input),
            Argon2Type.Argon2id => new Argon2id(input),
            _ => throw Errors.UndefinedEnum(nameof(Settings), Settings.Type)
        };
    }
}