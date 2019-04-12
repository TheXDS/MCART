/*
ExposedInfo.cs

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

namespace TheXDS.MCART.Component
{
    /// <summary>
    ///     Expone de manera aislada la información de un objeto
    ///     <see cref="IExposeInfo"/>.
    /// </summary>
    public class ExposedInfo : IExposeInfo
    {
        private readonly IExposeInfo _source;

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ExposedInfo"/>
        /// </summary>
        /// <param name="source"></param>
        public ExposedInfo(IExposeInfo source)
        {
            _source = source;
        }

        /// <summary>
        ///     Obtiene el autor del <see cref="IExposeInfo"/>.
        /// </summary>
        public string? Author => _source.Author;

        /// <summary>
        ///     Obtiene el Copyright del <see cref="IExposeInfo"/>
        /// </summary>
        public string? Copyright => _source.Copyright;

        /// <summary>
        ///     Obtiene la licencia del <see cref="IExposeInfo"/>
        /// </summary>
        public string? License => _source.License;

        /// <summary>
        ///     Obtiene la versión del <see cref="IExposeInfo"/>
        /// </summary>
        public Version? Version => _source.Version;

        /// <summary>
        ///     Obtiene un valor que indica si este <see cref="IExposeInfo"/>
        ///     contiene información de licencia.
        /// </summary>
        public bool HasLicense => _source.HasLicense;

        /// <summary>
        ///     Obtiene un valor que indica si este <see cref="IExposeInfo"/>
        ///     cumple con el Common Language Standard (CLS).
        /// </summary>
        public bool ClsCompliant => _source.ClsCompliant;

        /// <summary>
        ///     Obtiene el nombre del elemento.
        /// </summary>
        public string Name => _source.Name;

        /// <summary>
        ///     Obtiene la descripción del elemento.
        /// </summary>
        public string Description => _source.Description;                
    }
}