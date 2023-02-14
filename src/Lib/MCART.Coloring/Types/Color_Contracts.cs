﻿/*
Color_Contracts.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Resources;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Types;

/// <summary>
/// Estructura universal que describe un color en sus componentes alfa,
/// rojo, verde y azul.
/// </summary>
public partial struct Color
{
    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void Blend_Contract(in IEnumerable<Color> colors)
    {
        if (!colors.Any()) throw Errors.EmptyCollection(colors);
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void AreClose_Contract(in float delta)
    {
        if (!delta.IsBetween(0f, 1f)) throw Errors.ValueOutOfRange(nameof(delta), 0f, 1f);
    }
}