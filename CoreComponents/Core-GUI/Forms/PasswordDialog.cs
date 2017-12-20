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
    public partial class PasswordDialog : System.IDisposable
    {
        /// <summary>
        /// Delegado que permite verificar una contraseña sin cerrar el
        /// <see cref="PasswordDialog"/>.
        /// </summary>
        /// <param name="user">Usuario a verificar.</param>
        /// <param name="password">Contraseña a verificar.</param>
        /// <returns>
        /// <c>true</c> si la información de inicio de sesión es válida, 
        /// <c>false</c> en caso contrario.</returns>
        public delegate bool LoginValidator(string user, System.Security.SecureString password);
        #region Miembros privados
        PwDialogResult retVal = PwDialogResult.Null;
        PwEvaluator pwEvaluator;
        PwEvalResult pwEvalResult;
        LoginValidator loginVal;
        #endregion
        #region Métodos públicos
        /// <summary>
        /// Obtiene la información de inicio de sesión.
        /// </summary>
        /// <param name="defaultUsr">
        /// Parámetro opcional. Nombre de usuario a mostrar de manera 
        /// predeterminada en el cuadro.
        /// </param>
        /// <param name="loginValidator">
        /// Parámetro opcional. Delegado de validación de la contraseña.
        /// </param>
        /// <returns>
        /// Un <see cref="PwDialogResult"/> con el resultado de esta función.
        /// </returns>
        public PwDialogResult Login(string defaultUsr, LoginValidator loginValidator = null)
        {
            return Login(defaultUsr, string.Empty, loginValidator);
        }
        /// <summary>
        /// Obtiene la información de inicio de sesión.
        /// </summary>
        /// <param name="loginValidator">
        /// Parámetro opcional. Delegado de validación de la contraseña.
        /// </param>
        /// <returns>
        /// Un <see cref="PwDialogResult"/> con el resultado de esta función.
        /// </returns>
        public PwDialogResult Login(LoginValidator loginValidator = null)
        {
            return Login(string.Empty, string.Empty, loginValidator);
        }
        /// <summary>
        /// Obtiene una contraseña.
        /// </summary>
        /// <param name="loginValidator">
        /// Parámetro opcional. Delegado de validación de la contraseña.
        /// </param>
        /// <returns>
        /// Un <see cref="PwDialogResult"/> con el resultado de esta función.
        /// </returns>
        public PwDialogResult GetPassword(LoginValidator loginValidator = null)
        {
            return GetPassword(string.Empty, loginValidator);
        }
        #endregion
        #region IDisposable
        private bool disposedValue = false;
        /// <summary>
        /// Implementa la interfaz <see cref="System.IDisposable"/>
        /// </summary>
        /// <param name="disposing">
        /// Liberar recursos administrados.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing) { /* Nada que hacer, no existen objetos descartables */}
                retVal = default(PwDialogResult);
                pwEvaluator = null;
                pwEvalResult = default(PwEvalResult);
                loginVal = null;
                disposedValue = true;
            }
        }
        /// <summary>
        /// Libera los recursos utilizados por esta instancia de 
        /// <see cref="PasswordDialog"/>.
        /// </summary>
        public void Dispose() => Dispose(true);
        #endregion
    }
}
