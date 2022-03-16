/*
SpdxLicenseAttribute.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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

namespace TheXDS.MCART.Attributes;
using System;
using System.Collections.Generic;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Factory;
using static System.AttributeTargets;
using static TheXDS.MCART.Misc.Internals;
using static TheXDS.MCART.Types.Factory.DictionaryExtensions;

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
