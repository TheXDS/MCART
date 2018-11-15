/*
PluginBrowser.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TheXDS.MCART.Component;
using TheXDS.MCART.Dialogs.ViewModel;
using TheXDS.MCART.PluginSupport;

namespace TheXDS.MCART.Dialogs
{
    /// <inheritdoc cref="Window"/>
    /// <summary>
    ///     Diálogo que permite mostrar información acerca de los
    ///     <see cref="T:TheXDS.MCART.PluginSupport.Plugin" /> cargables por MCART.
    /// </summary>
    public partial class PluginBrowser
    {
        private PluginBrowserViewModel Vm => DataContext as PluginBrowserViewModel;

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Dialogs.PluginBrowser" />.
        /// </summary>
        public PluginBrowser()
        {
            InitializeComponent();
            DataContext = new PluginBrowserViewModel();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Dialogs.PluginBrowser" />.
        /// </summary>
        /// <param name="plugin">Plugin del cual mostrar información.</param>
        public PluginBrowser(IPlugin plugin) : this()
        {
            Vm.ShowPlugins = false;
            Vm.Selection = plugin;
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (!Vm.ShowPlugins) return;
            Vm.Selection = (sender as TreeView)?.SelectedItem;
        }

        private void Plugin_OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((sender as TreeView)?.SelectedItem is IExposeInfo x) AboutBox.Show(x);
        }
    }
}