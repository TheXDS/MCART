/*
DateTimeExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene numerosas extensiones para el tipo System.DateTime del
CLR, supliéndolo de nueva funcionalidad previamente no existente, o de
invocación compleja.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Globalization;
using TheXDS.MCART.Helpers;

namespace TheXDS.MCART.Types.Extensions;

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
    public static DateTime Epoch(in int year) => new(year, 1, 1, 0, 0, 0, DateTimeKind.Utc);

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
    public static DateTime FromUnixTimestamp(this in long seconds)
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
    public static DateTime FromUnixTimestampMs(this in long milliseconds)
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
        return MonthName(month, CultureInfo.CurrentCulture);
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
        return new DateTime(2001, month, 1).ToString("MMMM", culture);
    }
}
