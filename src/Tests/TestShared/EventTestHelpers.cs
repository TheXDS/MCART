// EventTestHelpers.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2023 César Andrés Morgan
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

using NUnit.Framework;
using System.ComponentModel;
using System.Linq.Expressions;
using TheXDS.MCART.Helpers;

namespace TheXDS.MCART.Tests;

public static class EventTestHelpers
{
    /// <summary>
    /// Ejecuta la validación de un evento.
    /// </summary>
    /// <typeparam name="TObject">
    /// Tipo de objeto para el cual comprobar el evento.
    /// </typeparam>
    /// <typeparam name="TEventHandler">
    /// Tipo delegado del manejador de eventos.
    /// </typeparam>
    /// <typeparam name="TEventArgs">
    /// Tipo de argumentos de evento del manejador de eventos. Debe heredar de
    /// <see cref="EventArgs"/>.
    /// </typeparam>
    /// <param name="obj">Objeto para el cual comprobar el evento.</param>
    /// <param name="eventName">Nombre del evento a comprobar.</param>
    /// <param name="testDelegate">Delegado de prueba.</param>
    /// <param name="firedExpected">
    /// Si se establece en <see langword="true"/>, se verificará que el evento
    /// sea disparado, <see langword="false"/> verificará que el evento no sea
    /// generado.
    /// </param>
    /// <returns>
    /// Un objeto de tipo <typeparamref name="TEventArgs"/> con los argumentos
    /// del evento producido, o <see langword="null"/> en caso que no se
    /// produzca el evento.
    /// </returns>
    public static TEventArgs? TestEvent<TObject, TEventHandler, TEventArgs>(TObject obj, string eventName, Action<TObject> testDelegate, bool firedExpected = true)
        where TObject : class
        where TEventHandler : Delegate
        where TEventArgs : EventArgs
    {
        TEventArgs? args = null;
        var entry = new EventTestEntry<TObject, TEventArgs>(typeof(TEventHandler), eventName, firedExpected, e => args = e);
        TestEvents(obj, testDelegate, entry);
        return args;
    }

    /// <summary>
    /// Ejecuta la validación de un evento de cambio de valor de propiedad.
    /// </summary>
    /// <typeparam name="TObject">
    /// Tipo de objeto para el cual comprobar el evento.
    /// </typeparam>
    /// <param name="obj">Objeto para el cual comprobar el evento.</param>
    /// <param name="testDelegate">Delegado de prueba.</param>
    /// <param name="firedExpected">
    /// Si se establece en <see langword="true"/>, se verificará que el evento
    /// sea disparado, <see langword="false"/> verificará que el evento no sea
    /// generado.
    /// </param>
    /// <returns>
    /// Un objeto de tipo <typeparamref name="PropertyChangedEventArgs"/> con
    /// los argumentos del evento producido, o <see langword="null"/> en caso
    /// que no se produzca el evento.
    /// </returns>
    public static PropertyChangedEventArgs? TestNpcProperty<TObject>(TObject obj, Action<TObject> testDelegate,
        bool firedExpected = true)
        where TObject : class, INotifyPropertyChanged
    {
        return TestEvent<TObject, PropertyChangedEventHandler, PropertyChangedEventArgs>(obj, nameof(INotifyPropertyChanged.PropertyChanged), testDelegate, firedExpected);
    }

