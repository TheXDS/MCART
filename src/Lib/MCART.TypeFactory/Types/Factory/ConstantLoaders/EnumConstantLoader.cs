/*
Int32ConstantLoader.cs

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

using System.Reflection.Emit;

namespace TheXDS.MCART.Types.Extensions.ConstantLoaders;

/// <summary>
/// Loads a constant value of type <see cref="Enum"/> into the MSIL instruction sequence.
/// </summary>
public class EnumConstantLoader : ConstantLoader<Enum>
{
    /// <inheritdoc/>
    public override void Emit(ILGenerator il, Enum value)
    {
        switch (value.ToUnderlyingType())
        {
            case byte b: il.LoadConstant(b); break;
            case sbyte sb: il.LoadConstant(sb); break;
            case short s: il.LoadConstant(s); break;
            case ushort us: il.LoadConstant(us); break;
            case int i: il.LoadConstant(i); break;
            case uint ui: il.LoadConstant(ui); break;
            case long l: il.LoadConstant(l); break;
            case ulong ul: il.LoadConstant(ul); break;
            default: throw new NotSupportedException();            
        }
    }
}