/*
NamedObject.cs

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
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Types.Extensions.MemberInfoExtensions;
using System.Linq;

namespace TheXDS.MCART.Types
{
    /// <summary>
    ///     Estructura que permite asignarle una etiqueta a cualquier objeto.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto.</typeparam>
    public struct NamedObject<T> : INameable
    {
        /// <summary>
        ///     Enumera a todos los miembros de la enumeración como objetos
        ///     nombrables.
        /// </summary>
        /// <returns>
        ///     Una enumeración de <see cref="NamedObject{T}"/> de todos los
        ///     valores de enumeración.
        /// </returns>
        public static IEnumerable<NamedObject<T>> FromEnum()
        {
            if (!typeof(T).IsEnum) throw new InvalidTypeException(typeof(T));
            return typeof(T).GetEnumValues().OfType<T>().Select(p => (NamedObject<T>)p);
        }

        /// <summary>
        ///     Valor del objeto.
        /// </summary>
        public T Value { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Etiqueta del objeto.
        /// </summary>
        public string Name { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la estructura
        ///     <see cref="NamedObject{T}" /> estableciendo un valor junto a una
        ///     etiqueta auto-generada a partir de
        ///     <see cref="M:System.Object.ToString" />.
        /// </summary>
        /// <param name="value">Objeto a etiquetar.</param>
        public NamedObject(T value) : this(value, Infer(value))
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la estructura
        ///     <see cref="NamedObject{T}" /> estableciendo un valor y una
        ///     etiqueta para el mismo.
        /// </summary>
        /// <param name="value">Objeto a etiquetar.</param>
        /// <param name="name">Etiqueta del objeto.</param>
        public NamedObject(T value, string name)
        {
            Value = value;
            Name = name;
        }

        /// <summary>
        ///     Infiere la etiqueta de un objeto.
        /// </summary>
        /// <param name="obj">Objeto para el cual inferir una etiqueta.</param>
        /// <returns>
        ///     El nombre inferido del objeto, o
        ///     <see cref="object.ToString()" /> de no poderse inferir una
        ///     etiqueta adecuada.
        /// </returns>
        public static string Infer(T obj)
        {
            return obj switch
            {
                INameable n => n.Name,
                MemberInfo m => Extensions.MemberInfoExtensions.NameOf(m),
                Enum e => EnumExtensions.NameOf(e),
                null => throw new ArgumentNullException(nameof(obj)),
                _ => obj!.GetAttr<NameAttribute>()?.Value ?? obj!.ToString() ?? obj.NameOf()
            };
        }

        /// <summary>
        ///     Convierte implícitamente un <typeparamref name="T" /> en un
        ///     <see cref="NamedObject{T}" />.
        /// </summary>
        /// <param name="obj">Objeto a convertir.</param>
        public static implicit operator NamedObject<T>(T obj)
        {
            return new NamedObject<T>(obj);
        }

        /// <summary>
        ///     Convierte implícitamente un <see cref="NamedObject{T}" /> en un
        ///     <typeparamref name="T" />.
        /// </summary>
        /// <param name="namedObj">Objeto a convertir.</param>
        public static implicit operator T(NamedObject<T> namedObj)
        {
            return namedObj.Value;
        }

        /// <summary>
        ///     Convierte implícitamente un <see cref="NamedObject{T}" /> en un
        ///     <see cref="string" />.
        /// </summary>
        /// <param name="namedObj">Objeto a convertir.</param>
        public static implicit operator string(NamedObject<T> namedObj)
        {
            return namedObj.Name;
        }

        /// <summary>
        ///     Convierte implícitamente un <see cref="NamedObject{T}" /> en un
        ///     <see cref="KeyValuePair{TKey,TValue}" />.
        /// </summary>
        /// <param name="namedObj">Objeto a convertir.</param>
        public static implicit operator KeyValuePair<string, T>(NamedObject<T> namedObj)
        {
            return new KeyValuePair<string, T>(namedObj.Name, namedObj.Value);
        }

        /// <summary>
        ///     Convierte implícitamente un
        ///     <see cref="KeyValuePair{TKey,TValue}" /> en un
        ///     <see cref="NamedObject{T}" />.
        /// </summary>
        /// <param name="keyValuePair">Objeto a convertir.</param>
        public static implicit operator NamedObject<T>(KeyValuePair<string, T> keyValuePair)
        {
            return new NamedObject<T>(keyValuePair.Value, keyValuePair.Key);
        }
    }
}