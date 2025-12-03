/*
DecimalConstantLoader.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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

using System.Reflection;
using System.Reflection.Emit;
using static System.Reflection.Emit.OpCodes;

namespace TheXDS.MCART.Types.Extensions.ConstantLoaders;

/// <summary>
/// <see cref="ConstantLoader{T}"/> that allows loading a <see cref="decimal"/>
/// constant value.
/// </summary>
public class DecimalConstantLoader : ConstantLoader<decimal>
{
    /// <inheritdoc/>
    public override void Emit(ILGenerator il, decimal value)
    {
        il.LoadConstant(typeof(decimal).GetProperty("Low", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(value));
        il.LoadConstant(typeof(decimal).GetProperty("Mid", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(value));
        il.LoadConstant(typeof(decimal).GetProperty("High", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(value));
        il.LoadConstant(value < 0);
        il.LoadConstant(value.Scale);
        il.Emit(Newobj, typeof(decimal).GetConstructor(
        [
            typeof(int),
            typeof(int),
            typeof(int),
            typeof(bool),
            typeof(byte)
        ])!);
    }
}
