/*
ObservableWrapBase_Tests.cs

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

using NUnit.Framework;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using TheXDS.MCART.Tests;
using TheXDS.MCART.Types.Base;
using static TheXDS.MCART.Tests.EventTestHelpers;

namespace TheXDS.MCART.Mvvm.Tests.Types.Base;

public class ObservableWrapBase_Tests
{
    private class TestClass : ObservableWrapBase
    {
        private static readonly string[] testData = { "test 1", "test 2", "test 3" };

        protected override IEnumerator OnGetEnumerator() => testData.GetEnumerator();

        public void ChangeCollection()
        {
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }

    private class NpcTest : NotifyPropertyChanged
    {
        private string? _StringProperty;
        private int _IntProperty;

        public string? StringProperty
        {
            get => _StringProperty;
            set => Change(ref _StringProperty, value);
        }

        public int IntProperty
        {
            get => _IntProperty;
            set => Change(ref _IntProperty, value);
        }
    }

    [TestCase("test 1", 0)]
    [TestCase("test 2", 1)]
    [TestCase("test 3", 2)]
    [TestCase("test 4", -1)]
    public void IndexOf_test(string item, int expectedIndex)
    {
        var c = new TestClass();
        Assert.That(c.IndexOf(item), Is.EqualTo(expectedIndex));
    }

    [Test]
    public void ForwardNotify_test()
    {
        var intPropNotified = false;
        var stringPropNotified = false;
        var c = new TestClass();
        var npc = new NpcTest();
        c.ForwardNotify(npc, nameof(NpcTest.IntProperty));
        c.ForwardNotify(npc, nameof(NpcTest.StringProperty));
        TestEvents(npc, _ => c.ChangeCollection(),
            new EventTestEntry<NpcTest, PropertyChangedEventHandler, PropertyChangedEventArgs>(nameof(NpcTest.PropertyChanged),
                e =>
                {
                    switch (e.PropertyName)
                    {
                        case nameof(NpcTest.IntProperty):
                            intPropNotified = true;
                            break;
                        case nameof(NpcTest.StringProperty):
                            stringPropNotified = true;
                            break;
                        default:
                            Assert.Fail();
                            break;
                    }
                })
        );
        Assert.That(intPropNotified);
        Assert.That(stringPropNotified);
    }
}
