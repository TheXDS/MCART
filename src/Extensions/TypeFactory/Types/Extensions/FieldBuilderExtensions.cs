/*
FieldBuilderExtensions.cs

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
using System.Linq;
using System.Reflection.Emit;
using TheXDS.MCART.Exceptions;
using static System.Reflection.Emit.OpCodes;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Contiene extensiones útiles para la manipulación de constructores de
    /// campos por medio de la clase <see cref="FieldBuilder"/>.
    /// </summary>
    public static class FieldBuilderExtensions
    {
        /// <summary>
        /// Inicializa un campo dentro del generador de código especificado.
        /// </summary>
        /// <param name="field">Campo a inicializar.</param>
        /// <param name="ilGen">
        /// Generador de código a utilizar para inicializar el campo.
        /// Generalmente, debe tratarse de un constructor de clase.
        /// </param>
        /// <param name="value">
        /// Valor constante con el cual debe inicializarse el campo.
        /// </param>
        public static void InitField(this FieldBuilder field, ILGenerator ilGen, object value)
        {
            ilGen.Emit(Ldarg_0);
            ilGen.LoadConstant(value);
            ilGen.Emit(Stfld, field);
        }

        /// <summary>
        /// Inicializa un campo dentro del generador de código especificado.
        /// </summary>
        /// <param name="field">Campo a inicializar.</param>
        /// <param name="ilGen">
        /// Generador de código a utilizar para inicializar el campo.
        /// Generalmente, debe tratarse de un constructor de clase.
        /// </param>
        /// <param name="instanceType">Tipo de objeto a instanciar.</param>
        /// <param name="args">
        /// Argumentos a pasar al constructor del tipo especificado.
        /// </param>
        /// <exception cref="InvalidTypeException">
        /// Se produce si el tipo no es instanciable.
        /// </exception>
        public static void InitField(this FieldBuilder field, ILGenerator ilGen, Type instanceType, params object[] args)
        {
            if (instanceType.IsAbstract) throw new InvalidTypeException(instanceType);
            ilGen.Emit(Ldarg_0);
            ilGen.NewObject(instanceType, args);
            ilGen.Emit(Stfld, field);
        }
    }
}