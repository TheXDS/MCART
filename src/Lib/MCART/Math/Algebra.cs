/*
Algebra.cs

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

using System.Collections.Concurrent;
using System.Globalization;
using System.IO.Compression;
using System.Reflection;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;

namespace TheXDS.MCART.Math;

/// <summary>
/// Contains algebraic and general-purpose mathematical functions.
/// </summary>
public static class Algebra
{
    private static int[]? _primes;

    private static int[] KnownPrimes
    {
        get
        {
            return _primes ??= [.. ReadKnownPrimes()];
        }
    }

    /// <summary>
    /// Checks if a number is prime using trial division.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the number is prime,
    /// <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="number">Number to check.</param>
    public static bool IsPrime(this in long number)
    {
        if (number == 1) return false;
        if (number < KnownPrimes[^1] && KnownPrimes.Contains((int)number))
            return true;
        foreach (int prime in KnownPrimes)
        {
            if (number % prime == 0) return false;
        }
        long l = number / 2;
        for (long k = KnownPrimes[^1] + 2; k <= l; k += 2)
        {
            if (number % k == 0) return false;
        }
        return true;
    }

    /// <summary>
    /// Checks if a number is prime using trial division, executing the
    /// operation on all processors in the system.
    /// </summary>
    /// <param name="number">Number to check.</param>
    /// <returns>
    /// <see langword="true" /> if the number is prime,
    /// <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsPrimeMp(this long number)
    {
        if (number == 1) return false;
        if (number < KnownPrimes[^1] && KnownPrimes.Contains((int)number)) return true;
        OrderablePartitioner<int>? part = Partitioner.Create(KnownPrimes);
        bool prime = true;
        void TestIfPrime(int j, ParallelLoopState loop)
        {
            if (number % j != 0) return;
            loop.Break();
            prime = false;
        }
        void TestIfPrime2(int j, ParallelLoopState loop)
        {
            if (number % ((j * 2) + 1) != 0) return;
            loop.Break();
            prime = false;
        }
        Parallel.ForEach(part, TestIfPrime);
        if (!prime) return prime;
        int l = (int)System.Math.Sqrt(number);
        Parallel.For(KnownPrimes[^1] / 2, l, TestIfPrime2);
        return prime;
    }

    /// <summary>
    /// Checks if a number is prime.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the number is prime,
    /// <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="number">Number to check.</param>
    public static bool IsPrime(this in int number)
    {
        return ((long)number).IsPrime();
    }

    /// <summary>
    /// Checks if a number is prime.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the number is prime,
    /// <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="number">Number to check.</param>
    public static bool IsPrime(this in short number)
    {
        return ((long)number).IsPrime();
    }

    /// <summary>
    /// Checks if a number is prime.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the number is prime,
    /// <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="number">Number to check.</param>
    public static bool IsPrime(this in byte number)
    {
        return ((long)number).IsPrime();
    }

    /// <summary>
    /// Checks if a number is prime using trial division, executing the
    /// operation on all processors in the system.
    /// </summary>
    /// <param name="number">Number to check.</param>
    /// <returns>
    /// <see langword="true" /> if the number is prime,
    /// <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsPrimeMp(this in int number)
    {
        return ((long)number).IsPrimeMp();
    }

    /// <summary>
    /// Checks if a number is prime using trial division, executing the
    /// operation on all processors in the system.
    /// </summary>
    /// <param name="number">Number to check.</param>
    /// <returns>
    /// <see langword="true" /> if the number is prime,
    /// <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsPrimeMp(this in short number)
    {
        return ((long)number).IsPrimeMp();
    }

    /// <summary>
    /// Checks if a number is prime using trial division, executing the
    /// operation on all processors in the system.
    /// </summary>
    /// <param name="number">Number to check.</param>
    /// <returns>
    /// <see langword="true" /> if the number is prime,
    /// <see langword="false" /> otherwise.
    /// </returns>
    [CLSCompliant(false)]
    public static bool IsPrimeMp(this in sbyte number)
    {
        return ((long)number).IsPrimeMp();
    }

    /// <summary>
    /// Checks if a number is prime using trial division, executing the
    /// operation on all processors in the system.
    /// </summary>
    /// <param name="number">Number to check.</param>
    /// <returns>
    /// <see langword="true" /> if the number is prime,
    /// <see langword="false" /> otherwise.
    /// </returns>
    [CLSCompliant(false)]
    public static bool IsPrimeMp(this in ushort number)
    {
        return ((long)number).IsPrimeMp();
    }

    /// <summary>
    /// Checks if a number is prime using trial division, executing the
    /// operation on all processors in the system.
    /// </summary>
    /// <param name="number">Number to check.</param>
    /// <returns>
    /// <see langword="true" /> if the number is prime,
    /// <see langword="false" /> otherwise.
    /// </returns>
    [CLSCompliant(false)]
    public static bool IsPrimeMp(this in uint number)
    {
        return ((long)number).IsPrimeMp();
    }

    /// <summary>
    /// Checks if a number is prime using trial division, executing the
    /// operation on all processors in the system.
    /// </summary>
    /// <param name="number">Number to check.</param>
    /// <returns>
    /// <see langword="true"/>if the number is prime, <see langword="false"/> otherwise.
    /// </returns>
    public static bool IsPrimeMp(this in byte number)
    {
        return ((long)number).IsPrimeMp();
    }

    /// <summary>
    /// Checks if a number is prime.
    /// </summary>
    /// <returns>
    /// <see langword="true"/>if the number is prime, <see langword="false"/> otherwise.
    /// </returns>
    /// <param name="number">Number to check.</param>
    [CLSCompliant(false)]
    public static bool IsPrime(this in uint number) => ((long)number).IsPrime();

    /// <summary>
    /// Checks if a number is prime.
    /// </summary>
    /// <returns>
    /// <see langword="true"/>if the number is prime, <see langword="false"/> otherwise.
    /// </returns>
    /// <param name="number">Number to check.</param>
    [CLSCompliant(false)]
    public static bool IsPrime(this in ushort number) => ((long)number).IsPrime();

    /// <summary>
    /// Checks if a number is prime.
    /// </summary>
    /// <returns>
    /// <see langword="true"/>if the number is prime, <see langword="false"/> otherwise.
    /// </returns>
    /// <param name="number">Number to check.</param>
    [CLSCompliant(false)]
    public static bool IsPrime(this in sbyte number) => ((long)number).IsPrime();

    /// <summary>
    /// Calculates the nearest power of two greater than or equal to the value.
    /// </summary>
    /// <param name="value">Input value.  A power of two greater than or equal to this value will be searched for.</param>
    /// <returns>A <see cref="long"/> value that is the result of the operation.</returns>
    public static long Nearest2Pow(in int value)
    {
        long c = 1;
        while (!(c >= value)) c <<= 1;
        return c;
    }

    /// <summary>
    /// Determines if the value is a power of 2.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <returns>
    /// <see langword="true"/> if the value is a power of 2,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool IsTwoPow(in byte value) => value.BitCount() == 1;

    /// <summary>
    /// Determines if the value is a power of 2.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <returns>
    /// <see langword="true"/> if the value is a power of 2,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool IsTwoPow(in short value) => value.BitCount() == 1;

    /// <summary>
    /// Determines if the value is a power of 2.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <returns>
    /// <see langword="true"/> if the value is a power of 2,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool IsTwoPow(in int value) => value.BitCount() == 1;

    /// <summary>
    /// Determines if the value is a power of 2.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <returns>
    /// <see langword="true"/> if the value is a power of 2,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool IsTwoPow(in long value) => value.BitCount() == 1;

    /// <summary>
    /// Determines if the value is a power of 2.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <returns>
    /// <see langword="true"/> if the value is a power of 2,
    /// <see langword="false"/> otherwise.
    /// </returns>
    [CLSCompliant(false)]
    public static bool IsTwoPow(in sbyte value) => value.BitCount() == 1;

    /// <summary>
    /// Determines if the value is a power of 2.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <returns>
    /// <see langword="true"/> if the value is a power of 2,
    /// <see langword="false"/> otherwise.
    /// </returns>
    [CLSCompliant(false)]
    public static bool IsTwoPow(in ushort value) => value.BitCount() == 1;

    /// <summary>
    /// Determines if the value is a power of 2.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <returns>
    /// <see langword="true"/> if the value is a power of 2,
    /// <see langword="false"/> otherwise.
    /// </returns>
    [CLSCompliant(false)]
    public static bool IsTwoPow(in uint value) => value.BitCount() == 1;

    /// <summary>
    /// Determines if the value is a power of 2.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <returns>
    /// <see langword="true"/> if the value is a power of 2,
    /// <see langword="false"/> otherwise.
    /// </returns>
    [CLSCompliant(false)]
    public static bool IsTwoPow(in ulong value) => value.BitCount() == 1;

    /// <summary>
    /// Returns the first power of <paramref name="powerBase"/> that is greater than <paramref name="value"/>.
    /// </summary>
    /// <param name="value">Target number.</param>
    /// <param name="powerBase">Power base.</param>
    /// <returns>
    /// A <see cref="double"/> that is the first power of <paramref name="powerBase"/> that is greater than
    /// <paramref name="value"/>.
    /// </returns>
    public static double NearestPowerUp(in double value, in double powerBase)
    {
        double a = 1;
        if (!ArePositive(value, powerBase)) return a;
        while (!(a > value))
            a *= powerBase;
        return a;
    }

    /// <summary>
    /// Returns <see langword="true"/> if all numbers are positive.
    /// </summary>
    /// <param name="values">Numbers to check.</param>
    /// <returns>
    /// <see langword="true"/> if all numbers in the collection are positive,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool ArePositive<T>(params T[] values) where T : struct, IComparable<T>
    {
        return ArePositive(values.AsEnumerable());
    }

    /// <summary>
    /// Returns <see langword="true"/> if all numbers are negative.
    /// </summary>
    /// <param name="values">Numbers to check.</param>
    /// <returns>
    /// <see langword="true"/> if all numbers in the collection are negative,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool AreNegative<T>(params T[] values) where T : struct, IComparable<T>
    {
        return AreNegative(values.AsEnumerable());
    }

    /// <summary>
    /// Returns <see langword="true"/> if all numbers are equal to zero.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements to check.
    /// </typeparam>
    /// <param name="values">Numbers to check.</param>
    /// <returns>
    /// <see langword="true"/> if all numbers in the collection are equal to
    /// zero, <see langword="false"/> otherwise.
    /// </returns>
    public static bool AreZero<T>(params T[] values) where T : struct, IComparable<T>
    {
        return AreZero(values.AsEnumerable());
    }

    /// <summary>
    /// Returns <see langword="true"/> if all numbers are negative.
    /// </summary>
    /// <param name="values">Numbers to check.</param>
    /// <returns>
    /// <see langword="true"/> if all numbers in the collection are negative,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool AreNegative<T>(this IEnumerable<T> values) where T : struct, IComparable<T>
    {
        return values.All(j => j.CompareTo(default) < 0);
    }

    /// <summary>
    /// Returns <see langword="true"/> if all numbers are equal to zero.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements to check.
    /// </typeparam>
    /// <param name="values">Numbers to check.</param>
    /// <returns>
    /// <see langword="true"/> if all numbers in the collection are equal to
    /// zero, <see langword="false"/> otherwise.
    /// </returns>
    public static bool AreZero<T>(this IEnumerable<T> values) where T : struct, IComparable<T>
    {
        return values.All(j => j.CompareTo(default) == 0);
    }

    /// <summary>
    /// Returns <see langword="true"/> if all numbers are positive.
    /// </summary>
    /// <param name="values">Numbers to check.</param>
    /// <returns>
    /// <see langword="true"/> if all numbers in the collection are positive,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool ArePositive<T>(this IEnumerable<T> values) where T : struct, IComparable<T>
    {
        return values.All(j => j.CompareTo(default) > 0);
    }

    /// <summary>
    /// Determines if a <see cref="double"/> is a whole number.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <returns><see langword="true"/> if the value is an integer; otherwise, <see langword="false"/></returns>
    public static bool IsWhole(this in double value)
    {
        return !value.ToString(CultureInfo.InvariantCulture).Contains('.');
    }

    /// <summary>
    /// Determines if a <see cref="double"/> is a valid operable real number.
    /// </summary>
    /// <param name="value"><see cref="double"/> to check.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is a valid real
    /// <see cref="double"/>, in other words, if it is not equal to
    /// <see cref="double.NaN"/>, <see cref="double.PositiveInfinity"/> or
    /// <see cref="double.NegativeInfinity"/>; otherwise, returns
    /// <see langword="false"/>.
    /// </returns>
    public static bool IsValid(this in double value)
    {
        return !(double.IsNaN(value) || double.IsInfinity(value));
    }

    /// <summary>
    /// Determines if a <see cref="float"/> is a valid operable real number.
    /// </summary>
    /// <param name="value"><see cref="float"/> to check.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is a valid real
    /// <see cref="float"/>, in other words, if it is not equal to
    /// <see cref="float.NaN"/>, <see cref="float.PositiveInfinity"/> or
    /// <see cref="float.NegativeInfinity"/>; otherwise, returns
    /// <see langword="false"/>.
    /// </returns>
    public static bool IsValid(this in float value)
    {
        return !(float.IsNaN(value) || float.IsInfinity(value));
    }

    /// <summary>
    /// Determines if a collection of <see cref="double"/> are valid operable real numbers.
    /// </summary>
    /// <param name="values">
    /// Collection of <see cref="double"/> to check.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if all elements of <paramref name="values"/> are
    /// operable numbers, in other words, if they are not NaN or Infinity; otherwise, returns <see langword="false"/>.
    /// </returns>
    public static bool AreValid(params double[] values)
    {
        return AreValid(values.AsEnumerable());
    }

    /// <summary>
    /// Determines if a collection of <see cref="float"/> are valid operable real numbers.
    /// </summary>
    /// <param name="values">
    /// Collection of <see cref="float"/> to check.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if all elements of <paramref name="values"/> are
    /// operable numbers, in other words, if they are not NaN or Infinity; otherwise, returns <see langword="false"/>.
    /// </returns>
    public static bool AreValid(params float[] values)
    {
        return AreValid(values.AsEnumerable());
    }

    /// <summary>
    /// Determines if a collection of <see cref="float"/> are valid operable real numbers.
    /// </summary>
    /// <param name="values">
    /// Collection of <see cref="float"/> to check.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if all elements of <paramref name="values"/> are
    /// operable numbers, in other words, if they are not NaN or Infinity; otherwise, returns <see langword="false"/>.
    /// </returns>
    public static bool AreValid(this IEnumerable<float> values)
    {
        return values.All(p => IsValid(p));
    }

    /// <summary>
    /// Determines if a collection of <see cref="double"/> are valid operable real numbers.
    /// </summary>
    /// <param name="values">
    /// Collection of <see cref="double"/> to check.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if all elements of <paramref name="values"/> are
    /// operable numbers, in other words, if they are not NaN or Infinity; otherwise, returns <see langword="false"/>.
    /// </returns>
    public static bool AreValid(this IEnumerable<double> values)
    {
        return values.All(p => IsValid(p));
    }

    /// <summary>
    /// Returns <see langword="true"/> if all numbers are not equal to zero.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements to check.
    /// </typeparam>
    /// <param name="x">Numbers to check.</param>
    /// <returns>
    /// <see langword="true"/> if all numbers in the collection are not equal to
    /// zero, <see langword="false"/> otherwise.
    /// </returns>
    public static bool AreNotZero<T>(params T[] x) where T : struct, IComparable<T>
    {
        return AreNotZero(x.AsEnumerable());
    }

    /// <summary>
    /// Returns <see langword="true"/> if all numbers are not equal to zero.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements to check.
    /// </typeparam>
    /// <param name="x">Numbers to check.</param>
    /// <returns>
    /// <see langword="true"/> if all numbers in the collection are not equal to
    /// zero, <see langword="false"/> otherwise.
    /// </returns>
    public static bool AreNotZero<T>(this IEnumerable<T> x) where T : struct, IComparable<T>
    {
        return x.All(j => j.CompareTo(default) != 0);
    }

    private static IEnumerable<int> ReadKnownPrimes()
    {
        using var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"TheXDS.MCART.Resources.Data.primes.deflate") ?? throw new TamperException();
        using var deflate = new DeflateStream(resourceStream, CompressionMode.Decompress);
        using BinaryReader? b = new(deflate);
        int c = b.ReadInt32();
        while (c-- > 0)
        {
            yield return b.ReadInt32();
        }
    }
}
