/*
TypeFactoryErrors.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Reflection.Emit;
using TheXDS.MCART.Exceptions;
using St = TheXDS.MCART.Resources.TypeFactoryStrings;

namespace TheXDS.MCART.Resources
{
    internal static class TypeFactoryErrors
    {
        internal static Exception IfaceNotImpl<T>() => IfaceNotImpl(typeof(T));

        internal static Exception IfaceNotImpl(Type t)
        {
            return new InvalidOperationException(string.Format(St.ErrIfaceNotImpl, t.Name));
        }
        internal static Exception PropGetterAlreadyDefined()
        {
            return new InvalidOperationException(St.ErrPropGetterAlreadyDefined);
        }
        internal static Exception PropFieldAlreadyDefined()
        {
            return new InvalidOperationException(St.ErrPropFieldAlreadyDefined);
        }
        internal static Exception PropCannotBeRead()
        {
            return new InvalidOperationException(St.ErrPropCannotBeRead);
        }

        internal static Exception IFaceMethodExpected()
        {
            return new InvalidOperationException(St.ErrIFaceMethodExpected);
        }

        internal static Exception TypeBuilderTypeMismatch<T>(TypeBuilder typebuilder)
        {
            return new ArgumentException(
                string.Format(St.TypeBuilderTypeMismatch,
                typebuilder.BaseType,
                typeof(T)),
                new InvalidTypeException(typebuilder));
        }
    }
}
