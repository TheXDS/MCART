/*
Pbkdf2Storage.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
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

namespace TheXDS.MCART.Security;
using System.Security;
using System.Security.Cryptography;
using TheXDS.MCART.Types.Extensions;

/// <summary>
/// Deriva claves a partir de contraseñas utilizando el algoritmo PBKDF2.
/// </summary>
public class Pbkdf2Storage : IPasswordStorage
{
    /// <inheritdoc/>
    public Pbkdf2Settings Settings { get; set; } = GetDefaultSettings();

    int IPasswordStorage.KeyLength => Settings.DerivedKeyLength;

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
            HashFunction = "SHA512",
            DerivedKeyLength = 64,
        };
    }

    void IPasswordStorage.ConfigureFrom(BinaryReader reader)
    {
        Settings = new()
        {
            Salt = reader.ReadBytes(reader.ReadInt16()),
            Iterations = reader.ReadInt32(),
            HashFunction = reader.ReadString(),
            DerivedKeyLength = reader.ReadInt32(),
        };
    }

    byte[] IPasswordStorage.DumpSettings()
    {
        using var ms = new MemoryStream();
        using var writer = new BinaryWriter(ms);
        writer.Write((short)Settings.Salt.Length);
        writer.Write(Settings.Salt);
        writer.Write(Settings.Iterations);
        writer.Write(Settings.HashFunction ?? "SHA1");
        writer.Write(Settings.DerivedKeyLength);
        return ms.ToArray();
    }

    byte[] IPasswordStorage.Generate(byte[] input)
    {
        using Rfc2898DeriveBytes? pbkdf2 = Settings.HashFunction.IsEmpty()
            ? new(input, Settings.Salt, Settings.Iterations)
            : new(input, Settings.Salt, Settings.Iterations, new HashAlgorithmName(Settings.HashFunction));
        return pbkdf2.GetBytes(Settings.DerivedKeyLength);
    }
}