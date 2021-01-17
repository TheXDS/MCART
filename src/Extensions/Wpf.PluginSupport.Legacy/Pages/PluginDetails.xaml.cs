﻿/*
PluginDetails.cs

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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TheXDS.MCART.Pages
{
    /// <summary>
    /// Página de detalles de plugins.
    /// </summary>
    public partial class PluginDetails
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="T:TheXDS.MCART.Pages.PluginDetails" />.
        /// </summary>
        public PluginDetails()
        {
            InitializeComponent();
        }

        private void LstInterfaces_OnDblClick(object sender, MouseButtonEventArgs e)
        {
            var i = sender as ListViewItem ?? throw new InvalidOperationException();
            if (!(i.Content is Type t)) return;
            new Window
            {
                Content = new TypeDetails(t)
            }.ShowDialog();
        }
    }
}