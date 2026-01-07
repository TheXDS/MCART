/*
ErrorManagerTests.cs

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

using Moq;
using System.ComponentModel;
using System.Globalization;
using TheXDS.MCART.ValueConverters;

namespace TheXDS.MCART.Wpf.Common.Tests.ValueConverters;

public class ErrorManagerTests
{
    [Test]
    public void ErrorManager_lists_errors()
    {
        string[] testMessages = ["Error 1", "Error 2", "Error 3"];
        var vm = new Mock<INotifyDataErrorInfo>();
        vm.Setup(p => p.GetErrors("Test")).Returns(testMessages);
        var err = new ErrorManager();
        object? result = err.Convert(vm.Object, typeof(string), "Test", CultureInfo.InvariantCulture);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<string>());
        Assert.That((string)result!, Is.EqualTo(string.Join(Environment.NewLine, testMessages)));
    }

    [Test]
    public void ErrorManager_returns_null_on_no_errors()
    {
        var vm = new Mock<INotifyDataErrorInfo>();
        vm.Setup(p => p.GetErrors("Test")).Returns(Array.Empty<string>());
        var err = new ErrorManager();
        object? result = err.Convert(vm.Object, typeof(string), "Test", CultureInfo.InvariantCulture);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ConvertBack_throws_InvalidOperationException()
    {
        var err = new ErrorManager();
        Assert.That(()=> _ = err.ConvertBack(null, typeof(string), null, CultureInfo.InvariantCulture), Throws.InvalidOperationException);
    }

    [Test]
    public void Convert_returns_null_on_no_value()
    {
        var err = new ErrorManager();
        object? result = err.Convert(null, typeof(string), "Test", CultureInfo.InvariantCulture);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Convert_throws_ArgumentException_on_invalid_INotifyDataErrorInfo_value()
    {
        var err = new ErrorManager();
        Assert.That(() => _ = err.Convert(new Random(), typeof(string), null, CultureInfo.InvariantCulture), Throws.ArgumentException);
    }

    [Test]
    public void Convert_throws_ArgumentException_on_invalid_property_name()
    {
        var vm = new Mock<INotifyDataErrorInfo>();

        var err = new ErrorManager();
        Assert.That(() => _ = err.Convert(vm.Object, typeof(string), null, CultureInfo.InvariantCulture), Throws.ArgumentException);
        Assert.That(() => _ = err.Convert(vm.Object, typeof(string), new Random(), CultureInfo.InvariantCulture), Throws.ArgumentException);
    }
}
