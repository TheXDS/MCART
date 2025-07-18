﻿/*
DelegateExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene numerosas extensiones para el tipo System.DateTime del
CLR, supliéndolo de nueva funcionalidad previamente no existente, o de
invocación compleja.

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

using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contains extensions for objects of type <see cref="Delegate"/>.
/// </summary>
public static class DelegateExtensions
{
    /// <summary>
    /// Gets a descriptive name for a <see cref="Delegate"/>.
    /// </summary>
    /// <param name="d">
    /// Delegate for which to get a descriptive name.
    /// </param>
    /// <returns>
    /// A descriptive name for a <see cref="Delegate"/>, or the name of
    /// the method represented by the delegate if this does not contain a
    /// <see cref="NameAttribute"/>.
    /// </returns>
    public static string NameOf(this Delegate d)
    {
        return d.Method.NameOf();
    }
}
