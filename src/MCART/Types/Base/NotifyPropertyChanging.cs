/*
NotifyPropertyChanging.cs

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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Helpers;

/// <summary>
/// Clase base para los objetos que puedan notificar sobre el cambio
/// del valor de una de sus propiedades.
/// </summary>
public abstract class NotifyPropertyChanging : NotifyPropertyChangeBase, INotifyPropertyChanging
{
    /// <summary>
    /// Se produce cuando cambia el valor de una propiedad.
    /// </summary>
    public event PropertyChangingEventHandler? PropertyChanging;

    /// <summary>
    /// Notifica a los clientes que el valor de una propiedad cambiará.
    /// </summary>
    protected virtual void OnPropertyChanging([CallerMemberName] string? propertyName = null)
    {
        if (propertyName is null) throw new ArgumentNullException(nameof(propertyName));
        PropertyChanging?.Invoke(this, new(propertyName));
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
    protected sealed override bool Change<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (propertyName is null) throw new ArgumentNullException(nameof(propertyName));
        if (field?.Equals(value) ?? Objects.AreAllNull(field, value)) return false;
        Notify(propertyName);
        field = value;
        return true;
    }

    /// <summary>
    /// Notifica el cambio en el valor de una propiedad.
    /// </summary>
    /// <param name="property">
    /// Propiedad a notificar.
    /// </param>
    public override void Notify(string property)
    {
        OnPropertyChanging(property);
    }
}
