﻿/*
Events.cs

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

#region Configuración de ReSharper

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable IntroduceOptionalParameters.Global

#endregion

using System;

namespace TheXDS.MCART.Events
{
    /// <inheritdoc />
    /// <summary>
    ///     Incluye información de evento para cualquier clase con eventos de
    ///     excepción.
    /// </summary>
    public class ExceptionEventArgs : ValueEventArgs<Exception>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de este objeto con la excepción
        ///     especificada.
        /// </summary>
        /// <param name="ex">
        ///     <see cref="T:System.Exception" /> que se ha producido en el código.
        /// </param>
        public ExceptionEventArgs(Exception ex) : base(ex)
        {
        }
    }
}