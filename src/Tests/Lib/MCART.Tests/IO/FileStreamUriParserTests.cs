/*
FileStreamUriParserTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene todas las pruebas pertenecientes a la clase estática
TheXDS.MCART.Common.

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

#pragma warning disable CS0618

namespace TheXDS.MCART.Tests.IO;
using NUnit.Framework;
using System;
using System.IO;
using TheXDS.MCART.IO;

public class FileStreamUriParserTests
{
    [Test]
    public void Open_WithFullUri_Test()
    {
        string? f = Path.GetTempFileName();
        string? furi = $"file://{f.Replace('\\', '/')}";
        File.WriteAllText(f, "test");

        FileStreamUriParser? fp = new();
        Stream? fu = fp.Open(new Uri(furi))!;

        Assert.That(fu, Is.Not.Null);
        Assert.That(fu, Is.InstanceOf<Stream>());
        using StreamReader? r = new(fu);
        Assert.That("test", Is.EqualTo(r.ReadToEnd()));
        fu.Dispose();
        r.Dispose();
        File.Delete(f);
    }

    [Test]
    public void Open_WithFilePath_Test()
    {
        string? f = Path.GetTempFileName();
        File.WriteAllText(f, "test");

        FileStreamUriParser? fp = new();
        Stream? fu = fp.Open(new Uri(f))!;

        Assert.That(fu, Is.Not.Null);
        Assert.That(fu, Is.InstanceOf<FileStream>());
        using StreamReader? r = new(fu);
        Assert.That("test", Is.EqualTo(r.ReadToEnd()));
        fu.Dispose();
        r.Dispose();
        File.Delete(f);
    }
}
