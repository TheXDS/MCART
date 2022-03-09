/*
NameValueCollectionExtensionsTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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

using System;
using System.Text;
using TheXDS.MCART.Types.Extensions;
using NUnit.Framework;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class StringBuilderExtensionsTests
    {
        [Test]
        public void AppendLineIfNotNull_Test()
        {
            StringBuilder sb = new();
            sb.AppendLineIfNotNull(null);
            Assert.True(sb.ToString().IsEmpty());
            sb.AppendLineIfNotNull("test");
            Assert.AreEqual($"test{Environment.NewLine}", sb.ToString());
            sb.AppendLineIfNotNull(null);
            Assert.AreEqual($"test{Environment.NewLine}", sb.ToString());
        }

        [Test]
        public void AppendAndWrap_Test()
        {
            StringBuilder sb = new();
            string s = new('x', 120);
            sb.AppendAndWrap(s, 80);
            string[]? sa = sb.ToString().Split(Environment.NewLine);
            Assert.AreEqual(80, sa[0].Length);
            Assert.AreEqual(40, sa[1].Length);
        }
    }
}