/*
HelpPresenter.cs

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
using TheXDS.MCART.Misc;
using TheXDS.MCART.Types.Extensions;
using System.Collections.Generic;
using System.Text;
using static TheXDS.MCART.Types.Extensions.StringExtensions;
using Ist = TheXDS.MCART.Resources.InternalStrings;

namespace TheXDS.MCART.Cmd.Base
{
    public abstract class HelpPresenter
    {
        protected CmdLineParser Parser { get; }

        public HelpPresenter(CmdLineParser parser)
        {
            Parser = parser;
        }

        protected abstract string GetFormattedName(Argument argument);
        
        protected virtual string GetHeader()
        {
            var ent = ReflectionHelpers.GetEntryPoint() ?? Internals.GetCallOutsideMcart(false);
            if (ent is null) return Ist.NoInfoExposed;
            IExposeInfo nfo = new AssemblyInfo(ent.DeclaringType?.Assembly ?? ent.Module.Assembly);
            
            return new StringBuilder()
                .AppendLine(nfo.Name)
                .AppendLineIfNotNull(nfo.Version?.ToString())
                .AppendLineIfNotNull(nfo.Copyright)
                .AppendLine()
                .AppendLine(nfo.Description)
                .ToString();
        }
    }
}
