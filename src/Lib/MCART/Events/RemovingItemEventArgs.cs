﻿/*
RemovingItemEventArgs.cs

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

using System.ComponentModel;
using TheXDS.MCART.Types;

namespace TheXDS.MCART.Events;

/// <summary>
/// Contiene información para el evento 
/// <see cref="ListEx{T}.RemovingItem"/>.
/// </summary>
/// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
public class RemovingItemEventArgs<T> : CancelEventArgs
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="RemovingItemEventArgs{T}"/>.
    /// </summary>
    /// <param name="index">Índice del elemento que será removido.</param>
    /// <param name="removedItem">Elemento que será removido.</param>
    public RemovingItemEventArgs(int index, T removedItem)
    {
        RemovedItem = removedItem;
        Index = index;
    }

    /// <summary>
    /// Objeto que será removido del <see cref="ListEx{T}"/> que generó
    /// el evento.
    /// </summary>
    public T RemovedItem { get; }

    /// <summary>
    /// Índice del elemento que será removido del
    /// <see cref="ListEx{T}"/> que generó el evento.
    /// </summary>
    public int Index { get; }
}
