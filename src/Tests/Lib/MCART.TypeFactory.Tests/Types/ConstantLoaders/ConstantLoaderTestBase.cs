// ConstantLoaderTestBase.cs
// 
// This file is part of Morgan's CLR Advanced Runtime (MCART)
// 
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
// 
// Released under the MIT License (MIT)
// Copyright © 2011 - 2023 César Andrés Morgan
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

using System.Reflection.Emit;
using NUnit.Framework;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.TypeFactory.Tests.Types.ConstantLoaders;

public abstract class ConstantLoaderTestBase<T> : TypeFactoryTestClassBase
{
    private readonly Func<IEnumerable<T>> _testValueSource;

    public ConstantLoaderTestBase(Func<IEnumerable<T>> testValueSource)
    {
        _testValueSource = testValueSource;
    }
    
    [Test]
    public void Loader_Emits_value_test()
    {
        int c = 0;
        foreach (var value in _testValueSource())
        {
            TypeBuilder t = Factory.NewClass($"{typeof(T)}ConstantLoaderTest{++c}");
            t.AddConstantProperty("Value", value);
            Assert.That(((dynamic)t.New()).Value, Is.EqualTo(value));
        }
    }
}