/*
ObservableWrapBase.cs

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

using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using Nccha = System.Collections.Specialized.NotifyCollectionChangedAction;
using NcchEa = System.Collections.Specialized.NotifyCollectionChangedEventArgs;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Clase base para los envoltorios observables de colecciones.
/// </summary>
[DebuggerStepThrough]
public abstract class ObservableWrapBase : NotifyPropertyChanged, INotifyCollectionChanged, IEnumerable
{
    /// <summary>
    /// Se produce al ocurrir un cambio en la colección.
    /// </summary>
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    /// <summary>
    /// Genera el evento <see cref="CollectionChanged"/>.
    /// </summary>
    /// <param name="eventArgs">Argumentos del evento.</param>
    protected void RaiseCollectionChanged(NcchEa eventArgs)
    {
        CollectionChanged?.Invoke(this, eventArgs);
    }

    /// <inheritdoc/>
    public IEnumerator GetEnumerator() => OnGetEnumerator();

    /// <summary>
    /// Obtiene un enumerador que itera sobre la colección.
    /// </summary>
    /// <returns>
    /// Un enumerador que puede ser utilizado para iterar sobre la colección.
    /// </returns>
    protected abstract IEnumerator OnGetEnumerator();

    /// <summary>
    /// Obtiene el índice del elemento especificado.
    /// </summary>
    /// <param name="item">
    /// Elemento para el cual buscar el índice.
    /// </param>
    /// <returns>
    /// El índice del elemento especificado, o <c>-1</c> si el elemento
    /// no se encuentra en la colección.
    /// </returns>
    public virtual int IndexOf(object item)
    {
        int c = 0;
        foreach (object? j in this)
        {
            if (j?.Equals(item) ?? item is null) return c;
            c++;
        }
        return -1;
    }

    /// <summary>
    /// Obliga a refrescar el estado de un elemento dentro de la
    /// colección.
    /// </summary>
    /// <param name="item">
    /// Elemento a refrescar.
    /// </param>
    public void RefreshItem(object? item)
    {
        if (item is null || !Contains(item)) return;
        switch (item)
        {
            case IRefreshable refreshable:
                refreshable.Refresh();
                break;
            default:
                RaiseCollectionChanged(new NcchEa(Nccha.Replace, item, item, IndexOf(item)));
                break;
        }
    }

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
    public virtual bool Contains(object item)
    {
        return IndexOf(item) != -1;
    }
}
