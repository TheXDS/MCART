/*
License.cs

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
using TheXDS.MCART.Types.Base;
using St = TheXDS.MCART.Resources.Strings;


namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Describe una licencia.
    /// </summary>
    public class License : INameable
    {
        private static License? _missing;
        private static License? _noLicense;
        private static License? _unspecified;

        /// <summary>
        /// Obtiene una referencia a una licencia no encontrada.
        /// </summary>
        public static License MissingLicense => _missing ??= new License(St.Common.LicenseNotFound, null);

        /// <summary>
        /// Obtiene una referencia a un objeto sin licencia.
        /// </summary>
        public static License NoLicense => _noLicense ??= new License(St.Common.NoLicense, null);
        
        /// <summary>
        /// Obtiene una referencia a un objeto con licencia no definida.
        /// </summary>
        public static License Unspecified => _unspecified ??= new License(St.Common.UnspecifiedLicense, null);

        /// <summary>
        /// Obtiene el nombre descriptivo de la licencia.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Obtiene la URL de la licencia.
        /// </summary>
        public Uri? LicenseUri { get; }

        /// <summary>
        /// Obtiene el contenido de la licencia.
        /// </summary>
        public virtual string LicenseContent
        {
            get
            {
                if (LicenseUri is null) return St.Composition.Warn(St.Common.NoContent);
                try
                {
                    using StreamReader sr = new(StreamUriParser.Get(LicenseUri)!);
                    return sr.ReadToEnd();
                }
                catch
                {
                    return St.Composition.Warn(St.Errors.ErrorLoadingLicense);
                }
            }
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="License"/>.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="uri"></param>
        public License(string name, Uri? uri)
        {
            Name = name;
            LicenseUri = uri;
        }
    }
}