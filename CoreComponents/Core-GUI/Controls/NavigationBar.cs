/*
NavigationBar.cs

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

namespace TheXDS.MCART.Controls
{
    /// <summary>
    /// Permite la navegación y búsqueda entre una lista de elementos por medio
    /// de la GUI.
    /// </summary>
    public partial class NavigationBar
    {
        /// <summary>
        /// Determina las capacidades de edición disponibles en este control.
        /// </summary>
        [System.Flags]
        public enum EditMode : byte
        {
            /// <summary>
            /// Control de sólo lectura. El usuario podrá navegar, pero no podrá editar.
            /// </summary>
            ReadOnly,
            /// <summary>
            /// Otorga al usuario la facultad de crear nuevos elementos.
            /// </summary>
            Newable,
            /// <summary>
            /// Otorga al usuario la facultad de editar elementos.
            /// </summary>
            Editable,
            /// <summary>
            /// Otorga al usuario la facultad de crear y editar elementos.
            /// </summary>
            NewEdit,
            /// <summary>
            /// Otorga al usuario la facultad de eliminar elementos.
            /// </summary>
            Deletable,
            /// <summary>
            /// Otorga al usuario la facultad de crear y eliminar elementos.
            /// </summary>
            NewDelete,
            /// <summary>
            /// Otorga al usuario la facultad de editar y eliminar elementos.
            /// </summary>
            EditDelete,
            /// <summary>
            /// Otorga al usuario todas las facultades de edición
            /// </summary>
            All
        }
    }
}