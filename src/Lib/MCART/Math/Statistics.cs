/*
Statistics.cs

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

using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Math;

/// <summary>
/// Contains a number of statistic functions.
/// </summary>
public static partial class Statistics
{
    /// <summary>
    /// Varies a value according to a percentage of its delta.
    /// </summary>
    /// <param name="value">Value to vary.</param>
    /// <param name="delta">Variance delta.</param>
    /// <returns>
    /// A value randomly varied according to a delta.
    /// </returns>
    public static double Variate(this in double value, in double delta)
    {
        return value + (RandomExtensions.Rnd.NextDouble() * delta * 2) - delta;
    }

    /// <summary>
    /// Gets a normalized range for a given data set, given an error margin.
    /// </summary>
    /// <param name="data">Data set to normalize.</param>
    /// <param name="errorMargin">Error margin tolerated.</param>
    /// <returns>
    /// An enumeration with the normalized values of the data set according
    /// to an error margin value.
    /// </returns>
    public static IEnumerable<Range<double>> Normalize(this IEnumerable<double> data, double errorMargin)
    {
        return from j in data select new Range<double>(j - errorMargin, j + errorMargin);
    }

    /// <summary>
    /// Gets a new average value from the specified parameters.
    /// </summary>
    /// <param name="value">New value to add to the average.</param>
    /// <param name="samples">Number of samples of the previous average.</param>
    /// <param name="oldAverage">Previous average.</param>
    /// <returns>
    /// A new average that includes <paramref name="value"/>.
    /// </returns>
    public static double Average(this in double value, in int samples, in double oldAverage)
    {
        Average_Contract(value, samples, oldAverage);
        return oldAverage + ((value - oldAverage) / (samples + 1));
    }

    /// <summary>
    /// Calculates the mean tendency of a data set.
    /// </summary>
    /// <param name="data">
    /// Data set for which to calculate the mean tendency.
    /// </param>
    /// <returns>The mean tendency of a data set.</returns>
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
    /// Calculates the geometric tendency of a data set.
    /// </summary>
    /// <param name="data">
    /// Data set for which to calculate the geometric tendency.
    /// </param>
    /// <returns>The geometric tendency of a data set.</returns>
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
    /// Calculates the harmonic tendency of a data set.
    /// </summary>
    /// <param name="data">
    /// Data set for which to calculate the harmonic tendency.
    /// </param>
    /// <returns>The harmonic tendency of a data set.</returns>
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
    /// Calculates the median of a data set.
    /// </summary>
    /// <param name="data">
    /// Data set for which to calculate the median.
    /// </param>
    /// <returns>The median of a data set.</returns>
    public static double Median(this IEnumerable<double> data)
    {
        List<double> d = [.. data];
        if (d.Count == 0) return double.NaN;
        d.Sort();
        int p = d.Count / 2;
        return d.Count % 2 == 1 ? d[p] : (d[p - 2] + d[p - 1]) / 2;
    }

    /// <summary>
    /// Calculates the mode of a data set.
    /// </summary>
    /// <param name="data">
    /// Data set for which to calculate the mode.
    /// </param>
    /// <returns>The mode of a data set.</returns>
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
    /// Calculates the mean absolute deviation of a data set.
    /// </summary>
    /// <param name="data">
    /// Data set for which to calculate the mean absolute deviation.
    /// </param>
    /// <returns>
    /// The mean absolute deviation of a data set.
    /// </returns>
    public static double MeanAbsoluteDeviation(this IEnumerable<double> data)
    {
        List<double> d = [.. data];
        return AbsoluteDeviation(d, d.Average());
    }

    /// <summary>
    /// Calculates the median absolute deviation of a data set.
    /// </summary>
    /// <param name="data">
    /// Data set for which to calculate the median absolute deviation.
    /// </param>
    /// <returns>The median absolute deviation of a data set.</returns>
    public static double MedianAbsoluteDeviation(this IEnumerable<double> data)
    {
        List<double> d = [.. data];
        return AbsoluteDeviation(d, d.Median());
    }

    /// <summary>
    /// Calculates the absolute deviation of a point in a data set.
    /// </summary>
    /// <param name="data">
    /// Data set for which to calculate the absolute deviation.
    /// </param>
    /// <param name="point">
    /// Point for which to calculate the absolute deviation.
    /// </param>
    /// <returns>
    /// The absolute deviation of the point in a data set.
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
    /// Calculates the level of correlation between two data sets.
    /// </summary>
    /// <param name="dataA">First data set.</param>
    /// <param name="dataB">Second data set.</param>
    /// <returns>
    /// <c>1.0</c> for maximum correlation, <c>0.0</c> for minimum correlation.
    /// </returns>
    /// <exception cref="IndexOutOfRangeException">
    /// Thrown if either set has a different quantity of elements with respect to the other.
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
    /// Calculates the covariance between two data sets.
    /// </summary>
    /// <param name="dataA">First data set.</param>
    /// <param name="dataB">Second data set.</param>
    /// <returns>
    /// <c>1.0</c> for maximum covariance, <c>0.0</c> for minimum covariance.
    /// </returns>
    /// <exception cref="IndexOutOfRangeException">
    /// Thrown if either set has a different quantity of elements with respect to the other.
    /// </exception>
    public static double Covariance(IEnumerable<double> dataA, IEnumerable<double> dataB)
    {
        double sigmaXY = 0.0;
        (_, _, int countA) = DataSetComputation(dataA, dataB, (in double a, in double b, in double avgA, in double avgB) => sigmaXY += (a - avgA) * (b - avgB));
        return sigmaXY / countA;
    }

    /// <summary>
    /// Calculates the squared deviation of a data set.
    /// </summary>
    /// <param name="data">
    /// Data set for which to calculate the squared deviation.
    /// </param>
    /// <returns>The squared deviation of a data set.</returns>
    public static double DeviationSquared(this IEnumerable<double> data)
    {
        List<double> d = [.. data];
        double avg = d.Average();
        return d.Sum(p => System.Math.Pow(p - avg, 2));
    }

    /// <summary>
    /// Applies the Fisher transformation to a discrete value.
    /// </summary>
    /// <param name="value">
    /// Value to which to apply the Fisher transformation.
    /// </param>
    /// <returns>
    /// The result of the Fisher transformation of the specified value.
    /// </returns>
    public static double Fisher(this in double value)
    {
        if (!value.IsBetween(-1, 1)) throw new ArgumentOutOfRangeException(nameof(value));
        return System.Math.Log((1 + value) / (1 - value)) / 2;
    }

    /// <summary>
    /// Given the value <paramref name="valueA"/> in the set
    /// <paramref name="dataA"/>, predicts the resulting value with the set
    /// <paramref name="dataB"/>.
    /// </summary>
    /// <param name="valueA">
    /// Output value of the set <paramref name="dataA"/>.
    /// </param>
    /// <param name="dataA">
    /// Data set that produced <paramref name="valueA"/>.
    /// </param>
    /// <param name="dataB">
    /// Data set with which to perform the prediction.
    /// </param>
    /// <returns>
    /// The prediction of the output value given the set
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
    /// Gets the standard deviation of a data set.
    /// </summary>
    /// <param name="data">
    /// Data set for which to calculate the standard deviation.
    /// </param>
    /// <returns>The standard deviation of a data set.</returns>
    public static double StandardDeviation(this IEnumerable<double> data)
    {
        List<double> d = [.. data];
        double avg = d.Average();
        return System.Math.Sqrt(d.Sum(p => System.Math.Pow(p - avg, 2)) / d.Count);
    }

    private delegate void DataSetAction(in double currentA, in double currentB, in double avgA, in double avgB);

    private static (double averageA, double averageB, int count) DataSetComputation(IEnumerable<double> dataA, IEnumerable<double> dataB, DataSetAction action)
    {
        List<double> dA = [.. dataA];
        List<double> dB = [.. dataB];
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
