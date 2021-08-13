/*
LicenseFileAttribute.cs

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
using System.IO;
using static System.AttributeTargets;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Attributes
{
    /// <summary>
    /// Establece un archivo de licencia externo a asociar con el elemento.
    /// </summary>
    [AttributeUsage(Class | Module | Assembly), Obsolete("Utilice LicenseUriAttribute en su lugar.")]
    public sealed class LicenseFileAttribute : TextAttribute
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="LicenseFileAttribute" />.
        /// </summary>
        /// <param name="licenseFile">
        /// Ruta del archivo de licencia adjunto.
        /// </param>
        public LicenseFileAttribute(string licenseFile) : base(licenseFile)
        {
            // HACK: forma simple y efectiva de validad una ruta de archivo.
            _ = new FileInfo(licenseFile);
        }

        /// <summary>
        /// Lee el archivo de licencia especificado por este atributo.
        /// </summary>
        /// <returns>
        /// El contenido del archivo de licencia especificado.
        /// </returns>
        public string ReadLicense()
        {
            try
            {
                using var fs = new FileStream(Value!, FileMode.Open);
                using var sr = new StreamReader(fs);
                return sr.ReadToEnd();
            }
            catch
            {
                return St.Composition.Warn(St.Common.UnspecifiedLicense);
            }
        }
    }
}