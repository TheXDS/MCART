/*
TypeExtensions_Contracts.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene numerosas extensiones para el tipo System.Type del CLR,
supliéndolo de nueva funcionalidad previamente no existente, o de invocación
compleja.

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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Resources;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Extensiones para todos los elementos de tipo <see cref="Type"/>.
    /// </summary>
    public static partial class TypeExtensions
    {
        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void ToNamedEnum_Contract(Type type)
        {
            NullCheck(type, nameof(type));
            if (!type.IsEnum) throw Errors.EnumExpected(nameof(type), type);
        }

        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void TryInstance_Contract(Type t, object[]? args)
        {
            NullCheck(t, nameof(t));
            if (args?.IsAnyNull() ?? false) throw new NullItemException(args);
        }

        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void NotNullable_Contract(Type t)
        {
            NullCheck(t, nameof(t));
        }

        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void GetCollectionType_Contract(Type collectionType)
        {
            NullCheck(collectionType, nameof(collectionType));
            if (!collectionType.IsCollectionType()) throw Errors.EnumerableTypeExpected(collectionType);
        }

        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void Derivates_Contract(IEnumerable<Assembly> assemblies)
        {
            NullCheck(assemblies, nameof(assemblies));
        }

        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void Derivates_Contract(Type type, IEnumerable<Type> types)
        {
            NullCheck(type, nameof(type));
            NullCheck(types, nameof(types));
        }

        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void Derivates_Contract(AppDomain domain)
        {
            NullCheck(domain, nameof(domain));
        }

        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void New_Contract(Type type, IEnumerable<object?> parameters)
        {
            NullCheck(type, nameof(type));
            if (!type.IsInstantiable(parameters.ToTypes()))
            {
                throw Errors.ClassNotInstantiable(type);
            }
        }

        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void FieldsOf_Contract(Type type)
        {
            NullCheck(type, nameof(type));
        }
    }
}