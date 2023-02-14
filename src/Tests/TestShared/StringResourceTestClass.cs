// StringResourceTestClass.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      C�sar Andr�s Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright � 2011 - 2023 C�sar Andr�s Morgan
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the �Software�), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED �AS IS�, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using NUnit.Framework;
using System;
using System.Globalization;
using System.Reflection;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;

namespace TheXDS.MCART.Tests;

public abstract class StringResourceTestClass
{
    private readonly Type resourceClass;

    protected StringResourceTestClass(Type resourceClass)
    {
        this.resourceClass = resourceClass;
    }

    private void SetCulture(CultureInfo culture)
    {
        (resourceClass.GetProperty("Culture", BindingFlags.NonPublic | BindingFlags.Static) ?? throw new TamperException()).SetValue(null, culture);
    }

    [TestCase("es-MX")]
    [TestCase("en-US")]
    public void Translations_Test(string culture)
    {
        SetCulture(CultureInfo.CreateSpecificCulture(culture));
        foreach (var property in resourceClass.GetPropertiesOf<string>(BindingFlags.Public | BindingFlags.Static))
        {
            Assert.That(property.GetValue(null) as string, Is.Not.Null.And.Not.Empty);
        }
    }
}