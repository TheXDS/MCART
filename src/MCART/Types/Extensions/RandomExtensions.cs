/*
RandomExtensions.cs

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

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Extensiones para la clase <see cref="Random" />
    /// </summary>
    public static class RandomExtensions
    {
        private const string Text = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        ///     Necesario para evitar que las funciones que requieren de números
        ///     aleatorios generen objetos <see cref="Random" /> con el mismo
        ///     número de semilla basada en tiempo.
        /// </summary>
        public static Random Rnd { get; } = new Random();

        /// <summary>
        ///     Obtiene una cadena de texto aleatorio.
        /// </summary>
        /// <param name="r">
        ///     Instancia del objeto <see cref="Random" /> a utilizar.
        /// </param>
        /// <param name="length">Longitud de la cadena a generar.</param>
        /// <returns>
        ///     Una cadena de texto aleatorio con la longitud especificada.
        /// </returns>
        public static string RndText(this Random r, in int length)
        {
            var x = string.Empty;
            while (x.Length < length) x += Text[r.Next(0, Text.Length)];
            return x;
        }

        /// <summary>
        ///     Devuelve un número entero aleatorio que se encuentra dentro del 
        ///     rango especificado.
        /// </summary>
        /// <param name="r">
        ///     Instancia del objeto <see cref="Random" /> a utilizar.
        /// </param>
        /// <param name="range">
        ///     <see cref="Range{T}" /> de números a seleccionar.
        /// </param>
        /// <returns>
        ///     Un número entero aleatorio que se encuentra dentro del rango
        ///     especificado.
        /// </returns>
        public static int Next(this Random r, in Range<int> range)
        {
            return r.Next(range.Minimum, range.Maximum);
        }

        /// <summary>
        ///     Obtiene aleatoriamente un valor <see langword="true"/> o
        ///     <see langword="false"/>.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> o <see langword="false"/>, de manera
        ///     aleatoria.
        /// </returns>
        /// <param name="r">
        ///     Instancia del objeto <see cref="Random" /> a utilizar.
        /// </param>
        public static bool CoinFlip(this Random r)
        {
            return (r.Next() & 1) == 0;
        }
    }
}