﻿/*
ITypeBuilder.cs

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
using System.Reflection.Emit;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Define una serie de miembros a implementar por un descriptor de
    /// <see cref="TypeBuilder"/> que incluye información fuertemente tipeada
    /// del tipo base a heredar.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo base a heredar por el <see cref="TypeBuilder"/>.
    /// </typeparam>
    public interface ITypeBuilder<out T>
    {
        /// <summary>
        /// <see cref="TypeBuilder"/> subyacente a utilizar para la creación
        /// del nuevo tipo.
        /// </summary>
        TypeBuilder Builder { get; }

        /// <summary>
        /// Referencia al tipo base fuertemente tipeado específico del
        /// <see cref="TypeBuilder"/>.
        /// </summary>
        Type SpecificBaseType => typeof(T);

        /// <summary>
        /// Referencia al tipo base real del <see cref="TypeBuilder"/>.
        /// </summary>
        Type ActualBaseType => Builder.BaseType ?? SpecificBaseType;
    }
}