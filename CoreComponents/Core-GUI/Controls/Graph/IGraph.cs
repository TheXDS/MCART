/*
IGraph.cs

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

namespace TheXDS.MCART.Controls
{
    /// <summary>
    /// Define una serie de métodos y propiedades a implementar por un control
    /// que permita mostrar gráficos de cualquier tipo.
    /// </summary>
    public interface IGraph
    {
        /// <summary>
        /// Realiza tareas finales de inicialización del control.
        /// </summary>
        /// <remarks>
        /// En esta función deben colocarse llamadas como 
        /// <c>InitializeComponent()</c> (Win32 y WPF) o <c>Build()</c> (Gtk#)
        /// </remarks>
        void Init();
        /// <summary>
        /// Obtiene o establece el título de este <see cref="IGraph"/>.
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// Obtiene o establece el tamaño de fuente a aplicar al título.
        /// </summary>
        double TitleFontSize { get; set; }
        /// <summary>
        /// Obtiene o establece un <see cref="IGraphColorizer"/> opcional a
        /// utilizar para establecer los colores de las series.
        /// </summary>
        IGraphColorizer Colorizer { get; set; }
        /// <summary>
        /// Solicita al control volver a dibujarse en su totalidad.
        /// </summary>
        void Redraw();
        /// <summary>
        /// Obtiene o establece un formato opcional de etiquetado flotante para
        /// aplicar a los puntos de datos o series dibujadas en este
        /// <see cref="IGraph"/>.
        /// </summary>
        string ToolTipFormat { get; set; }
    }
}