/*
SpdxLicenseAttribute.cs

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

using TheXDS.MCART.Resources;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Attributes;

/// <summary>
/// Marks an element with the corresponding SPDX license.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Module | AttributeTargets.Assembly)]
public sealed class SpdxLicenseAttribute : LicenseAttributeBase
{
    /// <summary>
    /// Marks the element with a specific SPDX license.
    /// </summary>
    /// <param name="license">
    /// License with which to mark the element.
    /// </param>
    public SpdxLicenseAttribute(SpdxLicenseId license) : base(license.ToString())
    {
        Id = CheckDefinedEnum(license, nameof(license));
    }

    /// <summary>
    /// Marks the element with a custom SPDX license expression.
    /// </summary>
    /// <param name="spdxShortIdentifier">
    /// License identifier according to the SPDX standard.
    /// </param>
    public SpdxLicenseAttribute(string spdxShortIdentifier) : base(spdxShortIdentifier)
    {
        if (Enum.TryParse(spdxShortIdentifier, false, out SpdxLicenseId id)) Id = id;
    }

    /// <summary>
    /// Gets the short name of the license.
    /// </summary>
    public SpdxLicenseId? Id { get; }

    /// <summary>
    /// Gets a license associated with this attribute.
    /// </summary>
    /// <param name="context">
    /// Object from which this attribute was extracted.
    /// </param>
    /// <returns>
    /// A license associated with this attribute.
    /// </returns>
    public override License GetLicense(object context)
    {
        return Id is not null ? SpdxLicense.FromId(Id.Value) : SpdxLicense.FromName(Value!);
    }
}
