/*
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
using System.Linq;
using System.Reflection.Emit;
using TheXDS.MCART.Exceptions;
using static System.Reflection.Emit.OpCodes;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Contiene extensiones útiles para la generación de código por medio
    ///     de la clase <see cref="ILGenerator"/>.
    /// </summary>
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
        /// <exception cref="T:System.NotImplementedException">
        ///     Se produce al intentar cargar un valor constante desconocido.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     Se produce al intentar cargar un valor que no es constante, como
        ///     una instancia de objeto.
        /// </exception>
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
                case Delegate _:
#pragma warning disable RECS0083 // Intencionalmente, producir una NotImplementedException.
                    throw new NotImplementedException();
#pragma warning restore RECS0083
                default:
                    throw new InvalidOperationException();
            }
        }
        public static void NewObject<T>(this ILGenerator ilGen) => NewObject(ilGen, typeof(T));
        public static void NewObject<T>(this ILGenerator ilGen, IEnumerable args) => NewObject(ilGen, typeof(T), args);
        public static void NewObject(this ILGenerator ilGen, Type type)
        {
            NewObject(ilGen, type, new object[0]);
        }
        /// <summary>
        ///     Crea un nuevo objeto del tipo especificado.
        /// </summary>
        /// <param name="ilGen">
        ///     Generador de código en el cual se instanciará el nuevo objeto.
        /// </param>
        /// <param name="type">Tipo a instanciar.</param>
        /// <param name="args">Argumentos del constructor a utilizar.</param>
        /// <exception cref="T:TheXDS.MCART.Exceptions.ClassNotInstantiableException">
        ///     Se produce si la clase no es instanciable, o si no existe un 
        ///     constructor que acepte los argumentos especificados.
        ///     También puede producirse si uno de los parámetros es un objeto,
        ///     y no contiene un constructor predeterminado sin argumentos, en
        ///     cuyo caso, la excepción indicará el tipo que no puede
        ///     instanciarse.
        /// </exception>
        public static void NewObject(this ILGenerator ilGen, Type type, IEnumerable args)
        {
            if (type.GetConstructor(args.ToTypes().ToArray()) is null)
                throw new ClassNotInstantiableException(type);
            foreach (var j in args)
            {
                if (j.GetType().IsClass)
                    NewObject(ilGen, j.GetType());
                else
                    LoadConstant(ilGen, j);
            }
        }
    }
}