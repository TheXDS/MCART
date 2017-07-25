//
//  UITools.cs
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

using MCART.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using St = MCART.Resources.Strings;

namespace MCART
{
    /// <summary>
    /// Contiene varias herramientas de UI para utilizar en proyectos de
    /// Win32.
    /// </summary>
    public static partial class UI
    {
        private static ErrorProvider errP = new ErrorProvider();
        /// <summary>
        /// Establece un estado de error para un control.
        /// </summary>
        /// <param name="c">Control a advertir.</param>
        /// <param name="ttip">
        /// <see cref="ToolTip"/> con un mensaje de error para mostrar.
        /// </param>
        public static void Warn(this Control c, string ttip = null)        
            => errP.SetError(c, ttip ?? St.InvalidData);        
        /// <summary>
        /// Quita el estado de error de un control.
        /// </summary>
        /// <param name="c">Control a limpiar.</param>
        public static void ClearWarn(this Control c)
            => errP.SetError(c, string.Empty);
        /// <summary>
        /// Obtiene un valor que determina si el control está advertido.
        /// </summary>
        /// <param name="c">Control a comprobar.</param>
        /// <returns><c>true</c> si el control está mostrando una advertencia;
        /// de lo contrario, <c>false</c>.</returns>
        public static bool IsWarned(this Control c)
            => !string.IsNullOrEmpty(errP.GetError(c));
        /// <summary>
        /// Oculta una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="Control"/> a ocultar.
        /// </param>
        public static void HideControls(params Control[] ctrls)
        {
            foreach (Control j in ctrls) j.Visible = false;
        }
        /// <summary>
        /// Oculta una lista de controles.
        /// </summary>
        /// <param name="ctrls"
        /// >Arreglo de <see cref="Control"/> a ocultar.
        /// </param>
        [Thunk]
        public static void HideControls(this IEnumerable<Control> ctrls)
            => HideControls(ctrls.ToArray());
        /// <summary>
        /// Muestra una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="Control"/> a mostrar.
        /// </param>
        public static void ShowControls(params Control[] ctrls)
        {
            foreach (Control j in ctrls) j.Visible = true;
        }
        /// <summary>
        /// Muestra una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="Control"/> a mostrar.
        /// </param>
        [Thunk]
        public static void ShowControls(this IEnumerable<Control> ctrls)
            => ShowControls(ctrls.ToArray());
        /// <summary>
        /// Deshabilita una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="Control"/> a deshabilitar.
        /// </param>
        public static void DisableControls(params Control[] ctrls)
        {
            foreach (Control j in ctrls) j.Enabled = false;
        }
        /// <summary>
        /// Deshabilita una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="Control"/> a deshabilitar.
        /// </param>
        [Thunk]
        public static void DisableControls(this IEnumerable<Control> ctrls)
            => DisableControls(ctrls.ToArray());
        /// <summary>
        /// Habilita una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="Control"/> a habilitar.
        /// </param>
        public static void EnableControls(params Control[] ctrls)
        {
            foreach (Control j in ctrls) j.Enabled = true;
        }
        /// <summary>
        /// Habilita una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="Control"/> a habilitar.
        /// </param>
        [Thunk]
        public static void EnableControls(this IEnumerable<Control> ctrls)
            => EnableControls(ctrls.ToArray());
        /// <summary>
        /// Habilita o deshabilita una lista de controles según su estado
        /// previo.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="Control"/> a habilitar/deshabilitar.
        /// </param>
        public static void ToggleControls(params Control[] ctrls)
        {
            foreach (Control j in ctrls) j.Enabled = !j.Enabled;
        }
        /// <summary>
        /// Habilita o deshabilita una lista de controles según su estado
        /// previo.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="Control"/> a habilitar/deshabilitar.
        /// </param>
        [Thunk]
        public static void ToggleControls(this IEnumerable<Control> ctrls)
            => ToggleControls(ctrls.ToArray());
    }
}