/*
TreeViewExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Contiene extensiones útiles para la clase <see cref="TreeView"/>.
    /// </summary>
    public static class TreeViewExtensions
    {
        /// <summary>
        ///     Deselecciona todos los ítems del <see cref="TreeView"/>.
        /// </summary>
        /// <param name="treeView"><see cref="TreeView"/> a procesar.</param>
        public static void UnSelectAll(this TreeView treeView)
        {
            foreach (var j in treeView.Items.OfType<TreeViewItem>())
                j.IsSelected = false;
        }
    }
}
