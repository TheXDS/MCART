/*
BooleanToBlurEffectConverterTests.cs

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

namespace TheXDS.MCART.Wpf.Tests.ValueConverters;
using NUnit.Framework;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Effects;
using TheXDS.MCART.ValueConverters;

public class BooleanToBlurEffectConverterTests
{
    [Test]
    public void GetEffect_Test()
    {
        BooleanToBlurEffectConverter c = new();
        Assert.Null(c.Convert(false, typeof(Effect), null, CultureInfo.CurrentCulture));
        object e = c.Convert(true, typeof(Effect), null, CultureInfo.CurrentCulture);
        Assert.IsAssignableFrom<BlurEffect>(e);
        Assert.AreEqual(5.0, ((BlurEffect)e).Radius);
    }

    [Test]
    public void Arguments_Test()
    {
        BooleanToBlurEffectConverter c = new();
        Assert.AreEqual(5.0, ((BlurEffect)c.Convert(true, typeof(Effect), null, CultureInfo.CurrentCulture)).Radius);
        Assert.AreEqual(4.0, ((BlurEffect)c.Convert(true, typeof(Effect), "4", CultureInfo.CurrentCulture)).Radius);
        Assert.AreEqual(3.0, ((BlurEffect)c.Convert(true, typeof(Effect), 3f, CultureInfo.CurrentCulture)).Radius);
        Assert.AreEqual(2.0, ((BlurEffect)c.Convert(true, typeof(Effect), 2.0, CultureInfo.CurrentCulture)).Radius);
        Assert.AreEqual(1.0, ((BlurEffect)c.Convert(true, typeof(Effect), 1m, CultureInfo.CurrentCulture)).Radius);
        Assert.AreEqual(2.0, ((BlurEffect)c.Convert(true, typeof(Effect), (byte)2, CultureInfo.CurrentCulture)).Radius);
        Assert.AreEqual(3.0, ((BlurEffect)c.Convert(true, typeof(Effect), (short)3, CultureInfo.CurrentCulture)).Radius);
        Assert.AreEqual(4.0, ((BlurEffect)c.Convert(true, typeof(Effect), 4, CultureInfo.CurrentCulture)).Radius);
        Assert.AreEqual(5.0, ((BlurEffect)c.Convert(true, typeof(Effect), 5L, CultureInfo.CurrentCulture)).Radius);
        Assert.AreEqual(5.0, ((BlurEffect)c.Convert(true, typeof(Effect), new object(), CultureInfo.CurrentCulture)).Radius);
    }

    [Test]
    public void ConvertBack_Test()
    {
        BooleanToBlurEffectConverter c = new();
        Assert.False(c.ConvertBack("Test"));
        Assert.True(c.ConvertBack(new BlurEffect()));
        Assert.False((bool)((IValueConverter)c).ConvertBack("Test", typeof(bool), null!, CultureInfo.CurrentCulture));
        Assert.True((bool)((IValueConverter)c).ConvertBack(new BlurEffect(), typeof(bool), null!, CultureInfo.CurrentCulture));
    }
}
