/*
IExposeInfo.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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

namespace TheXDS.MCART.Component
{
    /// <summary>
    ///     Define una serie de miembros a implementar para una clase que
    ///     exponga diversa información de identificación.
    /// </summary>
    [SuppressMessage("ReSharper", "PartialTypeWithSinglePart")]
    public partial interface IExposeInfo : INameable, IDescriptible
    {
        /// <summary>
        /// Devuelve el autor del <see cref="IExposeInfo"/>
        /// </summary>
        string? Author { get; }

        /// <summary>
        /// Devuelve el Copyright del <see cref="IExposeInfo"/>
        /// </summary>
        string? Copyright { get; }

        /// <summary>
        /// Devuelve la licencia del <see cref="IExposeInfo"/>
        /// </summary>
        string? License { get; }

        /// <summary>
        /// Devuelve la versión del <see cref="IExposeInfo"/>
        /// </summary>
        Version? Version { get; }

        /// <summary>
        /// Obtiene un valor que determina si este <see cref="IExposeInfo"/>
        /// contiene información de licencia.
        /// </summary>
        bool HasLicense { get; }

        /// <summary>
        ///     Obtiene un valor que indica si este <see cref="IExposeInfo"/>
        ///     cumple con el Common Language Standard (CLS)
        /// </summary>
        bool ClsCompliant { get; }
    }
}