/*
ConfiguredObservingCommand.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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
