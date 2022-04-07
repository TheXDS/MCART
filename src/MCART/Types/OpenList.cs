/*
OpenList.cs

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
using System.Linq;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Clase que representa una colección genérica que puede contener
/// elementos de cola al final de la misma.
/// </summary>
/// <typeparam name="T">
/// El tipo de elementos de la colección.
/// </typeparam>
public class OpenList<T> : IList<T>
{
    private readonly List<T> _head = new();
    private readonly List<T> _tail = new();

    /// <summary>
    /// Obtiene o establece el elemento en el índice especificado.
    /// </summary>
    /// <param name="index">
    /// Índice del elemento que se va a obtener o a establecer. Un 
    /// índice negativo hace referencia a un elemento en la cola del
    /// <see cref="OpenList{T}"/>.
    /// </param>
    /// <returns>
    /// El elemento en el índice especificado.
    /// </returns>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Se produce si <paramref name="index"/> no hace referencia a un
    /// elemento válido dentro de este <see cref="OpenList{T}"/>.
    /// </exception>
    public T this[int index]
    {
        get
        {
            return index < _head.Count ? _head[index] : _tail[index - _head.Count];
        }

        set
        {
            if (index < _head.Count)
            {
                _head[index] = value;
            }
            else
            {
                _tail[index - _head.Count] = value;
            }
        }
    }

    /// <summary>
    /// Obtiene el número de elementos incluidos en este
    /// <see cref="OpenList{T}"/>.
    /// </summary>
    public int Count => _head.Count + _tail.Count;

    /// <summary>
    /// Obtiene un valor que indica si esta implementación de la
    /// interfaz <see cref="ICollection{T}"/> es de solo lectura.
    /// </summary>
    /// <value>
    /// Para la clase <see cref="OpenList{T}"/>, esta propiedad
    /// siempre devuelve <see langword="false"/>.
    /// </value>
    public bool IsReadOnly => false;

    /// <summary>
    /// Obtiene un valor que indica si este <see cref="OpenList{T}"/>
    /// contiene una cola de elementos al final.
    /// </summary>
    public bool HasTail => _tail.Any();

    /// <summary>
    /// Enumera todos los elementos de la cabeza de este
    /// <see cref="OpenList{T}"/>.
    /// </summary>
    public IEnumerable<T> Head => _head.ToArray();

    /// <summary>
    /// Enumera todos los elementos de la cola de este
    /// <see cref="OpenList{T}"/>.
    /// </summary>
    public IEnumerable<T> Tail => _tail.ToArray();

    /// <summary>
    /// Agrega un objeto a este <see cref="OpenList{T}"/>.
    /// </summary>
    /// <param name="item">
    /// Objeto que se va a agregar a este
    /// <see cref="OpenList{T}"/>. El valor puede ser 
    /// <see langword="null"/> para los tipos de referencia.
    /// </param>
    public void Add(T item)
    {
        _head.Add(item);
    }

    /// <summary>
    /// Agrega un objeto al final de este
    /// <see cref="OpenList{T}"/>, garantizando que futuras
    /// adiciones por medio del método <see cref="Add(T)"/> no
    /// agregarán elementos después de este.
    /// </summary>
    /// <param name="item">
    /// Objeto que se va a agregar al final de este
    /// <see cref="OpenList{T}"/>. El valor puede ser 
    /// <see langword="null"/> para los tipos de referencia.
    /// </param>
    public void AddTail(T item)
    {
        _tail.Add(item);
    }

    /// <summary>
    /// Quita todos los elementos de este 
    /// <see cref="OpenList{T}"/>.
    /// </summary>
    public void Clear()
    {
        _head.Clear();
        _tail.Clear();
    }

    /// <summary>
    /// Mueve todos los elementos de la cola a la cabeza de la colección.
    /// </summary>
    public void JoinTail()
    {
        _head.AddRange(_tail);
        _tail.Clear();
    }

    /// <summary>
    /// Separa la colección de elementos en una cabeza y una cola en el
    /// índice especificado.
    /// </summary>
    /// <param name="index">
    /// Índice del elemento que será el principio de la cola.
    /// </param>
    public void SplitTail(int index)
    {
        if (index > _head.Count - 1) JoinTail();
        IEnumerable<T>? newTail = _head.Skip(index);
        _head.RemoveRange(index, _head.Count - index);
        _tail.InsertRange(0, newTail);
    }

    /// <summary>
    /// Determina si un elemento se encuentra en este
    /// <see cref="OpenList{T}"/>.
    /// </summary>
    /// <param name="item">
    /// Objeto que se va a buscar en este <see cref="OpenList{T}"/>. El
    /// valor puede ser <see langword="null"/> para los tipos de
    /// referencia.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si item se encuentra en este 
    /// <see cref="OpenList{T}"/>; en caso contrario, 
    /// <see langword="false"/>.
    /// </returns>
    public bool Contains(T item)
    {
        return _head.Contains(item) || _tail.Contains(item);
    }

    /// <summary>
    /// Copia la totalidad de este <see cref="OpenList{T}"/> en una
    /// matriz <see cref="System.Array"/> unidimensional compatible,
    /// comenzando en el índice especificado de la matriz de destino.
    /// </summary>
    /// <param name="array">
    /// <see cref="System.Array"/> unidimensional que constituye el
    /// destino de los elementos copiados de este
    /// <see cref="OpenList{T}"/>. La matriz <see cref="System.Array"/>
    /// debe tener una indización de base cero.
    /// </param>
    /// <param name="arrayIndex">
    /// Índice de base cero en <paramref name="array"/> donde comienza
    /// la copia.
    /// </param>
    /// <exception cref="System.ArgumentNullException">
    /// El valor de <paramref name="array"/> es <see langword="null"/>.
    /// </exception>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// <paramref name="arrayIndex"/> es menor que cero.
    /// </exception>
    /// <exception cref="System.ArgumentException">
    /// El número de elementos de este <see cref="OpenList{T}"/> de
    /// origen es mayor que el espacio disponible desde
    /// <paramref name="arrayIndex"/> hasta el final de el 
    /// <paramref name="array"/> de destino.
    /// </exception>
    public void CopyTo(T[] array, int arrayIndex)
    {
        _head.Concat(_tail).ToArray().CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Devuelve un enumerador que recorre en iteración este
    /// <see cref="OpenList{T}"/>.
    /// </summary>
    /// <returns>
    /// Estructura <see cref="IEnumerator{T}"/> para este
    /// <see cref="OpenList{T}"/>.
    /// </returns>
    public IEnumerator<T> GetEnumerator()
    {
        return _head.Concat(_tail).GetEnumerator();
    }

    /// <summary>
    /// Quita la primera aparición de un objeto específico de este
    /// <see cref="OpenList{T}"/>.
    /// </summary>
    /// <param name="item">
    /// Objeto que se va a quitar de este <see cref="OpenList{T}"/>. El
    /// valor puede ser <see langword="null"/> para los tipos de
    /// referencia.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si <paramref name="item"/> se quita
    /// correctamente; en caso contrario, <see langword="false"/>. Este
    /// método también devuelve <see langword="false"/> si no se
    /// encontró <paramref name="item"/> en este
    /// <see cref="OpenList{T}"/>.
    /// </returns>
    public bool Remove(T item)
    {
        return _head.Remove(item) || _tail.Remove(item);
    }

    /// <summary>
    /// Inserta un elemento en este <see cref="OpenList{T}"/>, en el
    /// índice especificado.
    /// </summary>
    /// <param name="index">
    /// Índice en el que debe insertarse <paramref name="item"/>.
    /// </param>
    /// <param name="item">
    /// Objeto que se va a insertar. El valor puede ser
    /// <see langword="null"/> para los tipos de referencia.
    /// </param>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Se produce si <paramref name="index"/> no hace referencia a un
    /// elemento válido dentro de este <see cref="OpenList{T}"/>.
    /// </exception>
    public void Insert(int index, T item)
    {
        if (index < _head.Count)
        {
            _head.Insert(index, item);
        }
        else
        {
            _tail.Insert(index - _head.Count, item);
        }
    }

    /// <summary>
    /// Quita el elemento situado en el índice especificado de este
    /// <see cref="OpenList{T}"/>.
    /// </summary>
    /// <param name="index">
    /// Índice del elemento que se va a quitar.
    /// </param>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Se produce si <paramref name="index"/> no hace referencia a un
    /// elemento válido dentro de este <see cref="OpenList{T}"/>.
    /// </exception>
    public void RemoveAt(int index)
    {
        if (index < _head.Count)
        {
            _head.RemoveAt(index);
        }
        else
        {
            _tail.RemoveAt(index - _head.Count);
        }
    }

    /// <summary>
    /// Determina el índice de base cero de un elemento específico 
    /// dentro de el <see cref="IList{T}"/>.
    /// </summary>
    /// <param name="item">
    /// Objeto que se va a buscar en el <see cref="IList{T}"/>.
    /// </param>
    /// <returns>
    /// El índice de <paramref name="item"/> si se encuentra en la
    /// lista; de lo contrario, devuelve -1.
    /// </returns>
    public int IndexOf(T item)
    {
        return _head.Concat(_tail).ToList().IndexOf(item);
    }

    /// <summary>
    /// Devuelve un enumerador que recorre en iteración una colección.
    /// </summary>
    /// <returns>
    /// Un <see cref="IEnumerator"/> que puede usarse para recorrer en 
    /// iteración la colección.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_head.Concat(_tail)).GetEnumerator();
    }
}
