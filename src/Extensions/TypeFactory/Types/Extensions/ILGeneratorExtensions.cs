﻿/*
ILGeneratorExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using static System.Reflection.BindingFlags;
using static System.Reflection.Emit.OpCodes;
using static System.Reflection.MethodAttributes;
using static TheXDS.MCART.Types.Extensions.EnumExtensions;
using static TheXDS.MCART.Types.Extensions.StringExtensions;

namespace TheXDS.MCART.Types.Extensions
{
    internal static class Helpers
    {
        internal static string UndName(string name)
        {
            if (name.IsEmpty()) throw new ArgumentNullException(name);
            return name.Length > 1
                ? $"_{name.Substring(0, 1).ToLower()}{name.Substring(1)}"
                : $"_{name.ToLower()}";
        }
        internal static string NoIfaceName(string name)
        {
            if (name.IsEmpty()) throw new ArgumentNullException(name);
            return name[0] != 'I' ? $"{name}Implementation" : name.Substring(1);
        }
    }
    public static class ILGeneratorExtensions
    {
        /// <summary>
        ///     Inserta una constante en la secuencia del lenguaje intermedio 
        ///     (MSIL) de Microsoft®.
        /// </summary>
        /// <param name="ilGen">
        ///     Secuencia de instrucciones en la cual insertar la carga de la
        ///     constante.
        /// </param>
        /// <param name="value">
        ///     Valor constante a cargar.
        /// </param>
        public static void LoadConstant(this ILGenerator ilGen, object value)
        {
            switch (value)
            {
                case Enum _:
                    LoadConstant(ilGen, ((Enum)value).ToUnderlyingType());
                    break;
                case byte _:
                case sbyte _:
                case char _:
                    ilGen.Emit(Ldc_I4_S, unchecked((int)value));
                    break;
                case bool b:
                    ilGen.Emit(b ? Ldc_I4_1 : Ldc_I4_0);
                    break;
                case short _:
                case ushort _:
                case int _:
                case uint _:
                    ilGen.Emit(Ldc_I4, unchecked((int)value));
                    break;
                case long _:
                case ulong _:
                    ilGen.Emit(Ldc_I8, unchecked((long)value));
                    break;
                case float _:
                    ilGen.Emit(Ldc_R4, (float)value);
                    break;
                case double _:
                    ilGen.Emit(Ldc_R8, (double)value);
                    break;
                case decimal _:
                    foreach (var j in decimal.GetBits((decimal)value))                    
                        ilGen.Emit(Ldc_I4, j);                    
                    ilGen.Emit(Newobj, typeof(decimal).GetConstructor(new Type[]
                    {
                        typeof(int),
                        typeof(int),
                        typeof(int),
                        typeof(bool),
                        typeof(byte)
                    }));
                    break;
                case string s:
                    ilGen.Emit(Ldstr, (string)value);
                    break;
                case Type _:
                    ilGen.Emit(Ldtoken, (Type)value);
                    ilGen.Emit(Call, typeof(Type).GetMethod("GetTypeFromHandle"));
                    break;
                case null:
                    ilGen.Emit(Ldnull);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
    public static class FieldBuilderExtensions
    {
        public static void InitField(this FieldBuilder field, ILGenerator ilGen, object value)
        {
            ilGen.Emit(Ldarg_0);
            ILGeneratorExtensions.LoadConstant(ilGen, value);
            ilGen.Emit(Stfld, field);
        }

        public static void InitField(this FieldBuilder field, ILGenerator ilGen, Type instanceType, params object[] args)
        {
            if (instanceType.IsAbstract) throw new InvalidTypeException(instanceType);
            var c = instanceType.GetConstructor(args.ToTypes().ToArray()) ?? throw new TypeLoadException();
            ilGen.Emit(Ldarg_0);
            foreach (var j in args) ILGeneratorExtensions.LoadConstant(ilGen, j);
            ilGen.Emit(Newobj, c);
            ilGen.Emit(Stfld, field);
        }
    }
    public static class TypeBuilderExtensions
    {
        public static bool Overridable(this TypeBuilder tb, string method, params Type[] args)
        {
            var bm = tb.BaseType?.GetMethod(method, args);
            return !(bm is null) && (bm.IsVirtual || bm.IsAbstract);
        }

        private static void AddAutoProp(this TypeBuilder tb, string name,Type type, MemberAccess access, bool writtable, out PropertyBuilder prop, out FieldBuilder field)
        {
            var flags = Access(access) | SpecialName | HideBySig;
            if (tb.Overridable($"get_{name}")) flags |= Virtual;

            field = tb.DefineField(Helpers.UndName(name), type, FieldAttributes.Private | (writtable ? 0 : FieldAttributes.InitOnly));
            prop = tb.DefineProperty(name, PropertyAttributes.HasDefault, type, null);
            var getM = tb.DefineMethod($"get_{name}", flags , type, null);
            var getIl = getM.GetILGenerator();
            getIl.Emit(Ldarg_0);
            getIl.Emit(Ldfld, field);
            getIl.Emit(Ret);
            prop.SetGetMethod(getM);

            if (writtable)
            {
                var setM = tb.DefineMethod($"set_{name}",flags, null, new[] { type });
                var setIl = setM.GetILGenerator();
                setIl.Emit(Ldarg_0);
                setIl.Emit(Ldarg_1);
                setIl.Emit(Stfld, field);
                setIl.Emit(Ret);
                prop.SetSetMethod(setM);
            }
        }

        public static PropertyBuilder AddAutoProp(this TypeBuilder tb, string name, Type type, MemberAccess access, bool writtable)
        {
            AddAutoProp(tb, name, type, access, writtable, out var prop, out _);
            return prop;
        }

        private static MethodAttributes Access(MemberAccess access)
        {
            switch (access)
            {
                case MemberAccess.Private:
                    return MethodAttributes.Private;
                case MemberAccess.Protected:
                    return MethodAttributes.Family;
                case MemberAccess.Internal:
                    return MethodAttributes.Assembly;
                case MemberAccess.Public:
                    return MethodAttributes.Public;
            }
            throw new NotImplementedException();
        }
    }
    public enum MemberAccess
    {
        Private,
        Protected,
        Internal,
        Public
    }
}