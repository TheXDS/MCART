/*
Pbkdf2Settings.cs

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

/// <summary>
/// Contiene valores de configuración a utilizar para derivar contraseñas
/// utilizando el algoritmo PBKDF2.
/// </summary>
public record struct Pbkdf2Settings
{
    /// <summary>
    /// Obtiene o inicializa el bloque de sal a utilizar para derivar la clave.
    /// </summary>
    public byte[] Salt { get; init; }

    /// <summary>
    /// Obtiene o inicializa un valor que determina la cantidad de iteraciones
    /// a ejecutar de PBKDF2 para derivar una clave.
    /// </summary>
    public int Iterations { get; init; }

    /// <summary>
    /// Obtiene o inicializa el nombre del algoritmo hash a utilizar para
    /// derivar la clave.
    /// </summary>
    public string? HashFunction { get; init; }

    /// <summary>
    /// Obtiene o inicializa un valor que determina la cantidad de bytes a
    /// derivar.
    /// </summary>
    public int DerivedKeyLength { get; init; }
}
