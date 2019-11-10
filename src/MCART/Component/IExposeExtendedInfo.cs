/*
IExposeExtendedInfo.cs

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

namespace TheXDS.MCART.Component
{
    /// <summary>
    ///     Define una serie de miembros a implementar para un tipo que exponga
    ///     información de identificación extendida.
    /// </summary>
    public interface IExposeExtendedInfo : IExposeInfo
    {
        /// <summary>
        ///     Obtiene un valor que indica si este 
        ///     <see cref="IExposeExtendedInfo"/> es considerado una versión
        ///     beta.
        /// </summary>
        bool Beta { get; }

        /// <summary>
        ///     Obtiene un valor que indica si este
        ///     <see cref="IExposeExtendedInfo"/> podría contener código
        ///     utilizado en contexto inseguro.
        /// </summary>
        bool Unmanaged { get; }

        /// <summary>
        ///     Obtiene un valor que indica si este <see cref="IExposeInfo"/>
        ///     cumple con el Common Language Standard (CLS).
        /// </summary>
        bool ClsCompliant { get; }
    }
}