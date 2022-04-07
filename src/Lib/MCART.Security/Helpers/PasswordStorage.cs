/*
PasswordStorage.cs

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

namespace TheXDS.MCART.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using TheXDS.MCART.Security;
using TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contiene métodos para crear Salty Hashes de contraseñas que son
/// seguros para ser almacenados, así como también comprobar la validez
/// de una contraseña.
/// </summary>
public static class PasswordStorage
{
    private static readonly IEnumerable<IPasswordStorage> _algs = Objects.FindAllObjects<IPasswordStorage>();

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
    /// El arrelgo inclute información sobre el algoritmo de derivación
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
            var algorithm = _algs.SingleOrDefault(p => p.AlgId == id);
            if (algorithm is null) return null;
            algorithm.ConfigureFrom(reader);
            return algorithm.Generate(password).SequenceEqual(reader.ReadBytes((int)ms.RemainingBytes()));
        }
        catch
        {
            return null;
        }
    }
}
