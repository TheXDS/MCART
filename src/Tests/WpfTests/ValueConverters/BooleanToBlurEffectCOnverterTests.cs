/*
BooleanToBlurEffectConverterTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using NUnit.Framework;
using System.Globalization;
using System.Windows.Media.Effects;
using TheXDS.MCART.ValueConverters;

namespace TheXDS.MCART.WpfTests.ValueConverters
{
    public class BooleanToBlurEffectConverterTests
    {
        [Test]
        public void GetEffectTest()
        {
            var c = new BooleanToBlurEffectConverter();
            Assert.Null(c.Convert(false, typeof(Effect), null, CultureInfo.CurrentCulture));
            var e = c.Convert(true, typeof(Effect), null, CultureInfo.CurrentCulture);
            Assert.IsAssignableFrom<BlurEffect>(e);
            Assert.AreEqual(5.0, ((BlurEffect)e).Radius);
        }

        [Test]
        public void ArgumentsTest()
        {
            var c = new BooleanToBlurEffectConverter();
            Assert.AreEqual(5.0, ((BlurEffect)c.Convert(true, typeof(Effect), null, CultureInfo.CurrentCulture)!).Radius);
            Assert.AreEqual(4.0, ((BlurEffect)c.Convert(true, typeof(Effect), "4", CultureInfo.CurrentCulture)!).Radius);
            Assert.AreEqual(3.0, ((BlurEffect)c.Convert(true, typeof(Effect), 3f, CultureInfo.CurrentCulture)!).Radius);
            Assert.AreEqual(2.0, ((BlurEffect)c.Convert(true, typeof(Effect), 2.0, CultureInfo.CurrentCulture)!).Radius);
            Assert.AreEqual(1.0, ((BlurEffect)c.Convert(true, typeof(Effect), 1m, CultureInfo.CurrentCulture)!).Radius);
            Assert.AreEqual(2.0, ((BlurEffect)c.Convert(true, typeof(Effect), (byte)2, CultureInfo.CurrentCulture)!).Radius);
            Assert.AreEqual(3.0, ((BlurEffect)c.Convert(true, typeof(Effect), (short)3, CultureInfo.CurrentCulture)!).Radius);
            Assert.AreEqual(4.0, ((BlurEffect)c.Convert(true, typeof(Effect), 4, CultureInfo.CurrentCulture)!).Radius);
            Assert.AreEqual(5.0, ((BlurEffect)c.Convert(true, typeof(Effect), 5L, CultureInfo.CurrentCulture)!).Radius);
    #if PreferExceptions
            Assert.Throws<ArgumentException>(() => c.Convert(true, typeof(Effect), new object(), CultureInfo.CurrentCulture));
    #else
            Assert.AreEqual(5.0, ((BlurEffect)c.Convert(true, typeof(Effect), new object(), CultureInfo.CurrentCulture)).Radius);
    #endif
        }
    }
}
