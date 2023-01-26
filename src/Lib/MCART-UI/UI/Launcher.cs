/*
Launcher.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Windows.Input;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.UI.Base;
using TheXDS.MCART.ViewModel;

namespace TheXDS.MCART.UI;

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
