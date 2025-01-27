/*
CopyrightAttribute.cs

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

using System.Reflection;
using TheXDS.MCART.Types;

namespace TheXDS.MCART.Attributes;

/// <summary>
/// Sets the Copyright information of the element.
/// </summary>
/// <param name="copyright">Value of the attribute.</param>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Module | AttributeTargets.Assembly)]
[Serializable]
public sealed class CopyrightAttribute(string copyright) : TextAttribute(GetCopyrightString(copyright))
{
    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="DescriptionAttribute" /> class.
    /// </summary>
    /// <param name="year">Year of Copyright registration.</param>
    /// <param name="holder">Copyright holder.</param>
    public CopyrightAttribute(int year, string holder)
        : this($"{year:0000} {holder}")
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="DescriptionAttribute" /> class.
    /// </summary>
    /// <param name="years">Years of Copyright registration.</param>
    /// <param name="holder">Copyright holder.</param>
    public CopyrightAttribute(Range<int> years, string holder)
        : this($"{years.Minimum:0000}-{years.Maximum:0000} {holder}")
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="DescriptionAttribute" /> class.
    /// </summary>
    /// <param name="startYear">Initial year of the Copyright</param>
    /// <param name="endYear">Final year of the Copyright</param>
    /// <param name="holder">Copyright holder.</param>
    public CopyrightAttribute(int startYear, int endYear, string holder) : this(new Range<int>(startYear, endYear), holder)
    {
    }

    /// <summary>
    /// Implicitly converts a <see cref="CopyrightAttribute"/> object to an <see cref="AssemblyCopyrightAttribute"/>
    /// </summary>
    /// <param name="attribute">Object to convert</param>
    public static implicit operator AssemblyCopyrightAttribute(CopyrightAttribute attribute)
    {
        return new AssemblyCopyrightAttribute(attribute.Value);
    }

    /// <summary>
    /// Implicitly converts an <see cref="AssemblyCopyrightAttribute"/> object to a <see cref="CopyrightAttribute"/>
    /// </summary>
    /// <param name="attribute">Object to convert</param>
    public static implicit operator CopyrightAttribute(AssemblyCopyrightAttribute attribute)
    {
        return new CopyrightAttribute(attribute.Copyright);
    }

    private static string GetCopyrightString(string input)
    {
        return input.ToLowerInvariant().StartsWith("copyright ", true, null)
            ? input
            : $"Copyright © {input}";
    }
}
