/*
BinaryWriterExtensions_Contracts.cs

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

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Types.Extensions;

public static partial class BinaryWriterExtensions
{
    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void DynamicWrite_Contract(this BinaryWriter bw, object value)
    {
        Internals.NullCheck(bw, nameof(bw));
        Internals.NullCheck(value, nameof(value));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void WriteStruct_Contract(this BinaryWriter bw, Type t)
    {
        Internals.NullCheck(bw, nameof(bw));
        if (typeof(BinaryWriter).GetMethods().FirstOrDefault(p => CanWrite(p, t)) is { } m)
        {
            throw Errors.BinaryWriteNotSupported(t, m);
        }
        if (typeof(BinaryWriterExtensions).GetMethods().FirstOrDefault(p => CanExWrite(p, t)) is { } mx)
        {
            throw Errors.BinaryWriteNotSupported(t, mx);
        }
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void WriteStruct_Contract(this BinaryWriter bw)
    {
        Internals.NullCheck(bw, nameof(bw));
    }
}
