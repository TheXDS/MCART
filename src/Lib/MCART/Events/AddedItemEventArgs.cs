﻿/*
AddedItemEventArgs.cs

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

using TheXDS.MCART.Types;

namespace TheXDS.MCART.Events;

/// <summary>
/// Contiene información para el evento <see cref="ListEx{T}.AddedItem"/>.
/// </summary>
/// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
/// <param name="newItem">Nuevo elemento que ha sido agregado.</param>
public class AddedItemEventArgs<T>(T newItem) : EventArgs
{
    /// <summary>
    /// Elemento que fue agregado al <see cref="ListEx{T}"/> que generó el
    /// evento.
    /// </summary>
    public T NewItem { get; } = newItem;

    /// <summary>
    /// Convierte implícitamente un <see cref="AddingItemEventArgs{T}"/> en
    /// un <see cref="AddedItemEventArgs{T}"/>.
    /// </summary>
    /// <param name="from">
    /// <see cref="AddingItemEventArgs{T}"/> a convertir.
    /// </param>
    public static implicit operator AddedItemEventArgs<T>(AddingItemEventArgs<T> from) => new(from.NewItem);

    /// <summary>
    /// Converte implícitamente un <see cref="AddedItemEventArgs{T}"/> en un
    /// objeto de tipo <typeparamref name="T"/>.
    /// </summary>
    /// <param name="from">
    /// <see cref="AddedItemEventArgs{T}"/> a convertir.
    /// </param>
    public static implicit operator T(AddedItemEventArgs<T> from) => from.NewItem;
}
