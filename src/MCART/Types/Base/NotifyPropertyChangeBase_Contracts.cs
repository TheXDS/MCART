/*
NotifyPropertyChangeBase_Contracts.cs

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
using System.Runtime.CompilerServices;
using static TheXDS.MCART.Misc.Internals;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Types.Extensions.DictionaryExtensions;

namespace TheXDS.MCART.Types.Base
{
    public abstract partial class NotifyPropertyChangeBase
    {
        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private void RegisterPropertyChangeBroadcast_Contract(string property, string[] affectedProperties)
        {
            if (property is null) throw new ArgumentNullException(nameof(property));
            if (property.IsEmpty()) throw Errors.InvalidValue(nameof(property));
            if (affectedProperties is null) throw new ArgumentNullException(nameof(affectedProperties));
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
    }
}