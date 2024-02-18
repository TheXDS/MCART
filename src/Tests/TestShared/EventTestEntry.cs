// EventTestHelpers.cs
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

using System.Reflection;

namespace TheXDS.MCART.Tests;

public class EventTestEntry<TObject, TEventArgs> : IEventTestEntry<TObject, TEventArgs> where TEventArgs : EventArgs
{
    private EventInfo? eventInfo;
    private EventTriggerTest<TEventArgs>? trigger;
    private Delegate? eventHandler;

    public EventTestEntry(Type eventHandlerType, string eventName, Action<TEventArgs>? eventArgsAssertions) : this(eventHandlerType, eventName, true, eventArgsAssertions) { }

    public EventTestEntry(Type eventHandlerType, string eventName, bool firedExpected = true, Action<TEventArgs>? eventArgsAssertions = null)
    {
        EventHandlerType = eventHandlerType;
        EventName = eventName;
        FiredExpected = firedExpected;
        EventArgsAssertions = eventArgsAssertions;
    }

    public Type EventHandlerType { get; }

    public string EventName { get; }

    public bool FiredExpected { get; }

    public Action<TEventArgs>? EventArgsAssertions { get; }

    void IEventTestEntry<TObject, TEventArgs>.SetupEventHandling(TObject obj)
    {
        eventInfo = typeof(TObject).GetEvent(EventName);
        if (eventInfo is null)
        {
            Assert.Fail();
            return;
        }
        trigger = new EventTriggerTest<TEventArgs>();
        eventHandler = Delegate.CreateDelegate(EventHandlerType, trigger, trigger.EventCallback, true)!;
        eventInfo.AddEventHandler(obj, eventHandler);
    }

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
