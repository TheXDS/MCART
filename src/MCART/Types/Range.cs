/*
Range.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo define la estructura Range<T>, la cual permite representar rangos
de valores.

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
using System.Linq;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Define un rango de valores.
    /// </summary>
    /// <typeparam name="T">Tipo base del rango de valores.</typeparam>
    public struct Range<T> : IRange<T>, IEquatable<IRange<T>>, ICloneable<Range<T>> where T : IComparable<T>
    {
        private T _minimum;
        private T _maximum;

        /// <summary>
        /// Valor mínimo del rango.
        /// </summary>
        public T Minimum
        {
            get => _minimum;
            set
            {
                if (value.CompareTo(Maximum) > 0) throw new ArgumentOutOfRangeException(nameof(value));
                _minimum = value;
            }
        }

        /// <summary>
        /// Valor máximo del rango.
        /// </summary>
        public T Maximum
        {
            get => _maximum;
            set
            {
                if (value.CompareTo(Minimum) < 0) throw new ArgumentOutOfRangeException(nameof(value));
                _maximum = value;
            }
        }

        /// <summary>
        /// Obtiene o establece un valor que determina si el valor mínimo
        /// es parte del rango.
        /// </summary>
        public bool MinInclusive { get; set; }

        /// <summary>
        /// Obtiene o establece un valor que determina si el valor máximo
        /// es parte del rango.
        /// </summary>
        public bool MaxInclusive { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la estructura <see cref="Range{T}" />
        /// </summary>
        /// <param name="maximum">Valor máximo del rango, inclusive.</param>
        public Range(T maximum) : this(default!, maximum, true, true) { }

        /// <summary>
        /// Inicializa una nueva instancia de la estructura <see cref="Range{T}" />
        /// </summary>
        /// <param name="minimum">Valor mínimo del rango, inclusive.</param>
        /// <param name="maximum">Valor máximo del rango, inclusive.</param>
        public Range(T minimum, T maximum) : this(minimum, maximum, true, true) { }

        /// <summary>
        /// Inicializa una nueva instancia de la estructura
        /// <see cref="Range{T}" />
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
        /// <see cref="Range{T}" />
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
            if (minimum.CompareTo(maximum) > 0) throw Errors.MinGtMax();
            _minimum = minimum;
            _maximum = maximum;
            MinInclusive = minInclusive;
            MaxInclusive = maxInclusive;
        }

        /// <summary>
        /// Convierte este <see cref="Range{T}"/> en su representación como una cadena.
        /// </summary>
        /// <returns>
        /// Una representación de este <see cref="Range{T}"/> como una cadena.
        /// </returns>
        public override string ToString()
        {
            return $"{Minimum} - {Maximum}";
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
        /// Crea un <see cref="Range{T}"/> a partir de una cadena.
        /// </summary>
        /// <param name="value">
        /// Valor a partir del cual crear un <see cref="Range{T}"/>.
        /// </param>
        /// <exception cref="FormatException">
        /// Se produce si la conversión ha fallado.
        /// </exception>
        /// <returns><see cref="Range{T}"/> que ha sido creado.</returns>
        public static Range<T> Parse(string value)
        {
            if (TryParse(value, out Range<T> retval)) return retval;
            throw new FormatException();
        }

        /// <summary>
        /// Intenta crear un <see cref="Range{T}"/> a partir de una cadena.
        /// </summary>
        /// <param name="value">
        /// Valor a partir del cual crear un <see cref="Range{T}"/>.
        /// </param>
        /// <param name="range">
        /// <see cref="Range{T}"/> que ha sido creado.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si la conversión ha tenido éxito,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool TryParse(string value, out Range<T> range)
        {
            string[] separators = 
            {
                ", ",
                "...",
                " - ",
                "; ",
                " : ",
                " | ",
                " ",
                ",",
                ";",
                ":",
                "|",
                " .. ",
                "..",
                " => ",
                " -> ",
                "=>",
                "->",
                "-"
            };

            return PrivateInternals.TryParseValues<T, Range<T>>(separators, value, 2, l => new Range<T>(l[0], l[1]), out range);
        }

        /// <summary>
        /// Combina este <see cref="Range{T}"/> con otro.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Range<T> Join(IRange<T> other)
        {
            return new(new[] { Minimum, other.Minimum }.Min()!, new[] { Maximum, other.Maximum }.Max()!);
        }

        /// <summary>
        /// Obtiene un rango de intersección a partir de este y otro rango especificado.
        /// </summary>
        /// <param name="other">Rango a intersectar.</param>
        /// <returns>
        /// La intersección entre este rango y <paramref name="other"/>.
        /// </returns>
        public Range<T> Intersect(IRange<T> other)
        {
            return new(new[] { Minimum, other.Minimum }.Max()!, new[] { Maximum, other.Maximum }.Min()!);
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
        /// Une dos rangos de valores.
        /// </summary>
        /// <param name="left">Operador de la izquierda.</param>
        /// <param name="right">Operador de la derecha.</param>
        /// <returns>
        /// La unión de ambos <see cref="Range{T}"/>. Si los rangos no
        /// intersectan, se incluirán todos los valores faltantes.
        /// </returns>
        public static Range<T> operator +(Range<T> left, Range<T> right)
        {
            return left.Join(right);
        }

        /// <summary>
        /// Indica si esta instancia y un objeto especificado son iguales.
        /// </summary>
        /// <param name="obj">
        /// Objeto que se va a compara con la instancia actual.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si esta instancia y
        /// <paramref name="obj" /> son iguales, <see langword="false" />
        /// en caso contrario.
        /// </returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as IRange<T>);
        }

        /// <summary>
        /// Devuelve el código Hash de esta instancia.
        /// </summary>
        /// <returns>El código Hash de esta instancia.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Minimum, Maximum, MinInclusive, MaxInclusive);
        }

        /// <summary>
        /// Indica si esta instancia y un objeto especificado son iguales.
        /// </summary>
        /// <param name="other">
        /// Objeto que se va a compara con la instancia actual.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si esta instancia y
        /// <paramref name="other" /> son iguales, <see langword="false" />
        /// en caso contrario.
        /// </returns>
        public bool Equals(IRange<T>? other)
        {
            if (other is null) return false;
            return
                Minimum.CompareTo(other.Minimum) == 0 &&
                Maximum.CompareTo(other.Maximum) == 0 &&
                MinInclusive == other.MinInclusive &&
                MaxInclusive == other.MaxInclusive;
        }

        /// <inheritdoc/>
        public Range<T> Clone()
        {
            return new(_minimum, _maximum, MinInclusive, MaxInclusive);
        }
        
        /// <summary>
        /// Compara la igualdad entre dos instancias de
        /// <see cref="Range{T}"/>.
        /// </summary>
        /// <param name="left">Objeto a comparar</param>
        /// <param name="right">Objeto contra el cual comparar.</param>
        /// <returns>
        /// <see langword="true" /> si ambas instancias son iguales,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool operator ==(Range<T> left, Range<T> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compara dos instancias de <see cref="Range{T}"/>, y devuelve
        /// <see langword="true"/> si son distintas la una de la otra.
        /// </summary>
        /// <param name="left">Objeto a comparar</param>
        /// <param name="right">Objeto contra el cual comparar.</param>
        /// <returns>
        /// <see langword="true" /> si ambas instancias son distintas,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool operator !=(Range<T> left, Range<T> right)
        {
            return !(left == right);
        }
    }
}