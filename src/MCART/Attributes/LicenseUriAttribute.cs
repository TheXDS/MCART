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

#nullable enable

using System;
using System.IO;
using System.Threading.Tasks;
using TheXDS.MCART.Types.Base;
using static System.AttributeTargets;
using static TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc />
    /// <summary>
    ///     Establece un archivo de licencia externo a asociar con el elemento.
    /// </summary>
    [AttributeUsage(Class | Module | Assembly | Field)]
    public sealed class LicenseUriAttribute : TextAttribute
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="LicenseFileAttribute" />.
        /// </summary>
        /// <param name="licenseUri">
        ///     Ruta Uri de la licencia.
        /// </param>
        public LicenseUriAttribute(string licenseUri) : base(licenseUri)
        {
            Uri = new Uri(licenseUri);
        }

        /// <summary>
        ///     Obtiene la ruta de almacenamiento de la licencia.
        /// </summary>
        public Uri Uri { get; }

        /// <summary>
        ///     Lee el archivo de licencia especificado por este atributo.
        /// </summary>
        /// <returns>
        ///     El contenido del archivo de licencia especificado.
        /// </returns>
        public string ReadLicense()
        {
            try
            {                
                using var sr = new StreamReader(StreamUriParser.Get(Uri)!);
                return sr.ReadToEnd();
            }
            catch
            {
                return Warn(UnspecLicense);
            }
        }
        /// <summary>
        ///     Lee el archivo de licencia especificado por este atributo.
        /// </summary>
        /// <returns>
        ///     El contenido del archivo de licencia especificado.
        /// </returns>
        public Task<string> ReadLicenseAsync()
        {
            try
            {
                using var sr = new StreamReader(StreamUriParser.Get(Uri)!);
                return sr.ReadToEndAsync();
            }
            catch
            {
                return Task.FromResult(Warn(UnspecLicense));
            }
        }
    }
}