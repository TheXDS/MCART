/*
BinaryReaderExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

#nullable enable

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Contiene extensiones útiles para la clase
    ///     <see cref="BinaryReader"/>.
    /// </summary>
    public static class BinaryReaderExtensions
    {
        internal static MethodInfo GetBinaryReadMethod(Type t)
        {
            return typeof(BinaryReader).GetMethods().FirstOrDefault(p =>
               p.Name.StartsWith("Read")
               && p.GetParameters().Length == 0
               && p.ReturnType == t)
                ?? throw new PlatformNotSupportedException();
        }

        /// <summary>
        ///     Lee un valor de enumeración.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de valor de enumeración a leer.
        /// </typeparam>
        /// <param name="br">
        ///     <see cref="BinaryReader"/> desde el cual obtener.
        /// </param>
        /// <returns>
        ///     Valor de enumeración obtenido desde el
        ///     <see cref="BinaryReader"/>.
        /// </returns>
        public static T ReadEnum<T>(this BinaryReader br) where T : struct, Enum
        {
            return (T)ReadEnum(br, typeof(T));
        }

        /// <summary>
        ///     Lee un valor de enumeración.
        /// </summary>
        /// <param name="br">
        ///     <see cref="BinaryReader"/> desde el cual obtener.
        /// </param>
        /// <param name="enumType">
        ///     Tipo de valor de enumeración a leer.
        /// </param>
        /// <returns>
        ///     Valor de enumeración obtenido desde el
        ///     <see cref="BinaryReader"/>.
        /// </returns>
        [DebuggerStepThrough]
        public static Enum ReadEnum(this BinaryReader br, Type enumType)
        {
            var t = enumType.GetEnumUnderlyingType();
            return (Enum)Enum.ToObject(enumType, GetBinaryReadMethod(t).Invoke(br, new object[0])!);
        }

        /// <summary>
        ///     Lee un <see cref="Guid"/>.
        /// </summary>
        /// <param name="br">
        ///     <see cref="BinaryReader"/> desde el cual obtener.
        /// </param>
        /// <returns>
        ///     <see cref="Guid"/> obtenido desde el
        ///     <see cref="BinaryReader"/>.
        /// </returns>
        public static Guid ReadGuid(this BinaryReader br)
        {
            return new Guid(br.ReadBytes(16));
        }
    }
}