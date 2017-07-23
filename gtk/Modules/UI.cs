//
//  UI.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using Gdk;
using Gtk;
using System.Linq;
using MCART.Types.Extensions;
namespace MCART
{
    /// <summary>
    /// Extensiones de UI para Gtk.
    /// </summary>
    public static class UI
    {
        /// <summary>
        /// Elimina a todos los <see cref="Widget"/> hijos de este
        /// <see cref="Container"/>.
        /// </summary>
        /// <param name="c"><see cref="Container"/> a limpiar.</param>
        public static void ClearContents(this Gtk.Container c)
        {
            Widget[] x = c.Children;
            foreach (Widget j in x) c.Remove(j);
            x = null;
        }
    }
}