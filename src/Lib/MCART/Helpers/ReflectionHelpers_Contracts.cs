/*
ReflectionHelpers_Contracts.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de manipulación de objetos, 

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

using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Exceptions;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Funciones auxiliares de reflexión.
/// </summary>
public static partial class ReflectionHelpers
{
    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void FieldsOf_Contract(IEnumerable<FieldInfo> fields, object? instance)
    {
        NullCheck(fields, nameof(fields));
        if (fields.IsAnyNull()) throw new NullItemException();
        if (instance is { } obj)
        {
            FieldInfo[]? f = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (FieldInfo? j in fields.Where(p => !p.IsStatic))
            {
                if (!f.Contains(j)) throw new MissingFieldException(obj.GetType().Name, j.Name);
            }
        }
    }
    
    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void FieldsOf_Contract(Type type)
    {
        NullCheck(type, nameof(type));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void FindAllObjects_Contract(IEnumerable? ctorArgs, Func<Type, bool> typeFilter)
    {
        NullCheck(typeFilter, nameof(typeFilter));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void FindFirstObject_Contract(IEnumerable? ctorArgs, Func<Type, bool> typeFilter)
    {
        NullCheck(typeFilter, nameof(typeFilter));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void FindSingleObject_Contract(IEnumerable? ctorArgs, Func<Type, bool> typeFilter)
    {
        NullCheck(typeFilter, nameof(typeFilter));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void FindType_Contract(string identifier, AppDomain domain)
    {
        EmptyCheck(identifier, nameof(identifier));
        NullCheck(domain, nameof(domain));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void GetCallingMethod_Contract(int nCaller)
    {
        if (checked(nCaller++) < 1) throw new ArgumentOutOfRangeException(nameof(nCaller));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void PublicTypes_Contract(AppDomain domain)
    {
        NullCheck(domain, nameof(domain));
    }
 
    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void PublicTypes_Contract(Type type, AppDomain domain)
    {
        NullCheck(type, nameof(type));
        NullCheck(domain, nameof(domain));
    }
}
