/*
Common_nonCLS.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene operaciones comunes de transformación de datos en los
programas y de algunas comparaciones especiales, utilizando tipos y/o métodos
que no forman parte del CLS.

Algunas de estas funciones también se implementan como extensiones, por lo que
para ser llamadas únicamente es necesario importar el espacio de nombres
"TheXDS.MCART" y utilizar sintáxis de instancia.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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

#if !CLSCompliance
using System;
using System.Linq;

namespace TheXDS.MCART
{
    public static partial class Common
    {
        /// <summary>
        ///     Invierte el Endianess de un valor <see cref="ushort" />.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Un <see cref="ushort" /> cuyo Endianess ha sido invertido.</returns>
        public static ushort FlipEndianess(this in ushort value)
        {
            return BitConverter.ToUInt16(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
        }

        /// <summary>
        ///     Invierte el Endianess de un valor <see cref="uint" />.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Un <see cref="uint" /> cuyo Endianess ha sido invertido.</returns>
        public static uint FlipEndianess(this in uint value)
        {
            return BitConverter.ToUInt32(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
        }

        /// <summary>
        ///     Invierte el Endianess de un valor <see cref="ulong" />.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Un <see cref="ulong" /> cuyo Endianess ha sido invertido.</returns>
        public static ulong FlipEndianess(this in ulong value)
        {
            return BitConverter.ToUInt64(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
        }
    }
}
#endif