/*
TypeDetailsViewModel.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Dialogs.ViewModel
{
    public class TypeDetailsViewModel : NotifyPropertyChanged
    {
        private Type _type;

        public IEnumerable<Type> BaseTypes
        {
            get
            {
                var baseType = Type;
                while (!(baseType is null))
                {
                    baseType = baseType.BaseType;
                    yield return baseType;
                }
            }
        }

        public static TypeDetailsViewModel Create => new TypeDetailsViewModel(null);

        public string DefaultValue => Type?.Default()?.ToString() ?? "null";

        public IEnumerable<Type> Inheritances => Type?.GetInterfaces();

        public IEnumerable<TypeDetailsViewModel> InheritancesVm
            => Inheritances?.Select(p => new TypeDetailsViewModel(p));

        public bool Instantiable => Type.IsInstantiable();

        public bool IsDynamic => Type.Assembly.IsDynamic;

        public bool IsStatic => Type.IsAbstract && Type.IsSealed;

        public IEnumerable<IGrouping<MemberTypes, MemberInfo>> MemberTree
        {
            get { return Type?.GetMembers().GroupBy(p => p.MemberType); }
        }

        public object NewValue => Type.IsInstantiable() ? Type.New() : null;

        public Type Type
        {
            get => _type;
            set
            {
                if (value == _type) return;
                _type = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Inheritances));
                OnPropertyChanged(nameof(InheritancesVm));
                OnPropertyChanged(nameof(BaseTypes));
                OnPropertyChanged(nameof(MemberTree));
                OnPropertyChanged(nameof(DefaultValue));
                OnPropertyChanged(nameof(Instantiable));
                OnPropertyChanged(nameof(IsStatic));
                OnPropertyChanged(nameof(IsDynamic));
                OnPropertyChanged(nameof(NewValue));
            }
        }

        public TypeDetailsViewModel()
        {
        }

        public TypeDetailsViewModel(Type type)
        {
            Type = type;
        }
    }
}