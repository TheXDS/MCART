/*
Statistics.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene diversas fórmulas de suavizado.

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
using System.Collections.Generic;
using System.Linq;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Math
{
    /// <summary>
    /// Contiene diversas funciones estadísticas.
    /// </summary>
    public static class Statistics
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
            double d = value * delta;
            return value + (RandomExtensions.Rnd.NextDouble() * d * 2) - d;
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
            return from j in data let v = j * errorMargin select new Range<double>(j - v, j + v);
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
            List<double>? c = new();
            double last;

            using IEnumerator<double>? e = data.GetEnumerator();
            if (e.MoveNext()) last = e.Current;
            else return double.NaN;

            while (e.MoveNext())
            {
                c.Add(e.Current - last);
                last = e.Current;
            }

            return c.Any() ? c.Average() : 0.0;
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
            double c = 0.0;
            int t = 0;
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
            List<double>? d = data.ToList();
            if (!d.Any()) return double.NaN;

            d.Sort();

            int p = d.Count / 2;

            return d.Count % 2 == 1 ? d[p] : (d[p - 1] + d[p]) / 2;

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
            Dictionary<double, int>? d = new();
            using IEnumerator<double>? e = data.GetEnumerator();
            while (e.MoveNext())
            {
                if (!d.ContainsKey(e.Current)) d.Add(e.Current, 1);
                else d[e.Current]++;
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
            List<double>? d = data.ToList();
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
            List<double>? d = data.ToList();
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
        public static double AbsoluteDeviation(this IEnumerable<double> data, double point)
        {
            double d = 0.0;
            int c = 0;
            using IEnumerator<double>? e = data.GetEnumerator();
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
            List<double>? da = dataA.ToList();
            List<double>? db = dataB.ToList();
            double avga = da.Average();
            double avgb = db.Average();
            using List<double>.Enumerator ea = da.GetEnumerator();
            using List<double>.Enumerator eb = db.GetEnumerator();
            double sigmaXY = 0.0, sigmaX = 0.0, sigmaY = 0.0;
            while (ea.MoveNext() && eb.MoveNext())
            {
                sigmaXY += (ea.Current - avga) * (eb.Current - avgb);
                sigmaX += System.Math.Pow(ea.Current - avga, 2);
                sigmaY += System.Math.Pow(eb.Current - avgb, 2);
            }
            if (ea.MoveNext() || eb.MoveNext()) throw new IndexOutOfRangeException();
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
            List<double>? da = dataA.ToList();
            List<double>? db = dataB.ToList();
            double avga = da.Average();
            double avgb = db.Average();
            using List<double>.Enumerator ea = da.GetEnumerator();
            using List<double>.Enumerator eb = db.GetEnumerator();

            double sigmaXY = 0.0;
            while (ea.MoveNext() && eb.MoveNext())
            {
                sigmaXY += (ea.Current - avga) * (eb.Current - avgb);
            }

            if (ea.MoveNext() || eb.MoveNext()) throw new IndexOutOfRangeException();

            return sigmaXY / da.Count;
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
            List<double>? d = data.ToList();
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
            List<double>? da = dataA.ToList();
            List<double>? db = dataB.ToList();
            double avga = da.Average();
            double avgb = db.Average();

            using List<double>.Enumerator ea = da.GetEnumerator();
            using List<double>.Enumerator eb = db.GetEnumerator();

            double sigmaXY = 0.0, sigmaX = 0.0;
            while (ea.MoveNext() && eb.MoveNext())
            {
                sigmaXY += (ea.Current - avga) * (eb.Current - avgb);
                sigmaX += System.Math.Pow(ea.Current - avga, 2);
            }

            if (ea.MoveNext() || eb.MoveNext()) throw new IndexOutOfRangeException();

            double b = sigmaXY / sigmaX;
            double a = avgb - b * avga;

            return a + b * valueA;
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
            List<double>? d = data.ToList();
            double avg = d.Average();
            return System.Math.Sqrt(d.Sum(p => System.Math.Pow(p - avg, 2)) / d.Count);
        }
    }
}