    /// <summary>
    /// Ejecuta la validación de un evento de cambio de valor de propiedad.
    /// </summary>
    /// <typeparam name="TObject">
    /// Tipo de objeto para el cual comprobar el evento.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Tipo de valor de la propiedad a comprobar.
    /// </typeparam>
    /// <param name="obj">Objeto para el cual comprobar el evento.</param>
    /// <param name="propertySelector">
    /// Selector de la propiedad a comprobar.
    /// </param>
    /// <param name="setTestValue">
    /// Valor de prueba. Se intentará establecer la propiedad a este valor.
    /// </param>
    /// <param name="firedExpected">
    /// Si se establece en <see langword="true"/>, se verificará que el evento
    /// sea disparado, <see langword="false"/> verificará que el evento no sea
    /// generado.
    /// </param>
    /// <param name="additionalPropChangeNotifications">
    /// Arreglo opcional de notificaciones adicionales que se deberían producir
    /// al cambiar el valor de la propiedad seleccionada.
    /// </param>
    public static void TestNpcProperty<TObject, TValue>(TObject obj, Expression<Func<TObject, TValue>> propertySelector, TValue setTestValue, bool firedExpected = true, params string[] additionalPropChangeNotifications)
        where TObject : class, INotifyPropertyChanged
    {
        var property = ReflectionHelpers.GetProperty(propertySelector);
        var allProps = additionalPropChangeNotifications.Concat(new[] { property.Name }).ToArray();
        var props = allProps.ToList();
        var changeEntry = new EventTestEntry<TObject, PropertyChangedEventArgs>(
            typeof(PropertyChangedEventHandler),
            nameof(INotifyPropertyChanged.PropertyChanged),
            firedExpected, e => Assert.That(props.Remove(e.PropertyName!)));
        TestEvents(obj, o => property.SetValue(o, setTestValue), changeEntry);
        if (firedExpected)
        {
            Assert.That(props, Is.Empty);
            Assert.That(property.GetValue(obj), Is.EqualTo(setTestValue));
        }
        else Assert.That(props, Is.EquivalentTo(allProps));
    }

    /// <summary>
    /// Ejecuta la validación de un evento de cambio de valor de propiedad.
    /// </summary>
    /// <typeparam name="TObject">
    /// Tipo de objeto para el cual comprobar el evento.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Tipo de valor de la propiedad a comprobar.
    /// </typeparam>
    /// <param name="obj">Objeto para el cual comprobar el evento.</param>
    /// <param name="propertySelector">
    /// Selector de la propiedad a comprobar.
    /// </param>
    /// <param name="setTestValue">
    /// Valor de prueba. Se intentará establecer la propiedad a este valor.
    /// </param>
    /// <param name="firedExpected">
    /// Si se establece en <see langword="true"/>, se verificará que el evento
    /// sea disparado, <see langword="false"/> verificará que el evento no sea
    /// generado.
    /// </param>
    /// <param name="additionalPropChangeNotifications">
    /// Arreglo opcional de notificaciones adicionales que se deberían producir
    /// al cambiar el valor de la propiedad seleccionada.
    /// </param>
    public static void TestNpcProperty<TObject, TValue>(TObject obj, Expression<Func<TObject, TValue>> propertySelector,
        TValue setTestValue, bool firedExpected = true, params Expression<Func<TObject, object?>>[] additionalPropChangeNotifications)
        where TObject : class, INotifyPropertyChanged
    {
        TestNpcProperty(
            obj, propertySelector, setTestValue, firedExpected,
            additionalPropChangeNotifications.Select(p => ReflectionHelpers.GetProperty(p).Name).ToArray());
    }
 
    /// <summary>
    /// Ejecuta la validación de un evento de cambio de valor de propiedad.
    /// </summary>
    /// <typeparam name="TObject">
    /// Tipo de objeto para el cual comprobar el evento.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Tipo de valor de la propiedad a comprobar.
    /// </typeparam>
    /// <param name="obj">Objeto para el cual comprobar el evento.</param>
    /// <param name="propertySelector">
    /// Selector de la propiedad a comprobar.
    /// </param>
    /// <param name="setTestValue">
    /// Valor de prueba. Se intentará establecer la propiedad a este valor.
    /// </param>
    /// <param name="firedExpected">
    /// Si se establece en <see langword="true"/>, se verificará que el evento
    /// sea disparado, <see langword="false"/> verificará que el evento no sea
    /// generado.
    /// </param>
    /// <param name="additionalPropChangeNotifications">
    /// Arreglo opcional de notificaciones adicionales que se deberían producir
    /// al cambiar el valor de la propiedad seleccionada.
    /// </param>
    public static void TestNpcProperty<TObject, TValue>(TObject obj, Expression<Func<TObject, TValue>> propertySelector,
        TValue setTestValue, params Expression<Func<TObject, object?>>[] additionalPropChangeNotifications)
        where TObject : class, INotifyPropertyChanged
    {
        TestNpcProperty(
            obj, propertySelector, setTestValue, true,
            additionalPropChangeNotifications.Select(p => ReflectionHelpers.GetProperty(p).Name).ToArray());
        TestNpcProperty(
            obj, propertySelector, setTestValue, false,
            additionalPropChangeNotifications.Select(p => ReflectionHelpers.GetProperty(p).Name).ToArray());
    }
    
