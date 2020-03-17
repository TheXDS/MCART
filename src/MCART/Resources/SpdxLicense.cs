/*
SpdxLicense.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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
using System.Diagnostics.CodeAnalysis;

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Representa una licencia registrada dentro de los estándares de
    /// Software Package Data Exchange (SPDX).
    /// </summary>
    public class SpdxLicense : License, IEquatable<SpdxLicense>
    {
        /// <summary>
        /// Obtiene el identificador corto de la licencia.
        /// </summary>
        public string SpdxShortName { get; }

        internal SpdxLicense(string id, string? name, Uri url) : base(name ?? id, url)
        {
            SpdxShortName = id;
        }

        /// <summary>
        /// Comprueba la igualdad entre dos instancias de la clase <see cref="SpdxLicense"/>.
        /// </summary>
        /// <param name="other">El otro objeto a comparar.</param>
        /// <returns>
        /// <see langword="true"/> si ambas instancias son consideradas
        /// iguales, <see langword="false"/> en caso contrario.
        /// </returns>
        public bool Equals([AllowNull] SpdxLicense other)
        {
            return SpdxShortName == other?.SpdxShortName;
        }
    }
}