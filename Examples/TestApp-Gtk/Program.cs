﻿//
//  Program.cs
//
//  Author:
//       César Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2017 César Morgan
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

#region Opciones de compilación

// Requerir una versión compatible de MCART
//#define RequireSupportedMCARTVersion

#endregion

using System;
using Gtk;
using static MCART.Resources.RTInfo;

namespace TestAppGtk
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            switch (!RTSupport(typeof(MainClass).Assembly))
            {
                case false:
                    Console.WriteLine(
                        "/!\\ Esta aplicación no es compatible con MCART " +
                        $"{RTVersion.ToString()}. La aplicación podría fallar. " +
                        "Actualice la aplicación y/o MCART.");

                    break;
                case null:
                    Console.WriteLine(
                        "/!\\ No se pudo verificar la compatibilidad con MCART " +
                        $"{RTVersion.ToString()}. La aplicación podría fallar. " +
                        "Actualice la aplicación y/o MCART.");
                    break;
#if RequireSupportedMCARTVersion
                case true:
                    Application.Init();
                    MainWindow win = new MainWindow();
                    win.Show();
                    Application.Run();
                    break;
            }
#else
            }
            Application.Init();
            MainWindow win = MainWindow.Create();
            win.Show();
            Application.Run();
#endif
        }
    }
}