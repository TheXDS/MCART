/*
IExposeExtendedInfo.cs

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

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Defines a series of members to be implemented for a type that 
/// exposes extended identification information.
/// </summary>
public interface IExposeExtendedInfo : IExposeInfo
{
    /// <summary>
    /// Gets a value indicating whether this 
    /// <see cref="IExposeExtendedInfo"/> is considered a beta version.
    /// </summary>
    bool Beta { get; }

    /// <summary>
    /// Gets a value indicating whether this <see cref="IExposeInfo"/> 
    /// complies with the Common Language Specification (CLS).
    /// </summary>
    bool ClsCompliant { get; }

    /// <summary>
    /// Gets a value indicating whether this 
    /// <see cref="IExposeExtendedInfo"/> might contain code used in 
    /// an unsafe context.
    /// </summary>
    bool Unmanaged { get; }
}
