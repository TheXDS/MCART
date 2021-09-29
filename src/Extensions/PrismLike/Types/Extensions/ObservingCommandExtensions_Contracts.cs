/*
ObservingCommandExtensions.cs

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

using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Resources;
using TheXDS.MCART.ViewModel;
using static TheXDS.MCART.Helpers.ReflectionHelpers;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Types.Extensions
{
    public static partial class ObservingCommandExtensions
    {
        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void ListensToProperties_Contract<T>(ObservingCommand command, Expression<Func<T>>[] properties)
        {
            NullCheck(command, nameof(command));
            if (!properties.Any()) throw Errors.EmptyCollection(properties);
            var t = command.ObservedSource.GetType().GetProperties();
            if (properties.Select(GetProperty).FirstOrDefault(p => !t.Contains(p)) is { } missingProp)
            {
                throw Errors.MissingMember(command.ObservedSource.GetType(), missingProp);
            }
        }

        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void ListensToProperty_Contract<T>(ObservingCommand command, Expression<Func<T>> propertySelector)
        {
            ListensToProperty_Contract(command, () => GetProperty(propertySelector), command.ObservedSource.GetType());
        }

        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void ListensToProperty_Contract<T>(ObservingCommand command, Expression<Func<T, object?>> propertySelector)
        {
            ListensToProperty_Contract(command, () => GetProperty(propertySelector), typeof(T));
        }

        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void ListensToCanExecute_Contract<T>(ObservingCommand command, Expression<Func<T, bool>> selector)
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

        private static void ListensToCanExecute_Contract(ObservingCommand command, Func<MemberInfo> selector, Type t)
        {
            NullCheck(command, nameof(command));
            NullCheck(selector, nameof(selector));
            var member = selector();
            if (!t.GetMembers().Contains(member))
            {
                throw Errors.MissingMember(t, member);
            }
        }

        private static void ListensToProperty_Contract(ObservingCommand command, Func<PropertyInfo> propertySelector, Type t)
        {
            NullCheck(command, nameof(command));
            NullCheck(propertySelector, nameof(propertySelector));
            var prop = propertySelector();
            if (!t.GetProperties().Contains(prop))
            {
                throw Errors.MissingMember(t, prop);
            }
        }
    }
}
