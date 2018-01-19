﻿//
//  PwDialogResult.cs
//
//  This file is part of Morgan's CLR Advanced Runtime (MCART)
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

#pragma warning disable CS0282 // No hay un orden específico entre los campos en declaraciones múltiples de la estructura parcial

using System.Security;

namespace TheXDS.MCART.Security.Password
{
    /// <summary>
    /// Representa el resultado de un cuadro de diálogo
    /// <see cref="Dialogs.PasswordDialog"/>.
    /// </summary>
    public partial struct PwDialogResult
    {
        string u;
        SecureString p;
        string h;
        PwEvalResult e;
        /// <summary>
        /// Obtiene el usuario introducido en el 
        /// <see cref="Dialogs.PasswordDialog"/>.
        /// </summary>
        /// <returns>
        /// Si se muestra este diálogo con <see cref="PwMode.Usr"/>, se 
        /// devuelve el usuario introducido en el 
        /// <see cref="Dialogs.PasswordDialog"/>; de lo contrario se devuelve
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
        public SecureString Pwd => p;
        /// <summary>
        /// Obtiene el indicio de contraseña introducido por el usuario.
        /// </summary>
        /// <returns>
        /// <see cref="string.Empty"/> si el cuadro se inicia con cualquier
        /// sobrecarga de los métodos
        /// <see cref="Dialogs.PasswordDialog.GetPassword(string, Dialogs.PasswordDialog.LoginValidator)"/>
        /// o con 
        /// <see cref="Dialogs.PasswordDialog.Login(string, string, Dialogs.PasswordDialog.LoginValidator)"/>.
        /// Si se inicia con 
        /// <see cref="Dialogs.PasswordDialog.ChoosePassword(PwMode, PwEvaluator, int)"/>,
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
        /// <summary>
        /// Evalúa si esta instancia y <paramref name="obj"/> son 
        /// iguales.
        /// </summary>
        /// <param name="obj">Valor a comparar.</param>
        /// <returns>
        /// <see langword="true"/> si ambos objetos son iguales, <see langword="false"/> en caso
        /// contrario.
        /// </returns>
        public override bool Equals(object obj) => base.Equals(obj);
        /// <summary>
        /// Obtiene el código hash de esta instancia.
        /// </summary>
        /// <returns>El código Hash que representa a esta instancia.</returns>
        public override int GetHashCode() => base.GetHashCode();
        /// <summary>
        /// Evalúa si <paramref name="left"/> y <paramref name="right"/> son 
        /// iguales.
        /// </summary>
        /// <param name="left">Valor a comparar.</param>
        /// <param name="right">Valor a comparar.</param>
        /// <returns>
        /// <see langword="true"/> si ambos objetos son iguales, <see langword="false"/> en caso
        /// contrario.
        /// </returns>
        public static bool operator ==(PwDialogResult left, PwDialogResult right)
        {
            return left.e == right.e && left.u == right.u;
        }
        /// <summary>
        /// Evalúa si <paramref name="left"/> y <paramref name="right"/> son 
        /// diferentes.
        /// </summary>
        /// <param name="left">Valor a comparar.</param>
        /// <param name="right">Valor a comparar.</param>
        /// <returns>
        /// <see langword="true"/> si los objetos son diferentes, <see langword="false"/> en caso
        /// contrario.
        /// </returns>
        public static bool operator !=(PwDialogResult left, PwDialogResult right) => !(left == right);
    }
}