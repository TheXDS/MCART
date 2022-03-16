/*
BinaryWriterExtensions_Contracts.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

namespace TheXDS.MCART.Types.Factory;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;

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

    private static void WriteStruct_Contract(this BinaryWriter bw)
    {
        Internals.NullCheck(bw, nameof(bw));
    }
}
