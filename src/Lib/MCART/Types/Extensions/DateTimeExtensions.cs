/*
DateTimeExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene numerosas extensiones para el tipo System.DateTime del
CLR, supliéndolo de nueva funcionalidad previamente no existente, o de
invocación compleja.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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

using System.Globalization;
using TheXDS.MCART.Helpers;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contains useful extensions of the <see cref="DateTime"/> structure.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Gets a <see cref="DateTime"/> that represents the beginning of
    /// the Unix time.
    /// </summary>
    public static readonly DateTime UnixEpoch = Epoch(1970);

    /// <summary>
    /// Gets a <see cref="DateTime"/> that represents the beginning of
    /// the 20th century time.
    /// </summary>
    /// <remarks>
    /// The Century Epoch is defined as the starting point of time
    /// for many 32-bit devices that do not follow the Unix standard,
    /// and is set as the first of January of the year 1900.
    /// The standard was defined by means of GMT time, in its absence
    /// UTC is used as a sufficiently close substitute.
    /// </remarks>
    public static readonly DateTime CenturyEpoch = Epoch(1900);

    /// <summary>
    /// Gets a <see cref="DateTime"/> that represents the beginning of
    /// the 21st century time.
    /// </summary>
    /// <remarks>
    /// The Y2K Epoch is defined as the starting point of time for
    /// 32-bit devices that do not follow the Unix standard,
    /// and is set as the first of January of the year 2000.
    /// </remarks>
    public static readonly DateTime Y2KEpoch = Epoch(2000);

    /// <summary>
    /// Creates a <see cref="DateTime"/> that represents the beginning of
    /// time for a specific year.
    /// </summary>
    public static DateTime Epoch(in int year) => new(year, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// Converts a <see cref="DateTime"/> to a 64-bit Unix Timestamp.
    /// </summary>
    /// <param name="dateTime">
    /// Time value to convert.
    /// </param>
    /// <returns>
    /// A <see cref="long"/> that represents the value of the Unix
    /// Timestamp of the specified <see cref="DateTime"/>.
    /// </returns>
    public static long ToUnixTimestamp(this in DateTime dateTime)
    {
        return ToTimestamp(dateTime, UnixEpoch);
    }

    /// <summary>
    /// Converts a <see cref="DateTime"/> to a 64-bit Unix Timestamp in
    /// milliseconds.
    /// </summary>
    /// <param name="dateTime">
    /// Time value to convert.
    /// </param>
    /// <returns>
    /// A <see cref="long"/> that represents the value of the Unix
    /// Timestamp in milliseconds of the specified <see cref="DateTime"/>.
    /// </returns>
    public static long ToUnixTimestampMs(this in DateTime dateTime)
    {
        return ToTimestampMs(dateTime, UnixEpoch);
    }

    /// <summary>
    /// Gets a <see cref="DateTime"/> from a Unix Timestamp.
    /// </summary>
    /// <param name="seconds">
    /// Number of seconds since the Unix Epoch.
    /// </param>
    /// <returns>
    /// A <see cref="DateTime"/> constructed from the Unix Timestamp.
    /// </returns>
    public static DateTime FromUnixTimestamp(this in long seconds)
    {
        return FromTimestamp(seconds, UnixEpoch);
    }

    /// <summary>
    /// Gets a <see cref="DateTime"/> from a Unix Timestamp.
    /// </summary>
    /// <param name="milliseconds">
    /// Number of milliseconds since the Unix Epoch.
    /// </param>
    /// <returns>
    /// A <see cref="DateTime"/> constructed from the Unix Timestamp.
    /// </returns>
    public static DateTime FromUnixTimestampMs(this in long milliseconds)
    {
        return FromTimestampMs(milliseconds, UnixEpoch);
    }

    /// <summary>
    /// Converts a <see cref="DateTime"/> to a 64-bit Timestamp.
    /// </summary>
    /// <param name="dateTime">
    /// Time value to convert.
    /// </param>
    /// <param name="epoch">
    /// Initial epoch to use.
    /// </param>
    /// <returns>
    /// A <see cref="long"/> that represents the value of the Timestamp of
    /// the specified <see cref="DateTime"/>.
    /// </returns>
    public static long ToTimestamp(this in DateTime dateTime, in DateTime epoch)
    {
        return (long)(dateTime - epoch).TotalSeconds;
    }

    /// <summary>
    /// Converts a <see cref="DateTime"/> to a 64-bit Timestamp.
    /// </summary>
    /// <param name="dateTime">
    /// Time value to convert.
    /// </param>
    /// <param name="epoch">
    /// Initial epoch to use.
    /// </param>
    /// <returns>
    /// A <see cref="long"/> that represents the value of the Timestamp of
    /// the specified <see cref="DateTime"/>.
    /// </returns>
    public static long ToTimestampMs(this in DateTime dateTime, in DateTime epoch)
    {
        return (long)(dateTime - epoch).TotalMilliseconds;
    }

    /// <summary>
    /// Gets a <see cref="DateTime"/> from a Timestamp.
    /// </summary>
    /// <param name="seconds">
    /// Number of seconds since the specified Epoch.
    /// </param>
    /// <param name="epoch">
    /// Initial epoch to use.
    /// </param>
    /// <returns>
    /// A <see cref="DateTime"/> constructed from the Timestamp.
    /// </returns>
    public static DateTime FromTimestamp(in long seconds, in DateTime epoch)
    {
        return epoch.AddSeconds(seconds);
    }

    /// <summary>
    /// Gets a <see cref="DateTime"/> from a Timestamp in milliseconds.
    /// </summary>
    /// <param name="milliseconds">
    /// Number of seconds since the specified Epoch.
    /// </param>
    /// <param name="epoch">
    /// Initial epoch to use.
    /// </param>
    /// <returns>
    /// A <see cref="DateTime"/> constructed from the Timestamp.
    /// </returns>
    public static DateTime FromTimestampMs(in long milliseconds, in DateTime epoch)
    {
        return epoch.AddMilliseconds(milliseconds);
    }

    /// <summary>
    /// Gets the name of the specified month.
    /// </summary>
    /// <param name="month">Month number from which to get the name.</param>
    /// <returns>The name of the month specified.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// This is thrown if <paramref name="month"/> does not represent a
    /// valid month number.
    /// </exception>
    public static string MonthName(in int month)
    {
        return MonthName(month, CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Gets the name of the specified month.
    /// </summary>
    /// <param name="month">Month number from which to get the name.</param>
    /// <param name="culture">Culture to use to get the name.</param>
    /// <returns>The name of the month specified.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// This is thrown if <paramref name="month"/> does not represent a
    /// valid month number.
    /// </exception>
    public static string MonthName(in int month, in CultureInfo culture)
    {
        if (!month.IsBetween(1, 12)) throw new ArgumentOutOfRangeException(nameof(month));
        return new DateTime(2001, month, 1).ToString("MMMM", culture);
    }
}
