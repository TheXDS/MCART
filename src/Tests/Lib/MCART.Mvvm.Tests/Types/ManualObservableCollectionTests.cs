// ManualObservableCollectionTests.cs
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
using System.Collections;
using TheXDS.MCART.Types;

namespace TheXDS.MCART.Mvvm.Tests.Types;

public class ManualObservableCollectionTests
{
    [Test]
    public void Observable_triggers_CollectionChanged_event()
    {
        var triggered = false;
        var obs = new ManualObservableCollection<int>(Array.Empty<int>());
        obs.CollectionChanged += (_, __) => triggered = true;
        obs.NotifyChange();
        Assert.That(triggered, Is.True);
    }

    [Test]
    public void Observable_implements_IEnumerable()
    {
        var obs = new ManualObservableCollection<int>(Enumerable.Range(0, 4));

        Assert.That(obs.ToArray(), Is.EquivalentTo(new[] { 0, 1, 2, 3 }));

        Assert.That((IEnumerable)obs, Is.EquivalentTo(new[] { 0, 1, 2, 3 }));

    }
}