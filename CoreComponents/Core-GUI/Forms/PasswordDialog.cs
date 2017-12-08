//
//  PasswordDialog.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
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

using MCART.Security.Password;

namespace MCART.Forms
{
    /// <summary>
    /// Cuadro de diálogo que permite al usuario validar contraseñas, así como
    /// también establecerlas y medir el nivel de seguridad de la contraseña
    /// escogida.
    /// </summary>
    public partial class PasswordDialog
    {
        #region Miembros privados
        PwDialogResult retVal = PwDialogResult.Null;
        PwEvaluator pwEvaluator;
        PwEvalResult pwEvalResult;
        #endregion
    }
}
