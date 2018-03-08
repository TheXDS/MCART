/*
PasswordDialog.cs

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

using TheXDS.MCART.Security.Password;

namespace TheXDS.MCART.Dialogs
{
    /// <summary>
    /// Cuadro de diálogo que permite al usuario validar contraseñas, así como
    /// también establecerlas y medir el nivel de seguridad de la contraseña
    /// escogida.
    /// </summary>
    public sealed partial class PasswordDialog : System.IDisposable
    {
        #region Miembros privados
        int passVal = 50;
        int maxTries = int.MaxValue;
        int tries = 0;
        PwDialogResult retVal = PwDialogResult.Null;
        PwEvaluator pwEvaluator;
        PwEvalResult pwEvalResult;
        LoginValidator loginVal;
        #endregion
        #region Métodos públicos
        /// <summary>
        /// Obtiene la información de inicio de sesión.
        /// </summary>
        /// <param name="loginValidator">
        /// Delegado de validación de inicio de sesión. Debe tratarse de un
        /// <see cref="System.Threading.Tasks.Task{TResult}"/> que devuelve un
        /// <see cref="bool"/>, <see langword="true"/> si el inicio de sesión
        /// fue exitoso, <see langword="false"/> en caso contrario.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si el inicio de sesión fue exitoso,
        /// <see langword="false"/> en caso contrario o al exceder la cantidad
        /// máxima de intentos, o <see langword="null"/> si el inicio de sesión
        /// fue cancelado.
        /// </returns>
        public static bool? Login(LoginValidator loginValidator) => Login(string.Empty, string.Empty, loginValidator, int.MaxValue);
        /// <summary>
        /// Obtiene la información de inicio de sesión.
        /// </summary>
        /// <param name="knownUser">
        /// Nombre de usuario a mostrar de manera predeterminada en el cuadro.
        /// </param>
        /// <param name="loginValidator">
        /// Delegado de validación de inicio de sesión. Debe tratarse de un
        /// <see cref="System.Threading.Tasks.Task{TResult}"/> que devuelve un
        /// <see cref="bool"/>, <see langword="true"/> si el inicio de sesión
        /// fue exitoso, <see langword="false"/> en caso contrario.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si el inicio de sesión fue exitoso,
        /// <see langword="false"/> en caso contrario o al exceder la cantidad
        /// máxima de intentos, o <see langword="null"/> si el inicio de sesión
        /// fue cancelado.
        /// </returns>
        public static bool? Login(string knownUser, LoginValidator loginValidator) => Login(knownUser, string.Empty, loginValidator, int.MaxValue);
        /// <summary>
        /// Obtiene la información de inicio de sesión.
        /// </summary>
        /// <param name="knownUser">
        /// Nombre de usuario a mostrar de manera predeterminada en el cuadro.
        /// </param>
        /// <param name="knownPassword">
        /// Proporciona una contraseña predeterminada para esta ventana.
        /// </param>
        /// <param name="loginValidator">
        /// Delegado de validación de inicio de sesión. Debe tratarse de un
        /// <see cref="System.Threading.Tasks.Task{TResult}"/> que devuelve un
        /// <see cref="bool"/>, <see langword="true"/> si el inicio de sesión
        /// fue exitoso, <see langword="false"/> en caso contrario.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si el inicio de sesión fue exitoso,
        /// <see langword="false"/> en caso contrario o al exceder la cantidad
        /// máxima de intentos, o <see langword="null"/> si el inicio de sesión
        /// fue cancelado.
        /// </returns>
        public static bool? Login(string knownUser, string knownPassword, LoginValidator loginValidator) => Login(knownUser, knownPassword, loginValidator, int.MaxValue);
        /// <summary>
        /// Obtiene la información de inicio de sesión.
        /// </summary>
        /// <param name="loginValidator">
        /// Delegado de validación de inicio de sesión. Debe tratarse de un
        /// <see cref="System.Threading.Tasks.Task{TResult}"/> que devuelve un
        /// <see cref="bool"/>, <see langword="true"/> si el inicio de sesión
        /// fue exitoso, <see langword="false"/> en caso contrario.
        /// </param>
        /// <param name="maxTries">
        /// Cantidad máxima de intentos. El inicio de sesión se cancela si el
        /// usuario excede esta cantidad de intentos inválidos.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si el inicio de sesión fue exitoso,
        /// <see langword="false"/> en caso contrario o al exceder la cantidad
        /// máxima de intentos, o <see langword="null"/> si el inicio de sesión
        /// fue cancelado.
        /// </returns>
        public static bool? Login(LoginValidator loginValidator, int maxTries) => Login(string.Empty, string.Empty, loginValidator, maxTries);
        /// <summary>
        /// Obtiene la información de inicio de sesión.
        /// </summary>
        /// <param name="knownUser">
        /// Nombre de usuario a mostrar de manera predeterminada en el cuadro.
        /// </param>
        /// <param name="loginValidator">
        /// Delegado de validación de inicio de sesión. Debe tratarse de un
        /// <see cref="System.Threading.Tasks.Task{TResult}"/> que devuelve un
        /// <see cref="bool"/>, <see langword="true"/> si el inicio de sesión
        /// fue exitoso, <see langword="false"/> en caso contrario.
        /// </param>
        /// <param name="maxTries">
        /// Cantidad máxima de intentos. El inicio de sesión se cancela si el
        /// usuario excede esta cantidad de intentos inválidos.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si el inicio de sesión fue exitoso,
        /// <see langword="false"/> en caso contrario o al exceder la cantidad
        /// máxima de intentos, o <see langword="null"/> si el inicio de sesión
        /// fue cancelado.
        /// </returns>
        public static bool? Login(string knownUser, LoginValidator loginValidator, int maxTries) => Login(knownUser, string.Empty, loginValidator, maxTries);
        /// <summary>
        /// Obtiene una contraseña.
        /// </summary>
        /// <param name="loginValidator">
        /// Delegado de validación de la contraseña. Debe tratarse de un
        /// <see cref="System.Threading.Tasks.Task{TResult}"/> que devuelve un
        /// <see cref="bool"/>, <see langword="true"/> si el inicio de sesión
        /// fue exitoso, <see langword="false"/> en caso contrario.
        /// </param>
        /// <param name="maxTries">
        /// Cantidad máxima de intentos. El inicio de sesión se cancela si el
        /// usuario excede esta cantidad de intentos inválidos.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si la contraseña es válida,
        /// <see langword="false"/> en caso contrario o al exceder la cantidad
        /// máxima de intentos, o <see langword="null"/> si el inicio de sesión
        /// fue cancelado.
        /// </returns>
        public static bool? GetPassword(LoginValidator loginValidator, int maxTries) => GetPassword(string.Empty, loginValidator, maxTries);
        /// <summary>
        /// Obtiene una contraseña.
        /// </summary>
        /// <param name="knownPassword">
        /// Proporciona una contraseña predeterminada para esta ventana.
        /// </param>
        /// <param name="loginValidator">
        /// Delegado de validación de la contraseña. Debe tratarse de un
        /// <see cref="System.Threading.Tasks.Task{TResult}"/> que devuelve un
        /// <see cref="bool"/>, <see langword="true"/> si el inicio de sesión
        /// fue exitoso, <see langword="false"/> en caso contrario.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si la contraseña es válida,
        /// <see langword="false"/> en caso contrario o al exceder la cantidad
        /// máxima de intentos, o <see langword="null"/> si el inicio de sesión
        /// fue cancelado.
        /// </returns>
        public static bool? GetPassword(string knownPassword,LoginValidator loginValidator) => GetPassword(knownPassword, loginValidator, int.MaxValue);
        /// <summary>
        /// Permite al usuario escoger una contraseña.
        /// </summary>
        /// <param name="pwEvaluator">
        /// Parámetro opcional. Objeto evaluador de contraseñas a utilizar. Si
        /// se omite, se utilizará un evaluador con un conjunto de reglas 
        /// predeterminado.
        /// </param>
        /// <returns>
        /// Un <see cref="PwDialogResult"/> con el resultado de este diálogo.
        /// </returns>
        public static PwDialogResult ChoosePassword(PwEvaluator pwEvaluator) => ChoosePassword(PwMode.Secur, pwEvaluator, 50);
        /// <summary>
        /// Permite al usuario escoger una contraseña.
        /// </summary>
        /// <param name="pwEvaluator">
        /// Parámetro opcional. Objeto evaluador de contraseñas a utilizar. Si
        /// se omite, se utilizará un evaluador con un conjunto de reglas 
        /// predeterminado.
        /// </param>
        /// <param name="passValue">
        /// Parámetro opcional. Determina la puntuación mínima requerida para 
        /// aceptar una contraseña. De forma predeterminada, se establece en
        /// un puntaje de al menos 50%.
        /// </param>
        /// <returns>
        /// Un <see cref="PwDialogResult"/> con el resultado de este diálogo.
        /// </returns>
        public static PwDialogResult ChoosePassword(PwEvaluator pwEvaluator, int passValue) => ChoosePassword(PwMode.Secur, pwEvaluator, passValue);
        /// <summary>
        /// Permite al usuario escoger una contraseña.
        /// </summary>
        /// <param name="mode">
        /// Parámetro opcional. Establece las opciones disponibles para esta 
        /// ventana. De forma predeterminada, únicamente se mostrará un cuadro
        /// para confirmar la contraseña.
        /// </param>
        /// <returns>
        /// Un <see cref="PwDialogResult"/> con el resultado de este diálogo.
        /// </returns>
        public static PwDialogResult ChoosePassword(PwMode mode) => ChoosePassword(mode, null, 50);
        #endregion
        #region IDisposable
        private bool disposedValue = false;
        /// <summary>
        /// Implementa la interfaz <see cref="System.IDisposable"/>
        /// </summary>
        /// <param name="disposing">
        /// Liberar recursos administrados.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing) { /* Nada que hacer, no existen objetos descartables */}
                retVal = default;
                pwEvaluator = null;
                pwEvalResult = default;
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
