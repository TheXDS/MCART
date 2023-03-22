/*
SpdxLicense.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Resources;

/// <summary>
/// Representa una licencia registrada dentro de los estándares de
/// Software Package Data Exchange (SPDX).
/// </summary>
public class SpdxLicense : License, IEquatable<SpdxLicense>
{
    private static readonly Dictionary<string, SpdxLicense> _licenses = new();

    /// <summary>
    /// Obtiene una instancia de <see cref="SpdxLicense"/> que representa una
    /// licencia del <paramref name="id"/> especificado.
    /// </summary>
    /// <param name="id">Id de la licencia a obtener.</param>
    /// <returns>
    /// Una instancia de la clase <see cref="SpdxLicense"/> que representa una
    /// licencia del <paramref name="id"/> especificado.
    /// </returns>
    public static SpdxLicense FromId(SpdxLicenseId id)
    {
        string n = GetSpdxLicenseName(id);
        if (_licenses.ContainsKey(n)) return _licenses[n];
        string? d = id.GetAttribute<Attributes.DescriptionAttribute>()?.Value ?? $"{n} License";
        Uri? u = id.GetAttribute<LicenseUriAttribute>()?.Uri ?? new Uri($"https://spdx.org/licenses/{n}.html");
        return new SpdxLicense(n, d, u).PushInto(n, _licenses);
    }

    /// <summary>
    /// Obtiene una instancia de <see cref="SpdxLicense"/> que representa una
    /// licencia con el nombre especificado.
    /// </summary>
    /// <param name="name">Nombre de la licencia a obtener.</param>
    /// <returns>
    /// Una instancia de la clase <see cref="SpdxLicense"/> que representa una
    /// licencia con el nombre especificado.
    /// </returns>
    public static SpdxLicense FromName(string name)
    {
        string n = InferSpdxLicenseName(name);
        if (_licenses.ContainsKey(n)) return _licenses[n];
        string? d = $"{n} License";
        Uri? u = new($"https://spdx.org/licenses/{n}.html");
        return new SpdxLicense(n, d, u).PushInto(n, _licenses);
    }

    /// <summary>
    /// Obtiene el identificador corto de la licencia.
    /// </summary>
    public string SpdxShortName { get; }

    internal SpdxLicense(string id, string? name, Uri url) : base(name ?? id, url)
    {
        SpdxShortName = id;
    }

    /// <summary>
    /// Comprueba la igualdad entre dos instancias de la clase <see cref="SpdxLicense"/>.
    /// </summary>
    /// <param name="other">El otro objeto a comparar.</param>
    /// <returns>
    /// <see langword="true"/> si ambas instancias son consideradas
    /// iguales, <see langword="false"/> en caso contrario.
    /// </returns>
    public bool Equals([AllowNull] SpdxLicense other)
    {
        return SpdxShortName == other?.SpdxShortName;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as SpdxLicense);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return SpdxShortName.GetHashCode();
    }

    private static string GetSpdxLicenseName(SpdxLicenseId id)
    {
        return id.GetAttribute<NameAttribute>()?.Value ?? InferSpdxLicenseName(id.ToString());
    }

    private static string InferSpdxLicenseName(string text)
    {
        for (byte i = 0; i <= 9; i++)
        {
            text = text.Replace(i.ToString(), $"-{i}-");
        }
        return text.Replace("-_-", ".").Replace("--", string.Empty).Replace('_', '-').Replace("--", "-").Trim('-');
    }
}
