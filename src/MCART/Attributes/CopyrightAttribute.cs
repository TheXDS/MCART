/*
CopyrightAttribute.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

namespace TheXDS.MCART.Attributes;
using System;
using TheXDS.MCART.Types;
using static System.AttributeTargets;

/// <summary>
/// Establece la información de Copyright del elemento.
/// </summary>
[AttributeUsage(Method | Class | Module | Assembly)]
[Serializable]
public sealed class CopyrightAttribute : TextAttribute
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="DescriptionAttribute" />.
    /// </summary>
    /// <param name="copyright">Valor del atributo.</param>
    public CopyrightAttribute(string copyright) 
        : base(GetCopyrightString(copyright))
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="DescriptionAttribute" />.
    /// </summary>
    /// <param name="year">Año de registro del Copyright.</param>
    /// <param name="holder">Poseedor del Copyright.</param>
#if CLSCompliance
    [CLSCompliant(false)]
#endif
    public CopyrightAttribute(ushort year, string holder) 
        : this($"{year:0000} {holder}")
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="DescriptionAttribute" />.
    /// </summary>
    /// <param name="years">Años de registro del Copyright.</param>
    /// <param name="holder">Poseedor del Copyright.</param>
#if CLSCompliance
    [CLSCompliant(false)]
#endif
    public CopyrightAttribute(Range<ushort> years, string holder) 
        : this($"{years.Minimum:0000}-{years.Maximum:0000} {holder}") 
    {
    }

    private static string GetCopyrightString(string input)
    {
        return input.ToLowerInvariant().StartsWith("copyright ", true, null)
            ? input
            : $"Copyright © {input}";
    }
}
