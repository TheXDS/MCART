/*
TextLicense.cs

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

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Licencia cuyo contenido se ha especificado directamente.
    /// </summary>
    public sealed class TextLicense : License
    {
        private readonly string? _content;

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="TextLicense"/>.
        /// </summary>
        /// <param name="name">Nombre descriptivo de la licencia.</param>
        /// <param name="content">Contenido de la licencia.</param>
        public TextLicense(string name, string? content) : base(name, null)
        {
            _content = content;
        }

        /// <summary>
        /// Obtiene el contenido de la licencia.
        /// </summary>
        public override string LicenseContent => _content ?? base.LicenseContent;
    }
}