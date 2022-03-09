/*
ByteUnitType.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene operaciones comunes de transformación de datos en los
programas, y de algunas comparaciones especiales.

Algunas de estas funciones también se implementan como extensiones, por lo que
para ser llamadas únicamente es necesario importar el espacio de nombres
"TheXDS.MCART" y utilizar sintaxis de instancia.

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

namespace TheXDS.MCART.Helpers;
public static partial class Common
{
    /// <summary>
    /// Enumera los tipos de unidades que se pueden utilizar para
    /// representar grandes cantidades de bytes.
    /// </summary>
    public enum ByteUnitType : byte
    {
        /// <summary>
        /// Numeración binaria. Cada orden de magnitud equivale a 1024 de su inferior.
        /// </summary>
        Binary,
        /// <summary>
        /// Numeración decimal. Cada orden de magnitud equivale a 1000 de su inferior. 
        /// </summary>
        Decimal,
        /// <summary>
        /// Numeración binaria con nombre largo. Cada orden de magnitud equivale a 1024 de su inferior.
        /// </summary>
        BinaryLong,
        /// <summary>
        /// Numeración decimal con nombre largo. Cada orden de magnitud equivale a 1000 de su inferior. 
        /// </summary>
        DecimalLong
    }
}
