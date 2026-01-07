/*
ExposedInfo.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Component;

/// <summary>
/// Wraps an <see cref="IExposeInfo"/> instance to expose the information in an
/// isolated manner.
/// <see cref="IExposeInfo"/>.
/// </summary>
public class ExposedInfo : IExposeInfo
{
    private readonly IExposeInfo _source;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExposedInfo"/> class.
    /// </summary>
    /// <param name="source">
    /// Object from which to extract the information.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/> is <see langword="null"/>.
    /// </exception>
    public ExposedInfo(IExposeInfo source)
    {
        ArgumentNullException.ThrowIfNull(source);
        _source = source;
    }

    /// <summary>
    /// Returns the author of the <see cref="IExposeInfo" />.
    /// </summary>
    public IEnumerable<string>? Authors => _source.Authors;

    /// <summary>
    /// Gets the Copyright information from the <see cref="IExposeInfo"/>.
    /// </summary>
    public string? Copyright => _source.Copyright;

    /// <summary>
    /// Gets the description for the <see cref="IExposeInfo" />.
    /// </summary>
    public string? Description => _source.Description;

    /// <summary>
    /// Gets a value that indicates whether this <see cref="IExposeInfo"/>
    /// contains third party licences.
    /// </summary>
    public bool Has3rdPartyLicense => _source.Has3rdPartyLicense;

    /// <summary>
    /// Gets a value that indicates whether this <see cref="IExposeInfo"/>
    /// contains license information.
    /// </summary>
    public bool HasLicense => _source.HasLicense;

    /// <summary>
    /// Gets the informational version of the <see cref="IExposeInfo"/>.
    /// </summary>
    public string? InformationalVersion => _source.InformationalVersion;

    /// <summary>
    /// Gets the license for the <see cref="IExposeInfo" />.
    /// </summary>
    public License? License => _source.License;

    /// <summary>
    /// Gets the common name for the <see cref="IExposeInfo" />.
    /// </summary>
    public string Name => _source.Name;

    /// <summary>
    /// Gets a collection that enumerates all the included components that are
    /// marked as third-party on the <see cref="IExposeInfo"/>.
    /// </summary>
    public IEnumerable<License>? ThirdPartyLicenses => _source.ThirdPartyLicenses;

    /// <summary>
    /// Gets the <see cref="IExposeInfo" /> version information.
    /// </summary>
    public Version? Version => _source.Version;
}
