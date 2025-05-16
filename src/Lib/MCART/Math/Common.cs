/*
Common.cs

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

namespace TheXDS.MCART.Math;

/// <summary>
/// Contiene métodos de manipulación matemática estándar.
/// </summary>
public static partial class Common
{
    /// <summary>
    /// Checks if a value is a valid, operable floating-point number.
    /// Returns the value if valid; otherwise, returns an alternate value.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <param name="alternateFunc">
    /// Function that returns the value to return if <paramref name="value"/>
    /// is invalid.
    /// </param>
    /// <returns>
    /// <paramref name="value"/> if it's a valid, operable float, or the
    /// result of <paramref name="alternateFunc"/> otherwise.
    /// </returns>
    public static float OrIfInvalid(this in float value, Func<float> alternateFunc)
    {
        return value.IsValid() ? value : alternateFunc();
    }

    /// <summary>
    /// Checks if a value is a valid, operable double-precision number.
    /// Returns the value if valid; otherwise, returns an alternate value.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <param name="alternateFunc">
    /// Function that returns the value to return if <paramref name="value"/>
    /// is invalid.
    /// </param>
    /// <returns>
    /// <paramref name="value"/> if it's a valid, operable double, or the
    /// result of <paramref name="alternateFunc"/> otherwise.
    /// </returns>
    public static double OrIfInvalid(this in double value, Func<double> alternateFunc)
    {
        return value.IsValid() ? value : alternateFunc();
    }

    /// <summary>
    /// Checks if a value is a valid, operable floating-point number.
    /// Returns the value if valid; otherwise, returns an alternate value.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <param name="alternateValue">
    /// Value to return if <paramref name="value"/> is invalid.
    /// </param>
    /// <returns>
    /// <paramref name="value"/> if it's a valid, operable float,
    /// <paramref name="alternateValue"/> otherwise.
    /// </returns>
    public static float OrIfInvalid(this in float value, in float alternateValue)
    {
        return value.IsValid() ? value : alternateValue;
    }

    /// <summary>
    /// Checks if a value is a valid, operable double-precision number.
    /// Returns the value if valid; otherwise, returns an alternate value.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <param name="alternateValue">
    /// Value to return if <paramref name="value"/> is invalid.
    /// </param>
    /// <returns>
    /// <paramref name="value"/> if it's a valid, operable double,
    /// <paramref name="alternateValue"/> otherwise.
    /// </returns>
    public static double OrIfInvalid(this in double value, in double alternateValue)
    {
        return value.IsValid() ? value : alternateValue;
    }

    /// <summary>
    /// Limits the range of an expression to prevent overflow.
    /// </summary>
    /// <typeparam name="T">The type of expression to limit.</typeparam>
    /// <param name="expression">The expression to evaluate.</param>
    /// <param name="min">The lower bound of the output range, inclusive.</param>
    /// <param name="max">The upper bound of the output range, inclusive.</param>
    /// <returns>
    /// The evaluated value that falls within the specified range.
    /// </returns>
    public static T Clamp<T>(this T expression, in T min, in T max) where T : IComparable<T>
    {
        if (expression.CompareTo(max) > 0) return max;
        if (expression.CompareTo(min) < 0) return min;
        return expression;
    }

    /// <summary>
    /// Limits the range of an expression to prevent overflow.
    /// </summary>
    /// <typeparam name="T">The type of expression to limit.</typeparam>
    /// <param name="expression">The expression to evaluate.</param>
    /// <param name="max">The upper bound of the output range, inclusive.</param>
    /// <returns>
    /// The evaluated value between 0 and <paramref name="max" />.
    /// </returns>
    public static T Clamp<T>(this T expression, in T max) where T : struct, IComparable<T>
    {
        return Clamp(expression, default, max);
    }

    /// <summary>
    /// Limits the range of an expression to prevent overflow.
    /// </summary>
    /// <param name="expression">The expression to evaluate.</param>
    /// <param name="min">The lower bound of the output range, inclusive.</param>
    /// <param name="max">The upper bound of the output range, inclusive.</param>
    /// <returns>
    /// The evaluated value within the specified range.
    /// </returns>
    /// <remarks>
    /// This implementation is included to allow parameters of type
    /// <see cref="double.NaN" />, <see cref="double.NegativeInfinity" />,
    /// and <see cref="double.PositiveInfinity" />.
    /// </remarks>
    public static double Clamp(this in double expression, in double min, in double max)
    {
        if (double.IsNaN(expression)) return double.NaN;
        if (expression > max) return max;
        if (expression < min) return min;
        return expression;
    }

    /// <summary>
    /// Limits the range of an expression to prevent overflow.
    /// </summary>
    /// <param name="expression">The expression to evaluate.</param>
    /// <param name="max">The upper bound of the output range, inclusive.</param>
    /// <returns>
    /// The evaluated value within the specified range.
    /// </returns>
    /// <remarks>
    /// This implementation is included to allow parameters of type
    /// <see cref="double.NaN" />, <see cref="double.NegativeInfinity" />,
    /// and <see cref="double.PositiveInfinity" />.
    /// </remarks>
    public static double Clamp(this in double expression, in double max)
    {
        return Clamp(expression, double.NegativeInfinity, max);
    }

    /// <summary>
    /// Limits the range of an expression to prevent overflow.
    /// </summary>
    /// <param name="expression">The expression to evaluate.</param>
    /// <param name="max">The upper bound of the output range, inclusive.</param>
    /// <returns>
    /// The evaluated value within the specified range.
    /// </returns>
    /// <remarks>
    /// This implementation is included to allow parameters of type
    /// <see cref="float.NaN" />, <see cref="float.NegativeInfinity" />,
    /// and <see cref="float.PositiveInfinity" />.
    /// </remarks>
    public static float Clamp(this in float expression, in float max)
    {
        return Clamp(expression, float.NegativeInfinity, max);
    }

    /// <summary>
    /// Limits the range of an expression to prevent overflow.
    /// </summary>
    /// <param name="expression">The expression to evaluate.</param>
    /// <param name="min">The lower bound of the output range, inclusive.</param>
    /// <param name="max">The upper bound of the output range, inclusive.</param>
    /// <returns>
    /// The evaluated value within the specified range.
    /// </returns>
    /// <remarks>
    /// This implementation is included to allow parameters of type
    /// <see cref="float.NaN" />, <see cref="float.NegativeInfinity" />,
    /// and <see cref="float.PositiveInfinity" />.
    /// </remarks>
    public static float Clamp(this in float expression, in float min, in float max)
    {
#if RatherDRY
        return (float) Clamp((double) expression, min, max);
#else
        if (float.IsNaN(expression)) return float.NaN;
        if (expression > max) return max;
        if (expression < min) return min;
        return expression;
#endif
    }

    /// <summary>
    /// Intentionally sets overflow points to evaluate an expression.
    /// </summary>
    /// <param name="expression">Expression to evaluate.</param>
    /// <param name="max">Upper bound of the output range, inclusive.</param>
    /// <param name="min">Lower bound of the output range, inclusive.</param>
    /// <returns>
    /// The evaluated value that is within the specified range.
    /// </returns>
    public static byte Wrap(this in byte expression, in byte min, in byte max)
    {
        unchecked
        {
            if (expression.CompareTo(max) > 0) return (byte)(expression - (1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (byte)(expression + (1 + max - min)).Wrap(min, max);
            return expression;
        }
    }

    /// <summary>
    /// Intentionally sets overflow points to evaluate an expression.
    /// </summary>
    /// <param name="expression">Expression to evaluate.</param>
    /// <param name="max">Upper bound of the output range, inclusive.</param>
    /// <param name="min">Lower bound of the output range, inclusive.</param>
    /// <returns>
    /// The evaluated value that is within the specified range.
    /// </returns>
    public static int Wrap(this in short expression, in short min, in short max)
    {
        if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
        if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
        return expression;
    }

    /// <summary>
    /// Intentionally sets overflow points to evaluate an expression.
    /// </summary>
    /// <param name="expression">Expression to evaluate.</param>
    /// <param name="max">Upper bound of the output range, inclusive.</param>
    /// <param name="min">Lower bound of the output range, inclusive.</param>
    /// <returns>
    /// The evaluated value that is within the specified range.
    /// </returns>
    public static int Wrap(this in char expression, in char min, in char max)
    {
        if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
        if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
        return expression;
    }

    /// <summary>
    /// Intentionally sets overflow points to evaluate an expression.
    /// </summary>
    /// <param name="expression">Expression to evaluate.</param>
    /// <param name="max">Upper bound of the output range, inclusive.</param>
    /// <param name="min">Lower bound of the output range, inclusive.</param>
    /// <returns>
    /// The evaluated value that is within the specified range.
    /// </returns>
    public static int Wrap(this in int expression, in int min, in int max)
    {
        if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
        if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
        return expression;
    }

    /// <summary>
    /// Intentionally sets overflow points to evaluate an expression.
    /// </summary>
    /// <param name="expression">Expression to evaluate.</param>
    /// <param name="max">Upper bound of the output range, inclusive.</param>
    /// <param name="min">Lower bound of the output range, inclusive.</param>
    /// <returns>
    /// The evaluated value that is within the specified range.
    /// </returns>
    public static long Wrap(this in long expression, in long min, in long max)
    {
        if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
        if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
        return expression;
    }

    /// <summary>
    /// Intentionally sets overflow points to evaluate an expression.
    /// </summary>
    /// <param name="expression">Expression to evaluate.</param>
    /// <param name="max">Upper bound of the output range, inclusive.</param>
    /// <param name="min">Lower bound of the output range, inclusive.</param>
    /// <returns>
    /// The evaluated value that is within the specified range.
    /// </returns>
    public static decimal Wrap(this in decimal expression, in decimal min, in decimal max)
    {
        if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
        if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
        return expression;
    }

    /// <summary>
    /// Intentionally sets overflow points to evaluate an expression.
    /// </summary>
    /// <param name="expression">Expression to evaluate.</param>
    /// <param name="max">Upper bound of the output range, inclusive.</param>
    /// <param name="min">Lower bound of the output range, inclusive.</param>
    /// <returns>
    /// The evaluated value that is within the specified range.
    /// </returns>
    public static double Wrap(this in double expression, in double min, in double max)
    {
        if (double.IsNaN(expression)) return double.NaN;
        if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
        if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
        return expression;
    }

    /// <summary>
    /// Intentionally sets overflow points to evaluate an expression.
    /// </summary>
    /// <param name="expression">Expression to evaluate.</param>
    /// <param name="max">Upper bound of the output range, inclusive.</param>
    /// <param name="min">Lower bound of the output range, inclusive.</param>
    /// <returns>
    /// The evaluated value that is within the specified range.
    /// </returns>
    public static float Wrap(this in float expression, in float min, in float max)
    {
        if (float.IsNaN(expression)) return float.NaN;
        if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
        if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
        return expression;
    }

    /// <summary>
    /// Intentionally sets overflow points to evaluate an expression.
    /// </summary>
    /// <param name="expression">Expression to evaluate.</param>
    /// <param name="max">Upper bound of the output range, inclusive.</param>
    /// <param name="min">Lower bound of the output range, inclusive.</param>
    /// <returns>
    /// The evaluated value that is within the specified range.
    /// </returns>
    [CLSCompliant(false)]
    public static int Wrap(this in sbyte expression, in sbyte min, in sbyte max)
    {
        if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
        if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
        return expression;
    }

    /// <summary>
    /// Intentionally sets overflow points to evaluate an expression.
    /// </summary>
    /// <param name="expression">Expression to evaluate.</param>
    /// <param name="max">Upper bound of the output range, inclusive.</param>
    /// <param name="min">Lower bound of the output range, inclusive.</param>
    /// <returns>
    /// The evaluated value that is within the specified range.
    /// </returns>
    [CLSCompliant(false)]
    public static int Wrap(this in ushort expression, in ushort min, in ushort max)
    {
        if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
        if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
        return expression;
    }

    /// <summary>
    /// Intentionally sets overflow points to evaluate an expression.
    /// </summary>
    /// <param name="expression">Expression to evaluate.</param>
    /// <param name="max">Upper bound of the output range, inclusive.</param>
    /// <param name="min">Lower bound of the output range, inclusive.</param>
    /// <returns>
    /// The evaluated value that is within the specified range.
    /// </returns>
    [CLSCompliant(false)]
    public static uint Wrap(this in uint expression, in uint min, in uint max)
    {
        if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
        if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
        return expression;
    }

    /// <summary>
    /// Intentionally sets overflow points to evaluate an expression.
    /// </summary>
    /// <param name="expression">Expression to evaluate.</param>
    /// <param name="max">Upper bound of the output range, inclusive.</param>
    /// <param name="min">Lower bound of the output range, inclusive.</param>
    /// <returns>
    /// The evaluated value that is within the specified range.
    /// </returns>
    [CLSCompliant(false)]
    public static ulong Wrap(this in ulong expression, in ulong min, in ulong max)
    {
        if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
        if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
        return expression;
    }
}
