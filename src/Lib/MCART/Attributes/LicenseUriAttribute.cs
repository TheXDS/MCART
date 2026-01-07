/*
LicenseFileAttribute.cs

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

namespace TheXDS.MCART.Attributes;

/// <summary>
/// Sets an external license file to associate with the element.
/// </summary>
/// <param name="licenseUri">
/// Uri path of the license.
/// </param>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Module | AttributeTargets.Assembly | AttributeTargets.Field)]
public sealed class LicenseUriAttribute(string licenseUri) : LicenseAttributeBase(licenseUri)
{
    /// <summary>
    /// Gets the storage path of the license.
    /// </summary>
    public Uri Uri { get; } = new Uri(licenseUri);

    /// <summary>
    /// Gets a license from the specified <see cref="Uri"/>
    /// for this attribute.
    /// </summary>
    /// <param name="context">
    /// Object from which this attribute was extracted.
    /// </param>
    /// <returns>
    /// A license from the specified <see cref="Uri"/> for
    /// this attribute.
    /// </returns>
    public override License GetLicense(object context)
    {
        return new(Path.GetFileNameWithoutExtension(Uri.LocalPath), Uri);
    }

    /// <summary>
    /// Gets a license from the specified <see cref="Uri"/>
    /// for this attribute.
    /// </summary>
    /// <returns>
    /// A license from the specified <see cref="Uri"/> for
    /// this attribute.
    /// </returns>
    public License GetLicense() => GetLicense(null!);
}
