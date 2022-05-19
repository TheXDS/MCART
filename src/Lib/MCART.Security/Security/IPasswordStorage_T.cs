/*
IPasswordStorage_T.cs

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

using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Security;

/// <summary>
/// Define una serie de miembros a implementar por un tipo que implementa
/// <see cref="IPasswordStorage"/> que incluya información de configuración del
/// algoritmo de derivación de clave.
/// </summary>
/// <typeparam name="T">
/// Tipo que contiene los valores de configuración. Se recomienda el uso de
/// tipos <see langword="record"/> con propiedades no mutables.
/// </typeparam>
public interface IPasswordStorage<T> : IPasswordStorage where T : struct
{
    /// <summary>
    /// Obtiene una referencia a la configuración activa de esta instancia.
    /// </summary>
    T Settings { get; set; }
    
    void IPasswordStorage.ConfigureFrom(BinaryReader reader)
    {
        Settings = (T)reader.Read(typeof(T));
    }

    /// <summary>
    /// Obtiene la configuración a partir del bloque especificado.
    /// </summary>
    /// <param name="data">
    /// Bloque de memoria que contiene los valores de configuración.
    /// </param>
    void ConfigureFrom(ReadOnlySpan<byte> data)
    {
        using var ms = new MemoryStream(data.ToArray());
        using var reader = new BinaryReader(ms);
        ConfigureFrom(reader);
    }

    byte[] IPasswordStorage.DumpSettings()
    {
        using var ms = new MemoryStream();
        using var writer = new BinaryWriter(ms);
        writer.DynamicWrite(Settings);
        writer.Flush();
        return ms.ToArray();
    }
}
