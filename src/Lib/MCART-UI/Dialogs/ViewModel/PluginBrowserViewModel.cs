/*
PluginBrowserViewModel.cs

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

#nullable enable

using System.Collections.Generic;
using TheXDS.MCART.PluginSupport.Legacy;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Dialogs.ViewModel
{
    /// <summary>
    /// Implementa la lógica de presentación de una ventana que permite ver
    /// detalles sobre los plugins encontrados en el directorio.
    /// </summary>
    public class PluginBrowserViewModel : NotifyPropertyChanged
    {
        private object? _selection;
        private bool _showPlugins = true;

        /// <summary>
        /// Enumera los plugins encontrados en el directorio actual.
        /// </summary>
        public Dictionary<string, IEnumerable<IPlugin>> Plugins
        {
            get
            {
                if (!ShowPlugins) return new Dictionary<string, IEnumerable<IPlugin>>(0);
                try
                {
                    return new PluginLoader(new RelaxedPluginChecker(), SanityChecks.IgnoreDanger).PluginTree();
                }
                catch
                {
                    return new Dictionary<string, IEnumerable<IPlugin>>(0);
                }
            }
        }

        /// <summary>
        /// Obtiene o establece el plugin actualmente seleccionado.
        /// </summary>
        public object? Selection
        {
            get => _selection;
            set
            {
                if (!(value is IPlugin)) value = null;
                if (Equals(value, _selection)) return;
                _selection = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si el árbol de plugins
        /// debe ser visible.
        /// </summary>
        public bool ShowPlugins
        {
            get => _showPlugins;
            set
            {
                if (value == _showPlugins) return;
                _showPlugins = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Plugins));
            }
        }
    }
}