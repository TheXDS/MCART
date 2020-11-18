/*
ViewModelBase.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Resources.UI;
using TheXDS.MCART.Types.Base;
using St = TheXDS.MCART.Resources.UI.ErrorStrings;

namespace TheXDS.MCART.ViewModel
{
    public abstract partial class ViewModelBase : NotifyPropertyChanged, IViewModel
    { 
        [Conditional("EnforceContracts")]
        private static void Observe_Contract<T>(Expression<Func<T>> propertySelector, Action handler)
        {
            if (propertySelector is null)
            {
                throw new ArgumentNullException(nameof(propertySelector));
            }
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }
        }

        [Conditional("EnforceContracts")]
        private void Observe_Contract(string propertyName, Action handler)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new InvalidArgumentException(nameof(propertyName));
            }
            if (GetType().GetProperty(propertyName) is null)
            {
                throw new MissingMemberException();
            }
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }
        }

        [Conditional("EnforceContracts")]
        private static void BusyOp_Contract(Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
        }

        [Conditional("EnforceContracts")]
        private static void BusyOp_Contract(Task task)
        {
            if (task is null)
            {
                throw new ArgumentNullException(nameof(task));
            }
        }

        [Conditional("EnforceContracts")]
        private static void BusyOp_Contract<T>(Func<T> func)
        {
            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }
        }

        [Conditional("EnforceContracts")]
        private static void BusyOp_Contract<T>(Task<T> task)
        {
            if (task is null)
            {
                throw new ArgumentNullException(nameof(task));
            }
        }
    }
}