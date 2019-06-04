﻿/*
ObservableWrap.cs

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

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System;
using System.Diagnostics;
using Nccha = System.Collections.Specialized.NotifyCollectionChangedAction;
using static System.Collections.Specialized.NotifyCollectionChangedAction;
using NcchEa = System.Collections.Specialized.NotifyCollectionChangedEventArgs;

namespace TheXDS.MCART.Types.Base
{
    /// <summary>
    ///     Clase base para los envoltorios observables de colecciones.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la colección.</typeparam>
    /// <typeparam name="TCollection">Tipo de colección.</typeparam>
    [DebuggerStepThrough]
    public abstract class ObservableWrap<T, TCollection> : ObservableWrapBase, INotifyCollectionChanged, ICollection<T> where TCollection : ICollection<T>
    {
        /// <summary>
        ///     Inicializa una nueva instancia de la clase 
        ///     <see cref="ObservableWrap{T, TCollection}"/>.
        /// </summary>
        /// <param name="collection"></param>
        protected ObservableWrap(TCollection collection)
        {
            UnderlyingCollection = collection;
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ObservableWrap{T, TCollection}"/>.
        /// </summary>
        protected ObservableWrap()
        {
        }

        /// <summary>
        ///     Obtiene acceso directo a la colección subyacente envuelta por
        ///     este <see cref="ObservableWrap{T, TCollection}"/>.
        /// </summary>
        public TCollection UnderlyingCollection { get; private set; }

        /// <summary>
        ///     Obtiene la cuenta de elementos contenidos dentro de la
        ///     colección.
        /// </summary>
        public int Count => UnderlyingCollection?.Count ?? 0;

        /// <summary>
        ///     Obtiene un valor que indica si la colección es de solo lectura.
        /// </summary>
        public bool IsReadOnly => UnderlyingCollection?.IsReadOnly ?? false;

        /// <summary>
        ///     Agrega un nuevo elemento al final de la colección.
        /// </summary>
        /// <param name="item">Elemento a agregar.</param>
        public void Add(T item)
        {
            if (UnderlyingCollection is null) throw new InvalidOperationException();
            UnderlyingCollection.Add(item);
            RaiseCollectionChanged(new NcchEa(Nccha.Add, item));
            Notify(nameof(Count));
        }

        /// <summary>
        ///     Limpia la colección.
        /// </summary>
        public void Clear()
        {
            if (UnderlyingCollection is null) return;
            UnderlyingCollection.Clear();
            RaiseCollectionChanged(new NcchEa(Reset));
            Notify(nameof(Count));
        }

        /// <summary>
        ///     Obtiene un valor que determina si el elemento existe en la
        ///     colección.
        /// </summary>
        /// <param name="item">Elemento a comprobar.</param>
        /// <returns>
        ///     <see langword="true"/> si el elemento existe dentro de la
        ///     colección, <see langword="false"/> en caso contrario.
        /// </returns>
        public bool Contains(T item)
        {
            return UnderlyingCollection?.Contains(item) ?? false;
        }

        /// <summary>
        ///     Copia el contenido de esta colección sobre un arreglo.
        /// </summary>
        /// <param name="array">Destino de la copia.</param>
        /// <param name="arrayIndex">
        ///     Índice donde empezar a copiar elementos.
        /// </param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            UnderlyingCollection?.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///     Quita un elemento de la colección.
        /// </summary>
        /// <param name="item">Elemento a quitar.</param>
        /// <returns>
        ///     <see langword="true"/> si el elemento ha sido quitado
        ///     exitosamente de la colección, <see langword="false"/> en caso
        ///     contrario. También se devuelve <see langword="false"/> si el
        ///     elemento no existía en la <see cref="ICollection{T}"/>
        ///     original.
        /// </returns>
        public bool Remove(T item)
        {
            var result = UnderlyingCollection?.Remove(item) ?? false;

            if (result)
            {
                RaiseCollectionChanged(new NcchEa(Nccha.Remove, item));
                Notify(nameof(Count));
            }

            return result;
        }

        /// <summary>
        ///     Obtiene un enumerador que itera sobre la colección.
        /// </summary>
        /// <returns>
        ///     Un enumerador que puede ser utilizado para iterar sobre la colección.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return UnderlyingCollection?.GetEnumerator() ?? new List<T>().GetEnumerator();
        }

        /// <summary>
        ///     Obtiene un enumerador que itera sobre la colección.
        /// </summary>
        /// <returns>
        ///     Un enumerador que puede ser utilizado para iterar sobre la colección.
        /// </returns>
        protected override IEnumerator OnGetEnumerator()
        {
            return UnderlyingCollection?.GetEnumerator() ?? new T[0].GetEnumerator();
        }

        /// <summary>
        ///     Obliga a notificar un cambio en la colección.
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();

            Substitute(UnderlyingCollection);
        }

        /// <summary>
        ///     Sustituye la colección subyacente por una nueva.
        /// </summary>
        /// <param name="newCollection">
        ///     Colección a establecer como la colección subyacente.
        /// </param>
        public void Substitute(TCollection newCollection)
        {
            UnderlyingCollection = default;
            RaiseCollectionChanged(new NcchEa(Reset));
            UnderlyingCollection = newCollection;
            RaiseCollectionChanged(new NcchEa(Nccha.Add, (IList)UnderlyingCollection));
        }
    }
}