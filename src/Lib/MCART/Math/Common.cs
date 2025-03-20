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
    /// Comprueba que un valor sea un número real operable válido; en
    /// caso de serlo lo devuelve, caso contrario devolverá un valor
    /// <see cref="float"/> alternativo.
    /// </summary>
    /// <param name="value">Valor a comprobar.</param>
    /// <param name="alternateFunc">
    /// Función que obtiene el valor a devolver si <paramref name="value"/>
    /// no es válido.
    /// </param>
    /// <returns>
    /// <paramref name="value"/> si el mismo es un número real operable
    /// válido, o el resultado de <paramref name="alternateFunc"/> en caso
    /// contrario.
    /// </returns>
    public static float OrIfInvalid(this in float value, Func<float> alternateFunc)
    {
        return value.IsValid() ? value : alternateFunc();
    }

    /// <summary>
    /// Comprueba que un valor sea un número real operable válido; en
    /// caso de serlo lo devuelve, caso contrario devolverá un valor
    /// <see cref="double"/> alternativo.
    /// </summary>
    /// <param name="value">Valor a comprobar.</param>
    /// <param name="alternateFunc">
    /// Función que obtiene el valor a devolver si <paramref name="value"/>
    /// no es válido.
    /// </param>
    /// <returns>
    /// <paramref name="value"/> si el mismo es un número real operable
    /// válido, o el resultado de <paramref name="alternateFunc"/> en caso
    /// contrario.
    /// </returns>
    public static double OrIfInvalid(this in double value, Func<double> alternateFunc)
    {
        return value.IsValid() ? value : alternateFunc();
    }

    /// <summary>
    /// Comprueba que un valor sea un número real operable válido; en
    /// caso de serlo lo devuelve, caso contrario devolverá un valor
    /// <see cref="float"/> alternativo.
    /// </summary>
    /// <param name="value">Valor a comprobar.</param>
    /// <param name="alternateValue">
    /// Valor a devolver si <paramref name="value"/> no es válido.
    /// </param>
    /// <returns>
    /// <paramref name="value"/> si el mismo es un número real operable
    /// válido, <paramref name="alternateValue"/> en caso contrario.
    /// </returns>
    public static float OrIfInvalid(this in float value, in float alternateValue)
    {
        return value.IsValid() ? value : alternateValue;
    }

    /// <summary>
    /// Comprueba que un valor sea un número real operable válido; en
    /// caso de serlo lo devuelve, caso contrario devolverá un valor
    /// <see cref="double"/> alternativo.
    /// </summary>
    /// <param name="value">Valor a comprobar.</param>
    /// <param name="alternateValue">
    /// Valor a devolver si <paramref name="value"/> no es válido.
    /// </param>
    /// <returns>
    /// <paramref name="value"/> si el mismo es un número real operable
    /// válido, <paramref name="alternateValue"/> en caso contrario.
    /// </returns>
    public static double OrIfInvalid(this in double value, in double alternateValue)
    {
        return value.IsValid() ? value : alternateValue;
    }

    /// <summary>
    /// Establece límites de sobreflujo para evaluar una expresión.
    /// </summary>
    /// <typeparam name="T"> Tipo de expresión a limitar. </typeparam>
    /// <param name="expression">Expresión a evaluar.</param>
    /// <param name="max">Límite superior de salida, inclusive.</param>
    /// <param name="min">Límite inferior de salida, inclusive.</param>
    /// <returns>
    /// El valor evaluado que se encuentra dentro del rango especificado.
    /// </returns>
    public static T Clamp<T>(this T expression, in T min, in T max) where T : IComparable<T>
    {
        if (expression.CompareTo(max) > 0) return max;
        if (expression.CompareTo(min) < 0) return min;
        return expression;
    }

    /// <summary>
    /// Establece límites de sobreflujo para evaluar una expresión.
    /// </summary>
    /// <typeparam name="T"> Tipo de expresión a limitar. </typeparam>
    /// <param name="expression">Expresión a evaluar.</param>
    /// <param name="max">Límite superior de salida, inclusive.</param>
    /// <returns>
    /// El valor evaluado que se encuentra entre 0 y
    /// <paramref name="max" />.
    /// </returns>
    public static T Clamp<T>(this T expression, in T max) where T : struct, IComparable<T>
    {
        return Clamp(expression, default, max);
    }

    /// <summary>
    /// Establece límites de sobreflujo para evaluar una expresión.
    /// </summary>
    /// <param name="expression">Expresión a evaluar.</param>
    /// <param name="min">Límite inferior de salida, inclusive.</param>
    /// <param name="max">Límite superior de salida, inclusive.</param>
    /// <returns>
    /// El valor evaluado que se encuentra dentro del rango especificado.
    /// </returns>
    /// <remarks>
    /// Esta implementación se incluye para permitir parámetros de tipo
    /// <see cref="double.NaN" />, <see cref="double.NegativeInfinity" /> y
    /// <see cref="double.PositiveInfinity" />.
    /// </remarks>
    public static double Clamp(this in double expression, in double min, in double max)
    {
        if (double.IsNaN(expression)) return double.NaN;
        if (expression > max) return max;
        if (expression < min) return min;
        return expression;
    }

    /// <summary>
    /// Establece límites de sobreflujo para evaluar una expresión.
    /// </summary>
    /// <param name="expression">Expresión a evaluar.</param>
    /// <param name="max">Límite superior de salida, inclusive.</param>
    /// <returns>
    /// El valor evaluado que se encuentra dentro del rango especificado.
    /// </returns>
    /// <remarks>
    /// Esta implementación se incluye para permitir parámetros de tipo
    /// <see cref="double.NaN" />, <see cref="double.NegativeInfinity" /> y
    /// <see cref="double.PositiveInfinity" />.
    /// </remarks>
    public static double Clamp(this in double expression, in double max)
    {
        return Clamp(expression, double.NegativeInfinity, max);
    }

    /// <summary>
    /// Establece límites de sobreflujo para evaluar una expresión.
    /// </summary>
    /// <param name="expression">Expresión a evaluar.</param>
    /// <param name="max">Límite superior de salida, inclusive.</param>
    /// <returns>
    /// El valor evaluado que se encuentra dentro del rango especificado.
    /// </returns>
    /// <remarks>
    /// Esta implementación se incluye para permitir parámetros de tipo
    /// <see cref="float.NaN" />, <see cref="float.NegativeInfinity" /> y
    /// <see cref="float.PositiveInfinity" />.
    /// </remarks>
    public static float Clamp(this in float expression, in float max)
    {
        return Clamp(expression, float.NegativeInfinity, max);
    }

    /// <summary>
    /// Establece límites de sobreflujo para evaluar una expresión.
    /// </summary>
    /// <param name="expression">Expresión a evaluar.</param>
    /// <param name="min">Límite inferior de salida, inclusive.</param>
    /// <param name="max">Límite superior de salida, inclusive.</param>
    /// <returns>
    /// El valor evaluado que se encuentra dentro del rango especificado.
    /// </returns>
    /// <remarks>
    /// Esta implementación se incluye para permitir parámetros de tipo
    /// <see cref="float.NaN" />, <see cref="float.NegativeInfinity" /> y
    /// <see cref="float.PositiveInfinity" />.
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
    /// Establece puntos de sobreflujo intencional para evaluar una expresión.
    /// </summary>
    /// <param name="expression">Expresión a evaluar.</param>
    /// <param name="max">Límite superior de salida, inclusive.</param>
    /// <param name="min">Límite inferior de salida, inclusive.</param>
    /// <returns>
    /// El valor evaluado que se encuentra dentro del rango especificado.
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
    /// Establece puntos de sobreflujo intencional para evaluar una expresión.
    /// </summary>
    /// <param name="expression">Expresión a evaluar.</param>
    /// <param name="max">Límite superior de salida, inclusive.</param>
    /// <param name="min">Límite inferior de salida, inclusive.</param>
    /// <returns>
    /// El valor evaluado que se encuentra dentro del rango especificado.
    /// </returns>
    public static int Wrap(this in short expression, in short min, in short max)
    {
        if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
        if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
        return expression;
    }

    /// <summary>
    /// Establece puntos de sobreflujo intencional para evaluar una expresión.
    /// </summary>
    /// <param name="expression">Expresión a evaluar.</param>
    /// <param name="max">Límite superior de salida, inclusive.</param>
    /// <param name="min">Límite inferior de salida, inclusive.</param>
    /// <returns>
    /// El valor evaluado que se encuentra dentro del rango especificado.
    /// </returns>
    public static int Wrap(this in char expression, in char min, in char max)
    {
        if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
        if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
        return expression;
    }

    /// <summary>
    /// Establece puntos de sobreflujo intencional para evaluar una expresión.
    /// </summary>
    /// <param name="expression">Expresión a evaluar.</param>
    /// <param name="max">Límite superior de salida, inclusive.</param>
    /// <param name="min">Límite inferior de salida, inclusive.</param>
    /// <returns>
    /// El valor evaluado que se encuentra dentro del rango especificado.
    /// </returns>
    public static int Wrap(this in int expression, in int min, in int max)
    {
        if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
        if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
        return expression;
    }

    /// <summary>
    /// Establece puntos de sobreflujo intencional para evaluar una expresión.
    /// </summary>
    /// <param name="expression">Expresión a evaluar.</param>
    /// <param name="max">Límite superior de salida, inclusive.</param>
    /// <param name="min">Límite inferior de salida, inclusive.</param>
    /// <returns>
    /// El valor evaluado que se encuentra dentro del rango especificado.
    /// </returns>
    public static long Wrap(this in long expression, in long min, in long max)
    {
        if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
        if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
        return expression;
    }

    /// <summary>
    /// Establece puntos de sobreflujo intencional para evaluar una expresión.
    /// </summary>
    /// <param name="expression">Expresión a evaluar.</param>
    /// <param name="max">Límite superior de salida, inclusive.</param>
    /// <param name="min">Límite inferior de salida, inclusive.</param>
    /// <returns>
    /// El valor evaluado que se encuentra dentro del rango especificado.
    /// </returns>
    public static decimal Wrap(this in decimal expression, in decimal min, in decimal max)
    {
        if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
        if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
        return expression;
    }

    /// <summary>
    /// Establece puntos de sobreflujo intencional para evaluar una expresión.
    /// </summary>
    /// <param name="expression">Expresión a evaluar.</param>
    /// <param name="max">Límite superior de salida, inclusive.</param>
    /// <param name="min">Límite inferior de salida, inclusive.</param>
    /// <returns>
    /// El valor evaluado que se encuentra dentro del rango especificado.
    /// </returns>
    public static double Wrap(this in double expression, in double min, in double max)
    {
        if (double.IsNaN(expression)) return double.NaN;
        if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
        if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
        return expression;
    }

    /// <summary>
    /// Establece puntos de sobreflujo intencional para evaluar una expresión.
    /// </summary>
    /// <param name="expression">Expresión a evaluar.</param>
    /// <param name="max">Límite superior de salida, inclusive.</param>
    /// <param name="min">Límite inferior de salida, inclusive.</param>
    /// <returns>
    /// El valor evaluado que se encuentra dentro del rango especificado.
    /// </returns>
    public static float Wrap(this in float expression, in float min, in float max)
    {
        if (float.IsNaN(expression)) return float.NaN;
        if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
        if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
        return expression;
    }

    /// <summary>
    /// Establece puntos de sobreflujo intencional para evaluar una expresión.
    /// </summary>
    /// <param name="expression">Expresión a evaluar.</param>
    /// <param name="max">Límite superior de salida, inclusive.</param>
    /// <param name="min">Límite inferior de salida, inclusive.</param>
    /// <returns>
    /// El valor evaluado que se encuentra dentro del rango especificado.
    /// </returns>
    [CLSCompliant(false)]
    public static int Wrap(this in sbyte expression, in sbyte min, in sbyte max)
    {
        if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
        if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
        return expression;
    }

    /// <summary>
    /// Establece puntos de sobreflujo intencional para evaluar una expresión.
    /// </summary>
    /// <param name="expression">Expresión a evaluar.</param>
    /// <param name="max">Límite superior de salida, inclusive.</param>
    /// <param name="min">Límite inferior de salida, inclusive.</param>
    /// <returns>
    /// El valor evaluado que se encuentra dentro del rango especificado.
    /// </returns>
    [CLSCompliant(false)]
    public static int Wrap(this in ushort expression, in ushort min, in ushort max)
    {
        if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
        if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
        return expression;
    }

    /// <summary>
    /// Establece puntos de sobreflujo intencional para evaluar una expresión.
    /// </summary>
    /// <param name="expression">Expresión a evaluar.</param>
    /// <param name="max">Límite superior de salida, inclusive.</param>
    /// <param name="min">Límite inferior de salida, inclusive.</param>
    /// <returns>
    /// El valor evaluado que se encuentra dentro del rango especificado.
    /// </returns>
    [CLSCompliant(false)]
    public static uint Wrap(this in uint expression, in uint min, in uint max)
    {
        if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
        if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
        return expression;
    }

    /// <summary>
    /// Establece puntos de sobreflujo intencional para evaluar una expresión.
    /// </summary>
    /// <param name="expression">Expresión a evaluar.</param>
    /// <param name="max">Límite superior de salida, inclusive.</param>
    /// <param name="min">Límite inferior de salida, inclusive.</param>
    /// <returns>
    /// El valor evaluado que se encuentra dentro del rango especificado.
    /// </returns>
    [CLSCompliant(false)]
    public static ulong Wrap(this in ulong expression, in ulong min, in ulong max)
    {
        if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
        if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
        return expression;
    }
}
