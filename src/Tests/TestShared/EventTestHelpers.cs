// EventTestHelpers.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      C�sar Andr�s Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright � 2011 - 2023 C�sar Andr�s Morgan
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the �Software�), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED �AS IS�, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

namespace TheXDS.MCART.Tests;

public static class EventTestHelpers
{
    /// <summary>
    /// Ejecuta la validaci�n de un evento.
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
    /// Si se establece en <see langword="true"/>, se verificar� que el evento
    /// sea disparado, <see langword="false"/> verificar� que el evento no sea
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
    /// Ejecuta una validaci�n de m�ltiples eventos.
    /// </summary>
    /// <typeparam name="TObject">
    /// Tipo de objeto para el cual comprobar los eventos.
    /// </typeparam>
    /// <param name="obj">Objeto para el cual comprobar los eventos.</param>
    /// <param name="testDelegate">
    /// Delegado de prueba de deber�a generar los eventos a comprobar.</param>
    /// <param name="events">Eventos a comprobar.</param>
    public static void TestEvents<TObject>(TObject obj, Action<TObject> testDelegate, IEnumerable<IEventTestEntry<TObject, EventArgs>> events)
        where TObject : class
    {
        foreach (var evt in events)
        {
            evt.SetupEventHandling(obj);
        }
        testDelegate.Invoke(obj);
        foreach (var evt in events)
        {
            evt.TeardownEventHandling(obj);
        }
    }

    /// <summary>
    /// Ejecuta una validaci�n de m�ltiples eventos.
    /// </summary>
    /// <typeparam name="TObject">
    /// Tipo de objeto para el cual comprobar los eventos.
    /// </typeparam>
    /// <param name="obj">Objeto para el cual comprobar los eventos.</param>
    /// <param name="testDelegate">
    /// Delegado de prueba de deber�a generar los eventos a comprobar.</param>
    /// <param name="events">Eventos a comprobar.</param>
    public static void TestEvents<TObject>(TObject obj, Action<TObject> testDelegate, params IEventTestEntry<TObject, EventArgs>[] events)
           where TObject : class
    {
        TestEvents(obj, testDelegate, events.AsEnumerable());
    }
}
