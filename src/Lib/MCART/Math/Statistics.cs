/*
Statistics.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene diversas fórmulas de suavizado.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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

using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Math;

/// <summary>
/// Contiene diversas funciones estadísticas.
/// </summary>
public static partial class Statistics
{
    /// <summary>
    /// Varía un valor de acuerdo a un porcentaje de su delta.
    /// </summary>
    /// <param name="value">Valor a variar.</param>
    /// <param name="delta">Delta de varianza.</param>
    /// <returns>
    /// Un valor variado aleatoriamente de acuerdo a un delta.
    /// </returns>
    public static double Variate(this in double value, in double delta)
    {
        return value + (RandomExtensions.Rnd.NextDouble() * delta * 2) - delta;
    }

    /// <summary>
    /// Obtiene un rango normalizado para un set de datos dado un margen de
    /// error.
    /// </summary>
    /// <param name="data">Set de datos a normalizar.</param>
    /// <param name="errorMargin">Margen de error tolerado.</param>
    /// <returns>
    /// Una enumeración con los valores normalizados del set de datos de 
    /// acuerdo a un valor de margen de error.
    /// </returns>
    public static IEnumerable<Range<double>> Normalize(this IEnumerable<double> data, double errorMargin)
    {
        return from j in data select new Range<double>(j - errorMargin, j + errorMargin);
    }

    /// <summary>
    /// Obtiene un nuevo valor de promedio a partir de los parámetros
    /// especificados.
    /// </summary>
    /// <param name="value">Nuevo valor a agregar al promedio.</param>
    /// <param name="samples">Número de muestras del promedio anterior.</param>
    /// <param name="oldAverage">Promedio anterior.</param>
    /// <returns>
    /// Un nuevo promedio que incluye a <paramref name="value"/>.
    /// </returns>
    public static double Average(this in double value, in int samples, in double oldAverage)
    {
        Average_Contract(value, samples, oldAverage);
        return oldAverage + ((value - oldAverage) / (samples + 1));
    }

    /// <summary>
    /// Calcula la tendencia media de un set de datos.
    /// </summary>
    /// <param name="data">
    /// Set de datos para el cual calcular la tendencia media.
    /// </param>
    /// <returns>La tendencia media de un set de datos.</returns>
    public static double MeanTendency(this IEnumerable<double> data)
    {
        List<double> c = [];
        double last;
        using IEnumerator<double> e = data.GetEnumerator();
        if (e.MoveNext()) last = e.Current;
        else return double.NaN;
        while (e.MoveNext())
        {
            c.Add(e.Current - last);
            last = e.Current;
        }
        return c.Count != 0 ? c.Average() : 0.0;
    }

    /// <summary>
    /// Calcula la tendencia geométrica de un set de datos.
    /// </summary>
    /// <param name="data">
    /// Set de datos para el cual calcular la tendencia geométrica.
    /// </param>
    /// <returns>La tendencia geométrica de un set de datos.</returns>
    public static double GeometricMean(this IEnumerable<double> data)
    {
        double c = 1.0;
        double t = 0;
        foreach (double j in data)
        {
            c *= j;
            t++;
        }
        return System.Math.Pow(c, 1.0 / t);
    }

    /// <summary>
    /// Calcula la tendencia harmónica de un set de datos.
    /// </summary>
    /// <param name="data">
    /// Set de datos para el cual calcular la tendencia harmónica.
    /// </param>
    /// <returns>La tendencia harmónica de un set de datos.</returns>
    public static double HarmonicMean(this IEnumerable<double> data)
    {
        double c = 0.0;
        int t = 0;
        foreach (double j in data)
        {
            c += 1 / j;
            t++;
        }
        return t / c;
    }

    /// <summary>
    /// Calcula la media de un set de datos.
    /// </summary>
    /// <param name="data">
    /// Set de datos para el cual calcular la media.
    /// </param>
    /// <returns>La media de un set de datos.</returns>
    public static double Median(this IEnumerable<double> data)
    {
        List<double> d = data.ToList();
        if (d.Count == 0) return double.NaN;
        d.Sort();
        int p = d.Count / 2;
        return d.Count % 2 == 1 ? d[p] : (d[p - 2] + d[p - 1]) / 2;
    }

    /// <summary>
    /// Calcula la moda de un set de datos.
    /// </summary>
    /// <param name="data">
    /// Set de datos para el cual calcular la moda.
    /// </param>
    /// <returns>La moda de un set de datos.</returns>
    public static IEnumerable<double> Mode(this IEnumerable<double> data)
    {
        Dictionary<double, int> d = [];
        using IEnumerator<double> e = data.GetEnumerator();
        while (e.MoveNext())
        {
            if (!d.TryGetValue(e.Current, out int value)) d.Add(e.Current, 1);
            else d[e.Current] = ++value;
        }
        int m = d.Max(p => p.Value);
        return d.Where(p => p.Value == m).Select(p => p.Key);
    }

    /// <summary>
    /// Calcula la desviación promedio absoluta de un set de datos.
    /// </summary>
    /// <param name="data">
    /// Set de datos para el cual calcular la desviación promedio absoluta.
    /// </param>
    /// <returns>
    /// La desviación promedio absoluta de un set de datos.
    /// </returns>
    public static double MeanAbsoluteDeviation(this IEnumerable<double> data)
    {
        List<double> d = data.ToList();
        return AbsoluteDeviation(d, d.Average());
    }

    /// <summary>
    /// Calcula la desviación media absoluta de un set de datos.
    /// </summary>
    /// <param name="data">
    /// Set de datos para el cual calcular la desviación media absoluta.
    /// </param>
    /// <returns>La desviación media absoluta de un set de datos.</returns>
    public static double MedianAbsoluteDeviation(this IEnumerable<double> data)
    {
        List<double> d = data.ToList();
        return AbsoluteDeviation(d, d.Median());
    }

    /// <summary>
    /// Calcula la desviación absoluta de un punto en un set de datos.
    /// </summary>
    /// <param name="data">
    /// Set de datos para el cual calcular la absoluta.
    /// </param>
    /// <param name="point">
    /// Punto para el cual calcular la desviación absoluta.
    /// </param>
    /// <returns>
    /// La desviación absoluta del punto en un set de datos.
    /// </returns>
    public static double AbsoluteDeviation(this IEnumerable<double> data, in double point)
    {
        double d = 0.0;
        int c = 0;
        using IEnumerator<double> e = data.GetEnumerator();
        while (e.MoveNext())
        {
            c++;
            d += System.Math.Abs(e.Current - point);
        }
        return d / c;
    }

    /// <summary>
    /// Calcula el nivel de correlación entre dos sets de datos.
    /// </summary>
    /// <param name="dataA">Primer set de datos.</param>
    /// <param name="dataB">Segundo set de datos.</param>
    /// <returns>
    /// <c>1.0</c> para correlación máxima, <c>0.0</c> para correlación
    /// mínima.
    /// </returns>
    /// <exception cref="IndexOutOfRangeException">
    /// Se produce si cualquiera de los sets tiene una cantidad distinta de
    /// elementos con respecto al otro.
    /// </exception>
    public static double Correlation(IEnumerable<double> dataA, IEnumerable<double> dataB)
    {
        double sigmaXY = 0.0, sigmaX = 0.0, sigmaY = 0.0;
        DataSetComputation(dataA, dataB, (in double a, in double b, in double avgA, in double avgB) => {
            sigmaXY += (a - avgA) * (b - avgB);
            sigmaX += System.Math.Pow(a - avgA, 2);
            sigmaY += System.Math.Pow(b - avgB, 2);
        });
        return sigmaXY / System.Math.Sqrt(sigmaX * sigmaY);
    }

    /// <summary>
    /// Calcula el nivel de covarianza entre dos sets de datos.
    /// </summary>
    /// <param name="dataA">Primer set de datos.</param>
    /// <param name="dataB">Segundo set de datos.</param>
    /// <returns>
    /// <c>1.0</c> para covarianza máxima, <c>0.0</c> para covarianza
    /// mínima.
    /// </returns>
    /// <exception cref="IndexOutOfRangeException">
    /// Se produce si cualquiera de los sets tiene una cantidad distinta de
    /// elementos con respecto al otro.
    /// </exception>
    public static double Covariance(IEnumerable<double> dataA, IEnumerable<double> dataB)
    {
        double sigmaXY = 0.0;        
        (_, _, int countA) = DataSetComputation(dataA, dataB, (in double a, in double b, in double avgA, in double avgB) => sigmaXY += (a - avgA) * (b - avgB));
        return sigmaXY / countA;
    }

    /// <summary>
    /// Calcula la desviación cuadrada de un set de datos.
    /// </summary>
    /// <param name="data">
    /// Set de datos para el cual calcular la desviación cuadrada.
    /// </param>
    /// <returns>La desviación cuadrada de un set de datos.</returns>
    public static double DeviationSquared(this IEnumerable<double> data)
    {
        List<double> d = data.ToList();
        double avg = d.Average();
        return d.Sum(p => System.Math.Pow(p - avg, 2));
    }

    /// <summary>
    /// Aplica la transformación de Fisher a un valor discreto.
    /// </summary>
    /// <param name="value">
    /// Valor al cual aplicar la transformación de Fisher.
    /// </param>
    /// <returns>
    /// El resultado de la transformación de Fisher del valor especificado.
    /// </returns>
    public static double Fisher(this in double value)
    {
        if (!value.IsBetween(-1, 1)) throw new ArgumentOutOfRangeException(nameof(value));
        return System.Math.Log((1 + value) / (1 - value)) / 2;
    }

    /// <summary>
    /// Dado el valor <paramref name="valueA"/> en el set
    /// <paramref name="dataA"/>, predice el valor resultante con el set 
    /// <paramref name="dataB"/>.
    /// </summary>
    /// <param name="valueA">
    /// Valor de salida del set <paramref name="dataA"/>.
    /// </param>
    /// <param name="dataA">
    /// Set de datos que produjo a <paramref name="valueA"/>.
    /// </param>
    /// <param name="dataB">
    /// Set de datos con el cual realizar la predicción.
    /// </param>
    /// <returns>
    /// La predicción del valor de salida dado el set
    /// <paramref name="dataB"/>.
    /// </returns>
    public static double Forecast(in double valueA, IEnumerable<double> dataA, IEnumerable<double> dataB)
    {
        double sigmaXY = 0.0, sigmaX = 0.0;
        (double averageA, double averageB, _) = DataSetComputation(dataA, dataB, (in double a, in double b, in double avgA, in double avgB) => 
        {
            sigmaXY += (a - avgA) * (b - avgB);
            sigmaX += System.Math.Pow(a - avgA, 2);
        });
        double b = sigmaXY / sigmaX;
        double a = averageB - (b * averageA);
        return a + (b * valueA);
    }

    /// <summary>
    /// Obtiene la desviación estándar de un set de datos.
    /// </summary>
    /// <param name="data">
    /// Set de datos para le cual calcular la desviación estándar.
    /// </param>
    /// <returns>La desviación estándar de un set de datos.</returns>
    public static double StandardDeviation(this IEnumerable<double> data)
    {
        List<double> d = data.ToList();
        double avg = d.Average();
        return System.Math.Sqrt(d.Sum(p => System.Math.Pow(p - avg, 2)) / d.Count);
    }

    private delegate void DataSetAction(in double currentA, in double currentB, in double avgA, in double avgB);

    private static (double averageA, double averageB, int count) DataSetComputation(IEnumerable<double> dataA, IEnumerable<double> dataB, DataSetAction action)
    {
        List<double> dA = dataA.ToList();
        List<double> dB = dataB.ToList();
        double avgA = dA.Average();
        double avgB = dB.Average();
        using List<double>.Enumerator eA = dA.GetEnumerator();
        using List<double>.Enumerator eB = dB.GetEnumerator();
        while (eA.MoveNext() && eB.MoveNext())
        {
            action(eA.Current, eB.Current, avgA, avgB);
        }
        if (eA.MoveNext() || eB.MoveNext()) throw new IndexOutOfRangeException();
        return (avgA, avgB, dA.Count);
    }
}
