/*
SpdxLicense.cs

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
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Types;

namespace TheXDS.MCART.Resources
{
    /// <summary>
    ///     Representa una licencia registrada dentro de los estándares de
    ///     Software Package Data Exchange (SPDX).
    /// </summary>
    public class SpdxLicense : INameable, IEquatable<SpdxLicense>
    {
        /// <summary>
        ///     Obtiene el nombre descriptivo de la licencia.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Obtiene la URL de la licencia.
        /// </summary>
        public Uri LicenseUrl { get; }

        /// <summary>
        ///     Obtiene el identificador corto de la licencia.
        /// </summary>
        public string SpdxShortName { get; }

        internal SpdxLicense(string id, string? name, Uri url)
        {
            SpdxShortName = id;
            Name = name ?? id;
            LicenseUrl = url;
        }

        /// <summary>
        ///     Comprueba la igualdad entre dos instancias de la clase <see cref="SpdxLicense"/>.
        /// </summary>
        /// <param name="other">El otro objeto a comparar.</param>
        /// <returns>
        ///     <see langword="true"/> si ambas instancias son consideradas
        ///     iguales, <see langword="false"/> en caso contrario.
        /// </returns>
#if NETCOREAPP3_0 || NETSTANDARD2_1
        public bool Equals([AllowNull] SpdxLicense other)
#else
        public bool Equals(SpdxLicense other)
#endif
        {
            return SpdxShortName == other?.SpdxShortName;
        }
    }
}