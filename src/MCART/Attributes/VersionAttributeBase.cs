/*
VersionAttributeBase.cs

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

using System.Globalization;

namespace TheXDS.MCART.Attributes;
using System;
using TheXDS.MCART.Math;
using TheXDS.MCART.Resources;

/// <summary>
/// Especifica la versión de un elemento, además de ser la clase base para
/// los atributos que describan un valor <see cref="Version" /> para un
/// elemento.
/// </summary>
public abstract class VersionAttributeBase : Attribute, IValueAttribute<Version>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="VersionAttributeBase" />.
    /// </summary>
    /// <param name="major">Número de versión mayor.</param>
    /// <param name="minor">Número de versión menor.</param>
    /// <param name="build">Número de compilación.</param>
    /// <param name="rev">Número de revisión.</param>
    protected VersionAttributeBase(int major, int minor, int build, int rev)
    {
        Value = new(major, minor, build, rev);
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="VersionAttributeBase" />.
    /// </summary>
    /// <param name="major">Número de versión mayor.</param>
    /// <param name="minor">Número de versión menor.</param>
    protected VersionAttributeBase(int major, int minor)
        : this(major, minor, 0, 0)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="VersionAttributeBase" />.
    /// </summary>
    /// <param name="version">Número de versión en formato <c>0.0</c>.</param>
    protected VersionAttributeBase(double version)
    {
        if (!version.IsValid()) throw Errors.InvalidValue(nameof(version));
        var v = version.ToString("0.0", CultureInfo.InvariantCulture).Split('.');
        Value = new(int.Parse(v[0]), int.Parse(v[1]), 0, 0);
    }

    /// <summary>
    /// Obtiene el valor asociado a este atributo.
    /// </summary>
    /// <value>El valor de este atributo.</value>
    public Version Value { get; }
}
