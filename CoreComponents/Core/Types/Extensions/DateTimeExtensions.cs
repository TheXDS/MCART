/*
DateTimeExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene numerosas extensiones para el tipo System.DateTime del
CLR, supli�ndolo de nueva funcionalidad previamente no existente, o de
invocaci�n compleja.

Author(s):
     C�sar Andr�s Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 C�sar Andr�s Morgan

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
using System.Diagnostics.CodeAnalysis;

namespace MCART.Types.Extensions
{
    /// <summary>
    /// Contiene extensiones �tiles de la estructura <see cref="DateTime"/>.
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Obtiene un <see cref="DateTime"/> que representa el inicio del tiempo de Unix.
        /// </summary>
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        /// <summary>
        /// Obtiene un <see cref="DateTime"/> que representa el inicio del tiempo del siglo 20.
        /// </summary>
        /// <remarks>
        ///     Se define el Century Epoch como el punto de inicio del tiempo
        ///     para muchos dispositivos de 32 bits que no siguen el est�ndar
        ///     de Unix, y se establece como el primero de enero del a�o 1900.
        ///
        ///     El est�ndar se defin�a por medio del tiempo GMT, en su ausencia
        ///     se utiliza UTC coom un substituto suficientemente cercano.
        /// </remarks>
        public static readonly DateTime CenturyEpoch = new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        ///     Convierte un <see cref="DateTime"/> a un Timestamp de Unix de
        ///     64 bits.
        /// </summary>
        /// <param name="dateTime">
        ///     Valor de tiempo a convertir.
        /// </param>
        /// <returns>
        ///     Un <see cref="long"/> que representa el valor de Timestamp de
        ///     Unix del <see cref="DateTime"/> especificado.
        /// </returns>
        public static long ToUnixTimestamp(this DateTime dateTime)
        {
            return ToTimestamp(dateTime, UnixEpoch);
        }

        /// <summary>
        ///     Convierte un <see cref="DateTime"/> a un Timestamp  en
        ///     milisegundos de Unix de 64 bits.
        /// </summary>
        /// <param name="dateTime">
        ///     Valor de tiempo a convertir.
        /// </param>
        /// <returns>
        ///     Un <see cref="long"/> que representa el valor de Timestamp de
        ///     Unix en milisegundos del <see cref="DateTime"/> especificado.
        /// </returns>
        public static long ToUnixTimestampMs(this DateTime dateTime)
        {
            return ToTimestampMs(dateTime, UnixEpoch);
        }

        /// <summary>
        /// Obtiene un <see cref="DateTime"/> a partir de un Timestamp de Unix.
        /// </summary>
        /// <param name="seconds">
        ///     N�mero de segundos transcurridos desde el Epoch de Unix.
        /// </param>
        /// <returns>
        ///     Un <see cref="DateTime"/> construido a partir del Timestamp de
        ///     Unix.
        /// </returns>
        public static DateTime FromUnixTimestamp(long seconds)
        {
            return FromTimestamp(seconds, UnixEpoch);
        }

        /// <summary>
        /// Obtiene un <see cref="DateTime"/> a partir de un Timestamp de Unix.
        /// </summary>
        /// <param name="milliseconds">
        ///     N�mero de milisegundos transcurridos desde el Epoch de Unix.
        /// </param>
        /// <returns>
        ///     Un <see cref="DateTime"/> construido a partir del Timestamp de
        ///     Unix.
        /// </returns>
        public static DateTime FromUnixTimestampMs(long milliseconds)
        {
            return FromTimestampMs(milliseconds, UnixEpoch);
        }

        /// <summary>
        ///     Convierte un <see cref="DateTime"/> a un Timestamp de 64 bits.
        /// </summary>
        /// <param name="dateTime">
        ///     Valor de tiempo a convertir.
        /// </param>
        /// <param name="epoch">
        ///     Epoch inicial a utilizar.
        /// </param>
        /// <returns>
        ///     Un <see cref="long"/> que representa el valor de Timestamp del
        ///     <see cref="DateTime"/> especificado.
        /// </returns>
        public static long ToTimestamp(this DateTime dateTime, DateTime epoch)
        {
            return (long)(dateTime - epoch).TotalSeconds;
        }

        /// <summary>
        ///     Convierte un <see cref="DateTime"/> a un Timestamp de 64 bits.
        /// </summary>
        /// <param name="dateTime">
        ///     Valor de tiempo a convertir.
        /// </param>
        /// <param name="epoch">
        ///     Epoch inicial a utilizar.
        /// </param>
        /// <returns>
        ///     Un <see cref="long"/> que representa el valor de Timestamp del
        ///     <see cref="DateTime"/> especificado.
        /// </returns>
        public static long ToTimestampMs(this DateTime dateTime, DateTime epoch)
        {
            return (long)(dateTime - epoch).TotalMilliseconds;
        }

        /// <summary>
        ///     Obtiene un <see cref="DateTime"/> a partir de un Timestamp.
        /// </summary>
        /// <param name="seconds">
        ///     N�mero de segundos transcurridos desde el Epoch especificado.
        /// </param>
        /// <param name="epoch">
        ///     Epoch inicial a utilizar.
        /// </param>
        /// <returns>
        ///     Un <see cref="DateTime"/> construido a partir del Timestamp.
        /// </returns>
        public static DateTime FromTimestamp(long seconds, DateTime epoch)
        {
            return epoch.AddSeconds(seconds);
        }
        /// <summary>
        ///     Obtiene un <see cref="DateTime"/> a partir de un Timestamp en
        ///     milisegundos.
        /// </summary>
        /// <param name="milliseconds">
        ///     N�mero de segundos transcurridos desde el Epoch especificado.
        /// </param>
        /// <param name="epoch">
        ///     Epoch inicial a utilizar.
        /// </param>
        /// <returns>
        ///     Un <see cref="DateTime"/> construido a partir del Timestamp.
        /// </returns>
        public static DateTime FromTimestampMs(long milliseconds, DateTime epoch)
        {
            return epoch.AddMilliseconds(milliseconds);
        }
    }
}