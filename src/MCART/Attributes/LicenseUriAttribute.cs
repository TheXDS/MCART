/*
LicenseFileAttribute.cs

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
using System.IO;
using TheXDS.MCART.Resources;
using static System.AttributeTargets;

namespace TheXDS.MCART.Attributes
{

    /// <inheritdoc />
    /// <summary>
    /// Establece un archivo de licencia externo a asociar con el elemento.
    /// </summary>
    [AttributeUsage(Class | Module | Assembly | Field)]
    public sealed class LicenseUriAttribute : LicenseAttributeBase
    {
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="LicenseFileAttribute" />.
        /// </summary>
        /// <param name="licenseUri">
        /// Ruta Uri de la licencia.
        /// </param>
        public LicenseUriAttribute(string licenseUri) : base(licenseUri)
        {
            Uri = new Uri(licenseUri);
        }

        /// <summary>
        /// Obtiene la ruta de almacenamiento de la licencia.
        /// </summary>
        public Uri Uri { get; }

        /// <summary>
        /// Obtiene una licencia a partir del <see cref="Uri"/>
        /// especificado para este atributo.
        /// </summary>
        /// <returns>
        /// Una licencia a partir del <see cref="Uri"/> especificado para
        /// este atributo.
        /// </returns>
        public override License GetLicense(object _)
        {
            return new License(Path.GetFileNameWithoutExtension(Uri.LocalPath), Uri);
        }
    }
}