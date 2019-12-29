/*
Interaction.cs

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
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.UI.Base
{
    /// <summary>
    /// Clase base para todos los objetos que describan una interacción
    /// disponible dentro de un sistema de menús.
    /// </summary>
    public abstract class InteractionBase : INameable, IDescriptible
    {
        /// <summary>
        /// Obtiene el nombre a mostrar para esta interacción.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Obtiene una descripción para esta interacción.
        /// </summary>
        public string? Description { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InteractionBase"/>, estableciendo un nombre
        /// descriptivo a mostrar.
        /// </summary>
        /// <param name="name">
        /// Nombre descriptivo a mostrar.
        /// </param>
        protected InteractionBase(string name) : this(name, null)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InteractionBase"/>, estableciendo un nombre
        /// y una descripción a mostrar.
        /// </summary>
        /// <param name="name">
        /// Nombre descriptivo a mostrar.
        /// </param>
        /// <param name="description">
        /// Descripción del elemento.
        /// </param>
        protected InteractionBase(string name, string? description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
        }
    }
}