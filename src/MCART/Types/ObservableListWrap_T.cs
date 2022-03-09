/*
ObservableListWrap_T.cs

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

namespace TheXDS.MCART.Types;
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
        get => UnderlyingCollection[index];
        set
        {
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
    public int IndexOf(T item) => UnderlyingCollection.IndexOf(item);

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
    public override bool Contains(T item) => UnderlyingCollection.Contains(item);

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
        T? item = UnderlyingCollection[index];
        UnderlyingCollection.RemoveAt(index);
        RaiseCollectionChanged(new NcchEa(Nccha.Remove, item, index));
        Notify(nameof(Count));
    }
}
