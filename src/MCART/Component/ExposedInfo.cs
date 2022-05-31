/*
ExposedInfo.cs

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

namespace TheXDS.MCART.Component;
using System;
using System.Collections.Generic;
using TheXDS.MCART.Resources;
using static TheXDS.MCART.Misc.Internals;

/// <summary>
/// Expone de manera aislada la información de un objeto
/// <see cref="IExposeInfo"/>.
/// </summary>
public class ExposedInfo : IExposeInfo
{
    private readonly IExposeInfo _source;

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ExposedInfo"/>
    /// </summary>
    /// <param name="source">
    /// Objeto del cual se expondrá la información.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="source"/> es <see langword="null"/>.
    /// </exception>
    public ExposedInfo(IExposeInfo source)
    {
        NullCheck(source, nameof(source));
        _source = source;
    }

    /// <summary>
    /// Obtiene el autor del <see cref="IExposeInfo"/>.
    /// </summary>
    public IEnumerable<string>? Authors => _source.Authors;

    /// <summary>
    /// Obtiene el Copyright del <see cref="IExposeInfo"/>
    /// </summary>
    public string? Copyright => _source.Copyright;

    /// <summary>
    /// Obtiene la descripción del elemento.
    /// </summary>
    public string? Description => _source.Description;

    /// <summary>
    /// Obtiene un valor que indica si este <see cref="IExposeInfo"/>
    /// contiene información de licencias de terceros.
    /// </summary>
    public bool Has3rdPartyLicense => _source.Has3rdPartyLicense;

    /// <summary>
    /// Obtiene un valor que indica si este <see cref="IExposeInfo"/>
    /// contiene información de licencia.
    /// </summary>
    public bool HasLicense => _source.HasLicense;

    /// <summary>
    /// Obtiene la versión informacional del <see cref="IExposeInfo"/>.
    /// </summary>
    public string? InformationalVersion => _source.InformationalVersion;

    /// <summary>
    /// Obtiene la licencia del <see cref="IExposeInfo"/>
    /// </summary>
    public License? License => _source.License;

    /// <summary>
    /// Obtiene el nombre del elemento.
    /// </summary>
    public string Name => _source.Name;

    /// <summary>
    /// Obtiene una colección con el contenido de licencias de terceros
    /// para el objeto.
    /// </summary>
    public IEnumerable<License>? ThirdPartyLicenses => _source.ThirdPartyLicenses;

    /// <summary>
    /// Obtiene la versión del <see cref="IExposeInfo"/>
    /// </summary>
    public Version? Version => _source.Version;
}
