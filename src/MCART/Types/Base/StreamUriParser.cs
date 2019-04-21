﻿/*
StreamUriParser.cs

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
using System.Linq;

namespace TheXDS.MCART.Types.Base
{
    /// <summary>
    ///     Clase base para un objeto que permite crear un <see cref="Stream"/>
    ///     a partir de una <see cref="Uri"/>.
    /// </summary>
    public abstract class StreamUriParser
    {
        /// <summary>
        ///     Determina si este <see cref="StreamUriParser"/> puede crear un
        ///     <see cref="Stream"/> a partir del <see cref="Uri"/>
        ///     especificado.
        /// </summary>
        /// <param name="uri">
        ///     <see cref="Uri"/> a comprobar.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> si este <see cref="StreamUriParser"/>
        ///     puede crear un <see cref="Stream"/> a partir del
        ///     <see cref="Uri"/> especificado, <see langword="false"/> en caso
        ///     contrario.
        /// </returns>
        public abstract bool Handles(Uri uri);

        /// <summary>
        ///     Abre un <see cref="Stream"/> desde el <see cref="Uri"/>
        ///     especificado.
        /// </summary>
        /// <param name="uri">
        ///     <see cref="Uri"/> a abrir para lectura.
        /// </param>
        /// <returns>
        ///     Un <see cref="Stream"/> desde el cual puede leerse el recurso
        ///     apuntado por el <see cref="Uri"/> especificado.
        /// </returns>
        public abstract Stream Open(Uri uri);
    }
}