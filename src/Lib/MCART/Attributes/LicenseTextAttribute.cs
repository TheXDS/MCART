/*
LicenseTextAttribute.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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

using TheXDS.MCART.Resources;
using static System.AttributeTargets;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Attributes;

/// <summary>
/// Establece el texto de licencia a asociar con el elemento.
/// </summary>
[AttributeUsage(Class | Module | Assembly)]
public sealed class LicenseTextAttribute : LicenseAttributeBase
{
    private readonly string _title;

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="LicenseTextAttribute" />.
    /// </summary>
    /// <param name="title">Título de la licencia</param>
    /// <param name="licenseText">Texto de la licencia.</param>
    public LicenseTextAttribute(string title, string licenseText) 
        : base(licenseText)
    {
        _title = EmptyChecked(title, nameof(title));
        EmptyCheck(licenseText, nameof(licenseText));
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="LicenseTextAttribute" />.
    /// </summary>
    /// <param name="licenseText">Texto de la licencia.</param>
    public LicenseTextAttribute(string licenseText)
        : this(EmptyChecked(licenseText, nameof(licenseText)).Split('\n', 2)[0].Trim(), licenseText)
    {
    }

    /// <summary>
    /// Obtiene una licencia asociada a este atributo.
    /// </summary>
    /// <param name="context">
    /// Objeto del cual se ha extraído este atributo.
    /// </param>
    /// <returns>
    /// Una licencia asociada a este atributo.
    /// </returns>
    public override License GetLicense(object context)
    {
        return new TextLicense(_title, Value!);
    }
}
