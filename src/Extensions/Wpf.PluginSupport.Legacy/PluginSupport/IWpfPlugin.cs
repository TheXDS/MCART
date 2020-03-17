/*
IWpfPlugin.cs

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

using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace TheXDS.MCART.PluginSupport.Legacy
{
    /// <inheritdoc />
    /// <summary>
    /// Describe una serie de miembros a implementar por una clase que
    /// represente a un <see cref="IPlugin" /> con extensiones para Windows
    /// Presentation Framework.
    /// </summary>
    public interface IWpfPlugin : IPlugin
    {
        /// <summary>
        /// Convierte el <see cref="IPlugin.PluginInteractions"/> en un 
        /// <see cref="MenuItem"/>.
        /// </summary>
        /// <returns>
        /// Un <see cref="MenuItem"/> que se puede agregar a la interfaz
        /// gráfica de la aplicación durante la ejecución.
        /// </returns>
        MenuItem UiMenu { get; }
        /// <summary>
        /// Convierte el <see cref="IPlugin.PluginInteractions"/> en un 
        /// <see cref="Panel"/>.
        /// </summary>
        /// <returns>
        /// Un <see cref="Panel"/> que se puede agregar a la interfaz gráfica
        /// de la aplicación durante la ejecución.
        /// </returns>
        /// <typeparam name="T">
        /// Tipo de controles a incluir dentro del panel. Debe heredar
        /// <see cref="ButtonBase"/>.</typeparam>
        /// <typeparam name="TPanel">
        /// Tipo de panel a generar. Debe heredar <see cref="Panel"/>.
        /// </typeparam>
        TPanel UiPanel<T, TPanel>()
            where T : ButtonBase, new() where TPanel : Panel, new();
    }
}