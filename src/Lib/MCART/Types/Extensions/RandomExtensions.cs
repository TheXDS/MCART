/*
RandomExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensions for the <see cref="Random" /> class.
/// </summary>
public static class RandomExtensions
{
    private const string Text = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    /// <summary>
    /// Ensures that functions requiring random numbers don't generate
    /// <see cref="Random" /> objects with the same time-based seed.
    /// </summary>
    public static Random Rnd { get; } = new();

    /// <summary>
    /// Gets a random text string.
    /// </summary>
    /// <param name="r">
    /// Instance of the <see cref="Random" /> object to use.
    /// </param>
    /// <param name="length">Length of the string to generate.</param>
    /// <returns>
    /// A random text string with the specified length.
    /// </returns>
    public static string RndText(this Random r, in int length)
    {
        string? x = string.Empty;
        while (x.Length < length) x += Text[r.Next(0, Text.Length)];
        return x;
    }

    /// <summary>
    /// Returns a random integer within the specified range.
    /// </summary>
    /// <param name="r">
    /// Instance of the <see cref="Random" /> object to use.
    /// </param>
    /// <param name="range">
    /// <see cref="Range{T}" /> of numbers to select from.
    /// </param>
    /// <returns>
    /// A random integer within the specified range.
    /// </returns>
    public static int Next(this Random r, in Range<int> range)
    {
        return r.Next(range.Minimum, range.Maximum);
    }

    /// <summary>
    /// Gets a random value of <see langword="true" /> or
    /// <see langword="false" />.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> or <see langword="false" /> randomly.
    /// </returns>
    /// <param name="r">
    /// Instance of the <see cref="Random" /> object to use.
    /// </param>
    public static bool CoinFlip(this Random r)
    {
        return (r.Next() & 1) == 0;
    }
}
