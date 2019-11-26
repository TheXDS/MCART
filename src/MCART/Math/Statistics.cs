/*
Statistics.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene diversas fórmulas de suavizado.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Math
{
    /// <summary>
    ///     Contiene diversas funciones estadísticas.
    /// </summary>
    public static class Statistics
    {
        /// <summary>
        ///     Varía un valor de acuerdo a un porcentaje de su delta.
        /// </summary>
        /// <param name="value">Valor a variar.</param>
        /// <param name="delta">Delta de varianza.</param>
        /// <returns>
        ///     Un valor variado aleatoriamente de acuerdo a un delta.
        /// </returns>
        public static double Variate(this in double value, in double delta)
        {
            var d = value * delta;
            return value + (RandomExtensions.Rnd.NextDouble() * d * 2) - d;
        }

        public static IEnumerable<Range<double>> Normalize(this IEnumerable<double> data, double errorMargin)
        {
            foreach (var j in data)
            {
                var v = j * errorMargin;
                yield return new Range<double>(j - v, j + v);
            }
        }

        public static double MeanTendency(this IEnumerable<double> data)
        {
            var c = new List<double>();
            double last;

            using var e = data.GetEnumerator();
            if (e.MoveNext()) last = e.Current;
            else return double.NaN;

            while (e.MoveNext())
            {
                c.Add(e.Current - last);
                last = e.Current;
            }

            return c.Any() ? c.Average() : 0.0;
        }

        public static double GeometricMean(this IEnumerable<double> data)
        {
            var c = 0.0;
            var t = 0;
            foreach (var j in data)
            {
                c *= j;
                t++;
            }
            return System.Math.Pow(c, 1.0 / t);
        }

        public static double HarmonicMean(this IEnumerable<double> data)
        {
            var c = 0.0;
            var t = 0;
            foreach (var j in data)
            {
                c += 1 / j;
                t++;
            }
            return t / c;
        }

        public static double Median(this IEnumerable<double> data)
        {
            var d = data.ToList();
            if (!d.Any()) return double.NaN;
            
            d.Sort();

            int p = d.Count / 2;

            return d.Count % 2 == 1 ? d[p] : (d[p - 1] + d[p]) / 2;

        }

        public static IEnumerable<double> Mode(this IEnumerable<double> data)
        {
            var d = new Dictionary<double, int>();
            using var e = data.GetEnumerator();
            while (e.MoveNext())
            {
                if (!d.ContainsKey(e.Current)) d.Add(e.Current, 1);
                else d[e.Current]++;
            }
            var m = d.Max(p => p.Value);
            return d.Where(p => p.Value == m).Select(p => p.Key);
        }

        public static double MeanAbsoluteDeviation(this IEnumerable<double> data)
        {
            var d = data.ToList();
            return AbsoluteDeviation(d, d.Average());
        }

        public static double MedianAbsoluteDeviation(this IEnumerable<double> data)
        {
            var d = data.ToList();
            return AbsoluteDeviation(d, d.Median());
        }

        public static double AbsoluteDeviation(this IEnumerable<double> data, double point)
        {
            var d = 0.0;
            var c = 0;
            using var e = data.GetEnumerator();
            while (e.MoveNext())
            {
                c++;
                d += System.Math.Abs(e.Current - point);
            }
            return d / c;
        }

        public static double Correlation(IEnumerable<double> dataA, IEnumerable<double> dataB)
        {
            var da = dataA.ToList();
            var db = dataB.ToList();
            var avga = da.Average();
            var avgb = db.Average();
            using var ea = da.GetEnumerator();
            using var eb = db.GetEnumerator();

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

        public static double Covariance(IEnumerable<double> dataA, IEnumerable<double> dataB)
        {
            var da = dataA.ToList();
            var db = dataB.ToList();
            var avga = da.Average();
            var avgb = db.Average();
            using var ea = da.GetEnumerator();
            using var eb = db.GetEnumerator();

            double sigmaXY = 0.0;
            while (ea.MoveNext() && eb.MoveNext())
            {
                sigmaXY += (ea.Current - avga) * (eb.Current - avgb);
            }

            if (ea.MoveNext() || eb.MoveNext()) throw new IndexOutOfRangeException();

            return sigmaXY / da.Count;
        }

        public static double DeviationSquared(this IEnumerable<double> data)
        {
            var d = data.ToList();
            var avg = d.Average();
            return d.Sum(p => System.Math.Pow(p - avg, 2));
        }

        public static double Fisher(this in double value)
        {
            if (!value.IsBetween(-1, 1)) throw new ArgumentOutOfRangeException(nameof(value));
            return System.Math.Log((1 + value) / (1 - value)) / 2;
        }

        public static double Forecast(in double valueA, IEnumerable<double> dataA, IEnumerable<double> dataB)
        {
            var da = dataA.ToList();
            var db = dataB.ToList();
            var avga = da.Average();
            var avgb = db.Average();

            using var ea = da.GetEnumerator();
            using var eb = db.GetEnumerator();

            double sigmaXY = 0.0, sigmaX = 0.0;
            while (ea.MoveNext() && eb.MoveNext())
            {
                sigmaXY += (ea.Current - avga) * (eb.Current - avgb);
                sigmaX += System.Math.Pow(ea.Current - avga, 2);
            }

            if (ea.MoveNext() || eb.MoveNext()) throw new IndexOutOfRangeException();

            var b = sigmaXY / sigmaX;
            var a = avgb - b * avga;

            return a + b * valueA;
        }

        public static double StandardDeviation(this IEnumerable<double> data)
        {
            var d = data.ToList();
            var avg = d.Average();

            return System.Math.Sqrt(d.Sum(p => System.Math.Pow(p - avg, 2)) / d.Count);
        }
    }
}