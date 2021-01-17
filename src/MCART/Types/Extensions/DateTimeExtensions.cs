/*
DateTimeExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene numerosas extensiones para el tipo System.DateTime del
CLR, supliéndolo de nueva funcionalidad previamente no existente, o de
invocación compleja.

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
using System.Globalization;

namespace TheXDS.MCART.Types.Extensions
{

    /// <summary>
    /// Contiene extensiones útiles de la estructura <see cref="DateTime"/>.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Obtiene un <see cref="DateTime"/> que representa el inicio del
        /// tiempo de Unix.
        /// </summary>
        public static readonly DateTime UnixEpoch = Epoch(1970);

        /// <summary>
        /// Obtiene un <see cref="DateTime"/> que representa el inicio del
        /// tiempo del siglo 20.
        /// </summary>
        /// <remarks>
        /// Se define el Century Epoch como el punto de inicio del tiempo
        /// para muchos dispositivos de 32 bits que no siguen el estándar
        /// de Unix, y se establece como el primero de enero del año 1900.
        /// El estándar se definía por medio del tiempo GMT, en su ausencia
        /// se utiliza UTC como un substituto suficientemente cercano.
        /// </remarks>
        public static readonly DateTime CenturyEpoch = Epoch(1900);

        /// <summary>
        /// Obtiene un <see cref="DateTime"/> que representa el inicio del
        /// tiempo del siglo 21.
        /// </summary>
        /// <remarks>
        /// Se define el Y2K Epoch como el punto de inicio del tiempo para
        /// los dispositivos de 32 bits que no siguen el estándar de Unix,
        /// y se establece como el primero de enero del año 2000.
        /// </remarks>
        public static readonly DateTime Y2KEpoch = Epoch(2000);

        /// <summary>
        /// Crea un <see cref="DateTime"/> que representa el inicio del
        /// tiempo para un año específico.
        /// </summary>
        public static DateTime Epoch(in int year) => new DateTime(year, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Convierte un <see cref="DateTime"/> a un Timestamp de Unix de
        /// 64 bits.
        /// </summary>
        /// <param name="dateTime">
        /// Valor de tiempo a convertir.
        /// </param>
        /// <returns>
        /// Un <see cref="long"/> que representa el valor de Timestamp de
        /// Unix del <see cref="DateTime"/> especificado.
        /// </returns>
        public static long ToUnixTimestamp(this in DateTime dateTime)
        {
            return ToTimestamp(dateTime, UnixEpoch);
        }

        /// <summary>
        /// Convierte un <see cref="DateTime"/> a un Timestamp en
        /// milisegundos de Unix de 64 bits.
        /// </summary>
        /// <param name="dateTime">
        /// Valor de tiempo a convertir.
        /// </param>
        /// <returns>
        /// Un <see cref="long"/> que representa el valor de Timestamp de
        /// Unix en milisegundos del <see cref="DateTime"/> especificado.
        /// </returns>
        public static long ToUnixTimestampMs(this in DateTime dateTime)
        {
            return ToTimestampMs(dateTime, UnixEpoch);
        }

        /// <summary>
        /// Obtiene un <see cref="DateTime"/> a partir de un Timestamp de Unix.
        /// </summary>
        /// <param name="seconds">
        /// Número de segundos transcurridos desde el Epoch de Unix.
        /// </param>
        /// <returns>
        /// Un <see cref="DateTime"/> construido a partir del Timestamp de
        /// Unix.
        /// </returns>
        public static DateTime FromUnixTimestamp(in long seconds)
        {
            return FromTimestamp(seconds, UnixEpoch);
        }

        /// <summary>
        /// Obtiene un <see cref="DateTime"/> a partir de un Timestamp de Unix.
        /// </summary>
        /// <param name="milliseconds">
        /// Número de milisegundos transcurridos desde el Epoch de Unix.
        /// </param>
        /// <returns>
        /// Un <see cref="DateTime"/> construido a partir del Timestamp de
        /// Unix.
        /// </returns>
        public static DateTime FromUnixTimestampMs(in long milliseconds)
        {
            return FromTimestampMs(milliseconds, UnixEpoch);
        }

        /// <summary>
        /// Convierte un <see cref="DateTime"/> a un Timestamp de 64 bits.
        /// </summary>
        /// <param name="dateTime">
        /// Valor de tiempo a convertir.
        /// </param>
        /// <param name="epoch">
        /// Epoch inicial a utilizar.
        /// </param>
        /// <returns>
        /// Un <see cref="long"/> que representa el valor de Timestamp del
        /// <see cref="DateTime"/> especificado.
        /// </returns>
        public static long ToTimestamp(this in DateTime dateTime, in DateTime epoch)
        {
            return (long)(dateTime - epoch).TotalSeconds;
        }

        /// <summary>
        /// Convierte un <see cref="DateTime"/> a un Timestamp de 64 bits.
        /// </summary>
        /// <param name="dateTime">
        /// Valor de tiempo a convertir.
        /// </param>
        /// <param name="epoch">
        /// Epoch inicial a utilizar.
        /// </param>
        /// <returns>
        /// Un <see cref="long"/> que representa el valor de Timestamp del
        /// <see cref="DateTime"/> especificado.
        /// </returns>
        public static long ToTimestampMs(this in DateTime dateTime, in DateTime epoch)
        {
            return (long)(dateTime - epoch).TotalMilliseconds;
        }

        /// <summary>
        /// Obtiene un <see cref="DateTime"/> a partir de un Timestamp.
        /// </summary>
        /// <param name="seconds">
        /// Número de segundos transcurridos desde el Epoch especificado.
        /// </param>
        /// <param name="epoch">
        /// Epoch inicial a utilizar.
        /// </param>
        /// <returns>
        /// Un <see cref="DateTime"/> construido a partir del Timestamp.
        /// </returns>
        public static DateTime FromTimestamp(in long seconds, in DateTime epoch)
        {
            return epoch.AddSeconds(seconds);
        }

        /// <summary>
        /// Obtiene un <see cref="DateTime"/> a partir de un Timestamp en
        /// milisegundos.
        /// </summary>
        /// <param name="milliseconds">
        /// Número de segundos transcurridos desde el Epoch especificado.
        /// </param>
        /// <param name="epoch">
        /// Epoch inicial a utilizar.
        /// </param>
        /// <returns>
        /// Un <see cref="DateTime"/> construido a partir del Timestamp.
        /// </returns>
        public static DateTime FromTimestampMs(in long milliseconds, in DateTime epoch)
        {
            return epoch.AddMilliseconds(milliseconds);
        }

        /// <summary>
        /// Obtiene el nombre del mes especificado.
        /// </summary>
        /// <param name="month">Número de mes del cual obtener el nombre.</param>
        /// <returns>El nombre del mes especificado.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Se produce si <paramref name="month"/> no representa un número
        /// de mes válido.
        /// </exception>
        public static string MonthName(in int month)
        {
            if (!month.IsBetween(1,12)) throw new ArgumentOutOfRangeException(nameof(month));
            var t = DateTime.Now;
            return new DateTime(t.Year,month,t.Day).ToString("MMMM");
        }

        /// <summary>
        /// Obtiene el nombre del mes especificado.
        /// </summary>
        /// <param name="month">Número de mes del cual obtener el nombre.</param>
        /// <param name="culture">Cultura a utilizar para obtener el nombre.</param>
        /// <returns>El nombre del mes especificado.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Se produce si <paramref name="month"/> no representa un número
        /// de mes válido.
        /// </exception>
        public static string MonthName(in int month, in CultureInfo culture)
        {
            if (!month.IsBetween(1, 12)) throw new ArgumentOutOfRangeException(nameof(month));
            var t = DateTime.Now;
            return new DateTime(t.Year, month, t.Day).ToString("MMMM",culture);
        }
    }
}