/*
PackStreamUriParser.cs

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

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Windows;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.IO
{
    /// <summary>
    /// Traduce un URI de recursos incrustados (pack://) a un
    /// <see cref="Stream"/> de lectura para el recurso.
    /// </summary>
    public class PackStreamUriParser : SimpleStreamUriParser
    {
        /// <summary>
        /// Inicializa la clase <see cref="PackStreamUriParser"/>
        /// </summary>
        static PackStreamUriParser()
        {
            if (!UriParser.IsKnownScheme(PackUriHelper.UriSchemePack))
            {
                UriParser.Register(new GenericUriParser(GenericUriParserOptions.GenericAuthority), PackUriHelper.UriSchemePack, -1);
            }
        }

        /// <summary>
        /// Enumera los esquemas de URI soportados por este 
        /// <see cref="StreamUriParser"/>.
        /// </summary>
        protected override IEnumerable<string> SchemeList
        {
            get
            {
                yield return PackUriHelper.UriSchemePack;
            }
        }

        /// <summary>
        /// Abre un <see cref="Stream"/> que permita leer el recurso incrustado
        /// referenciado por el <see cref="Uri"/> especificado.
        /// </summary>
        /// <param name="uri">
        /// <see cref="Uri"/> que indica la ubicación del recurso incrustado.
        /// </param>
        /// <returns>
        /// Un <see cref="Stream"/> desde el cual es posible leer el recurso
        /// incrustado, o <see langword="null"/> si el <see cref="Uri"/> no
        /// apunta a un recurso incrustado válido.
        /// </returns>
        public override Stream? Open(Uri uri)
        {
            try
            {
                return Application.GetResourceStream(uri).Stream;
            }
            catch
            {
                return null;
            }
        }
    }
}