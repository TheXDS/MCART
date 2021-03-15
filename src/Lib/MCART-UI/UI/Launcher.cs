/*
Launcher.cs

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
using System.Windows.Input;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.UI.Base;
using TheXDS.MCART.ViewModel;

namespace TheXDS.MCART.UI
{
    /// <summary>
    /// Describe una acción interactiva disponible al usuario final de la 
    /// aplicación.
    /// </summary>
    public class Launcher : InteractionBase
    {
        /// <summary>
        /// Obtiene el comando a ejecutar para esta interacción.
        /// </summary>
        public ICommand Command { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="Launcher"/>, estableciendo un nombre  y comando a
        /// ejecutar.
        /// </summary>
        /// <param name="name">
        /// Nombre amigable para la interacción.
        /// </param>
        /// <param name="command">
        /// Comando a ejecutar para la interacción.
        /// </param>
        public Launcher(string name, ICommand command) : this(name, null, command)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="Launcher"/>, estableciendo un nombre, descripción
        /// y comando a ejecutar.
        /// </summary>
        /// <param name="name">
        /// Nombre amigable para la interacción.
        /// </param>
        /// <param name="description">
        /// Descripción de la interacción.
        /// </param>
        /// <param name="command">
        /// Comando a ejecutar para la interacción.
        /// </param>
        public Launcher(string name, string? description, ICommand command) : base(name, description)
        {
            Command = command ?? throw new ArgumentNullException(nameof(command));
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="Launcher"/>, estableciendo una acción simple como
        /// el comando a ejecutar.
        /// </summary>
        /// <param name="action">
        /// Acción a ejecutar. El nombre de la interacción será inferido a
        /// partir de este objeto.
        /// </param>
        public Launcher(Action action) : this(action.NameOf(), action)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="Launcher"/>, estableciendo un nombre y una acción
        /// simple como el comando a ejecutar.
        /// </summary>
        /// <param name="name">
        /// Nombre amigable para la interacción.
        /// </param>
        /// <param name="action">Acción a ejecutar para el comando.</param>
        public Launcher(string name, Action action) : this(name, action.GetAttr<DescriptionAttribute>()?.Value, new SimpleCommand(action))
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="Launcher"/>, estableciendo un nombre, descripción
        /// y una acción simple como el comando a ejecutar.
        /// </summary>
        /// <param name="name">
        /// Nombre amigable para la interacción.
        /// </param>
        /// <param name="description">
        /// Descripción de la interacción.
        /// </param>
        /// <param name="action">Acción a ejecutar para el comando.</param>
        public Launcher(string name, string? description, Action action) : this(name, description, new SimpleCommand(action))
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="Launcher"/>, estableciendo una acción simple como
        /// el comando a ejecutar.
        /// </summary>
        /// <param name="action">
        /// Acción a ejecutar. El nombre de la interacción será inferido a
        /// partir de este objeto.
        /// </param>
        public Launcher(Action<object?> action) : this(action.NameOf(), action)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="Launcher"/>, estableciendo un nombre y una acción
        /// simple como el comando a ejecutar.
        /// </summary>
        /// <param name="name">
        /// Nombre amigable para la interacción.
        /// </param>
        /// <param name="action">Acción a ejecutar para el comando.</param>
        public Launcher(string name, Action<object?> action) : this(name, null, new SimpleCommand(action))
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="Launcher"/>, estableciendo un nombre, descripción
        /// y una acción simple como el comando a ejecutar.
        /// </summary>
        /// <param name="name">
        /// Nombre amigable para la interacción.
        /// </param>
        /// <param name="description">
        /// Descripción de la interacción.
        /// </param>
        /// <param name="action">Acción a ejecutar para el comando.</param>
        public Launcher(string name, string? description, Action<object?> action) : this(name, description, new SimpleCommand(action))
        {
        }

        /// <summary>
        /// Convierte implícitamente un <see cref="Launcher"/> en un objeto de
        /// tipo <see cref="NamedObject{T}"/>.
        /// </summary>
        /// <param name="launcher">
        /// Objeto a convertir.
        /// </param>
        public static implicit operator NamedObject<Action>(Launcher launcher) => new(() => launcher.Command.Execute(null));
    }
}