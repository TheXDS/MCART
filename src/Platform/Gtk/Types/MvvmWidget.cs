/*
MvvmWidget.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be
useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.ComponentModel;
using System.Diagnostics;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.ViewModel;
using System.Reflection;
using System.Collections.Generic;

namespace TheXDS.MCART.Types
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public abstract class MvvmWidget : Gtk.Widget, IDataContext
    {
        private List<GtkBinding> _bindings = new List<GtkBinding>();
        private INotifyPropertyChanged? _dataContext;


        protected MvvmWidget(IntPtr raw) : base(raw)
        {
        }

        public INotifyPropertyChanged? DataContext
        {
            get => _dataContext;
            set
            {
                _dataContext = value;
                foreach (var j in _bindings)
                {
                    j.UpdateValue(value);
                }
            }
        }

        private string GetDebuggerDisplay()
        {
            return $"{GetType().NameOf()} ({this.Handle})";
        }
    }

    public class GtkBinding
    {
        public Type SourceType {get;}
        public Type TargetType {get;}
        public PropertyInfo SourceProperty {get;}
        public PropertyInfo TargetProperty {get;}

        public object Target {get;}

        public void UpdateValue(object? source)
        {
            TargetProperty.SetValue(Target, source != null ? SourceProperty.GetValue(source) : SourceProperty.PropertyType.Default());
        }
    }
}