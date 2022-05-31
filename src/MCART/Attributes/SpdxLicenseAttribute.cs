/*
SpdxLicenseAttribute.cs

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
using System.Collections.Generic;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Extensions;
using static System.AttributeTargets;
using static TheXDS.MCART.Misc.Internals;
using static TheXDS.MCART.Types.Extensions.DictionaryExtensions;

/// <summary>
/// Marca un elemento con la licencia Open-Source correspondiente.
/// </summary>
[AttributeUsage(Class | Module | Assembly)]
public sealed class SpdxLicenseAttribute : LicenseAttributeBase
{
    private static readonly Dictionary<string, SpdxLicense> _licenses = new();

    /// <summary>
    /// Marca al elemento con una licencia SPDX específica.
    /// </summary>
    /// <param name="license">
    /// Licencia con la cual marcar al elemento.
    /// </param>
    public SpdxLicenseAttribute(SpdxLicenseId license) : base(license.GetAttr<NameAttribute>()?.Value ?? license.ToString())
    {
        Id = CheckDefinedEnum(license, nameof(license));
    }

    /// <summary>
    /// Marca al elemento con una expresión SPDX de licencia personalizada.
    /// </summary>
    /// <param name="spdxShortIdentifier">
    /// Identificador de licencia según el estándar SPDX.
    /// </param>
    public SpdxLicenseAttribute(string spdxShortIdentifier) : base(spdxShortIdentifier)
    {
        if (System.Enum.TryParse(spdxShortIdentifier, false, out SpdxLicenseId id)) Id = id;
    }

    /// <summary>
    /// Obtiene el nombre corto de la licencia.
    /// </summary>
    public SpdxLicenseId? Id { get; }

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
        string? n = Id?.GetAttr<NameAttribute>()?.Value;

        if (n is null)
        {
            string? id = Id.ToString() ?? Value!;
            for (byte i = 0; i <= 9; i++)
            {
                id = id.Replace(i.ToString(), $"-{i}-");
            }
            n = id.Replace("-_-", ".").Replace("--", string.Empty).Replace('_', '-').Replace("--", "-").Trim('-');
        }

        if (_licenses.ContainsKey(n)) return _licenses[n];
        string? d = Id?.GetAttr<DescriptionAttribute>()?.Value ?? $"{n} License";
        Uri? u = Id?.GetAttr<LicenseUriAttribute>()?.Uri ?? new Uri($"https://spdx.org/licenses/{n}.html");

        return new SpdxLicense(n, d, u).PushInto(n, _licenses);
    }
}
