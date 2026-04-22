// EventTestEntry.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2026 César Andrés Morgan
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

using System.Reflection;

namespace TheXDS.MCART.Tests;

/// <summary>
/// Represents a test entry for verifying event handling functionality.
/// </summary>
/// <typeparam name="TObject">The type of object that raises the event.</typeparam>
/// <typeparam name="TEventArgs">The type of event arguments raised by the event.</typeparam>
public class EventTestEntry<TObject, TEventArgs>(Type eventHandlerType, string eventName, bool firedExpected = true, Action<TEventArgs>? eventArgsAssertions = null)
    : IEventTestEntry<TObject, TEventArgs> where TEventArgs : EventArgs
{
    private EventInfo? eventInfo;
    private EventTriggerTest<TEventArgs>? trigger;
    private Delegate? eventHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventTestEntry{TObject, TEventArgs}"/> class.
    /// </summary>
    /// <param name="eventHandlerType">The type of the event handler delegate.</param>
    /// <param name="eventName">The name of the event to test.</param>
    /// <param name="firedExpected">A value indicating whether the event is expected to be fired.</param>
    /// <param name="eventArgsAssertions">Optional assertions to perform on event arguments.</param>
    public EventTestEntry(Type eventHandlerType, string eventName, Action<TEventArgs>? eventArgsAssertions) : this(eventHandlerType, eventName, true, eventArgsAssertions) { }

    /// <summary>
    /// Gets the type of the event handler associated with the event.
    /// </summary>
    public Type EventHandlerType { get; } = eventHandlerType;

    /// <summary>
    /// Gets the name of the event being tested.
    /// </summary>
    public string EventName { get; } = eventName;

    /// <summary>
    /// Gets a value indicating whether the event is expected to be fired.
    /// </summary>
    public bool FiredExpected { get; } = firedExpected;

    /// <summary>
    /// Gets the assertions to perform on event arguments, if any.
    /// </summary>
    public Action<TEventArgs>? EventArgsAssertions { get; } = eventArgsAssertions;

    /// <summary>
    /// Sets up event handling for the specified object.
    /// </summary>
    /// <param name="obj">The object for which to set up event handling.</param>
    void IEventTestEntry<TObject, TEventArgs>.SetupEventHandling(TObject obj)
    {
        eventInfo = typeof(TObject).GetEvent(EventName);
        if (eventInfo is null)
        {
            Assert.Fail();
            return;
        }
        trigger = [];
        eventHandler = Delegate.CreateDelegate(EventHandlerType, trigger, trigger.EventCallback, true)!;
        eventInfo.AddEventHandler(obj, eventHandler);
    }

    /// <summary>
    /// Tears down event handling for the specified object.
    /// </summary>
    /// <param name="obj">The object for which to tear down event handling.</param>
    void IEventTestEntry<TObject, TEventArgs>.TeardownEventHandling(TObject obj)
    {
        eventInfo?.RemoveEventHandler(obj, eventHandler);
        if (trigger is not null)
        {
            Assert.That(trigger.EventFired, Is.EqualTo(FiredExpected));
            foreach (var eventItem in trigger)
            {
                Assert.That(eventItem, Is.Not.Null);
                EventArgsAssertions?.Invoke(eventItem!);
            }
        }
    }
}
