﻿/*
License.cs

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

using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Types.Base;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Resources;

/// <summary>
/// Describe una licencia.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="License"/>.
/// </remarks>
/// <param name="name">Nombre de la licencia.</param>
/// <param name="uri">
/// Uri que obtiene la ubicación del contenido de la licencia.
/// </param>
public class License(string name, Uri? uri) : INameable
{
    private static License? _missing;
    private static License? _noLicense;
    private static License? _unspecified;

    /// <summary>
    /// Obtiene una referencia a una licencia no encontrada.
    /// </summary>
    public static License MissingLicense => _missing ??= new License(St.Common.LicenseNotFound, null);

    /// <summary>
    /// Obtiene una referencia a un objeto sin licencia.
    /// </summary>
    public static License NoLicense => _noLicense ??= new License(St.Common.NoLicense, null);

    /// <summary>
    /// Obtiene una referencia a un objeto con licencia no definida.
    /// </summary>
    public static License Unspecified => _unspecified ??= new License(St.Common.UnspecifiedLicense, null);

    /// <summary>
    /// Obtiene el nombre descriptivo de la licencia.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Obtiene la URL de la licencia.
    /// </summary>
    public Uri? LicenseUri { get; } = uri;

    /// <summary>
    /// Obtiene el contenido de la licencia.
    /// </summary>
    public virtual string LicenseContent
    {
        get
        {
            if (LicenseUri is null) return St.Composition.Warn(St.Common.NoContent);
            try
            {
                using StreamReader sr = new(StreamUriParser.Get(LicenseUri)!);
                return sr.ReadToEnd();
            }
            catch
            {
                return St.Composition.Warn(St.Errors.ErrorLoadingLicense);
            }
        }
    }
}
