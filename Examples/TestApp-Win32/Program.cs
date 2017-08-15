//
//  AssemblyInfo.cs
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

using System;
using System.Diagnostics;
using System.Windows.Forms;
using static MCART.Resources.RTInfo;
using St = MCART.Resources.Strings;

namespace TestApp_Win32
{
    /// <summary>
    /// Esta clase contiene al punto de entrada principal para la aplicación.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            switch(RTSupport(typeof(Program).Assembly))
            {
                case false:
                    Debug.WriteLine(St.Warn($"Esta aplicación no es compatible con MCART {RTVersion.ToString()}."));
                    return;
                case null:
                    Debug.WriteLine(St.Warn("No se pudo determinar la compatibilidad de esta aplicación."));
                    break;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}