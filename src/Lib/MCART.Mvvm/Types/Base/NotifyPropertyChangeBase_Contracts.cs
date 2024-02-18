﻿/*
NotifyPropertyChangeBase_Contracts.cs

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

using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Types.Base;

public abstract partial class NotifyPropertyChangeBase
{
    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private void RegisterPropertyChangeBroadcast_Contract(string property, string[] affectedProperties)
    {
        EmptyCheck(property, nameof(property));
        NullCheck(affectedProperties, nameof(affectedProperties));
        if (affectedProperties.Length == 0) throw Errors.EmptyCollection(affectedProperties);
        if (affectedProperties.Any(StringExtensions.IsEmpty)) throw Errors.InvalidValue(nameof(affectedProperties));
        if (affectedProperties
            .Where(j => _observeTree.ContainsKey(j))
            .Any(j => BranchScanFails(property, j, _observeTree, new HashSet<string>())))
        {
            throw Errors.CircularOpDetected();
        }
    }

    private static bool BranchScanFails<T>(T a, T b, IDictionary<T, ICollection<T>> tree, ICollection<T> keysChecked) where T : notnull
    {
        if (!tree.ContainsKey(b)) return false;
        foreach (T? j in tree[b])
        {
            if (keysChecked.Contains(j)) return false;
            keysChecked.Add(j);
            if (j.Equals(a)) return true;
            if (BranchScanFails(a, j, tree, keysChecked)) return true;
        }
        return false;
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private protected void Change_Contract(string propertyName, PropertyInfo p)
    {
        EmptyCheck(propertyName, nameof(propertyName));
        if (p.Name != propertyName) throw MvvmErrors.PropChangeSame();
    }
}
