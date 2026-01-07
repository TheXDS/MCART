/*
ObservingCommandExtensions_Contracts.cs

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

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Component;
using TheXDS.MCART.Resources;
using static TheXDS.MCART.Helpers.ReflectionHelpers;

namespace TheXDS.MCART.Types.Extensions;

public static partial class ObservingCommandExtensions
{
    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void ListensToProperties_Contract<TProperty>(ObservingCommand command, Expression<Func<TProperty>>[] properties)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        if (properties.Length == 0) throw Errors.EmptyCollection(properties);
        PropertyInfo[] t = command.ObservedSource.GetType().GetProperties();
        if (properties.Select(GetProperty).FirstOrDefault(p => !t.Contains(p)) is { } missingProp)
        {
            throw Errors.MissingMember(command.ObservedSource.GetType(), missingProp);
        }
    }
    
    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void ListensToProperties_Contract<T, TProperty>(ObservingCommand command, Expression<Func<T, TProperty>>[] properties)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        if (properties.Length == 0) throw Errors.EmptyCollection(properties);
        PropertyInfo[] t = command.ObservedSource.GetType().GetProperties();
        if (properties.Select(GetProperty).FirstOrDefault(p => !t.Contains(p)) is { } missingProp)
        {
            throw Errors.MissingMember(command.ObservedSource.GetType(), missingProp);
        }
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void ListensToProperty_Contract(ObservingCommand command, Expression<Func<object?>> propertySelector)
    {
        ListensToProperty_Contract(command, () => GetProperty(propertySelector), command.ObservedSource.GetType());
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void ListensToProperty_Contract<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(ObservingCommand command, Expression<Func<T, object?>> propertySelector)
    {
        ListensToProperty_Contract(command, () => GetProperty(propertySelector), typeof(T));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void ListensToCanExecute_Contract<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(ObservingCommand command, Expression<Func<T, bool>> selector)
    {
        ListensToCanExecute_Contract(command, () => GetMember(selector), typeof(T));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void ListensToCanExecute_Contract<T>(ObservingCommand command, Expression<Func<T>> selector)
    {
        ListensToCanExecute_Contract(command, () => GetMember(selector), command.ObservedSource.GetType());
    }

    private static void ListensToCanExecute_Contract(ObservingCommand command, Func<MemberInfo> selector, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type t)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));
        MemberInfo member = selector();
        if (!GetAll<MemberInfo>(t).Contains(member))
        {
            throw Errors.MissingMember(t, member);
        }
    }

    private static void ListensToProperty_Contract(ObservingCommand command, Func<PropertyInfo> propertySelector, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type t)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        ArgumentNullException.ThrowIfNull(propertySelector, nameof(propertySelector));
        PropertyInfo prop = propertySelector();
        if (!GetAll<PropertyInfo>(t).Contains(prop))
        {
            throw Errors.MissingMember(t, prop);
        }
    }

    private static IEnumerable<T> GetAll<T>([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type? t) where T : MemberInfo
    {
        if (t is null || t == typeof(object)) return [];
        return t.GetMembers().Concat(GetAll<T>(t.BaseType)).Concat(t.GetInterfaces().SelectMany(([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]p) => p.GetMembers())).OfType<T>();
    }
}
