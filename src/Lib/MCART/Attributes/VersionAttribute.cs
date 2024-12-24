/*
VersionAttribute.cs

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

namespace TheXDS.MCART.Attributes;

/// <summary>
/// Specifies the version of the element.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Module | AttributeTargets.Assembly)]
[Serializable]
public sealed class VersionAttribute : VersionAttributeBase
{
    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="VersionAttribute" /> class.
    /// </summary>
    /// <param name="version">Version number in <c>0.0</c> format.</param>
    public VersionAttribute(double version) : base(version)
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="VersionAttribute" /> class.
    /// </summary>
    /// <param name="major">Major version number.</param>
    /// <param name="minor">Minor version number.</param>
    public VersionAttribute(int major, int minor)
        : base(major, minor)
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="VersionAttribute" /> class.
    /// </summary>
    /// <param name="major">Major version number.</param>
    /// <param name="minor">Minor version number.</param>
    /// <param name="build">Build number.</param>
    /// <param name="rev">Revision number.</param>
    public VersionAttribute(int major, int minor, int build, int rev)
        : base(major, minor, build, rev)
    {
    }
}
