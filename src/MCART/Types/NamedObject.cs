/*
NamedObject.cs

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
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Estructura que permite asignarle una etiqueta a cualquier objeto.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto.</typeparam>
    public struct NamedObject<T> : INameable
    {
        /// <summary>
        /// Enumera a todos los miembros de la enumeración como objetos
        /// nombrables.
        /// </summary>
        /// <returns>
        /// Una enumeración de <see cref="NamedObject{T}"/> de todos los
        /// valores de enumeración.
        /// </returns>
        public static IEnumerable<NamedObject<T>> FromEnum()
        {
            if (!typeof(T).IsEnum) throw new InvalidTypeException(typeof(T));
            return typeof(T).GetEnumValues().OfType<T>().Select(p => (NamedObject<T>)p);
        }

        /// <summary>
        /// Valor del objeto.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Etiqueta del objeto.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la estructura
        /// <see cref="NamedObject{T}" /> estableciendo un valor junto a una
        /// etiqueta auto-generada a partir de
        /// <see cref="M:System.Object.ToString" />.
        /// </summary>
        /// <param name="value">Objeto a etiquetar.</param>
        public NamedObject(T value) : this(value, Infer(value))
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la estructura
        /// <see cref="NamedObject{T}" /> estableciendo un valor y una
        /// etiqueta para el mismo.
        /// </summary>
        /// <param name="value">Objeto a etiquetar.</param>
        /// <param name="name">Etiqueta del objeto.</param>
        public NamedObject(T value, string name)
        {
            Value = value;
            Name = name;
        }

        /// <summary>
        /// Infiere la etiqueta de un objeto.
        /// </summary>
        /// <param name="obj">Objeto para el cual inferir una etiqueta.</param>
        /// <returns>
        /// El nombre inferido del objeto, o
        /// <see cref="object.ToString()" /> de no poderse inferir una
        /// etiqueta adecuada.
        /// </returns>
        public static string Infer(T obj)
        {
            return obj switch
            {
                INameable n => n.Name,
                MemberInfo m => Extensions.MemberInfoExtensions.NameOf(m),
                Enum e => EnumExtensions.NameOf(e),
                null => throw new ArgumentNullException(nameof(obj)),
                _ => obj.GetAttr<NameAttribute>()?.Value ?? obj.ToString() ?? obj.GetType().NameOf()
            };
        }

        /// <summary>
        /// Convierte implícitamente un <typeparamref name="T" /> en un
        /// <see cref="NamedObject{T}" />.
        /// </summary>
        /// <param name="obj">Objeto a convertir.</param>
        public static implicit operator NamedObject<T>(T obj)
        {
            return new(obj);
        }

        /// <summary>
        /// Convierte implícitamente un <see cref="NamedObject{T}" /> en un
        /// <typeparamref name="T" />.
        /// </summary>
        /// <param name="namedObj">Objeto a convertir.</param>
        public static implicit operator T(NamedObject<T> namedObj)
        {
            return namedObj.Value;
        }

        /// <summary>
        /// Convierte implícitamente un <see cref="NamedObject{T}" /> en un
        /// <see cref="string" />.
        /// </summary>
        /// <param name="namedObj">Objeto a convertir.</param>
        public static implicit operator string(NamedObject<T> namedObj)
        {
            return namedObj.Name;
        }

        /// <summary>
        /// Convierte implícitamente un <see cref="NamedObject{T}" /> en un
        /// <see cref="KeyValuePair{TKey,TValue}" />.
        /// </summary>
        /// <param name="namedObj">Objeto a convertir.</param>
        public static implicit operator KeyValuePair<string, T>(NamedObject<T> namedObj)
        {
            return new(namedObj.Name, namedObj.Value);
        }

        /// <summary>
        /// Convierte implícitamente un
        /// <see cref="KeyValuePair{TKey,TValue}" /> en un
        /// <see cref="NamedObject{T}" />.
        /// </summary>
        /// <param name="keyValuePair">Objeto a convertir.</param>
        public static implicit operator NamedObject<T>(KeyValuePair<string, T> keyValuePair)
        {
            return new(keyValuePair.Value, keyValuePair.Key);
        }

        /// <summary>
        /// Compara la igualdad entre este <see cref="NamedObject{T}"/> y
        /// otro objeto.
        /// </summary>
        /// <param name="obj">
        /// Objeto contra el cual comparar esta instancia.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si ambas instancias son consideradas
        /// iguales, <see langword="false"/> en caso contrario.
        /// </returns>
        public override bool Equals(object? obj)
        {
            return obj switch
            {
                NamedObject<T> other => Equals(other.Value),
                T other => Value?.Equals(other) ?? false,
                null => Value is null,
                _ => false
            };
        }

        /// <summary>
        /// Obtiene el código Hash para esta instancia.
        /// </summary>
        /// <returns>El código Hash para esta instancia.</returns>
        public override int GetHashCode()
        {
            return Value?.GetHashCode() ?? base.GetHashCode();
        }

        /// <summary>
        /// Compara la igualdad entre dos instancias de
        /// <see cref="NamedObject{T}"/>.
        /// </summary>
        /// <param name="left">
        /// Objeto a comparar.
        /// </param>
        /// <param name="right">
        /// Objeto contra el cual comparar.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si ambas instancias son consideradas
        /// iguales, <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool operator ==(NamedObject<T> left, NamedObject<T> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Comprueba si ambas instancias de <see cref="NamedObject{T}"/>
        /// son consideradas distintas.
        /// </summary>
        /// <param name="left">
        /// Objeto a comparar.
        /// </param>
        /// <param name="right">
        /// Objeto contra el cual comparar.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si ambas instancias son consideradas
        /// distintas, <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool operator !=(NamedObject<T> left, NamedObject<T> right)
        {
            return !(left == right);
        }
    }
}