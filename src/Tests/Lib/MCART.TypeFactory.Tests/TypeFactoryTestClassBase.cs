// TypeFactoryTestClassBase.cs
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

using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Types.Extensions;
using Tf = TheXDS.MCART.Types.TypeFactory;

namespace TheXDS.MCART.TypeFactory.Tests;

public abstract class TypeFactoryTestClassBase
{
    protected static readonly Tf Factory = new("TheXDS.MCART.TypeFactory.Tests._Generated", true);

    protected static TypeBuilder NewClass([CallerMemberName]string name = null!)
    {
        return Factory.NewClass(name);
    }

    protected static (TypeBuilder builder, ILGenerator il) NewTestMethod([CallerMemberName] string name = null!)
    {
        var tb = NewClass($"{name}_Class");
        tb.AddPublicConstructor().CallBaseCtor<object>().Return();
        return (tb, tb.DefineMethod(name, MethodAttributes.Public, CallingConventions.HasThis).GetILGenerator());
    }

    protected static object InvokeTestMethod(TypeBuilder builder, [CallerMemberName] string name = null!)
    {
        var obj = builder.New();
        obj.GetType().GetMethod(name)!.Invoke(obj, []);
        return obj;
    }

    protected static object? GetField(object obj, string fieldName)
    {
        return obj.GetType().GetField(fieldName)!.GetValue(obj);
    }
}
