/*
Series.cs

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

namespace TheXDS.MCART.Math;

/// <summary>
/// Contains functions for enumerating mathematical series.
/// </summary>
public static class Series
{
    /* NOTE:
     * These functions utilize enumerators to expose complete series in an
     * infinite manner.
     *
     * It is important to note that improper use of these functions 
     * can lead to overflow or stack errors, or the program may become
     * unresponsive.
     *
     * All infinite enumeration functions should be used in conjunction
     * with the extension method
     * System.Linq.Enumerable.Take<TSource>(IEnumerable<TSource>, int)
     */

    /// <summary>
    /// Exposes an enumerator that contains the complete Fibonacci 
    /// sequence.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerable{T}" /> with the infinite Fibonacci 
    /// sequence.
    /// </returns>
    /// <remarks>
    /// Series utilize enumerators to expose complete series in an 
    /// infinite manner. It is important to note that improper use of 
    /// these functions can lead to overflow or stack errors, or the 
    /// program may become unresponsive for an extended period.
    /// All infinite enumeration functions should be used in conjunction
    /// with the extension method
    /// <c>System.Linq.Enumerable.Take&lt;TSource&gt;(IEnumerable&lt;TSource&gt;, int)</c>
    /// </remarks>
    /// <seealso cref="Enumerable.Take{TSource}(IEnumerable{TSource}, int)"/>
    public static IEnumerable<long> Fibonacci() => MakeSeriesAdditive(0, 1);

    /// <summary>
    /// Exposes an enumerator that contains the complete Lucas sequence.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerable{T}" /> with the infinite Lucas 
    /// sequence.
    /// </returns>
    /// <remarks>
    /// Series utilize enumerators to expose complete series in an 
    /// infinite manner. It is important to note that improper use of 
    /// these functions can lead to overflow or stack errors, or the 
    /// program may become unresponsive for an extended period.
    /// All infinite enumeration functions should be used in conjunction
    /// with the extension method
    /// <c>System.Linq.Enumerable.Take&lt;TSource&gt;(IEnumerable&lt;TSource&gt;, int)</c>
    /// </remarks>
    /// <seealso cref="Enumerable.Take{TSource}(IEnumerable{TSource}, int)"/>
    public static IEnumerable<long> Lucas() => MakeSeriesAdditive(2, 1);

    /// <summary>
    /// Creates an enumerator that generates an additive numerical series
    /// by providing the two initial elements of the series.
    /// </summary>
    /// <param name="a">First initial element.</param>
    /// <param name="b">Second initial element.</param>
    /// <returns>
    /// An enumerator that will generate an additive numerical series 
    /// for each iteration.
    /// </returns>
    /// <remarks>
    /// Series utilize enumerators to expose complete series in an 
    /// infinite manner. It is important to note that improper use of 
    /// these functions can lead to overflow or stack errors, or the 
    /// program may become unresponsive for an extended period.
    /// All infinite enumeration functions should be used in conjunction
    /// with the extension method
    /// <c>System.Linq.Enumerable.Take&lt;TSource&gt;(IEnumerable&lt;TSource&gt;, int)</c>
    /// </remarks>
    /// <seealso cref="Enumerable.Take{TSource}(IEnumerable{TSource}, int)"/>
    public static IEnumerable<long> MakeSeriesAdditive(long a, long b)
    {
        while (a >= 0)
        {
            yield return a;
            yield return b;
            a += b;
            b += a;
        }
    }
}
