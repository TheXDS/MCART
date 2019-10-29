/*
ILGeneratorExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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
using System.Linq;
using System.Reflection;
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
        private static readonly HashSet<IConstantLoader> _constantLoaders;

        /// <summary>
        ///     Inicializa la clase <see cref="ILGeneratorExtensions"/>.
        /// </summary>
        static ILGeneratorExtensions()
        {
            _constantLoaders = new HashSet<IConstantLoader>(new ConstantLoaderComparer());
            foreach (var j in Objects.FindAllObjects<IConstantLoader>())
            {
                RegisterConstantLoader(j);
            }
        }

        /// <summary>
        ///     Registra un <see cref="IConstantLoader"/> para el método
        ///     <see cref="LoadConstant(ILGenerator, object)"/>.
        /// </summary>
        /// <param name="loader">
        ///     <see cref="IConstantLoader"/> a registrar.
        /// </param>
        public static void RegisterConstantLoader(IConstantLoader loader)
        {
            _constantLoaders.Add(loader);
        }

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
            var t = value?.GetType() ?? typeof(object);
            if (_constantLoaders.FirstOrDefault(p=>p.ConstantType == t) is IConstantLoader cl)
            {
                cl.Emit(ilGen, value);
            }
            else if (!t.IsStruct())
            {
                ilGen.Emit(Newobj, t.GetConstructor(Array.Empty<Type>()));
            }
            else
            {
                ilGen.Emit(Ldnull);
            }
        }

        /// <summary>
        ///     Inserta la instanciación de un objeto en la secuencia del
        ///     lenguaje intermedio (MSIL) de Microsoft®.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de objeto a instanciar.
        /// </typeparam>
        /// <param name="ilGen">
        ///     Secuencia de instrucciones en la cual insertar la instanciación
        ///     del objeto.
        /// </param>
        /// <exception cref="ClassNotInstantiableException">
        ///     Se produce si la clase no es instanciable, o si no existe un 
        ///     constructor que acepte los argumentos especificados.
        ///     También puede producirse si uno de los parámetros es un objeto,
        ///     y no contiene un constructor predeterminado sin argumentos, en
        ///     cuyo caso, la excepción indicará el tipo que no puede
        ///     instanciarse.
        /// </exception>
        public static void NewObject<T>(this ILGenerator ilGen) => NewObject(ilGen, typeof(T));

        /// <summary>
        ///     Inserta la instanciación de un objeto en la secuencia del
        ///     lenguaje intermedio (MSIL) de Microsoft®.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de objeto a instanciar.
        /// </typeparam>
        /// <param name="ilGen">
        ///     Secuencia de instrucciones en la cual insertar la instanciación
        ///     del objeto.
        /// </param>
        /// <param name="args">
        ///     Argumentos a pasar al constructor del objeto.
        /// </param>
        /// <exception cref="ClassNotInstantiableException">
        ///     Se produce si la clase no es instanciable, o si no existe un 
        ///     constructor que acepte los argumentos especificados.
        ///     También puede producirse si uno de los parámetros es un objeto,
        ///     y no contiene un constructor predeterminado sin argumentos, en
        ///     cuyo caso, la excepción indicará el tipo que no puede
        ///     instanciarse.
        /// </exception>
        public static void NewObject<T>(this ILGenerator ilGen, IEnumerable args) => NewObject(ilGen, typeof(T), args);

        /// <summary>
        ///     Inserta la instanciación de un objeto en la secuencia del
        ///     lenguaje intermedio (MSIL) de Microsoft®.
        /// </summary>
        /// <param name="type">
        ///     Tipo de objeto a instanciar.
        /// </param>
        /// <param name="ilGen">
        ///     Secuencia de instrucciones en la cual insertar la instanciación
        ///     del objeto.
        /// </param>
        /// <exception cref="ClassNotInstantiableException">
        ///     Se produce si la clase no es instanciable, o si no existe un 
        ///     constructor que acepte los argumentos especificados.
        ///     También puede producirse si uno de los parámetros es un objeto,
        ///     y no contiene un constructor predeterminado sin argumentos, en
        ///     cuyo caso, la excepción indicará el tipo que no puede
        ///     instanciarse.
        /// </exception>
        public static void NewObject(this ILGenerator ilGen, Type type)
        {
            NewObject(ilGen, type, Array.Empty<object>());
        }

        /// <summary>
        ///     Inserta la instanciación de un objeto en la secuencia del
        ///     lenguaje intermedio (MSIL) de Microsoft®.
        /// </summary>
        /// <param name="type">
        ///     Tipo de objeto a instanciar.
        /// </param>
        /// <param name="ilGen">
        ///     Secuencia de instrucciones en la cual insertar la instanciación
        ///     del objeto.
        /// </param>
        /// <param name="args">
        ///     Argumentos a pasar al constructor del objeto.
        /// </param>
        /// <exception cref="ClassNotInstantiableException">
        ///     Se produce si la clase no es instanciable, o si no existe un 
        ///     constructor que acepte los argumentos especificados.
        ///     También puede producirse si uno de los parámetros es un objeto,
        ///     y no contiene un constructor predeterminado sin argumentos, en
        ///     cuyo caso, la excepción indicará el tipo que no puede
        ///     instanciarse.
        /// </exception>
        public static void NewObject(this ILGenerator ilGen, Type type, IEnumerable args)
        {
            if (!(type.GetConstructor(args.ToTypes().ToArray()) is ConstructorInfo c))
                throw new ClassNotInstantiableException(type);
            foreach (var j in args)
            {
                if (j.GetType().IsClass)
                    NewObject(ilGen, j.GetType());
                else
                    LoadConstant(ilGen, j);
            }
            ilGen.Emit(Newobj, c);
        }
    }
}