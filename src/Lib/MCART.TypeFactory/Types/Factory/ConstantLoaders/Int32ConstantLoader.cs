/*
Int32ConstantLoader.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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
using static System.Reflection.Emit.OpCodes;

namespace TheXDS.MCART.Types.Extensions.ConstantLoaders;

/// <summary>
/// Loads a constant value of type <see cref="int"/> into the MSIL instruction sequence.
/// </summary>
public class Int32ConstantLoader : ConstantLoader<int>
{
    /// <inheritdoc/>
    public override void Emit(ILGenerator il, int value)
    {
        switch (value)
        {
            case -1:
                il.Emit(Ldc_I4_M1);
                break;
            case 0:
                il.Emit(Ldc_I4_0);
                break;
            case 1:
                il.Emit(Ldc_I4_1);
                break;
            case 2:
                il.Emit(Ldc_I4_2);
                break;
            case 3:
                il.Emit(Ldc_I4_3);
                break;
            case 4:
                il.Emit(Ldc_I4_4);
                break;
            case 5:
                il.Emit(Ldc_I4_5);
                break;
            case 6:
                il.Emit(Ldc_I4_6);
                break;
            case 7:
                il.Emit(Ldc_I4_7);
                break;
            case 8:
                il.Emit(Ldc_I4_8);
                break;
            default:
                il.Emit(Ldc_I4, value);
                break;
        }
    }
}
