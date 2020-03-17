/*
PasswordDialogMode.cs

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

using System;

namespace TheXDS.MCART.Dialogs.ViewModel
{
    /// <summary>
    /// Enumeración de estados de componentes activos del ViewModel
    /// <see cref="PasswordDialogViewModelBase"/>.
    /// </summary>
    [Flags]
    public enum PasswordDialogMode : byte
    {
        /// <summary>
        /// Únicamente mostrar un cuadro de contraseña.
        /// </summary>
        PasswordOnly,
        /// <summary>
        /// Mostrar cuadro de usuario.
        /// </summary>
        User,
        /// <summary>
        /// Mostrar cuadro de confirmación.
        /// </summary>
        Confirm,
        /// <summary>
        /// Mostrar cuadro de indicio. Implica <see cref="Confirm"/>.
        /// </summary>
        Hint = 4 | Confirm,
        /// <summary>
        /// Mostrar evaluador de calidad de contraseña. Implica
        /// <see cref="Confirm"/>.
        /// </summary>
        PwQuality = 8 | Confirm,
        /// <summary>
        /// Mostrar un generador de contraseñas. Implica
        /// <see cref="Confirm"/>.
        /// </summary>
        Generator = 16 | Confirm,
    }
}