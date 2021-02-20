/*
WpfIcons.cs

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

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Contiene una serie de miembros que instancian excepciones que pueden
    /// producirse dentro de MCART.WPF.
    /// </summary>
    public static class WpfErrors
    {
        /// <summary>
        /// Obtiene un error que ocurre cuando no se ha encontrado un Id
        /// correspondiente a un recurso solicitado.
        /// </summary>
        /// <param name="id">Id del recurso que no ha sido encontrado.</param>
        /// <param name="argName">Nombre del argumento que ha fallado.</param>
        /// <returns>
        /// Una nueva instancia de la clase <see cref="ArgumentException"/>.
        /// </returns>
        public static Exception ResourceNotFound(string id, string argName)
        {
            return new ArgumentException(string.Format(ErrorStrings.ResourceNotFound, id), argName);
        }
    }
}