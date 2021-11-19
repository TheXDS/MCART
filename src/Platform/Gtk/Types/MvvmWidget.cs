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
using System.Collections.Generic;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Clase base para un <see cref="Gtk.Widget"/> que incluye funcionalidad
    /// base de MVVM. Esta clase base es útil para aplicarse a contenedores de
    /// UI (páginas, ventanas, contenedores) y no directamente a widgets
    /// indivisuales interactivos.
    /// </summary>
    [CLSCompliant(false)]
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public abstract class MvvmWidget : Gtk.Widget, IDataContext
    {
        private readonly List<GtkBinding> _bindings = new();
        private INotifyPropertyChanged? _dataContext;

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="MvvmWidget"/>.
        /// </summary>
        /// <param name="raw">
        /// Apuntador a una estructura que contiene los datos básicos de esta
        /// instancia para Gtk#. Este valor generalmente es provisto por los
        /// métodos de instanciación e inicialización de widgets de Gtk#.
        /// </param>
        protected MvvmWidget(IntPtr raw) : base(raw)
        {
        }

        /// <summary>
        /// Obtiene o establece el contexto de datos a utilizar para este
        /// Widget.
        /// </summary>
        public INotifyPropertyChanged? DataContext
        {
            get => _dataContext;
            set
            {
                _dataContext = value;
                foreach (GtkBinding? j in _bindings)
                {
                    j.UpdateValue(value);
                }
            }
        }

        private string GetDebuggerDisplay()
        {
            return $"{GetType().NameOf()} ({Handle})";
        }
    }
}