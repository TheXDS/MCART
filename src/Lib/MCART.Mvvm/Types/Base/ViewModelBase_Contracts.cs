/*
ViewModelBase_Contracts.cs

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

using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Exceptions;
using static TheXDS.MCART.Misc.Internals;
using Err = TheXDS.MCART.Resources.Errors;

namespace TheXDS.MCART.Types.Base;

public abstract partial class ViewModelBase : NotifyPropertyChanged
{
    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void Observe_Contract<T>(Expression<Func<T, object?>> propertySelector)
    {
        NullCheck(propertySelector, nameof(propertySelector));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void Observe_Contract<T>(T source, PropertyInfo property)
    {
        NullCheck(source, nameof(source));
        NullCheck(property, nameof(property));
        if (!source!.GetType().GetProperties().Contains(property)) throw new MissingMemberException(source.GetType().Name, property.Name);
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void Observe_Contract<T>(Expression<Func<T>> propertySelector)
    {
        NullCheck(propertySelector, nameof(propertySelector));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void Observe_Contract(string propertyName, Action handler)
    {
        NullCheck(propertyName, nameof(propertyName));
        NullCheck(handler, nameof(handler));

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new InvalidArgumentException(nameof(propertyName));
        }
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void BusyOp_Contract(Action action)
    {
        NullCheck(action, nameof(action));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void BusyOp_Contract(Task task)
    {
        NullCheck(task, nameof(task));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void BusyOp_Contract<T>(Func<T> function)
    {
        NullCheck(function, nameof(function));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void BusyOp_Contract<T>(Task<T> task)
    {
        NullCheck(task, nameof(task));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void OnInvokeObservedProps_Contract(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is null) throw Err.NullArgumentValue(nameof(e.PropertyName), nameof(e));
    }
}
