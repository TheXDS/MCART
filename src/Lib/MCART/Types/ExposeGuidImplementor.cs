﻿/*
ExposeGuidImplementor.cs

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

using System.Runtime.InteropServices;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Types;

/// <summary>
/// Directly implements <see cref="IExposeGuid"/>.
/// </summary>
/// <param name="o">
/// Object from which to expose the 
/// Guid; typically <see langword="this"/>.
/// </param>
public class ExposeGuidImplementor(object o) : IExposeGuid
{
    private readonly Type _t = o as Type ?? o?.GetType() ?? throw new ArgumentNullException(nameof(o));

    /// <summary>
    /// Gets the <see cref="Guid"/> associated 
    /// with this object.
    /// </summary>
    public virtual Guid Guid
    {
        get
        {
            GuidAttribute? g = _t.GetAttribute<GuidAttribute>() ?? throw Errors.MissingGuidAttribute(_t);
            return new Guid(g.Value);
        }
    }
}
