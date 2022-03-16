/*
ConfiguredObservingCommand.cs

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

namespace TheXDS.MCART.Helpers;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using TheXDS.MCART.ViewModel;

/// <summary>
/// Contiene métodos que permiten crear objetos, 
/// <see cref="ConfiguredObservingCommand{T}"/> los cuales permiten
/// configurar un <see cref="ObservingCommand"/>.
/// </summary>
public static class ConfiguredObservingCommand
{
    /// <summary>
    /// Crea un nuevo <see cref="ConfiguredObservingCommand{T}"/>.
    /// </summary>
    /// <typeparam name="T">Tipo del objeto que será observado.</typeparam>
    /// <param name="observedObject">Objeto observado.</param>
    /// <param name="action">Acción a asociar al comando.</param>
    /// <returns>
    /// Un nuevo <see cref="ConfiguredObservingCommand{T}"/> que puede
    /// utilizarse para configurar y crear un
    /// <see cref="ObservingCommand"/>.
    /// </returns>
    public static ConfiguredObservingCommand<T> Create<T>(this T observedObject, Action action) where T : INotifyPropertyChanged
    {
        return new ConfiguredObservingCommand<T>(observedObject, action);
    }

    /// <summary>
    /// Crea un nuevo <see cref="ConfiguredObservingCommand{T}"/>.
    /// </summary>
    /// <typeparam name="T">Tipo del objeto que será observado.</typeparam>
    /// <param name="observedObject">Objeto observado.</param>
    /// <param name="action">Acción a asociar al comando.</param>
    /// <returns>
    /// Un nuevo <see cref="ConfiguredObservingCommand{T}"/> que puede
    /// utilizarse para configurar y crear un
    /// <see cref="ObservingCommand"/>.
    /// </returns>
    public static ConfiguredObservingCommand<T> Create<T>(this T observedObject, Action<object?> action) where T : INotifyPropertyChanged
    {
        return new ConfiguredObservingCommand<T>(observedObject, action);
    }

    /// <summary>
    /// Crea un nuevo <see cref="ConfiguredObservingCommand{T}"/>.
    /// </summary>
    /// <typeparam name="T">Tipo del objeto que será observado.</typeparam>
    /// <param name="observedObject">Objeto observado.</param>
    /// <param name="action">Acción a asociar al comando.</param>
    /// <returns>
    /// Un nuevo <see cref="ConfiguredObservingCommand{T}"/> que puede
    /// utilizarse para configurar y crear un
    /// <see cref="ObservingCommand"/>.
    /// </returns>
    public static ConfiguredObservingCommand<T> Create<T>(this T observedObject, Func<Task> action) where T : INotifyPropertyChanged
    {
        return new ConfiguredObservingCommand<T>(observedObject, action);
    }

    /// <summary>
    /// Crea un nuevo <see cref="ConfiguredObservingCommand{T}"/>.
    /// </summary>
    /// <typeparam name="T">Tipo del objeto que será observado.</typeparam>
    /// <param name="observedObject">Objeto observado.</param>
    /// <param name="action">Acción a asociar al comando.</param>
    /// <returns>
    /// Un nuevo <see cref="ConfiguredObservingCommand{T}"/> que puede
    /// utilizarse para configurar y crear un
    /// <see cref="ObservingCommand"/>.
    /// </returns>
    public static ConfiguredObservingCommand<T> Create<T>(this T observedObject, Func<object?, Task> action) where T : INotifyPropertyChanged
    {
        return new ConfiguredObservingCommand<T>(observedObject, action);
    }
}
