/*
ConfiguredObservingCommand_Contracts.cs

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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Helpers
{
    public partial class ConfiguredObservingCommand<T> where T : INotifyPropertyChanged
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
            if (!properties.Any()) throw Errors.EmptyCollection(properties);
        }



        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private void ListensToCanExecute_Contract(MemberInfo member, Type t)
        {
            IsBuilt_Contract();
            if (!GetAll<MemberInfo>(t).Contains(member))
            {
                throw Errors.MissingMember(t, member);
            }
        }

        private static IEnumerable<TMember> GetAll<TMember>(Type? t) where TMember : MemberInfo
        {
            if (t is null || t == typeof(object)) return Array.Empty<TMember>();
            return t.GetMembers().Concat(GetAll<TMember>(t.BaseType)).Concat(t.GetInterfaces().SelectMany(p => p.GetMembers())).OfType<TMember>();
        }
    }
}
