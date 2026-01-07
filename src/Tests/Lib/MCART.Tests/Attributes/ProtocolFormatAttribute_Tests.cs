// ProtocolFormatAttribute_Tests.cs
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

using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Tests.Attributes;

public class ProtocolFormatAttribute_Tests
{
    [Test]
    public void Instancing_test()
    {
        ProtocolFormatAttribute attr = new("test://{0}");
        Assert.That(attr, Is.InstanceOf<ProtocolFormatAttribute>());
        Assert.That(attr, Is.AssignableTo<IValueAttribute<string>>());
        Assert.That(attr, Is.AssignableTo<Attribute>());
    }

    [TestCase("https://{0}")]
    [TestCase("ftp://{0}")]
    [TestCase("file://{0}")]
    public void Format_test(string format)
    {
        ProtocolFormatAttribute attr = new(format);
        IValueAttribute<string> iattr = attr;
        Assert.That(attr.Format, Is.EqualTo(format));
        Assert.That(iattr.Value, Is.EqualTo(format));
    }

    [Test]
    public void Open_test()
    {
        var tmpFile = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.txt");
        File.CreateText(tmpFile).Dispose();
        ProtocolFormatAttribute attr = new("{0}");
        var proc = attr.Open(tmpFile);
        Assert.That(proc, Is.Not.Null);
        proc!.Kill();
        File.Delete(tmpFile);
    }

    [Test]
    public void Open_returns_null_on_empty_url()
    {
        ProtocolFormatAttribute attr = new("{0}");
        var proc = attr.Open("");
        Assert.That(proc, Is.Null);
    }
}
