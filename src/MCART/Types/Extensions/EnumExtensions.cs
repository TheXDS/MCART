/*
EnumExtensions.cs

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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Attributes;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Contiene extensiones útiles para la clase <see cref="Enum" />.
    /// </summary>
    public static class EnumExtensions
    {
        private static byte[] BypassByte(byte b) => new[] {b};

        /// <summary>
        /// Obtiene un <see cref="MethodInfo" /> para un método que permita
        /// realizar la conversi�n de <typeparamref name="T" /> a un arreglo
        /// de bytes.
        /// </summary>
        /// <typeparam name="T">Tipo de la enumeración a convertir.</typeparam>
        /// <returns>
        /// Un <see cref="MethodInfo" /> para un método que convierte desde
        /// el tipo base de la enumeración a un arreglo de bytes.
        /// </returns>
        [DebuggerStepThrough]
        public static MethodInfo ByteConversionMethod<T>() where T : struct, Enum
        {
            return ByteConversionMethodInternal(typeof(T));
        }

        /// <summary>
        /// Obtiene un <see cref="MethodInfo" /> para un método que permita
        /// realizar la conversión del tipo de la enumeración a un arreglo
        /// de bytes.
        /// </summary>
        /// <param name="enumType">Tipo de la enumeración a convertir.</param>
        /// <returns>
        /// Un <see cref="MethodInfo" /> para un método que convierte desde
        /// el tipo base de la enumeración a un arreglo de bytes.
        /// </returns>
        [DebuggerStepThrough]
        public static MethodInfo ByteConversionMethod(in Type enumType)
        {
            if (!enumType.IsEnum) throw new ArgumentException(St.EnumTypeExpected, nameof(enumType));
            return ByteConversionMethodInternal(enumType);
        }

        [DebuggerStepThrough]
        private static MethodInfo ByteConversionMethodInternal(in Type enumType)
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
        /// Convierte un valor de enumeraci�n a su representaci�n en bytes.
        /// </summary>
        /// <param name="value">Valor de enumeraci�n a convertir.</param>
        /// <returns>
        /// Un arreglo de bytes con la representaci�n del valor de
        /// enumeraci�n.
        /// </returns>
        /// <exception cref="PlatformNotSupportedException">
        /// Se produce en caso que la plataforma no sea soportada, y que el
        /// <see cref="Enum" /> utilice un tipo base inusual para el cual no
        /// sea posible obtener un convertidor a bytes.
        /// </exception>
        [DebuggerStepThrough]
        public static byte[] ToBytes(this Enum value)
        {
            return (byte[]) ByteConversionMethodInternal(value.GetType())
                .Invoke(null, new object[] {value})!;
        }

        /// <summary>
        /// Crea un delegado que convierte un valor de enumeración del tipo
        /// especificado en un arreglo de bytes.
        /// </summary>
        /// <typeparam name="T">Tipo de la enumeración a convertir.</typeparam>
        /// <returns>
        /// Un delegado que convierte un valor de enumeración del tipo
        /// especificado en un arreglo de bytes.
        /// </returns>
        [DebuggerStepThrough]
        public static Func<T, byte[]> ToBytes<T>() where T : struct, Enum
        {
            return (Func<T, byte[]>) Delegate.CreateDelegate(typeof(Func<T, byte[]>),
                ByteConversionMethodInternal(typeof(T)), true)!;
        }

        /// <summary>
        /// Obtiene un nombre personalizado para un valor de enumeración.
        /// </summary>
        /// <param name="value">
        /// <see cref="Enum" /> del cual obtener el nombre.
        /// </param>
        /// <returns>
        /// Un nombre amigable para <paramref name="value" />, o el nombre
        /// compilado de <paramref name="value" /> si no se ha definido un
        /// nombre amigable por medio del atributo
        /// <see cref="NameAttribute"/>.
        /// </returns>
        public static string NameOf(this Enum value)
        {
            return value.GetAttr<NameAttribute>()?.Value ?? value.ToString();
        }

        /// <summary>
        /// Expone los valores de un <see cref="Enum"/> como una colección
        /// de <see cref="NamedObject{T}"/>.
        /// </summary>
        /// <typeparam name="T">Tipo de la enumeración a obtener.</typeparam>
        /// <returns>
        /// Un enumerador que expone los valores del <see cref="Enum"/>
        /// como una colección de <see cref="NamedObject{T}"/>.
        /// </returns>
        public static IEnumerable<NamedObject<T>> NamedEnums<T>() where T : Enum
        {
            return typeof(T).GetEnumValues().OfType<T>()
                .Select(j => new NamedObject<T>(j, j.NameOf()));
        }

        /// <summary>
        /// Conveirte un valor de enumeración a su tipo base.
        /// </summary>
        /// <typeparam name="T">Tipo de la enumeración.</typeparam>
        /// <param name="value">Valor de enumeración a convertir.</param>
        /// <returns>
        /// Un valor primitivo igual al valor de enumeración.
        /// </returns>
        public static object ToUnderlyingType<T>(this T value) where T : struct, Enum
        {
            return Convert.ChangeType(value, Enum.GetUnderlyingType(typeof(T)));            
        }

        /// <summary>
        /// Conveirte un valor de enumeración a su tipo base.
        /// </summary>
        /// <param name="value">Valor de enumeración a convertir.</param>
        /// <returns>
        /// Un valor primitivo igual al valor de enumeración.
        /// </returns>
        public static object ToUnderlyingType(this Enum value)
        {
            return Convert.ChangeType(value, Enum.GetUnderlyingType(value.GetType()));
        }
    }
}