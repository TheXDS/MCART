﻿/*
ProtocolFormatAttribute.cs

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
using static System.AttributeTargets;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc cref="Attribute"/>
    /// <summary>
    ///     Marca un elemento con un valor de prioridad.
    /// </summary>
    [AttributeUsage(Assembly | Class | Module | Event | GenericParameter | Interface | Method | Module | Parameter | Property | Struct)]
    [Serializable]
    public sealed class PriorityAttribute : Attribute, IValueAttribute<int>
    {
        /// <summary>
        ///     Obtiene el valor de prioridad asociado al elemento.
        /// </summary>
        public int Value { get; }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="PriorityAttribute"/>.
        /// </summary>
        /// <param name="priority">Valor de prioridad a asociar.</param>
        public PriorityAttribute(int priority)
        {
            Value = priority;
        }
    }
}