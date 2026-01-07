// WindowViewModel_Tests.cs
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

using TheXDS.MCART.Tests;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Mvvm.Tests.Types;

public class WindowViewModel_Tests
{
    [Test]
    public void Title_test()
    {
        WindowViewModel vm = new();
        Assert.That(EventTestHelpers.TestNpcProperty(vm, p => p.Title = "Test")!.PropertyName, Is.EqualTo(nameof(WindowViewModel.Title)));
        Assert.That(vm.Title, Is.EqualTo("Test"));
    }

    [Test]
    public void WindowHeight_test()
    {
        EventTestHelpers.TestNpcProperty<WindowViewModel, double>(p => p.WindowHeight, 2.0, p => p.WindowSize);
    }

    [Test]
    public void WindowWidth_test()
    {
        EventTestHelpers.TestNpcProperty<WindowViewModel, double>(p => p.WindowWidth, 2.0, p => p.WindowSize);
    }

    [Test]
    public void WindowSize_test()
    {
        WindowViewModel vm = new();
        EventTestHelpers.TestNpcProperty(vm, p => p.WindowSize, new Size(2, 3), p => p.WindowHeight, p => p.WindowWidth);
    }
}
