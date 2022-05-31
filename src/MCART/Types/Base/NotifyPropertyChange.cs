/*
NotifyPropertyChange.cs

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

namespace TheXDS.MCART.Types.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Resources;

/// <summary>
/// Clase base para los objetos que puedan notificar sobre el cambio
/// del valor de una de sus propiedades, tanto antes como después de
/// haber ocurrido dicho cambio.
/// </summary>
public abstract class NotifyPropertyChange : NotifyPropertyChangeBase, INotifyPropertyChanging, INotifyPropertyChanged
{
    private readonly HashSet<WeakReference<PropertyChangeObserver>> _observeSubscriptions = new();

    /// <summary>
    /// Se produce cuando se cambiará el valor de una propiedad.
    /// </summary>
    public event PropertyChangingEventHandler? PropertyChanging;

    /// <summary>
    /// Ocurre cuando el valor de una propiedad ha cambiado.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Notifica a los clientes que el valor de una propiedad cambiará.
    /// </summary>
    protected virtual void OnPropertyChanging([CallerMemberName] string propertyName = null!)
    {
        if (propertyName is null) throw new ArgumentNullException(nameof(propertyName));
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
    }

    /// <summary>
    /// Notifica a los clientes que el valor de una propiedad ha
    /// cambiado.
    /// </summary>
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!)
    {
        if (propertyName is null) throw new ArgumentNullException(nameof(propertyName));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        NotifyRegistroir(propertyName);
        foreach (INotifyPropertyChangeBase? j in _forwardings) j.Notify(propertyName);
    }

    /// <summary>
    /// Cambia el valor de un campo, y genera los eventos de
    /// notificación correspondientes.
    /// </summary>
    /// <typeparam name="T">Tipo de valores a procesar.</typeparam>
    /// <param name="field">Campo a actualizar.</param>
    /// <param name="value">Nuevo valor del campo.</param>
    /// <param name="propertyName">
    /// Nombre de la propiedad. Por lo general, este valor debe
    /// omitirse.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si el valor de la propiedad ha
    /// cambiado, <see langword="false"/> en caso contrario.
    /// </returns>
    [NpcChangeInvocator]
    protected sealed override bool Change<T>(ref T field, T value, [CallerMemberName] string propertyName = null!)
    {
        if (field?.Equals(value) ?? Objects.AreAllNull(field, value)) return false;
        if (ReflectionHelpers.GetCallingMethod() is not { } m || GetType().GetProperties().SingleOrDefault(q => q.SetMethod == m) is not { } p)
        {
            throw Errors.PropSetMustCall();
        }
        if (p.Name != propertyName) throw Errors.PropChangeSame();

        OnPropertyChanging(propertyName);
        field = value;

        HashSet<WeakReference<PropertyChangeObserver>>? rm = new();
        foreach (WeakReference<PropertyChangeObserver>? j in _observeSubscriptions)
        {
            if (j.TryGetTarget(out PropertyChangeObserver? t)) t.Invoke(this, p);
            else rm.Add(j);
        }
        foreach (WeakReference<PropertyChangeObserver>? j in rm)
        {
            _observeSubscriptions.Remove(j);
        }
        OnPropertyChanged(propertyName);
        return true;
    }

    /// <summary>
    /// Suscribe a un delegado para observar el cambio de una propiedad.
    /// </summary>
    /// <param name="callback">Delegado a suscribir.</param>
    public void Subscribe(PropertyChangeObserver callback)
    {
        _observeSubscriptions.Add(new WeakReference<PropertyChangeObserver>(callback));
    }

    /// <summary>
    /// Quita al delegado previamente suscrito de la lista de
    /// suscripciones de notificación de cambios de propiedad.
    /// </summary>
    /// <param name="callback">Delegado a quitar.</param>
    public void Unsubscribe(PropertyChangeObserver callback)
    {
        WeakReference<PropertyChangeObserver>? rm = _observeSubscriptions.FirstOrDefault(p => p.TryGetTarget(out PropertyChangeObserver? t) && t == callback);
        if (rm is not null) _observeSubscriptions.Remove(rm);
    }

    /// <summary>
    /// Notifica el cambio en el valor de una propiedad.
    /// </summary>
    /// <param name="property">
    /// Propiedad a notificar.
    /// </param>
    public override void Notify(string property)
    {
        OnPropertyChanged(property);
    }
}

/// <summary>
/// Delegado que define un método para observar y procesar cambios en
/// el valor de una propiedad asociada a un objeto.
/// </summary>
/// <param name="instance">
/// Instancia del objeto a observar.
/// </param>
/// <param name="property">
/// Propiedad observada.
/// </param>
public delegate void PropertyChangeObserver(object instance, PropertyInfo property);
