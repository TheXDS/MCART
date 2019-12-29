/*
Helpers.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

#nullable enable

using System;
using System.Reflection;
using TheXDS.MCART.Component;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Types.Extensions.StringExtensions;

namespace TheXDS.MCART.Cmd
{

    /// <summary>
    /// Funciones auxiliares de consola.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Muestra información sobre la aplicación actual.
        /// </summary>
        public static void About()
        {
            var ent = ReflectionHelpers.GetEntryPoint() ?? throw new InvalidOperationException();
            About(ent.DeclaringType?.Assembly ?? ent.Module.Assembly);
        }

        /// <summary>
        /// Muestra información sobre el ensamblado especificado.
        /// </summary>
        /// <param name="assembly">
        /// Ensamblado sobre el cual mostrar información.
        /// </param>
        public static void About(Assembly assembly)
        {
            About((IExposeInfo)new AssemblyInfo(assembly));
        }

        /// <summary>
        /// Muestra información sobre el ensamblado especificado.
        /// </summary>
        /// <param name="assembly">
        /// Ensamblado sobre el cual mostrar información.
        /// </param>
        public static void About(IExposeAssembly assembly)
        {
            About(assembly.Assembly);
        }

        /// <summary>
        /// Muestra información sobre el elemento especificado.
        /// </summary>
        /// <param name="info">
        /// <see cref="IExposeInfo"/> del cual mostrar información.
        /// </param>
        public static void About(IExposeInfo info)
        {
            Console.WriteLine(info.Name);
            Console.WriteLine(info.Version?.ToString());
            if (!info.Copyright.IsEmpty()) Console.WriteLine(info.Copyright);
            Console.WriteLine();
            if (!info.Description.IsEmpty() && info.Description != info.Name)
            {
                try
                {
                    foreach (var j in info.Description!.TextWrap(Console.BufferWidth))
                    {
                        Console.WriteLine(j);
                    }
                }
                catch
                {
                    Console.WriteLine(info.Description);
                }
            }
        }
    }
}
