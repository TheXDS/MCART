/*
Range.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo define la estructura Range<T>, la cual permite representar rangos
de valores.

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
using System.Linq;
using TheXDS.MCART.Types.Base;

// ReSharper disable UnusedMember.Global

namespace TheXDS.MCART.Types
{
    /// <summary>
    ///     Define un rango de valores.
    /// </summary>
    /// <typeparam name="T">Tipo base del rango de valores.</typeparam>
    public struct Range<T> : IRange<T> where T : IComparable<T>
    {
        private T _minimum;
        private T _maximum;

        /// <summary>
        ///     Valor mínimo del rango.
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
        ///     Valor máximo del rango.
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
        ///     Obtiene o establece un valor que determina si el valor mínimo
        ///     es parte del rango.
        /// </summary>
        public bool MinInclusive { get; set; }

        /// <summary>
        ///     Obtiene o establece un valor que determina si el valor máximo
        ///     es parte del rango.
        /// </summary>
        public bool MaxInclusive { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la estructura <see cref="T:TheXDS.MCART.Types.Range`1" />
        /// </summary>
        /// <param name="maximum">Valor máximo del rango, inclusive.</param>
        public Range(T maximum) : this(default, maximum, true, true) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la estructura <see cref="T:TheXDS.MCART.Types.Range`1" />
        /// </summary>
        /// <param name="minimum">Valor mínimo del rango, inclusive.</param>
        /// <param name="maximum">Valor máximo del rango, inclusive.</param>
        public Range(T minimum, T maximum) : this(minimum, maximum, true, true) { }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la estructura
        ///     <see cref="T:TheXDS.MCART.Types.Range`1" />
        /// </summary>
        /// <param name="maximum">Valor máximo del rango.</param>
        /// <param name="inclusive">
        ///     Si se establece en <see langword="true"/>, el valor máximo será
        ///     incluido dentro del rango.
        /// </param>
        public Range(T maximum, bool inclusive) : this(default, maximum, inclusive, inclusive)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la estructura
        ///     <see cref="T:TheXDS.MCART.Types.Range`1" />
        /// </summary>
        /// <param name="minimum">Valor mínimo del rango.</param>
        /// <param name="maximum">Valor máximo del rango.</param>
        /// <param name="inclusive">
        ///     Si se establece en <see langword="true" />, los valores mínimo y
        ///     máximo serán incluidos dentro del rango.
        /// </param>
        public Range(T minimum, T maximum, bool inclusive) : this(minimum, maximum, inclusive, inclusive)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la estructura
        ///     <see cref="Range{T}"/>
        /// </summary>
        /// <param name="minimum">Valor mínimo del rango.</param>
        /// <param name="maximum">Valor máximo del rango.</param>
        /// <param name="minInclusive">
        ///     Si se establece en <see langword="true"/>, el valor mínimo será
        ///     incluido dentro del rango.
        /// </param>
        /// <param name="maxInclusive">
        ///     Si se establece en <see langword="true"/>, el valor máximo será
        ///     incluido dentro del rango.
        /// </param>
        public Range(T minimum, T maximum, bool minInclusive, bool maxInclusive)
        {
            if (minimum.CompareTo(maximum) > 0) throw new ArgumentOutOfRangeException();
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
        ///     Crea un <see cref="Range{T}"/> a partir de una cadena.
        /// </summary>
        /// <param name="value">
        ///     Valor a partir del cual crear un <see cref="Range{T}"/>.
        /// </param>
        /// <exception cref="FormatException">
        ///     Se produce si la conversión ha fallado.
        /// </exception>
        /// <returns><see cref="Range{T}"/> que ha sido creado.</returns>
        public static Range<T> Parse(string value)
        {
            if (TryParse(value, out var retval)) return retval;
            throw new FormatException();
        }

        /// <summary>
        ///     Intenta crear un <see cref="Range{T}"/> a partir de una cadena.
        /// </summary>
        /// <param name="value">
        ///     Valor a partir del cual crear un <see cref="Range{T}"/>.
        /// </param>
        /// <param name="range">
        ///     <see cref="Range{T}"/> que ha sido creado.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> si la conversión ha tenido éxito,
        ///     <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool TryParse(string value, out Range<T> range)
        {
            var separators = new[]
            {
                ", ",
                "...",
                " - ",
                " ",
                ";",
                ":",
                "|",
                "..",
                " to ",
                " a "
            };

            var t = Common.FindConverter<T>();
            if (!(t is null))
            {
                foreach (var j in separators)
                {
                    var l = value.Split(new[] { j }, StringSplitOptions.RemoveEmptyEntries);
                    if (l.Length != 2) continue;

                    try
                    {
                        range = new Range<T>(
                            (T)t.ConvertTo(l[0].Trim(), typeof(T)),
                            (T)t.ConvertTo(l[1].Trim(), typeof(T)));
                        return true;
                    }
                    catch
                    {
                        break;
                    }
                }
            }

            range = default;
            return false;
        }

        /// <summary>
        /// Combina este <see cref="Range{T}"/> con otro.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Range<T> Join(IRange<T> other)
        {
            return new Range<T>(new[] { Minimum, other.Minimum }.Min(), new[] { Maximum, other.Maximum }.Max());
        }

        /// <summary>
        ///     Obtiene un rango de intersección a partir de este y otro rango especificado.
        /// </summary>
        /// <param name="other">Rango a intersectar.</param>
        /// <returns>
        ///     La intersección entre este rango y <paramref name="other"/>.
        /// </returns>
        public Range<T> Intersect(IRange<T> other)
        {
            return new Range<T>(new[] { Minimum, other.Minimum }.Max(), new[] { Maximum, other.Maximum }.Min());
        }

        /// <summary>
        ///     Determina si un <see cref="Range{T}"/> intersecta a este.
        /// </summary>
        /// <param name="other">Rango a comprobar.</param>
        /// <returns>
        ///     <see langword="true"/> si <paramref name="other"/> intersecta a
        ///     este <see cref="Range{T}"/>, <see langword="false"/> en caso
        ///     contrario.
        /// </returns>
        public bool Intersects(IRange<T> other)
        {
            return IsWithin(other.Maximum) || IsWithin(other.Minimum);
        }

        /// <summary>
        ///     Une dos rangos de valores.
        /// </summary>
        /// <param name="left">Operador de la izquierda.</param>
        /// <param name="right">Operador de la derecha.</param>
        /// <returns>
        ///     La unión de ambos <see cref="Range{T}"/>. Si los rangos no
        ///     intersectan, se incluirán todos los valores faltantes.
        /// </returns>
        public static Range<T> operator +(Range<T> left, Range<T> right)
        {
            return left.Join(right);
        }
    }
}