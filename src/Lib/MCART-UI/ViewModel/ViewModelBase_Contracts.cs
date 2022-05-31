﻿/*
ViewModelBase_Contracts.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

namespace TheXDS.MCART.ViewModel;
using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Base;
using static TheXDS.MCART.Misc.Internals;

public abstract partial class ViewModelBase : NotifyPropertyChanged, IViewModel
{
    [Conditional("EnforceContracts")]
    private static void Observe_Contract<T>(Expression<Func<T, object?>> propertySelector)
    {
        NullCheck(propertySelector, nameof(propertySelector));
    }

    [Conditional("EnforceContracts")]
    private static void Observe_Contract<T>(T source, PropertyInfo property)
    {
        NullCheck(source, nameof(source));
        NullCheck(property, nameof(property));
        if (!source!.GetType().GetProperties().Contains(property)) throw new MissingMemberException(source.GetType().Name, property.Name);
    }

    [Conditional("EnforceContracts")]
    private void Observe_Contract<T>(Expression<Func<T>> propertySelector)
    {
        NullCheck(propertySelector, nameof(propertySelector));
    }

    [Conditional("EnforceContracts")]
    private void Observe_Contract(string propertyName, Action handler)
    {
        NullCheck(propertyName, nameof(propertyName));
        NullCheck(handler, nameof(handler));

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new InvalidArgumentException(nameof(propertyName));
        }
    }

    [Conditional("EnforceContracts")]
    private static void BusyOp_Contract(Action action)
    {
        NullCheck(action, nameof(action));
    }

    [Conditional("EnforceContracts")]
    private static void BusyOp_Contract(Task task)
    {
        NullCheck(task, nameof(task));
    }

    [Conditional("EnforceContracts")]
    private static void BusyOp_Contract<T>(Func<T> func)
    {
        NullCheck(func, nameof(func));
    }

    [Conditional("EnforceContracts")]
    private static void BusyOp_Contract<T>(Task<T> task)
    {
        NullCheck(task, nameof(task));
    }
}
