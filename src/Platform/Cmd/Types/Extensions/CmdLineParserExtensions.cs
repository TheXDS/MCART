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
using System.Linq;
using TheXDS.MCART.Component;


namespace TheXDS.MCART.Types.Extensions
{
    public static class CmdLineParserExtensions
    {
        public static void PrintHelp(this CmdLineParser args)
        {
            IExposeInfo nfo = new AssemblyInfo(ReflectionHelpers.GetEntryPoint().DeclaringType.Assembly);
            Cmd.Helpers.About(nfo);

            var width = args.AvailableArguments.Max(p => p.LongName.Length);
            foreach (var j in args.AvailableArguments)
            {
                Console.Write($"  /{j.LongName}  - ");
                var _1st = false;
                foreach (var k in (j.Summary ?? string.Empty).TextWrap(Console.BufferWidth - width))
                {
                    if (_1st)
                    {
                        Console.Write(new string(' ', width + 6));
                    }
                    _1st = true;
                    Console.WriteLine(k);
                }
                Console.WriteLine();
            }
        }
    }
}
