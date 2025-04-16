/*
BooleanToBlurEffectConverterTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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

using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Effects;
using TheXDS.MCART.ValueConverters;

namespace TheXDS.MCART.Wpf.Common.Tests.ValueConverters;

public class BooleanToBlurEffectConverterTests
{
    [Test]
    public void GetEffect_Test()
    {
        BooleanToBlurEffectConverter c = new();
        Assert.That(c.Convert(false, typeof(Effect), null, CultureInfo.CurrentCulture), Is.Null);
        object e = c.Convert(true, typeof(Effect), null, CultureInfo.CurrentCulture);
        Assert.That(e, Is.AssignableFrom<BlurEffect>());
        Assert.That(5.0, Is.EqualTo(((BlurEffect)e).Radius));
    }

    [Test]
    public void Arguments_Test()
    {
        BooleanToBlurEffectConverter c = new();
        Assert.That(5.0, Is.EqualTo(((BlurEffect)c.Convert(true, typeof(Effect), null, CultureInfo.CurrentCulture)).Radius));
        Assert.That(4.0, Is.EqualTo(((BlurEffect)c.Convert(true, typeof(Effect), "4", CultureInfo.CurrentCulture)).Radius));
        Assert.That(3.0, Is.EqualTo(((BlurEffect)c.Convert(true, typeof(Effect), 3f, CultureInfo.CurrentCulture)).Radius));
        Assert.That(2.0, Is.EqualTo(((BlurEffect)c.Convert(true, typeof(Effect), 2.0, CultureInfo.CurrentCulture)).Radius));
        Assert.That(1.0, Is.EqualTo(((BlurEffect)c.Convert(true, typeof(Effect), 1m, CultureInfo.CurrentCulture)).Radius));
        Assert.That(2.0, Is.EqualTo(((BlurEffect)c.Convert(true, typeof(Effect), (byte)2, CultureInfo.CurrentCulture)).Radius));
        Assert.That(3.0, Is.EqualTo(((BlurEffect)c.Convert(true, typeof(Effect), (short)3, CultureInfo.CurrentCulture)).Radius));
        Assert.That(4.0, Is.EqualTo(((BlurEffect)c.Convert(true, typeof(Effect), 4, CultureInfo.CurrentCulture)).Radius));
        Assert.That(5.0, Is.EqualTo(((BlurEffect)c.Convert(true, typeof(Effect), 5L, CultureInfo.CurrentCulture)).Radius));
        Assert.That(5.0, Is.EqualTo(((BlurEffect)c.Convert(true, typeof(Effect), new object(), CultureInfo.CurrentCulture)).Radius));
    }

    [Test]
    public void ConvertBack_Test()
    {
        BooleanToBlurEffectConverter c = new();
        Assert.That(c.ConvertBack("Test"), Is.False);
        Assert.That(c.ConvertBack(new BlurEffect()), Is.True);
        Assert.That((bool)((IValueConverter)c).ConvertBack("Test", typeof(bool), null!, CultureInfo.CurrentCulture), Is.False);
        Assert.That((bool)((IValueConverter)c).ConvertBack(new BlurEffect(), typeof(bool), null!, CultureInfo.CurrentCulture), Is.True);
    }
}
