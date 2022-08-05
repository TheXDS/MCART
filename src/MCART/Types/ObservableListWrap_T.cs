/*
ObservableListWrap_T.cs

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

namespace TheXDS.MCART.Types;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using TheXDS.MCART.Types.Base;
using Nccha = System.Collections.Specialized.NotifyCollectionChangedAction;
using NcchEa = System.Collections.Specialized.NotifyCollectionChangedEventArgs;

/// <summary>
/// Envuelve una lista genérica para proveerla de notificación de
/// cambios en el contenido de la colección.
/// </summary>
/// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
[DebuggerStepThrough]
public class ObservableListWrap<T> : ObservableWrap<T, IList<T>>, IList<T>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ObservableListWrap{T}"/>.
    /// </summary>
    public ObservableListWrap()
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ObservableListWrap{T}"/>.
    /// </summary>
    /// <param name="collection"></param>
    public ObservableListWrap(IList<T> collection) : base(collection)
    {
    }

    /// <summary>
    /// Permite acceder de forma indexada al contenido de este
    /// <see cref="ObservableListWrap{T}"/>.
    /// </summary>
    /// <param name="index">
    /// Índice del elemento a obtener o establecer.
    /// </param>
    /// <returns>
    /// El elemento encontrado en el índice especificado dentro de la
    /// colección.
    /// </returns>
    public T this[int index]
    {
        get => UnderlyingCollection is not null ? UnderlyingCollection[index] : default!;
        set
        {
            if (UnderlyingCollection is null) throw new InvalidOperationException();
            T? oldItem = UnderlyingCollection[index];
            UnderlyingCollection[index] = value;
            RaiseCollectionChanged(new NcchEa(Nccha.Replace, value, oldItem));
        }
    }

    /// <summary>
    /// Determina el índice de un elemento específico dentro del
    /// <see cref="ObservableListWrap{T}"/>.
    /// </summary>
    /// <param name="item">
    /// Elemento del cual obtener el índice.
    /// </param>
    /// <returns>
    /// El índice del elemento especificado.
    /// </returns>
    public int IndexOf(T item) => UnderlyingCollection?.IndexOf(item) ?? -1;

    /// <summary>
    /// Determina si la secuencia subyacente contiene al elemento
    /// especificado.
    /// </summary>
    /// <param name="item">
    /// Elemento a buscar dentro de la secuencia.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si la secuencia contiene al elemento
    /// especificado, <see langword="false"/> en caso contrario.
    /// </returns>
    public override bool Contains(T item) => UnderlyingCollection?.Contains(item) ?? false;

    /// <summary>
    /// Inserta un elemento dentro de este
    /// <see cref="ObservableListWrap{T}"/> en un índice específico.
    /// </summary>
    /// <param name="index">
    /// Posición en la cual insertar el nuevo elemento.
    /// </param>
    /// <param name="item">
    /// Elemento a insertar en el índice especificado.
    /// </param>
    public void Insert(int index, T item)
    {
        if (UnderlyingCollection is null) throw new InvalidOperationException();
        UnderlyingCollection.Insert(index, item);
        RaiseCollectionChanged(new NcchEa(Nccha.Add, item));
        Notify(nameof(Count));
    }

    /// <summary>
    /// Quita el elemento de este <see cref="ObservableListWrap{T}"/>
    /// en el índice especificado.
    /// </summary>
    /// <param name="index">
    /// Índice del elemento a remover.
    /// </param>
    public void RemoveAt(int index)
    {
        if (UnderlyingCollection is null) throw new IndexOutOfRangeException();
        T? item = UnderlyingCollection[index];
        UnderlyingCollection.RemoveAt(index);
        RaiseCollectionChanged(new NcchEa(Nccha.Remove, item, index));
        Notify(nameof(Count));
    }
}
