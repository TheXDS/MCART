/*
TimeSpanExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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
using TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contains useful extensions for the <see cref="TimeSpan"/> structure.
/// </summary>
public static class TimeSpanExtensions
{
    /// <summary>
    /// Converts a <see cref="TimeSpan"/> into a human-readable string.
    /// </summary>
    /// <param name="timeSpan">Value to convert.</param>
    /// <returns>
    /// A user-friendly string indicating the days, hours,
    /// minutes, and seconds components of <paramref name="timeSpan"/>.
    /// </returns>
    public static string Verbose(this in TimeSpan timeSpan)
    {
        HashSet<string>? messages = [];
        if (timeSpan.Days > 0)
            messages.Add(Composition.Days(timeSpan.Days));
        if (timeSpan.Hours > 0)
            messages.Add(Composition.Hours(timeSpan.Hours));
        if (timeSpan.Minutes > 0)
            messages.Add(Composition.Minutes(timeSpan.Minutes));
        if (timeSpan.Seconds > 0)
            messages.Add(Composition.Seconds(timeSpan.Seconds));
        return string.Join(", ", messages);
    }

    /// <summary>
    /// Converts a <see cref="TimeSpan"/> to its time representation.
    /// </summary>
    /// <param name="timeSpan">Value to convert.</param>
    /// <returns>A string representing the time value.</returns>
    public static string AsTime(this in TimeSpan timeSpan)
    {
        return AsTime(timeSpan, CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Converts a <see cref="TimeSpan"/> to its time representation.
    /// </summary>
    /// <param name="timeSpan">Value to convert.</param>
    /// <param name="culture">
    /// The culture to use for formatting the time string.
    /// </param>
    /// <returns>A string representing the time value.</returns>
    public static string AsTime(this in TimeSpan timeSpan, in CultureInfo culture)
    {
        string? f = $"{{0:{culture.DateTimeFormat.ShortTimePattern}}}";
        return string.Format(f, DateTime.MinValue.Add(timeSpan));
    }
}
