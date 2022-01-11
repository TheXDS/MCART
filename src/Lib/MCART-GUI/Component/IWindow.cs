/*
IWindow.cs

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

using TheXDS.MCART.Types;

namespace TheXDS.MCART.Component
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que represente
    /// una ventana en cualquier sistema operativo con interfaz gráfica.
    /// </summary>
    public interface IWindow : ICloseable
    {
        /// <summary>
        /// Obtiene o establece el tamaño de la ventana.
        /// </summary>
        Size Size { get; set; }

        /// <summary>
        /// Obtiene o establece la posición de la ventana en coordenadas
        /// absolutas de pantalla.
        /// </summary>
        Point Location { get; set; }

        /// <summary>
        /// Oculta la ventana sin cerrarla.
        /// </summary>
        void Hide();

        /// <summary>
        /// Maximiza la ventana.
        /// </summary>
        void Maximize();

        /// <summary>
        /// Minimiza la ventana.
        /// </summary>
        void Minimize();

        /// <summary>
        /// Restaura el tamaño de la ventana.
        /// </summary>
        void Restore();

        /// <summary>
        /// Cambia el estado de la ventana entre Maximizar y Restaurar.
        /// </summary>
        void ToggleMaximize();
    }
}
