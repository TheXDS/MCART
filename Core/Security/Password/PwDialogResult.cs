//
//  PwDialogResult.cs
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

namespace MCART.Security.Password
{
    /// <summary>
    /// Representa el resultado de un cuadro de diálogo
    /// <see cref="Forms.PasswordDialog"/>.
    /// </summary>
    public sealed partial class PwDialogResult
    {
        //TODO: considerar cambiar los campos de string a SecureString.
        private string u;       
        private string p;
        private string h;
        private PwEvalResult e;
        /// <summary>
        /// Obtiene el usuario introducido en el 
        /// <see cref="Forms.PasswordDialog"/>.
        /// </summary>
        /// <returns>
        /// Si se muestra este diálogo con <see cref="PwMode.Usr"/>, se 
        /// devuelve el usuario introducido en el 
        /// <see cref="Forms.PasswordDialog"/>; de lo contrario se devuelve
        /// <see cref="string.Empty"/>.
        /// </returns>
        public string Usr => u;
        /// <summary>
        /// Obtiene la contraseña que el usuario ha introducido.
        /// </summary>
        /// <returns>
        /// Un <see cref="string"/> con la contraseña que el usuario ha 
        /// introducido.
        /// </returns>
        public string Pwd => p;
        /// <summary>
        /// Obtiene el indicio de contraseña introducido por el usuario.
        /// </summary>
        /// <returns>
        /// <see cref="string.Empty"/> si el cuadro se inicia con 
        /// <see cref="Forms.PasswordDialog.GetPassword(string, string, bool)"/>.
        /// Si se inicia con 
        /// <see cref="Forms.PasswordDialog.ChoosePassword(PwMode, PwEvaluator)"/>,
        /// se devuelve un <see cref="string"/> con el indicio de contraseña 
        /// que el usuario ha introducido.
        /// </returns>
        public string Hint => h;
        /// <summary>
        /// Obtiene el resultado de la evaluación de la contraseña.
        /// </summary>
        /// <returns>
        /// Si se muestra este diálogo con <see cref="PwMode.Secur"/>, se
        /// devuelve el resultado de la evaluación de las reglas especificadas;
        /// de lo contrario se devuelve <see cref="PwEvalResult.Null"/>.
        /// </returns>
        public PwEvalResult Evaluation => e;
    }
}