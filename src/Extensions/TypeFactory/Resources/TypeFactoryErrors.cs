/*
TypeFactoryErrors.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

using System;
using System.Reflection.Emit;
using TheXDS.MCART.Exceptions;
using St = TheXDS.MCART.Resources.TypeFactoryStrings;

namespace TheXDS.MCART.Resources;

internal static class TypeFactoryErrors
{
    internal static InvalidOperationException IfaceNotImpl<T>() => IfaceNotImpl(typeof(T));

    internal static InvalidOperationException IfaceNotImpl(Type t)
    {
        return new InvalidOperationException(string.Format(St.ErrIfaceNotImpl, t.Name));
    }
    internal static InvalidOperationException PropGetterAlreadyDefined()
    {
        return new InvalidOperationException(St.ErrPropGetterAlreadyDefined);
    }
    internal static InvalidOperationException PropFieldAlreadyDefined()
    {
        return new InvalidOperationException(St.ErrPropFieldAlreadyDefined);
    }
    internal static InvalidOperationException PropCannotBeRead()
    {
        return new InvalidOperationException(St.ErrPropCannotBeRead);
    }

    internal static InvalidOperationException IFaceMethodExpected()
    {
        return new InvalidOperationException(St.ErrIFaceMethodExpected);
    }

    internal static ArgumentException TypeBuilderTypeMismatch<T>(TypeBuilder typebuilder)
    {
        return new ArgumentException(
            string.Format(St.TypeBuilderTypeMismatch,
            typebuilder.BaseType,
            typeof(T)),
            new InvalidTypeException(typebuilder));
    }

    internal static InvalidOperationException CannotJoinObjects()
    {
        return new InvalidOperationException(St.ErrCannotJoinTypes);
    }
}
