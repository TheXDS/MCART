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
using Gdk;
using Gtk;
using System.Collections.Generic;
namespace MCART.UI
{
    public static class UITools
    {
        sealed class OrigCtrlColor
        {
            internal Widget rf;
            internal string ttip;
        }
        static List<OrigCtrlColor> origctrls = new List<OrigCtrlColor>();
        public static void WarnCtrl(this Widget control, string tooltip = null)
        {
            origctrls.Add(new OrigCtrlColor
            {
                rf = control,
                ttip = control.TooltipText
            });
            control.ModifyFg(StateType.Normal, new Color(255, 160, 160));
            control.ModifyBg(StateType.Normal, new Color(128, 0, 0));
        }
        /// <summary>
        /// Mezcla un color de temperatura basado en el porcentaje.
        /// </summary>
        /// <returns>El color qaue representa la temperatura del porcentaje.</returns>
        /// <param name="x">Valor porcentual utilizado para calcular la temperatura.</param>
        public static Color BlendHeatColor(double x)
        {
            byte r = (byte)(1020 * (x + 0.5) - 1020).Clamp(255, 0);
            byte g = (byte)((-System.Math.Abs(2040 * (x - 0.5)) + 1020) / 2).Clamp(255, 0);
            byte b = (byte)(-1020 * (x + 0.5) + 1020).Clamp(255, 0);
            return new Color(r, g, b);
        }
        /// <summary>
        /// Mezcla un color de salud basado en el porcentaje.
        /// </summary>
        /// <returns>El color qaue representa la salud del porcentaje.</returns>
        /// <param name="x">The x coordinate.</param>
        public static Color BlendHealthColor(double x)
        {
            byte g = (byte)(510 * x).Clamp(255, 0);
            byte r = (byte)(510 - (510 * x)).Clamp(255, 0);
            return new Color(r, g, 0);
        }
    }
}