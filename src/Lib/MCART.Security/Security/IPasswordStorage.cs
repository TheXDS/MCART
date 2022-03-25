/*
IPasswordStorage.cs

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
using TheXDS.MCART.Types.Extensions;

/// <summary>
/// Define una serie de miembros a implementar por un tipo que provea de 
/// métodos para generar Hashes a partir de contraseñas que puedan ser
/// almacenados de forma segura.
/// </summary>
public interface IPasswordStorage
{
    /// <summary>
    /// Obtiene el nombre del algoritmo.
    /// </summary>
    string AlgId => GetType().Name.ChopEndAny("PasswordStorage", "Storage").ToUpperInvariant();

    /// <summary>
    /// Obtiene la configuración a partir del bloque especificado.
    /// </summary>
    /// <param name="reader">
    /// Objeto a partir del cual leer los valores de configuración.
    /// </param>
    void ConfigureFrom(BinaryReader reader);

    /// <summary>
    /// Vuelca los valores de configuración en formato binario.
    /// </summary>
    /// <returns>
    /// Un arreglo de bytes a partir del cual se puede volver a construir el
    /// objeto que contiene los valores de configuración de derivación de
    /// claves para esta instancia.
    /// </returns>
    byte[] DumpSettings();

    /// <summary>
    /// Genera un blob binario que puede ser almacenado en una base de datos.
    /// </summary>
    /// <param name="input">
    /// Contraseña a partir de la cual derivar una clave.
    /// </param>
    /// <returns>
    /// Un arreglo de bytes con la clave derivada a partir de la contraseña 
    /// especificada.</returns>
    byte[] Generate(SecureString input);

    /// <summary>
    /// Obtiene un valor que indica la cantidad de bytes que esta instancia generará.
    /// </summary>
    int KeyLength { get; }
}
