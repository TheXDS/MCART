/*
MemberInfoExtensions.cs

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

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Helpers;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Extensiones varias para objetos <see cref="MemberInfo" />.
    /// </summary>
    public static partial class MemberInfoExtensions
    {
        /// <summary>
        /// Obtiene un nombre personalizado para un miembro.
        /// </summary>
        /// <param name="member">
        /// <see cref="MemberInfo" /> del cual obtener el nombre.
        /// </param>
        /// <returns>
        /// Un nombre amigable para <paramref name="member" />, o el nombre
        /// definido para <paramref name="member" /> si no se ha definido
        /// un nombre amigable por medio del atributo
        /// <see cref="NameAttribute"/>.
        /// </returns>
        public static string NameOf(this MemberInfo member)
        {
            return member.GetAttr<NameAttribute>()?.Value ?? member.Name;
        }

        /// <summary>
        /// Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
        /// </typeparam>
        /// <param name="member">
        /// Miembro del cual se extraerá el atributo.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
        /// en caso contrario.
        /// </returns>
        public static bool HasAttr<T>(this MemberInfo member) where T : Attribute
        {
            return HasAttr<T>(member, out _);
        }

        /// <summary>
        /// Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
        /// </typeparam>
        /// <param name="member">
        /// Miembro del cual se extraerá el atributo.
        /// </param>
        /// <param name="attribute">
        /// Parámetro de salida. Si un atributo de tipo
        /// <typeparamref name="T" /> ha sido encontrado, el mismo es devuelto.
        /// Se devolverá <see langword="null" /> si el miembro no posee el atributo
        /// especificado.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
        /// en caso contrario.
        /// </returns>
        public static bool HasAttrs<T>(this MemberInfo member, out IEnumerable<T> attribute) where T : Attribute
        {
            HasAttrs_Contract(member);
            attribute = Attribute.GetCustomAttributes(member, typeof(T)).OfType<T>();
            return attribute.Any();
        }

        /// <summary>
        /// Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
        /// </typeparam>
        /// <param name="member">
        /// Miembro del cual se extraerá el atributo.
        /// </param>
        /// <param name="attribute">
        /// Parámetro de salida. Si un atributo de tipo
        /// <typeparamref name="T" /> ha sido encontrado, el mismo es devuelto.
        /// Se devolverá <see langword="null" /> si el miembro no posee el atributo
        /// especificado.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
        /// en caso contrario.
        /// </returns>
        public static bool HasAttr<T>(this MemberInfo member, [MaybeNullWhen(false)] out T attribute) where T : notnull, Attribute
        {
            bool retVal = HasAttrs<T>(member, out IEnumerable<T>? attrs);
            attribute = attrs.FirstOrDefault();
            return retVal;
        }

        /// <summary>
        /// Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="TValue">
        /// Tipo de valor a devolver.
        /// </typeparam>
        /// <typeparam name="TAttribute">
        /// Tipo de atributo a buscar. Debe heredar de
        /// <see cref="Attribute"/> y de <see cref="IValueAttribute{T}"/>.
        /// </typeparam>
        /// <param name="member">
        /// Miembro del cual se extraerá el atributo.
        /// </param>
        /// <param name="value">
        /// Parámetro de salida. Si un atributo de tipo
        /// <typeparamref name="TAttribute" /> ha sido encontrado, el valor
        /// del mismo es devuelto.
        /// Se devolverá <see langword="default" /> si el miembro no posee el atributo
        /// especificado.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
        /// en caso contrario.
        /// </returns>
        public static bool HasAttrValue<TAttribute, TValue>(this MemberInfo member, out TValue value)
            where TAttribute : Attribute, IValueAttribute<TValue>
        {
            bool retVal = HasAttrs<TAttribute>(member, out IEnumerable<TAttribute>? attrs);
            TAttribute? a = attrs.FirstOrDefault();
            value = a is not null ? a.Value : default!;
            return retVal;
        }

        /// <summary>
        /// Devuelve el atributo asociado a la declaración del objeto
        /// especificado.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
        /// </typeparam>
        /// <param name="member">
        /// Miembro del cual se extraerá el atributo.
        /// </param>
        /// <returns>
        /// Un atributo del tipo <typeparamref name="T" /> con los datos
        /// asociados en la declaración del miembro; o <see langword="null" /> en caso de
        /// no encontrarse el atributo especificado.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="member"/> es
        /// <see langword="null"/>.
        /// </exception>
        [Sugar]
        public static T? GetAttr<T>(this MemberInfo member) where T : Attribute
        {
            HasAttr<T>(member, out T? attr);
            return attr;
        }

        /// <summary>
        /// Devuelve el atributo asociado al ensamblado especificado.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
        /// </typeparam>
        /// <param name="member">
        /// <see cref="MemberInfo" /> del cual se extraerá el
        /// atributo.
        /// </param>
        /// <returns>
        /// Un atributo del tipo <typeparamref name="T" /> con los datos
        /// asociados en la declaración del ensamblado; o <see langword="null" /> en caso
        /// de no encontrarse el atributo especificado.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="member"/> es
        /// <see langword="null"/>.
        /// </exception>
        [Sugar]
        public static IEnumerable<T> GetAttrs<T>(this MemberInfo member) where T : Attribute
        {
            HasAttrs(member, out IEnumerable<T> attr);
            return attr;
        }
    }
}