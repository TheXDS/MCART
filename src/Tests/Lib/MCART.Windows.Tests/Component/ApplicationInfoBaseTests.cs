// ApplicationInfoBaseTests.cs
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

using System.Drawing;
using System.Reflection;
using TheXDS.MCART.Component;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Exceptions;

namespace TheXDS.MCART.Windows.Tests.Component;

internal class ApplicationInfoBaseTests
{
    private class TestInfo : ApplicationInfoBase<TamperException>
    {
        public TestInfo(Assembly assembly, Icon? icon) : base(assembly, icon)
        {
        }

        public TestInfo(Assembly assembly, bool inferIcon) : base(assembly, inferIcon)
        {
        }

        public TestInfo(TamperException application, Icon? icon) : base(application, icon)
        {
        }

        public TestInfo(TamperException application, bool inferIcon) : base(application, inferIcon)
        {
        }
    }

    [Test]
    public void Class_implements_IExposeExtendedGuiInfo()
    {
        var asm = typeof(TheXDS.MCART.Helpers.Common).Assembly;
        Icon? icon = Icon.ExtractAssociatedIcon(@"C:\Windows\System32\cmd.exe");
        var test = new TestInfo(asm, icon);
        Assert.That(test, Is.AssignableTo<IExposeExtendedGuiInfo<Icon?>>());
        using (Assert.EnterMultipleScope())
        {
            Assert.That(test.Name, Is.Not.Empty);
            Assert.That(test.Description, Is.Not.Empty);
            Assert.That(test.Icon, Is.SameAs(icon));
            Assert.That(test.Authors, Is.Not.Empty);
            Assert.That(test.Copyright, Is.Not.Empty);
            Assert.That(test.License, Is.Not.Null);
            Assert.That(test.Version, Is.Not.Null);
            Assert.That(test.HasLicense, Is.True);
            Assert.That(test.ClsCompliant, Is.InstanceOf<bool>());
            Assert.That(test.Assembly, Is.SameAs(asm));
            Assert.That(test.InformationalVersion, Is.Not.Empty);
            Assert.That(test.Beta, Is.InstanceOf<bool>());
            Assert.That(test.Unmanaged, Is.InstanceOf<bool>());
            Assert.That(test.ThirdPartyLicenses?.ToArray(), Is.InstanceOf<TheXDS.MCART.Resources.License[]>());
            Assert.That(test.Has3rdPartyLicense, Is.InstanceOf<bool>());
        }
    }

    [Test]
    public void Ctor_with_app_initializes_props()
    {
        var app = new TamperException();
        Icon? icon = Icon.ExtractAssociatedIcon(@"C:\Windows\System32\cmd.exe");
        var test = new TestInfo(app, icon);
        Assert.That(test, Is.AssignableTo<IExposeExtendedGuiInfo<Icon?>>());
        using (Assert.EnterMultipleScope())
        {
            Assert.That(test.Assembly, Is.EqualTo(typeof(TamperException).Assembly));
            Assert.That(test.Icon, Is.SameAs(icon));
        }
    }

    [Test]
    public void Ctor_can_infer_icon()
    {
        var asm = typeof(TheXDS.MCART.Helpers.Common).Assembly;
        var test = new TestInfo(asm, inferIcon: true);
        Assert.That(test.Icon, Is.Not.Null);
    }

    [Test]
    public void Ctor_skips_infer_icon()
    {
        var asm = typeof(TheXDS.MCART.Helpers.Common).Assembly;
        var test = new TestInfo(asm, inferIcon: false);
        Assert.That(test.Icon, Is.Null);
    }

    [Test]
    public void Ctor_with_app_skips_infer_icon()
    {
        var app = new TamperException();
        var test = new TestInfo(app, inferIcon: false);
        Assert.That(test.Icon, Is.Null);
    }

    [Test]
    public void Ctor_with_app_can_infer_icon()
    {
        var app = new TamperException();
        var test = new TestInfo(app, inferIcon: true);
        Assert.That(test.Icon, Is.Not.Null);
    }
}
