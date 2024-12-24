﻿/*
SpdxLicense.cs

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

using System.Diagnostics.CodeAnalysis;

namespace TheXDS.MCART.Resources;

/// <summary>
/// Representa una licencia registrada dentro de los estándares de
/// Software Package Data Exchange (SPDX).
/// </summary>
public partial class SpdxLicense : License, IEquatable<SpdxLicense>
{
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
        string? name = null;
        string? description = null;
        string? uri = null;
        if (licenseInfo.TryGetValue(id, out var license))
        {
            name = license.Name;
            description = license.Description;
            uri = license.LicenseUri;
        }
        name ??= InferSpdxLicenseName(id);
        description ??= $"{name} License";
        uri ??= $"https://spdx.org/licenses/{name}.html";
        return new SpdxLicense(name, description, new Uri(uri));
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
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        string? description = null;
        string? uri = null;
        if (licenseInfo.FirstOrDefault(pair => (pair.Value.Name ?? InferSpdxLicenseName(pair.Key)).Equals(name, StringComparison.InvariantCultureIgnoreCase)) is { Value: { Description: { } d, LicenseUri: { } l } })
        {
            description = d;
            uri = l;
        }
        description ??= $"{name} License";
        uri ??= $"https://spdx.org/licenses/{name}.html";
        return new SpdxLicense(name, description, new Uri(uri));
    }

    /// <summary>
    /// Obtiene el identificador corto de la licencia.
    /// </summary>
    public string SpdxShortName { get; }

    internal SpdxLicense(string id, string name, Uri url) : base(name, url)
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

    private static string InferSpdxLicenseName(SpdxLicenseId id)
    {
        var text = id.ToString();
        for (byte i = 0; i <= 9; i++)
        {
            text = text.Replace(i.ToString(), $"-{i}-");
        }
        return text.Replace("-_-", ".").Replace("--", string.Empty).Replace('_', '-').Replace("--", "-").Trim('-');
    }
}
