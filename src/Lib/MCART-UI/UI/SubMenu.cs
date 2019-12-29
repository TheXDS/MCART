/*
SubMenu.cs

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
using TheXDS.MCART.UI.Base;

namespace TheXDS.MCART.UI
{
    /// <summary>
    /// Representa una entrada de menú que contiene a otros elementos hijos
    /// interactivos.
    /// </summary>
    public class SubMenu : InteractionBase
    {
        /// <summary>
        /// Obtiene una secuencia de interacciones que forman parte de un 
        /// sub-menú de este elemento.
        /// </summary>
        public IEnumerable<InteractionBase> Children { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="SubMenu"/>, estableciendo las entradas de
        /// sub-menú a mostrar.
        /// </summary>
        /// <param name="name">
        /// Nombre a mostrar para el elemento.
        /// </param>
        /// <param name="subMenu">
        /// Sub-menú que forma parte de este elemento.
        /// </param>
        public SubMenu(string name, IEnumerable<InteractionBase> subMenu) : this(name, null, subMenu)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="SubMenu"/>, estableciendo las entradas de
        /// sub-menú a mostrar.
        /// </summary>
        /// <param name="name">
        /// Nombre a mostrar para este elemento.
        /// </param>
        /// <param name="description">
        /// Descripción a mostrar para este elemento.
        /// </param>
        /// <param name="subMenu">
        /// Sub-menú que forma parte de este elemento.
        /// </param>
        public SubMenu(string name,string? description, IEnumerable<InteractionBase> subMenu) : base(name, description)
        {
            Children = subMenu;
        }
    }
}