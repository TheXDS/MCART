//
//  PwDialogResult.cs
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

#pragma warning disable CS0282 // No hay un orden específico entre los campos en declaraciones múltiples de la estructura parcial

using System.Windows.Forms;

namespace MCART.Security.Password
{
    public partial struct PwDialogResult
    {
        DialogResult r;
        /// <summary>
        /// Obtiene el resultado del cuadro de diálogo.
        /// </summary>
        /// <returns>
        /// Un <see cref="DialogResult"/> que indica la acción realizada
        /// por el usuario.
        /// </returns>
        public DialogResult Result => r;
        /// <summary>
        /// Constante. Resultado de evaluación nulo.
        /// </summary>
        public static PwDialogResult Null => new PwDialogResult(null, null, null, DialogResult.Cancel, PwEvalResult.Null);
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
        internal PwDialogResult(string us, string pw, string hn, DialogResult re, PwEvalResult ev)
        {
            u = us;
            p = pw;
            h = hn;
            r = re;
            e = ev;
        }
    }
}