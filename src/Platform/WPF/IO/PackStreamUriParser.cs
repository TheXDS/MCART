/*
FileStreamUriParser.cs

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
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Windows;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.IO
{
    public class PackStreamUriParser : SimpleStreamUriParser
    {
        /// <summary>
        ///     Inicializa la clase <see cref="PackStreamUriParser"/>
        /// </summary>
        static PackStreamUriParser()
        {
            if (!UriParser.IsKnownScheme(PackUriHelper.UriSchemePack))
            {
                UriParser.Register(new GenericUriParser(GenericUriParserOptions.GenericAuthority), PackUriHelper.UriSchemePack, -1);
            }
        }

        protected override IEnumerable<string> SchemeList
        {
            get
            {
                yield return PackUriHelper.UriSchemePack;
            }
        }

        public override Stream? Open(Uri uri)
        {
            return Application.GetResourceStream(uri).Stream;
        }
    }
}