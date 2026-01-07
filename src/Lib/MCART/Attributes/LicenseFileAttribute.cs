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

using TheXDS.MCART.Misc;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Attributes;

/// <summary>
/// Sets an external license file to associate with the element.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Module | AttributeTargets.Assembly)]
[Obsolete(AttributeErrorMessages.UseLicenseUriAttributeInstead)]
public sealed class LicenseFileAttribute : TextAttribute
{
    /// <summary>
    /// Initializes a new instance of the class
    /// <see cref="LicenseFileAttribute" />.
    /// </summary>
    /// <param name="licenseFile">
    /// Path of the attached license file.
    /// </param>
    public LicenseFileAttribute(string licenseFile) : base(licenseFile)
    {
        // HACK: simple and effective way to validate a file path.
        _ = new FileInfo(licenseFile);
    }

    /// <summary>
    /// Reads the license file specified by this attribute.
    /// </summary>
    /// <returns>
    /// The content of the specified license file.
    /// </returns>
    public string ReadLicense()
    {
        try
        {
            using FileStream fs = new(Value, FileMode.Open);
            using StreamReader sr = new(fs);
            return sr.ReadToEnd();
        }
        catch
        {
            return St.Composition.Warn(St.Common.UnspecifiedLicense);
        }
    }
}
