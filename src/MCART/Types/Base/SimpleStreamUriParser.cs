/*
SimpleStreamUriParser.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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
using System.Linq;

namespace TheXDS.MCART.Types.Base
{
    /// <summary>
    /// Clase base para un <see cref="StreamUriParser"/> simple cuya lógica
    /// de comprobación de <see cref="Uri"/> requiere únicamente una lista
    /// de esquemas o protocolos compatibles.
    /// </summary>
    public abstract class SimpleStreamUriParser : StreamUriParser
    {
        /// <summary>
        /// Enumera los protocolos aceptados de <see cref="Uri"/> para la
        /// apertura de un nuevo <see cref="Stream"/> para la lectura del
        /// recurso.
        /// </summary>
        protected abstract IEnumerable<string> SchemeList { get; }

        /// <summary>
        /// Determina si este <see cref="StreamUriParser"/> puede crear un
        /// <see cref="Stream"/> a partir del <see cref="Uri"/>
        /// especificado.
        /// </summary>
        /// <param name="uri">
        /// <see cref="Uri"/> a comprobar.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si este <see cref="StreamUriParser"/>
        /// puede crear un <see cref="Stream"/> a partir del
        /// <see cref="Uri"/> especificado, <see langword="false"/> en caso
        /// contrario.
        /// </returns>
        public override bool Handles(Uri uri)
        {
            return SchemeList.Contains(uri.Scheme);
        }
    }
}