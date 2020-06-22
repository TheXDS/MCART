/*
Range.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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
using System.ComponentModel.DataAnnotations.Schema;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Types.Entity
{
    /// <summary>
    /// Define un rango de valores.
    /// </summary>
    /// <typeparam name="T">Tipo base del rango de valores.</typeparam>
    [ComplexType]
    public class Range<T> : IRange<T> where T : IComparable<T>
    {
        /// <summary>
        /// Valor máximo del rango.
        /// </summary>
        public T Maximum { get; set; }

        /// <summary>
        /// Obtiene o establece un valor que determina si el valor máximo
        /// es parte del rango.
        /// </summary>
        public bool MaxInclusive { get; set; }

        /// <summary>
        /// Valor mínimo del rango.
        /// </summary>
        public T Minimum { get; set; }

        /// <summary>
        /// Obtiene o establece un valor que determina si el valor mínimo
        /// es parte del rango.
        /// </summary>
        public bool MinInclusive { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la estructura
        /// <see cref="Range{T}" />.
        /// </summary>
        public Range() : this(default!, default!, true, true) { }

        /// <summary>
        /// Inicializa una nueva instancia de la estructura
        /// <see cref="Range{T}" />.
        /// </summary>
        /// <param name="maximum">Valor máximo del rango, inclusive.</param>
        public Range(T maximum) : this(default!, maximum, true, true) { }

        /// <summary>
        /// Inicializa una nueva instancia de la estructura
        /// <see cref="Range{T}" />.
        /// </summary>
        /// <param name="minimum">Valor mínimo del rango, inclusive.</param>
        /// <param name="maximum">Valor máximo del rango, inclusive.</param>
        public Range(T minimum, T maximum) : this(minimum, maximum, true, true) { }

        /// <summary>
        /// Inicializa una nueva instancia de la estructura
        /// <see cref="Range{T}" />.
        /// </summary>
        /// <param name="maximum">Valor máximo del rango.</param>
        /// <param name="inclusive">
        /// Si se establece en <see langword="true"/>, el valor máximo será
        /// incluido dentro del rango.
        /// </param>
        public Range(T maximum, bool inclusive) : this(default!, maximum, inclusive, inclusive)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la estructura
        /// <see cref="Range{T}" />.
        /// </summary>
        /// <param name="minimum">Valor mínimo del rango.</param>
        /// <param name="maximum">Valor máximo del rango.</param>
        /// <param name="inclusive">
        /// Si se establece en <see langword="true" />, los valores mínimo y
        /// máximo serán incluidos dentro del rango.
        /// </param>
        public Range(T minimum, T maximum, bool inclusive) : this(minimum, maximum, inclusive, inclusive)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la estructura
        /// <see cref="Range{T}"/>
        /// </summary>
        /// <param name="minimum">Valor mínimo del rango.</param>
        /// <param name="maximum">Valor máximo del rango.</param>
        /// <param name="minInclusive">
        /// Si se establece en <see langword="true"/>, el valor mínimo será
        /// incluido dentro del rango.
        /// </param>
        /// <param name="maxInclusive">
        /// Si se establece en <see langword="true"/>, el valor máximo será
        /// incluido dentro del rango.
        /// </param>
        public Range(T minimum, T maximum, bool minInclusive, bool maxInclusive)
        {
            if (minimum.CompareTo(maximum) > 0) throw new ArgumentOutOfRangeException();
            Minimum = minimum;
            Maximum = maximum;
            MinInclusive = minInclusive;
            MaxInclusive = maxInclusive;
        }

        /// <summary>
        /// Determina si un <see cref="Range{T}"/> intersecta a este.
        /// </summary>
        /// <param name="other">Rango a comprobar.</param>
        /// <returns>
        /// <see langword="true"/> si <paramref name="other"/> intersecta a
        /// este <see cref="Range{T}"/>, <see langword="false"/> en caso
        /// contrario.
        /// </returns>
        public bool Intersects(IRange<T> other)
        {
            return IsWithin(other.Maximum) || IsWithin(other.Minimum);
        }

        /// <summary>
        /// Comprueba si un valor <typeparamref name="T"/> se encuentra dentro de este <see cref="Range{T}"/>.
        /// </summary>
        /// <param name="value">Valor a comporbar.</param>
        /// <returns>
        /// <see langword="true"/> si el valor se encuentra dentro de este <see cref="Range{T}"/>,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public bool IsWithin(T value)
        {
            return value.IsBetween(Minimum, Maximum, MinInclusive, MaxInclusive);
        }

        /// <summary>
        /// Convierte implcitamente un <see cref="Types.Range{T}"/> en un
        /// <see cref="Range{T}"/>.
        /// </summary>
        /// <param name="value">
        /// <see cref="Types.Range{T}"/> a convertir.
        /// </param>
        public static implicit operator Range<T>(Types.Range<T> value)
        {
            return new Range<T>(value.Minimum, value.Maximum, value.MinInclusive, value.MaxInclusive);
        }

        /// <summary>
        /// Convierte implcitamente un <see cref="Range{T}"/> en un
        /// <see cref="Types.Range{T}"/>.
        /// </summary>
        /// <param name="value">
        /// <see cref="Range{T}"/> a convertir.
        /// </param>
        public static implicit operator Types.Range<T>(Range<T> value)
        {
            return new Types.Range<T>(value.Minimum, value.Maximum, value.MinInclusive, value.MaxInclusive);
        }
    }
}