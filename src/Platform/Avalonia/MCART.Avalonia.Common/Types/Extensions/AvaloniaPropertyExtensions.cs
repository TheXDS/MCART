// AvaloniaPropertyExtensions.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2024 César Andrés Morgan
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the “Software”), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Avalonia;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Incluye extensiones que simplifican la sintaxis necesaria para trabajar con
/// objetos de tipo <see cref="AvaloniaProperty{TValue}"/>.
/// </summary>
public static class AvaloniaPropertyExtensions
{
    /// <summary>
    /// Define un método a invocar cuando un
    /// <see cref="AvaloniaProperty{TValue}"/> cambia su valor.
    /// </summary>
    /// <typeparam name="TValue">Tipo de valor de la propiedad.</typeparam>
    /// <param name="change">
    /// Información sobre el cambio de valor de la propiedad.
    /// </param>
    public delegate void ChangeAction<TValue>(AvaloniaPropertyChangedEventArgs<TValue> change);

    /// <summary>
    /// Define un método a invocar cuando un
    /// <see cref="AvaloniaProperty{TValue}"/> cambia su valor.
    /// </summary>
    /// <typeparam name="TObj">
    /// Tipo donde se ha definido la propiedad.
    /// </typeparam>
    /// <typeparam name="TValue">Tipo de valor de la propiedad.</typeparam>
    /// <param name="obj">
    /// Instancia en donde la propiedad ha cambiado su valor.
    /// </param>
    /// <param name="oldValue">Valor anterior de la propiedad.</param>
    /// <param name="newValue">Nuevo valor de la propiedad..</param>
    public delegate void ChangeAction<TObj, TValue>(TObj obj, TValue? oldValue, TValue? newValue);

    private class Observer<TValue>(ChangeAction<TValue> callback) : IObserver<AvaloniaPropertyChangedEventArgs<TValue>>
    {
        private readonly ChangeAction<TValue> callback = callback;

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(AvaloniaPropertyChangedEventArgs<TValue> value)
        {
            callback.Invoke(value);
        }
    }

    /// <summary>
    /// Registra un método a invocar cuando la propiedad cambie su valor.
    /// </summary>
    /// <typeparam name="TValue">Tipo de valor de la propiedad.</typeparam>
    /// <param name="prop">
    /// Propiedad en la cual registrar el método a ejecutar cuando la propiedad
    /// cambie de valor.
    /// </param>
    /// <param name="callback">
    /// Método a ejecutar cuando la propiedad cambie de valor.
    /// </param>
    /// <returns>
    /// Un nuevo objeto que puede ser utilizado para desechar la instancia
    /// interna de <see cref="IObserver{T}"/> asociado al método que se ejecuta
    /// cuando la propiedad cambie su valor.
    /// </returns>
    public static IDisposable OnChanged<TValue>(this AvaloniaProperty<TValue> prop, ChangeAction<TValue> callback)
    {
        return prop.Changed.Subscribe(new Observer<TValue>(callback));
    }

    /// <summary>
    /// Registra un método a invocar cuando la propiedad cambie su valor.
    /// </summary>
    /// <typeparam name="TObj">
    /// Tipo donde se ha definido la propiedad.
    /// </typeparam>
    /// <typeparam name="TValue">Tipo de valor de la propiedad.</typeparam>
    /// <param name="prop">
    /// Propiedad en la cual registrar el método a ejecutar cuando la propiedad
    /// cambie de valor.
    /// </param>
    /// <param name="callback">Método a ejecutar.</param>
    /// <returns>
    /// Un nuevo objeto que puede ser utilizado para desechar la instancia
    /// interna de <see cref="IObserver{T}"/> asociado al método que se ejecuta
    /// cuando la propiedad cambie su valor.
    /// </returns>
    public static IDisposable OnChanged<TObj, TValue>(this AvaloniaProperty<TValue> prop, ChangeAction<TObj, TValue> callback)
        where TObj : AvaloniaObject
    {
        return prop.Changed.Subscribe(new Observer<TValue>(change =>
        {
            callback.Invoke(
                (TObj)change.Sender,
                change.OldValue.HasValue ? change.OldValue.Value : default,
                change.NewValue.HasValue ? change.NewValue.Value : default);
        }));
    }
}