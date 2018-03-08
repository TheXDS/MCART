/*
PwDialogResult.cs

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


using System.Security;
using System.Windows;

namespace TheXDS.MCART.Security.Password
{
#pragma warning disable CS0282 // No hay un orden específico entre los campos en declaraciones múltiples de la estructura parcial
    public partial struct PwDialogResult
    {
        /// <summary>
        /// Obtiene el resultado del cuadro de diálogo.
        /// </summary>
        /// <returns>
        /// Un <see cref="MessageBoxResult"/> que indica la acción realizada
        /// por el usuario.
        /// </returns>
        public MessageBoxResult Result { get; }
        /// <summary>
        /// Constante. Resultado de evaluación nulo.
        /// </summary>
        public static PwDialogResult Null => new PwDialogResult(null, null, null, MessageBoxResult.Cancel, PwEvalResult.Null);
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PwDialogResult"/>.
        /// </summary>
        /// <param name="us">
        /// Usuario del cuadro de diálogo.
        /// </param>
        /// <param name="pw">
        /// Contraseña introducida por el usuario.
        /// </param>
        /// <param name="hn">
        /// Indicio de contraseña introducida por el usuario.
        /// </param>
        /// <param name="re">
        /// Resultado del cuadro de diálogo.
        /// </param>
        /// <param name="ev">
        /// Resultado de la evaluación.
        /// </param>
        internal PwDialogResult(string us, SecureString pw, string hn, MessageBoxResult re, PwEvalResult ev)
        {
            User = us;
            Password = pw;
            if (!(Password?.IsReadOnly() ?? true)) Password.MakeReadOnly();
            Hint = hn;
            Result = re;
            Evaluation = ev;
        }
    }
}