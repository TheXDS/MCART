/*
Tween.cs

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
/// Contains a series of motion tween functions.
/// </summary>
public static class Tween
{
    /// <summary>
    /// Describes a function that applies a smoothing transformation.
    /// </summary>
    /// <returns>
    /// A value corresponding to the applied smoothing.
    /// </returns>
    /// <param name="position">
    /// Value between <c>0.0</c> and <c>1.0</c> indicating the position in the timeline.
    /// </param>
    public delegate double TweenFunction(in double position);

    /// <summary>
    /// Performs a linear smoothing of a value.
    /// </summary>
    /// <returns>
    /// A value corresponding to the applied smoothing.
    /// </returns>
    /// <param name="position">
    /// Value between <c>0.0</c> and <c>1.0</c> indicating the position in the timeline.
    /// </param>
    public static double Linear(in double position) => position;

    /// <summary>
    /// Performs a quadratic smoothing of a value.
    /// </summary>
    /// <returns>
    /// A value corresponding to the applied smoothing.
    /// </returns>
    /// <param name="position">
    /// Value between <c>0.0</c> and <c>1.0</c> indicating the position in the timeline.
    /// </param>
    public static double Quadratic(in double position)
    {
        double x2 = System.Math.Pow(position, 2);
        return x2 / ((2 * x2) - (2 * position) + 1);
    }

    /// <summary>
    /// Performs a cubic smoothing of a value.
    /// </summary>
    /// <returns>
    /// A value corresponding to the applied smoothing.
    /// </returns>
    /// <param name="position">
    /// Value between <c>0.0</c> and <c>1.0</c> indicating the position in the timeline.
    /// </param>
    public static double Cubic(in double position)
    {
        return System.Math.Pow(position, 3) / ((3 * System.Math.Pow(position, 2)) - (3 * position) + 1);
    }

    /// <summary>
    /// Performs a quartic smoothing of a value.
    /// </summary>
    /// <returns>
    /// A value corresponding to the applied smoothing.
    /// </returns>
    /// <param name="position">
    /// Value between <c>0.0</c> and <c>1.0</c> indicating the position in the timeline.
    /// </param>
    public static double Quartic(in double position)
    {
        return -System.Math.Pow(position - 1, 4) + 1;
    }

    /// <summary>
    /// Performs a shaky smoothing of a value.
    /// </summary>
    /// <returns>
    /// A value corresponding to the applied smoothing.
    /// </returns>
    /// <param name="position">
    /// Value between <c>0.0</c> and <c>1.0</c> indicating the position in the timeline.
    /// </param>
    public static double Shaky(in double position) => Shaky(position, 10);

    /// <summary>
    /// Performs a shaky smoothing of a value.
    /// </summary>
    /// <returns>
    /// A value corresponding to the applied smoothing.
    /// </returns>
    /// <param name="position">
    /// Value between <c>0.0</c> and <c>1.0</c> indicating the position in the timeline.
    /// </param>
    /// <param name="shakes">Number of shakes to perform.</param>
    public static double Shaky(in double position, in int shakes)
    {
        return 1 - (System.Math.Cos(shakes * System.Math.PI * position) * System.Math.Cos(System.Math.PI / 2 * position));
    }

    /// <summary>
    /// Performs a bouncy smoothing of a value.
    /// </summary>
    /// <returns>
    /// A value corresponding to the applied smoothing.
    /// </returns>
    /// <param name="position">
    /// Value between <c>0.0</c> and <c>1.0</c> indicating the position in the timeline.
    /// </param>
    public static double Bouncy(in double position) => Bouncy(position, 10);

    /// <summary>
    /// Performs a bouncy smoothing of a value.
    /// </summary>
    /// <returns>
    /// A value corresponding to the applied smoothing.
    /// </returns>
    /// <param name="position">
    /// Value between <c>0.0</c> and <c>1.0</c> indicating the position in the timeline.
    /// </param>
    /// <param name="bounces">Number of bounces to calculate.</param>
    public static double Bouncy(in double position, in int bounces) => Bouncy(position, bounces, 8);

    /// <summary>
    /// Performs a bouncy smoothing of a value.
    /// </summary>
    /// <returns>
    /// A value corresponding to the applied smoothing.
    /// </returns>
    /// <param name="position">
    /// Value between <c>0.0</c> and <c>1.0</c> indicating the position in the timeline.
    /// </param>
    /// <param name="bounces">Number of bounces to calculate.</param>
    /// <param name="damping">Damping to calculate.</param>
    public static double Bouncy(in double position, in int bounces, in int damping)
    {
        return 1 - (System.Math.Cos(bounces * System.Math.PI * position) * (1 - System.Math.Pow(position, 1.0 / damping)));
    }
}
