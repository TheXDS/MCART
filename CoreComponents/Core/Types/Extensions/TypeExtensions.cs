/*
TypeExtensions.cs

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
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Extensiones para todos los elementos de tipo <see cref="Type" />.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Determina si el tipo implementa a <paramref name="type"/>.
        /// </summary>
        /// <param name="t">Tipo a comprobar</param>
        /// <param name="type">Herencia de tipo a verificar.</param>
        /// <returns>
        /// <see langword="true"/> si <paramref name="t"/> implementa a <paramref name="type"/>,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool Implements(this Type t, Type type)
        {
            if (!type.ContainsGenericParameters) return type.IsAssignableFrom(t);
            var gt = type.MakeGenericType(t);
            return !gt.ContainsGenericParameters && gt.IsAssignableFrom(t);
        }

        /// <summary>
        /// Determina si el tipo implementa a <paramref name="baseType"/> con los argumentos de tipo genérico especificados.
        /// </summary>
        /// <param name="t">Tipo a comprobar</param>
        /// <param name="baseType">Herencia de tipo a verificar.</param>
        /// <param name="typeArgs">Tipos de argumentos genéricos a utilizar para crear el tipo genérico a comprobar.</param>
        /// <returns>
        /// <see langword="true"/> si <paramref name="t"/> implementa a <paramref name="baseType"/>,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool Implements(this Type t, Type baseType, params Type[] typeArgs)
        {
            if (!baseType.ContainsGenericParameters) return baseType.IsAssignableFrom(t);
            var gt = baseType.MakeGenericType(typeArgs);
            return !gt.ContainsGenericParameters && gt.IsAssignableFrom(t);
        }
        /// <summary>
        /// Determina si el tipo implementa a <typeparamref name="T"/>.
        /// </summary>
        /// <param name="type">Tipo a comprobar</param>
        /// <typeparam name="T">Herencia de tipo a verificar.</typeparam>
        /// <returns>
        /// <see langword="true"/> si <paramref name="type"/> implementa a <typeparamref name="T"/>,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool Implements<T>(this Type type) => Implements(type, typeof(T));

        /// <summary>
        ///     Equivalente programático de <see langword="default" />, obtiene
        ///     el valor predeterminado del tipo.
        /// </summary>
        /// <param name="t">
        ///     <see cref="Type" /> del cual obtener el valor predeterminado.
        /// </param>
        /// <returns>
        ///     Una nueva instancia del tipo si el mismo es un
        ///     <see langword="struct" />, o <see langword="null" /> si es una
        ///     <see langword="class" />.
        /// </returns>
        public static object Default(this Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }

        /// <summary>
        ///     Obtiene un nombre personalizado para un tipo.
        /// </summary>
        /// <param name="t">
        ///     <see cref="Type" /> del cual obtener el nombre.
        /// </param>
        /// <returns>
        ///     Un nombre amigable para <paramref name="t" />, o el nombre de
        ///     tipo de <paramref name="t" /> si no se ha definido un nombre
        ///     amigable por medio del atributo <see cref="NameAttribute" />.
        /// </returns>
        public static string TypeName(this Type t)
        {
            return t.GetAttr<NameAttribute>()?.Value ?? t.Name;
        }
    }
}