    /// <summary>
    /// Ejecuta la validación de un evento de cambio de valor de propiedad.
    /// </summary>
    /// <typeparam name="TObject">
    /// Tipo de objeto para el cual comprobar el evento.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Tipo de valor de la propiedad a comprobar.
    /// </typeparam>
    /// <param name="propertySelector">
    /// Selector de la propiedad a comprobar.
    /// </param>
    /// <param name="setTestValue">
    /// Valor de prueba. Se intentará establecer la propiedad a este valor.
    /// </param>
    /// <param name="firedExpected">
    /// Si se establece en <see langword="true"/>, se verificará que el evento
    /// sea disparado, <see langword="false"/> verificará que el evento no sea
    /// generado.
    /// </param>
    /// <param name="additionalPropChangeNotifications">
    /// Arreglo opcional de notificaciones adicionales que se deberían producir
    /// al cambiar el valor de la propiedad seleccionada.
    /// </param>
    public static void TestNpcProperty<TObject, TValue>(Expression<Func<TObject, TValue>> propertySelector,
        TValue setTestValue, bool firedExpected = true, params Expression<Func<TObject, object?>>[] additionalPropChangeNotifications)
        where TObject : class, INotifyPropertyChanged, new()
    {
        TestNpcProperty(
            new TObject(), propertySelector, setTestValue, firedExpected,
            additionalPropChangeNotifications);
    }
    
    /// <summary>
    /// Ejecuta la validación de un evento de cambio de valor de propiedad.
    /// </summary>
    /// <typeparam name="TObject">
    /// Tipo de objeto para el cual comprobar el evento.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Tipo de valor de la propiedad a comprobar.
    /// </typeparam>
    /// <param name="propertySelector">
    /// Selector de la propiedad a comprobar.
    /// </param>
    /// <param name="setTestValue">
    /// Valor de prueba. Se intentará establecer la propiedad a este valor.
    /// </param>
    /// <param name="additionalPropChangeNotifications">
    /// Arreglo opcional de notificaciones adicionales que se deberían producir
    /// al cambiar el valor de la propiedad seleccionada.
    /// </param>
    public static void TestNpcProperty<TObject, TValue>(Expression<Func<TObject, TValue>> propertySelector,
        TValue setTestValue, params Expression<Func<TObject, object?>>[] additionalPropChangeNotifications)
        where TObject : class, INotifyPropertyChanged, new()
    {
        var instance = new TObject();
        TestNpcProperty(instance, propertySelector, setTestValue, true,
            additionalPropChangeNotifications);
        TestNpcProperty(instance, propertySelector, setTestValue, false,
            additionalPropChangeNotifications);
    }

    /// <summary>
    /// Ejecuta una validación de múltiples eventos.
    /// </summary>
    /// <typeparam name="TObject">
    /// Tipo de objeto para el cual comprobar los eventos.
    /// </typeparam>
    /// <param name="obj">Objeto para el cual comprobar los eventos.</param>
    /// <param name="testDelegate">
    /// Delegado de prueba de debería generar los eventos a comprobar.</param>
    /// <param name="events">Eventos a comprobar.</param>
    public static void TestEvents<TObject>(TObject obj, Action<TObject> testDelegate, IEnumerable<IEventTestEntry<TObject, EventArgs>> events)
        where TObject : class
    {
        var e = events.ToArray();
        foreach (var evt in e)
        {
            evt.SetupEventHandling(obj);
        }
        testDelegate.Invoke(obj);
        foreach (var evt in e)
        {
            evt.TeardownEventHandling(obj);
        }
    }

    /// <summary>
    /// Ejecuta una validación de múltiples eventos.
    /// </summary>
    /// <typeparam name="TObject">
    /// Tipo de objeto para el cual comprobar los eventos.
    /// </typeparam>
    /// <param name="obj">Objeto para el cual comprobar los eventos.</param>
    /// <param name="testDelegate">
    /// Delegado de prueba de debería generar los eventos a comprobar.</param>
    /// <param name="events">Eventos a comprobar.</param>
    public static void TestEvents<TObject>(TObject obj, Action<TObject> testDelegate, params IEventTestEntry<TObject, EventArgs>[] events)
           where TObject : class
    {
        TestEvents(obj, testDelegate, events.AsEnumerable());
    }
}
