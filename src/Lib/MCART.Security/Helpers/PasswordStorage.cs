﻿/*
PasswordStorage.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     Taylor Hornby (Original implementation)
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

using System.Security;
using TheXDS.MCART.Security;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Contiene métodos para crear Salty Hashes de contraseñas que son
/// seguros para ser almacenados, así como también comprobar la validez
/// de una contraseña.
/// </summary>
public static partial class PasswordStorage
{
    private static IEnumerable<IPasswordStorage>? _algorithms;

    /// <summary>
    /// Crea un Hash seguro para almacenar la contraseña.
    /// </summary>
    /// <typeparam name="T">
    /// Algoritmo de derivación de claves a utilizar.
    /// </typeparam>
    /// <param name="password">Contraseña a almacenar.</param>
    /// <returns>
    /// Un arreglo de bytes que puede ser almacenado de forma segura en una
    /// base de datos que luego puede utilizarse para verificar una contraseña.
    /// </returns>
    public static byte[] CreateHash<T>(SecureString password) where T : IPasswordStorage, new()
    {
        return CreateHash(new T(), password);
    }

    /// <summary>
    /// Crea un Hash seguro para almacenar la contraseña.
    /// </summary>
    /// <param name="algorithm">
    /// Algoritmo de derivación de claves a utilizar.
    /// </param>
    /// <param name="password">Contraseña a almacenar.</param>
    /// <returns>
    /// Un arreglo de bytes que puede ser almacenado de forma segura en una
    /// base de datos que luego puede utilizarse para verificar una contraseña.
    /// El arreglo incluye información sobre el algoritmo de derivación
    /// utilizado, además de todos los valores de configuración del mismo.
    /// </returns>
    public static byte[] CreateHash(IPasswordStorage algorithm, SecureString password)
    {
        using var ms = new MemoryStream();
        using var writer = new BinaryWriter(ms);
        writer.Write(algorithm.AlgId);
        writer.Write(algorithm.DumpSettings());
        writer.Write(algorithm.Generate(password));
        writer.Flush();
        return ms.ToArray();
    }

    /// <summary>
    /// Verifica una contraseña.
    /// </summary>
    /// <param name="password">
    /// Contraseña a verificar.
    /// </param>
    /// <param name="hash">
    /// Hash contra el cual comparar. Debe incluir información sobre el
    /// algoritmo de derivación utilizado, además de cualquier valor de 
    /// configuración requerido por el mismo.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si la contraseña es válida,
    /// <see langword="false"/> en caso contrario, o
    /// <see langword="null"/> si hay un problema al verificar la
    /// contraseña, como ser, debido a tampering.
    /// </returns>
    public static bool? VerifyPassword(SecureString password, byte[] hash)
    {
        try
        {
            using var ms = new MemoryStream(hash);
            using var reader = new BinaryReader(ms);
            var id = reader.ReadString();
            var algorithm = (_algorithms ??= ReflectionHelpers.FindAllObjects<IPasswordStorage>().ToArray()).SingleOrDefault(p => p.AlgId == id);
            if (algorithm is null) return null;
            CheckSettings(algorithm.Settings?.GetType());
            algorithm.ConfigureFrom(reader);
            return algorithm.Generate(password).SequenceEqual(reader.ReadBytes((int)ms.RemainingBytes()));
        }
        catch
        {
            return null;
        }
    }
}
