/*
BinaryReaderExtensions_Contracts.cs

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

using System.Diagnostics;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Resources;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Types.Extensions;

public static partial class BinaryReaderExtensions
{
    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void ReadStruct_Contract(this BinaryReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void ReadArray_Contract(BinaryReader reader, Type arrayType)
    {
        ArgumentNullException.ThrowIfNull(reader, nameof(reader));
        if (!NullChecked(arrayType).IsArray)
        {
            throw Errors.UnexpectedType(arrayType, typeof(Array));
        }
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void ReadBytesAt_Contract(this BinaryReader reader, long offset, int count)
    {
        ReadStruct_Contract(reader);
        if (!offset.IsBetween(0, reader.BaseStream.Length)) throw Errors.ValueOutOfRange(nameof(offset), 0, reader.BaseStream.Length);
        if (!count.IsBetween(0, (int)(reader.BaseStream.Length - offset))) throw Errors.ValueOutOfRange(nameof(count), 0, reader.BaseStream.Length - offset);
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void MarshalReadArray_Contract(BinaryReader br, int count)
    {
        ReadStruct_Contract(br);
        if (!count.IsBetween(0, int.MaxValue)) throw Errors.ValueOutOfRange(nameof(count), 0, int.MaxValue);
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void MarshalReadArray_Contract(BinaryReader br, long offset, int count)
    {
        ReadStruct_Contract(br);
        if (!offset.IsBetween(0, br.BaseStream.Length)) throw Errors.ValueOutOfRange(nameof(offset), 0, br.BaseStream.Length);
        if (!count.IsBetween(0, int.MaxValue)) throw Errors.ValueOutOfRange(nameof(count), 0, int.MaxValue);
    }
}
