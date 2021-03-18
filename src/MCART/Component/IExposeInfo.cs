/*
IExposeInfo.cs

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
using System.Collections.Generic;
using System.Linq;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Component
{
    /// <summary>
    /// Define una serie de miembros a implementar para un tipo que exponga
    /// diversa información de identificación.
    /// </summary>
    public interface IExposeInfo : INameable, IDescriptible
    {
        /// <summary>
        /// Obtiene el autor del <see cref="IExposeInfo"/>.
        /// </summary>
        IEnumerable<string>? Authors { get; }

        /// <summary>
        /// Obtiene el Copyright del <see cref="IExposeInfo"/>
        /// </summary>
        string? Copyright { get; }

        /// <summary>
        /// Obtiene la licencia del <see cref="IExposeInfo"/>
        /// </summary>
        License? License { get; }

        /// <summary>
        /// Obtiene la versión del <see cref="IExposeInfo"/>
        /// </summary>
        Version? Version { get; }

        /// <summary>
        /// Obtiene una colección con el contenido de licencias de terceros
        /// para el objeto.
        /// </summary>
        IEnumerable<License>? ThirdPartyLicenses { get; }

        /// <summary>
        /// Obtiene la versión informacional del <see cref="IExposeInfo"/>.
        /// </summary>
        string? InformationalVersion => Version?.ToString();

        /// <summary>
        /// Obtiene un valor que indica si este <see cref="IExposeInfo"/>
        /// contiene información de licencia.
        /// </summary>
        bool HasLicense => License is { };

        /// <summary>
        /// Obtiene un valor que indica si este <see cref="IExposeInfo"/>
        /// contiene información de licencias de terceros.
        /// </summary>
        bool Has3rdPartyLicense => ThirdPartyLicenses?.Any() ?? false;
    }
}