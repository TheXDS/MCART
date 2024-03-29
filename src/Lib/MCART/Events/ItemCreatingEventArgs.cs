﻿/*
ItemCreatingEventArgs.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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

using System.ComponentModel;

namespace TheXDS.MCART.Events;

/// <summary>
/// Contiene información de evento para cualquier clase con eventos donde
/// se guardará información.
/// </summary>
public class ItemCreatingEventArgs<T> : CancelEventArgs where T : notnull
{
    /// <summary>
    /// Inicializa una nueva instancia de esta clase con la información de
    /// evento provista.
    /// </summary>
    /// <param name="item">Objeto que ha sido guardado.</param>
    public ItemCreatingEventArgs(T item) : this(item, false)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de esta clase con la información de
    /// evento provista.
    /// </summary>
    /// <param name="item">Objeto que ha sido guardado.</param>
    /// <param name="cancel">
    /// Determina si este evento se cancelará de forma predeterminada.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="item"/> es <see langword="null"/>.
    /// </exception>
    public ItemCreatingEventArgs(T item, bool cancel) : base(cancel)
    {
        Misc.Internals.NullCheck(item, nameof(item));
        Item = item;
    }

    /// <summary>
    /// Convierte implícitamente un <see cref="ItemCreatingEventArgs{T}" /> en un
    /// <see cref="ItemCreatedEventArgs{T}" />.
    /// </summary>
    /// <param name="fromValue">Objeto a convertir.</param>
    /// <returns>
    /// Un <see cref="ItemCreatedEventArgs{T}" /> con la misma información de
    /// evento que el <see cref="ItemCreatingEventArgs{T}" /> especificado.
    /// </returns>
    public static implicit operator ItemCreatedEventArgs<T>(ItemCreatingEventArgs<T> fromValue)
    {
        return new(fromValue.Item);
    }

    /// <summary>
    /// Obtiene el elemento que ha sido creado/editado.
    /// </summary>
    /// <returns>
    /// Una referencia de instancia al objeto creado/editado.
    /// </returns>
    public T Item { get; }
}
