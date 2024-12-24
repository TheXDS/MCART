/*
ConfiguredObservingCommand_Contracts.cs

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
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;
using static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes;

namespace TheXDS.MCART.Helpers;

public partial class ObservingCommandBuilder<T> where T : INotifyPropertyChanged
{
    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private void IsBuilt_Contract()
    {
        if (IsBuilt) throw new InvalidOperationException();
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private void ListensToProperty_Contract<TValue>(Expression<Func<T, TValue>>[] properties)
    {
        IsBuilt_Contract();
        if (properties.Length == 0) throw Errors.EmptyCollection(properties);
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private void ListensToCanExecute_Contract(MemberInfo member, [DynamicallyAccessedMembers(All)] Type t)
    {
        IsBuilt_Contract();
        if (!GetAll<MemberInfo>(t).Contains(member))
        {
            throw Errors.MissingMember(t, member);
        }
    }

    private static IEnumerable<TMember> GetAll<TMember>([DynamicallyAccessedMembers(All)]Type? t) where TMember : MemberInfo
    {
        if (t is null || t == typeof(object)) return [];
        return t.GetMembers()
            .Concat(GetAll<TMember>(t.BaseType))
            .Concat(t.GetInterfaces().SelectMany(([DynamicallyAccessedMembers(PublicConstructors | PublicFields | PublicMethods | PublicNestedTypes | PublicProperties | PublicEvents)] p) => p.GetMembers()))
            .OfType<TMember>();
    }
}
