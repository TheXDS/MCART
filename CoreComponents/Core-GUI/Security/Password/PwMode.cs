/*
PwMode.cs

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

namespace TheXDS.MCART.Security.Password
{
    /// <summary>
    /// Configura el cuadro de contraseña al utilizar la función 
    /// <see cref="Dialogs.PasswordDialog.ChoosePassword(PwMode, PwEvaluator, int)"/>.
    /// </summary>
    [System.Flags]
    public enum PwMode : byte
    {
        /// <summary>
        /// Únicamente confirmar
        /// </summary>
        JustConfirm,
        /// <summary>
        /// Mostrar cuadro de indicio
        /// </summary>
        Hint,
        /// <summary>
        /// Mostrar indicador de seguridad
        /// </summary>
        Secur,
        /// <summary>
        /// Mostrar cuadro de indicio e indicador de seguridad
        /// </summary>
        Both,
        /// <summary>
        /// Mostrar cuadro de usuario
        /// </summary>
        Usr,
        /// <summary>
        /// Mostrar cuadro de usuario y cuadro de indicio
        /// </summary>
        UsrHint,
        /// <summary>
        /// Mostrar cuadro de usuario e indicador de seguridad
        /// </summary>
        UsrSecur,
        /// <summary>
        /// Mostrar cuadro de usuario, cuadro de indicio e indicador de seguridad
        /// </summary>
        UsrBoth
    }
}