/*
EnumValueProvierTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

using TheXDS.MCART.Component;
using TheXDS.MCART.Types;

namespace TheXDS.MCART.Tests.Platform.Shared.Xaml.Component;

public class NamedEnumValueProviderTests
{
    [Test]
    public void NamedEnumValueProvider_Returns_Correct_Values()
    {
        var provider = new NamedEnumValueProvider() { EnumType = typeof(DayOfWeek) };
        var values = provider.ProvideValue(null!);
        Assert.That(values, Is.AssignableTo<IEnumerable<NamedObject<Enum>>>());
        var dict = ((IEnumerable<NamedObject<Enum>>)values).Select(kv => (KeyValuePair<string, Enum>)kv).ToDictionary();
        Assert.Multiple(() =>
        {
            Assert.That(dict.Count, Is.EqualTo(7));
            Assert.That(dict, Does.ContainKey("Monday").WithValue(DayOfWeek.Monday));
            Assert.That(dict, Does.ContainKey("Tuesday").WithValue(DayOfWeek.Tuesday));
            Assert.That(dict, Does.ContainKey("Wednesday").WithValue(DayOfWeek.Wednesday));
            Assert.That(dict, Does.ContainKey("Thursday").WithValue(DayOfWeek.Thursday));
            Assert.That(dict, Does.ContainKey("Friday").WithValue(DayOfWeek.Friday));
            Assert.That(dict, Does.ContainKey("Saturday").WithValue(DayOfWeek.Saturday));
            Assert.That(dict, Does.ContainKey("Sunday").WithValue(DayOfWeek.Sunday));
        });
    }

    [Test]
    public void NamedEnumValueProvider_returns_empty_on_forcefully_initialized_non_enum_type()
    {
        var provider = new NamedEnumValueProvider();

        typeof(EnumValueProvider)
            .GetField("enumType", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
            .SetValue(provider, typeof(object));

        var values = provider.ProvideValue(null!);
        Assert.That(values, Is.AssignableTo<IEnumerable<NamedObject<Enum>>>());
        Assert.That(((IEnumerable<NamedObject<Enum>>)values).Count(), Is.EqualTo(0));
    }

    [Test]
    public void NamedEnumValueProvider_returns_empty_on_null_enum_type()
    {
        var provider = new NamedEnumValueProvider() { EnumType = null };
        var values = provider.ProvideValue(null!);
        Assert.That(values, Is.AssignableTo<IEnumerable<NamedObject<Enum>>>());
        Assert.That(((IEnumerable<NamedObject<Enum>>)values).Count(), Is.EqualTo(0));
    }
}
