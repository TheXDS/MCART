/*
IExposeInfo.cs

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

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Define una serie de miembros a implementar para un tipo que exponga
/// diversa información de identificación.
/// </summary>
public interface IExposeInfo : INameable, IDescriptible
{
    /// <summary>
    /// Obtiene el autor del <see cref="IExposeInfo"/>.
    /// </summary>
    IEnumerable<string>? Authors { get; }

    /// <summary>
    /// Obtiene el Copyright del <see cref="IExposeInfo"/>
    /// </summary>
    string? Copyright { get; }

    /// <summary>
    /// Obtiene un valor que indica si este <see cref="IExposeInfo"/>
    /// contiene información de licencias de terceros.
    /// </summary>
    bool Has3rdPartyLicense => ThirdPartyLicenses?.Any() ?? false;

    /// <summary>
    /// Obtiene un valor que indica si este <see cref="IExposeInfo"/>
    /// contiene información de licencia.
    /// </summary>
    bool HasLicense => License is { };

    /// <summary>
    /// Obtiene la versión informacional del <see cref="IExposeInfo"/>.
    /// </summary>
    string? InformationalVersion => Version?.ToString();

    /// <summary>
    /// Obtiene la licencia del <see cref="IExposeInfo"/>
    /// </summary>
    License? License { get; }

    /// <summary>
    /// Obtiene una colección con el contenido de licencias de terceros
    /// para el objeto.
    /// </summary>
    IEnumerable<License>? ThirdPartyLicenses { get; }

    /// <summary>
    /// Obtiene la versión del <see cref="IExposeInfo"/>
    /// </summary>
    Version? Version { get; }
}
