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
using System.Collections.Generic;
using System.Windows.Input;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.ViewModel;

namespace TheXDS.MCART.UI
{
    /// <summary>
    ///     Clase base para todos los objetos que describan una interacción
    ///     disponible dentro de un sistema de menús.
    /// </summary>
    public abstract class InteractionBase : INameable, IDescriptible
    {
        /// <summary>
        ///     Obtiene el nombre a mostrar para esta interacción.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Obtiene una descripción para esta interacción.
        /// </summary>
        public string? Description { get; }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="InteractionBase"/>, estableciendo un nombre
        ///     descriptivo a mostrar.
        /// </summary>
        /// <param name="name">
        ///     Nombre descriptivo a mostrar.
        /// </param>
        protected InteractionBase(string name) : this (name, null)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="InteractionBase"/>, estableciendo un nombre
        ///     y una descripción a mostrar.
        /// </summary>
        /// <param name="name">
        ///     Nombre descriptivo a mostrar.
        /// </param>
        /// <param name="description">
        ///     Descripción del elemento.
        /// </param>
        protected InteractionBase(string name, string? description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
        }
    }

    /// <summary>
    ///     Representa una entrada de menú que contiene a otros elementos hijos
    ///     interactivos.
    /// </summary>
    public class SubMenu : InteractionBase
    {
        /// <summary>
        ///     Obtiene una secuencia de interacciones que forman parte de un 
        ///     sub-menú de este elemento.
        /// </summary>
        public IEnumerable<InteractionBase> Children { get; }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="SubMenu"/>, estableciendo las entradas de
        ///     sub-menú a mostrar.
        /// </summary>
        /// <param name="name">
        ///     Nombre a mostrar para el elemento.
        /// </param>
        /// <param name="subMenu">
        ///     Sub-menú que forma parte de este elemento.
        /// </param>
        public SubMenu(string name, IEnumerable<InteractionBase> subMenu) : this(name, null, subMenu)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="SubMenu"/>, estableciendo las entradas de
        ///     sub-menú a mostrar.
        /// </summary>
        /// <param name="name">
        ///     Nombre a mostrar para este elemento.
        /// </param>
        /// <param name="description">
        ///     Descripción a mostrar para este elemento.
        /// </param>
        /// <param name="subMenu">
        ///     Sub-menú que forma parte de este elemento.
        /// </param>
        public SubMenu(string name,string? description, IEnumerable<InteractionBase> subMenu) : base(name, description)
        {
            Children = subMenu;
        }
    }

    /// <summary>
    ///     Describe una acción interactiva disponible al usuario final de la 
    ///     aplicación.
    /// </summary>
    public class Launcher : InteractionBase
    {
        /// <summary>
        ///     Obtiene el comando a ejecutar para esta interacción.
        /// </summary>
        public ICommand Command { get; }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="Launcher"/>, estableciendo un nombre  y comando a
        ///     ejecutar.
        /// </summary>
        /// <param name="name">
        ///     Nombre amigable para la interacción.
        /// </param>
        /// <param name="command">
        ///     Comando a ejecutar para la interacción.
        /// </param>
        public Launcher(string name, ICommand command) : this(name, null, command)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="Launcher"/>, estableciendo un nombre, descripción
        ///     y comando a ejecutar.
        /// </summary>
        /// <param name="name">
        ///     Nombre amigable para la interacción.
        /// </param>
        /// <param name="description">
        ///     Descripción de la interacción.
        /// </param>
        /// <param name="command">
        ///     Comando a ejecutar para la interacción.
        /// </param>
        public Launcher(string name, string? description, ICommand command) : base(name, description)
        {
            Command = command ?? throw new ArgumentNullException(nameof(command));
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="Launcher"/>, estableciendo una acción simple como
        ///     el comando a ejecutar.
        /// </summary>
        /// <param name="action">
        ///     Acción a ejecutar. El nombre de la interacción será inferido a
        ///     partir de este objeto.
        /// </param>
        public Launcher(Action action) : this(action.NameOf(), action)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="Launcher"/>, estableciendo un nombre y una acción
        ///     simple como el comando a ejecutar.
        /// </summary>
        /// <param name="name">
        ///     Nombre amigable para la interacción.
        /// </param>
        /// <param name="action">Acción a ejecutar para el comando.</param>
        public Launcher(string name, Action action) : this(name, action.GetAttr<DescriptionAttribute>()?.Value, new SimpleCommand(action))
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="Launcher"/>, estableciendo un nombre, descripción
        ///     y una acción simple como el comando a ejecutar.
        /// </summary>
        /// <param name="name">
        ///     Nombre amigable para la interacción.
        /// </param>
        /// <param name="description">
        ///     Descripción de la interacción.
        /// </param>
        /// <param name="action">Acción a ejecutar para el comando.</param>
        public Launcher(string name, string? description, Action action) : this(name, description, new SimpleCommand(action))
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="Launcher"/>, estableciendo una acción simple como
        ///     el comando a ejecutar.
        /// </summary>
        /// <param name="action">
        ///     Acción a ejecutar. El nombre de la interacción será inferido a
        ///     partir de este objeto.
        /// </param>
        public Launcher(Action<object> action) : this(action.NameOf(), action)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="Launcher"/>, estableciendo un nombre y una acción
        ///     simple como el comando a ejecutar.
        /// </summary>
        /// <param name="name">
        ///     Nombre amigable para la interacción.
        /// </param>
        /// <param name="action">Acción a ejecutar para el comando.</param>
        public Launcher(string name, Action<object> action) : this(name, null, new SimpleCommand(action))
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="Launcher"/>, estableciendo un nombre, descripción
        ///     y una acción simple como el comando a ejecutar.
        /// </summary>
        /// <param name="name">
        ///     Nombre amigable para la interacción.
        /// </param>
        /// <param name="description">
        ///     Descripción de la interacción.
        /// </param>
        /// <param name="action">Acción a ejecutar para el comando.</param>
        public Launcher(string name, string? description, Action<object> action) : this(name, description, new SimpleCommand(action))
        {
        }
    }
}