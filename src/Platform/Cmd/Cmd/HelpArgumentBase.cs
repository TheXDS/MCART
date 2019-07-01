/*
HelpArgumentBase.cs

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

using TheXDS.MCART.Component;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Cmd
{
    /// <summary>
    ///     Clase base para crear argumentos de ayuda.
    /// </summary>
    public abstract class HelpArgumentBase : Argument
    {
        /// <summary>
        ///     Obtiene el nombre largo de este argumento.
        /// </summary>
        public override sealed string LongName => @"Help";

        /// <summary>
        ///     Obtiene el nombre corto de este argumento.
        /// </summary>
        public override sealed char? ShortName => '?';

        /// <summary>
        ///     Obtiene el tipo de este argumento.
        /// </summary>
        public override sealed ValueKind Kind => ValueKind.Flag;

        /// <summary>
        ///     Obtiene el valor predeterminado de este argumento.
        /// </summary>
        public override sealed string? Default => null;

        /// <summary>
        ///     Ejecuta la operación asociada a este argumento.
        /// </summary>
        /// <param name="args">
        ///     Instancia de <see cref="CmdLineParser"/> en la cual se ha
        ///     establecido y desde la cual se ejecuta este argumento.
        /// </param>
        public override sealed void Run(CmdLineParser args)
        {
            args.PrintHelp();
        }

        /// <summary>
        ///     Describe a este argumento.
        /// </summary>
        public override string? Summary => Resources.CmdStrings.HelpArgSummary;
    }
}
