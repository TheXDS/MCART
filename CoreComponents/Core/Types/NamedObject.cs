/*
NamedObject.cs

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
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Types
{
    /// <summary>
    ///     Estructura que permite asignarle una etiqueta a cualquier objeto.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto.</typeparam>
    public struct NamedObject<T>
    {
        /// <summary>
        ///     Valor del objeto.
        /// </summary>
        public T Value { get; }

        /// <summary>
        ///     Etiqueta del objeto.
        /// </summary>
        public string Label { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la estructura
        ///     <see cref="T:TheXDS.MCART.Types.NamedObject`1" /> estableciendo un valor junto a una
        ///     etiqueta auto-generada a partir de
        ///     <see cref="M:System.Object.ToString" />.
        /// </summary>
        /// <param name="value">Objeto a etiquetar.</param>
        public NamedObject(T value) : this(value, Infer(value)) { }

        /// <summary>
        ///     Inicializa una nueva instancia de la estructura
        ///     <see cref="NamedObject{T}"/> estableciendo un valor y una
        ///     etiqueta para el mismo.
        /// </summary>
        /// <param name="value">Objeto a etiquetar.</param>
        /// <param name="label">Etiqueta del objeto.</param>
        public NamedObject(T value, string label)
        {
            Value = value;
            Label = label;
        }

        /// <summary>
        ///     Convierte implícitamente un <typeparamref name="T"/> en un
        ///     <see cref="NamedObject{T}"/>.
        /// </summary>
        /// <param name="obj">Objeto a convertir.</param>
        public static implicit operator NamedObject<T>(T obj)
        {
            return new NamedObject<T>(obj);
        }

        /// <summary>
        ///     Convierte implícitamente un <see cref="NamedObject{T}"/> en un
        ///     <typeparamref name="T"/>.
        /// </summary>
        /// <param name="namedObj">Objeto a convertir.</param>
        public static implicit operator T(NamedObject<T> namedObj)
        {
            return namedObj.Value;
        }

        /// <summary>
        ///     Convierte implícitamente un <see cref="NamedObject{T}"/> en un
        ///     <see cref="string"/>.
        /// </summary>
        /// <param name="namedObj">Objeto a convertir.</param>
        public static implicit operator string(NamedObject<T> namedObj)
        {
            return namedObj.Label;
        }

        /// <summary>
        ///     Infiere la etiqueta de un objeto.
        /// </summary>
        /// <param name="o">Objeto para el cual inferir una etiqueta.</param>
        /// <returns>
        ///     El nombre inferido del objeto, o
        ///     <see cref="object.ToString()"/> de no poderse inferir una
        ///     etiqueta adecuada.
        /// </returns>
        private static string Infer(T o)
        {
            return
                (o as MemberInfo)?.NameOf() ??
                (o as Enum)?.NameOf() ??
                o.GetAttr<NameAttribute>()?.Value ??
                o.ToString();
        }
    }
}