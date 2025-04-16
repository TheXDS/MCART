/*
ObservableListWrap.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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
using TheXDS.MCART.Types.Base;
using static System.Collections.Specialized.NotifyCollectionChangedAction;
using Nccha = System.Collections.Specialized.NotifyCollectionChangedAction;
using NcchEa = System.Collections.Specialized.NotifyCollectionChangedEventArgs;

namespace TheXDS.MCART.Types;

/// <summary>
/// Envuelve una lista para proveerla de notificación de cambios en el
/// contenido de la colección.
/// </summary>
[DebuggerStepThrough]
public class ObservableListWrap : ObservableWrapBase, IList, INotifyCollectionChanged
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="ObservableListWrap"/>.
    /// </summary>
    public ObservableListWrap()
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="ObservableListWrap"/>.
    /// </summary>
    /// <param name="list">
    /// Lista a establecer como la lista subyacente.
    /// </param>
    public ObservableListWrap(IList list)
    {
        UnderlyingList = list;
    }

    /// <summary>
    /// Obtiene acceso directo a la lista subyacente controlada por
    /// este <see cref="ObservableListWrap"/>.
    /// </summary>
    public IList? UnderlyingList { get; private set; }

    /// <summary>
    /// Permite acceder de forma indexada al contenido de este
    /// <see cref="ObservableListWrap"/>.
    /// </summary>
    /// <param name="index">
    /// Índice del elemento a obtener o establecer.
    /// </param>
    /// <returns>
    /// El elemento encontrado en el índice especificado dentro de la
    /// colección.
    /// </returns>
    public object? this[int index]
    {
        get => GetUnderlyingList()[index];
        set
        {
            object? oldItem = GetUnderlyingList()[index];
            UnderlyingList![index] = value;
            RaiseCollectionChanged(new NcchEa(Replace, value, oldItem, index));
        }
    }

    /// <summary>
    /// Obliga a refrescar el estado de un elemento dentro de la lista.
    /// </summary>
    /// <param name="index">Índice del elemento a refrescar.</param>
    public void RefreshAt(int index)
    {
        RefreshItem(this[index]);
    }

    /// <summary>
    /// Obtiene un valor que determina si esta lista es de tamaño fijo.
    /// </summary>
    public bool IsFixedSize => GetUnderlyingList().IsFixedSize;

    /// <summary>
    /// Obtiene un valor que determina si esta lista es de solo
    /// lectura.
    /// </summary>
    public bool IsReadOnly => GetUnderlyingList().IsReadOnly;

    /// <summary>
    /// Obtiene la cuenta de elementos contenidos dentro de la
    /// colección.
    /// </summary>
    public int Count => GetUnderlyingList().Count;

    /// <summary>
    /// Obtiene un valor que indica si el acceso a esta
    /// <see cref="ICollection"/> es sincronizado (seguro para
    /// multihilo).
    /// </summary>
    public bool IsSynchronized => GetUnderlyingList().IsSynchronized;

    /// <summary>
    /// Obtiene un objeto que puede ser utilizado para sincronizar el
    /// acceso al <see cref="ICollection"/>.
    /// </summary>
    public object SyncRoot => GetUnderlyingList().SyncRoot;

    /// <summary>
    /// Agrega un elemento a este <see cref="ObservableListWrap"/>.
    /// </summary>
    /// <param name="value">
    /// Valor a agregar a este <see cref="ObservableListWrap"/>.
    /// </param>
    /// <returns>
    /// El índice en el cual ha sido agregado el elemento.
    /// </returns>
    public int Add(object? value)
    {
        int returnValue = GetUnderlyingList().Add(value);
        RaiseCollectionChanged(new NcchEa(Nccha.Add, value, returnValue));
        Notify(nameof(Count));
        return returnValue;
    }

    /// <summary>
    /// Quita todos los elementos de este
    /// <see cref="ObservableListWrap"/>.
    /// </summary>
    public void Clear()
    {
        GetUnderlyingList().Clear();
        RaiseCollectionChanged(new NcchEa(Reset));
        Notify(nameof(Count));
    }

    /// <summary>
    /// Determina si este <see cref="ObservableListWrap"/> contiene un
    /// valor especificado.
    /// </summary>
    /// <param name="value">
    /// Valor a comprobar.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si este <see cref="ObservableListWrap"/>
    /// contiene el valor especificado, <see langword="false"/> en caso
    /// contrario.
    /// </returns>
    public override bool Contains(object? value)
    {
        return GetUnderlyingList().Contains(value);
    }

    /// <summary>
    /// Copia el contenido de este <see cref="ObservableListWrap"/> a
    /// un arreglo, iniciando en un índice en particular dentro del
    /// arreglo.
    /// </summary>
    /// <param name="array">Arreglo de destino de la copia.</param>
    /// <param name="index">
    /// Índice dentro del arreglo desde el cual iniciar a copiar.
    /// </param>
    public void CopyTo(Array array, int index)
    {
        GetUnderlyingList().CopyTo(array, index);
    }

    /// <summary>
    /// Obtiene un enumerador que itera sobre la colección.
    /// </summary>
    /// <returns>
    /// Un enumerador que puede ser utilizado para iterar sobre la colección.
    /// </returns>
    protected override IEnumerator OnGetEnumerator()
    {
        return GetUnderlyingList().GetEnumerator();
    }

    /// <summary>
    /// Obtiene el índice de un elemento específico dentro de este
    /// <see cref="ObservableListWrap"/>.
    /// </summary>
    /// <param name="value">
    /// Valor del cual obtener el índice dentro de este
    /// <see cref="ObservableListWrap"/>.
    /// </param>
    /// <returns>
    /// El índice del elemento dentro de este
    /// <see cref="ObservableListWrap"/>.
    /// </returns>
    public override int IndexOf(object? value)
    {
        return GetUnderlyingList().IndexOf(value);
    }

    /// <summary>
    /// Inserta un elemento dentro de este
    /// <see cref="ObservableListWrap"/> en el índice especificado.
    /// </summary>
    /// <param name="index">
    /// Índice en el cual realizar la inserción.
    /// </param>
    /// <param name="value">
    /// Valor a insertar en este <see cref="ObservableListWrap"/>.
    /// </param>
    public void Insert(int index, object? value)
    {
        GetUnderlyingList().Insert(index, value);
        RaiseCollectionChanged(new NcchEa(Nccha.Add, value, index));
        Notify(nameof(Count));
    }

    /// <summary>
    /// Quita un elemento de este <see cref="ObservableListWrap"/>.
    /// </summary>
    /// <param name="value">
    /// valor a quitar de este <see cref="ObservableListWrap"/>.
    /// </param>
    public void Remove(object? value)
    {
        if (GetUnderlyingList().Contains(value))
        {
            int index = UnderlyingList!.IndexOf(value);
            UnderlyingList.Remove(value);
            RaiseCollectionChanged(new NcchEa(Nccha.Remove, value, index));
            Notify(nameof(Count));
        }
    }

    /// <summary>
    /// Quita el elemento en el índice especificado de este
    /// <see cref="ObservableListWrap"/>.
    /// </summary>
    /// <param name="index">
    /// Índice del elemento a remover.
    /// </param>
    public void RemoveAt(int index)
    {
        object? item = GetUnderlyingList()[index];
        UnderlyingList!.RemoveAt(index);
        RaiseCollectionChanged(new NcchEa(Nccha.Remove, item, index));
        Notify(nameof(Count));
    }

    /// <summary>
    /// Obliga a notificar un cambio en la lista.
    /// </summary>
    public override void Refresh()
    {
        _ = GetUnderlyingList();
        base.Refresh();
        Substitute(UnderlyingList!);
    }

    /// <summary>
    /// Sustituye la lista subyacente por una nueva.
    /// </summary>
    /// <param name="newList">
    /// Lista a establecer como la lista subyacente.
    /// </param>
    public void Substitute(IList newList)
    {
        UnderlyingList = Array.Empty<object>();
        RaiseCollectionChanged(new NcchEa(Reset));
        UnderlyingList = newList;
        if (newList is not null) RaiseCollectionChanged(new NcchEa(Nccha.Add, newList, 0));
    }

    private IList GetUnderlyingList()
    {
        return UnderlyingList ?? throw new InvalidOperationException();
    }
}
