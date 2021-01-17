/*
LicenseAttributeBase.cs

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

using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Attributes
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que obtenga
    /// licencias a partir del valor de un atributo.
    /// </summary>
    public abstract class LicenseAttributeBase : TextAttribute
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="LicenseAttributeBase"/>
        /// </summary>
        /// <param name="text"></param>
        protected LicenseAttributeBase(string text) : base(text)
        {
        }

        /// <summary>
        /// Obtiene una licencia asociada a este atributo.
        /// </summary>
        /// <param name="context">
        /// Objeto del cual se ha extraído este atributo.
        /// </param>
        /// <returns>
        /// Una licencia asociada a este atributo.
        /// </returns>
        public abstract License GetLicense(object context);
    }
}