/*
EnumExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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

using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Contiene extensiones útiles para la clase <see cref="Enum" />.
    /// </summary>
    public static class EnumExtensions
    {
        private static byte[] BypassByte(byte b) => new[] {b};

        /// <summary>
        ///     Obtiene un <see cref="MethodInfo" /> para un método que permita
        ///     realizar la conversión de <typeparamref name="T" /> a un arreglo
        ///     de bytes.
        /// </summary>
        /// <typeparam name="T">Tipo de la enumeración a convertir.</typeparam>
        /// <returns>
        ///     Un <see cref="MethodInfo" /> para un método que convierte desde
        ///     el tipo base de la enumeración a un arreglo de bytes.
        /// </returns>
        [DebuggerStepThrough]
        public static MethodInfo ByteConversionMethod<T>() where T : struct, Enum
        {
            return ByteConversionMethodInternal(typeof(T));
        }

        /// <summary>
        ///     Obtiene un <see cref="MethodInfo" /> para un método que permita
        ///     realizar la conversión del tipo de la enumeración a un arreglo
        ///     de bytes.
        /// </summary>
        /// <param name="enumType">Tipo de la enumeración a convertir.</param>
        /// <returns>
        ///     Un <see cref="MethodInfo" /> para un método que convierte desde
        ///     el tipo base de la enumeración a un arreglo de bytes.
        /// </returns>
        [DebuggerStepThrough]
        public static MethodInfo ByteConversionMethod(Type enumType)
        {
            if (!enumType.IsEnum) throw new ArgumentException(St.EnumTypeExpected, nameof(enumType));
            return ByteConversionMethodInternal(enumType);
        }

        [DebuggerStepThrough]
        private static MethodInfo ByteConversionMethodInternal(Type enumType)
        {
            var tRsp = enumType.GetEnumUnderlyingType();
            return tRsp != typeof(byte)
                ? typeof(BitConverter).GetMethods().FirstOrDefault(p =>
                {
                    var pars = p.GetParameters();
                    return p.Name == nameof(BitConverter.GetBytes)
                           && pars.Length == 1
                           && pars[0].ParameterType == tRsp;
                }) ?? throw new PlatformNotSupportedException()
                : new Func<byte, byte[]>(BypassByte).Method;
        }

        /// <summary>
        ///     Convierte un valor de enumeración a su representación en bytes.
        /// </summary>
        /// <param name="value">Valor de enumeración a convertir.</param>
        /// <returns>
        ///     Un arreglo de bytes con la representación del valor de
        ///     enumeración.
        /// </returns>
        /// <exception cref="PlatformNotSupportedException">
        ///     Se produce en caso que la plataforma no sea soportada, y que el
        ///     <see cref="Enum" /> utilice un tipo base inusual para el cual no
        ///     sea posible obtener un convertidor a bytes.
        /// </exception>
        [DebuggerStepThrough]
        public static byte[] ToBytes(this Enum value)
        {
            return (byte[]) ByteConversionMethodInternal(value.GetType())
                .Invoke(null, new object[] {value});
        }

        /// <summary>
        ///     Crea un delegado que convierte un valor de enumeración del tipo
        ///     especificado en un arreglo de bytes.
        /// </summary>
        /// <typeparam name="T">Tipo de la enumeración a convertir.</typeparam>
        /// <returns>
        ///     Un delegado que convierte un valor de enumeración del tipo
        ///     especificado en un arreglo de bytes.
        /// </returns>
        [DebuggerStepThrough]
        public static Func<T, byte[]> ToBytes<T>() where T : struct, Enum
        {
            return (Func<T, byte[]>) Delegate.CreateDelegate(typeof(Func<T, byte[]>),
                ByteConversionMethodInternal(typeof(T)), true);
        }
    }
}