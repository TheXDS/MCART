/*
ManualObservableCollection.cs

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

using System.Collections;
using System.Collections.Specialized;

namespace TheXDS.MCART.Types;

/// <summary>
/// Implementa un envoltorio con notificación de cambio de colección de forma
/// manual para cualquier objeto que implemente <see cref="IEnumerable{T}"/>.
/// </summary>
/// <typeparam name="T">Tipo de objetos de la colección.</typeparam>
public class ManualObservableCollection<T> : IEnumerable<T>, INotifyCollectionChanged
{
    private readonly IEnumerable<T> _source;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ManualObservableCollection{T}"/>.
    /// </summary>
    /// <param name="source"></param>
    public ManualObservableCollection(IEnumerable<T> source)
    {
        _source = source;
    }

    /// <inheritdoc/>
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator()
    {
        return _source.GetEnumerator();
    }

    /// <summary>
    /// Notifica a los escuchas registrados que la colección ha cambiado.
    /// </summary>
    public void NotifyChange()
    {
        NotifyChange(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    /// <summary>
    /// Notifica a los escuchas registrados que la colección ha cambiado.
    /// </summary>
    /// <param name="args">
    /// Argumentos de cambio de colección a pasar a los escuchas del evento
    /// <see cref="CollectionChanged"/>.
    /// </param>
    public void NotifyChange(NotifyCollectionChangedEventArgs args)
    {
        CollectionChanged?.Invoke(this, args);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_source).GetEnumerator();
    }
}