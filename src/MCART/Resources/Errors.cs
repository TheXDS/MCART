/*
Errors.cs

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
using TheXDS.MCART.Exceptions;
using Str = TheXDS.MCART.Resources.Strings.Common;
using Ers = TheXDS.MCART.Resources.Strings.Errors;
using TheXDS.MCART.Types.Extensions;
using System.Reflection;

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Contiene recursos que generan nuevas instancias de excepción a ser lanzadas.
    /// </summary>
    public static class Errors
    {
        /// <summary>
        /// Crea una nueva instancia de un <see cref="FormatException"/> con un
        /// mensaje formateado predefinido.
        /// </summary>
        /// <param name="offendingFormat">
        /// Cadena de formato que ha causado la excepción.
        /// </param>
        /// <returns>
        /// Una nueva instancia de la clase <see cref="FormatException"/>.
        /// </returns>
        public static FormatException FormatNotSupported(string offendingFormat)
        {
            return new(string.Format(Ers.FormatNotSupported, offendingFormat));
        }

        /// <summary>
        /// Crea una nueva instancia de un <see cref="ArgumentException"/> que
        /// indica que el valor proporcionado a un argumento no es válido.
        /// </summary>
        /// <param name="argName">
        /// Nombre del argumento para el cual se generará la excepción.
        /// </param>
        /// <param name="value">
        /// Valor del argumento que ha generado la excepción.
        /// </param>
        /// <returns>
        /// Una nueva instancia de la clase <see cref="ArgumentException"/>.
        /// </returns>
        public static ArgumentException InvalidValue(string? argName, object? value)
        {
            var msg = string.Format(Ers.InvalidXValue, value?.ToString() ?? Str.Null);
            return argName is { } s
                ? new(msg, s)
                : new(msg);
        }

        /// <summary>
        /// Crea una nueva instancia de un <see cref="ArgumentException"/> que
        /// indica que el valor proporcionado a un argumento no es válido.
        /// </summary>
        /// <param name="argName">
        /// Nombre del argumento para el cual se generará la excepción.
        /// </param>
        /// <returns>
        /// Una nueva instancia de la clase <see cref="ArgumentException"/>.
        /// </returns>
        public static ArgumentException InvalidValue(string argName)
        {
            return new(null, argName);
        }

        /// <summary>
        /// Crea una nueva instancia de un
        /// <see cref="ArgumentOutOfRangeException"/> que indica que el valor
        /// del argumento se encuentra fuera de un rango de valores
        /// específicos.
        /// </summary>
        /// <param name="argName">
        /// Nombre del argumento para el cual se generará la excepción.
        /// </param>
        /// <param name="min">Valor mínimo aceptado.</param>
        /// <param name="max">Valor máximo aceptado.</param>
        /// <returns>
        /// Una nueva instancia de la clase <see cref="ArgumentOutOfRangeException"/>.
        /// </returns>
        public static ArgumentOutOfRangeException ValueOutOfRange(string argName, object min, object max)
        {
            return new(string.Format(Ers.ValueMustBeBetweenXandY, min, max), argName);
        }

        /// <summary>
        /// Crea una nueva instancia de un
        /// <see cref="ArgumentOutOfRangeException"/> que indica que el valor
        /// se encuentra fuera de los valores definidos de la enumeración.
        /// </summary>
        /// <typeparam name="T">Tipo de enuemración.</typeparam>
        /// <param name="argName">
        /// Nombre del argumento para el cual se generará la excepción.
        /// </param>
        /// <param name="offendingValue">Valor que ha causado la excepción.</param>
        /// <returns>
        /// Una nueva instancia de la clase
        /// <see cref="ArgumentOutOfRangeException"/>.
        /// </returns>
        public static ArgumentOutOfRangeException UndefinedEnum<T>(string argName, T offendingValue) where T : Enum
        {
            return new(string.Format(Ers.UndefinedEnum, offendingValue, typeof(T).NameOf()), argName);
        }

        /// <summary>
        /// Crea una nueva instancia de un
        /// <see cref="InvalidOperationException"/> que indica que el objeto
        /// especificado no puso ser escrito.
        /// </summary>
        /// <param name="t">Tipo del objeto que no pudo ser escrito.</param>
        /// <returns>
        /// Una nueva instancia de la clase 
        /// <see cref="InvalidOperationException"/>.
        /// </returns>
        public static InvalidOperationException CantWriteObj(Type t)
        {
            return new(string.Format(Ers.CantWriteObjOfT, t.NameOf()));
        }

        /// <summary>
        /// Crea una nueva instancia de un
        /// <see cref="InvalidOperationException"/> que indica que la propiedad
        /// es de solo lectura.
        /// </summary>
        /// <param name="prop">
        /// Propiedad para la cual se ha generado la excepción.
        /// </param>
        /// <returns>
        /// Una nueva instancia de la clase 
        /// <see cref="InvalidOperationException"/>.
        /// </returns>
        public static InvalidOperationException PropIsReadOnly(PropertyInfo prop)
        {
            return new(string.Format(Ers.PropIsReadOnly, prop.NameOf()));
        }

        /// <summary>
        /// Crea una nueva instancia de un <see cref="MissingMemberException"/>
        /// que indica que se ha intentado acceder a un miembro no existente en
        /// el tipo especificado.
        /// </summary>
        /// <param name="type">
        /// Tipo en el cual se ha intentado acceder al miembro no existente.
        /// </param>
        /// <param name="missingMember">
        /// Miembro al que se ha intentado acceder.
        /// </param>
        /// <returns>
        /// Una nueva instancia de la clase 
        /// <see cref="MissingMemberException"/>.
        /// </returns>
        public static MissingMemberException MissingMember(Type type, MemberInfo missingMember)
        {
            return new(type.Name, missingMember.Name);
        }

        /// <summary>
        /// Crea una nueva instancia de un <see cref="InvalidTypeException"/>
        /// que indica que el tipo especificado no es un tipo que hereda de
        /// <see cref="Enum"/> (declarado como <see langword="enum"/>).
        /// </summary>
        /// <param name="argName">Nombre del argumento que ha producido la excepción.</param>
        /// <param name="offendingType">
        /// Tipo por el cual se ha producido la excepción.
        /// </param>
        /// <returns>
        /// Una nueva instancia de la clase
        /// <see cref="InvalidTypeException"/>.
        /// </returns>
        public static ArgumentException EnumExpected(string argName, Type offendingType)
        {
            return new(string.Format(Ers.EnumTypeExpected, offendingType.NameOf()), argName);
        }

        /// <summary>
        /// Crea una nueva instancia de un <see cref="ArgumentException"/> que
        /// indica que un valor mínimo es mayor al máximo al especificar un
        /// rango de valores.
        /// </summary>
        /// <returns>
        /// Una nueva instancia de la clase <see cref="ArgumentException"/>.
        /// </returns>
        public static ArgumentException MinGtMax()
        {
            return new(Ers.MinGtMax);
        }

        /// <summary>
        /// Crea una nueva instancia de un
        /// <see cref="ClassNotInstantiableException"/> indicando el tipo de la
        /// clase que no pudo ser instanciada.
        /// </summary>
        /// <returns>
        /// Una nueva instancia de la clase
        /// <see cref="ClassNotInstantiableException"/>.
        /// </returns>
        public static ClassNotInstantiableException ClassNotInstantiable()
        {
            return ClassNotInstantiable(null);
        }

        /// <summary>
        /// Crea una nueva instancia de un
        /// <see cref="ClassNotInstantiableException"/> indicando el tipo de la
        /// clase que no pudo ser instanciada.
        /// </summary>
        /// <param name="class">
        /// Tipo de la clase que no pudo ser instanciada.
        /// </param>
        /// <returns>
        /// Una nueva instancia de la clase
        /// <see cref="ClassNotInstantiableException"/>.
        /// </returns>
        public static ClassNotInstantiableException ClassNotInstantiable(Type? @class)
        {
            return new(@class);
        }

        /// <summary>
        /// Crea una nueva instancia de un
        /// <see cref="IncompleteTypeException"/> indicando que el tipo debe
        /// contener un
        /// <see cref="System.Runtime.InteropServices.GuidAttribute"/> en su
        /// declaración para ser válido en este contexto.
        /// </summary>
        /// <param name="type">
        /// Tipo por el cual se ha producido la excepción.
        /// </param>
        /// <returns>
        /// Una nueva instancia de la clase
        /// <see cref="IncompleteTypeException"/>.
        /// </returns>
        public static IncompleteTypeException MissingGuidAttr(Type type)
        {
            return new(string.Format(Ers.MissingGuidAttrFromType, type), type);
        }

        /// <summary>
        /// Crea una nueva instancia de un <see cref="InvalidTypeException"/>
        /// que indica que el tipo especificado no es un tipo de enumeración.
        /// </summary>
        /// <param name="offendingType">
        /// Tipo por el cual se ha producido la excepción.
        /// </param>
        /// <returns>
        /// Una nueva instancia de la clase
        /// <see cref="InvalidTypeException"/>.
        /// </returns>
        public static InvalidTypeException EnumerableTypeExpected(Type offendingType)
        {
            return new(Ers.EnumerableTypeExpected, offendingType);
        }

        /// <summary>
        /// Crea una nueva instancia de un
        /// <see cref="InvalidOperationException"/> que indica que ejecutar la
        /// operación causa una operación circular.
        /// </summary>
        /// <returns>
        /// Una nueva instancia de la clase
        /// <see cref="InvalidOperationException"/>.
        /// </returns>
        public static InvalidOperationException CircularOpDetected()
        {
            return new(Ers.CircularOpDetected);
        }

        /// <summary>
        /// Crea una nueva instancia de un <see cref="TypeLoadException"/> que
        /// indica que el tipo no pudo ser instanciado con los argumentos de
        /// constructor provistos.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="inner"></param>
        /// <returns>
        /// Una nueva instancia de la clase <see cref="TypeLoadException"/>.
        /// </returns>
        /// <remarks>
        /// El error se capturará cuando ocurre un error dentro del
        /// constructor del tipo a ser instanciado. Si la clase no contiene un
        /// constructor que acepte los argumentos, llame al método
        /// <see cref="ClassNotInstantiable()"/> o a una de sus sobrecargas en
        /// su lugar.
        /// </remarks>
        /// <seealso cref="ClassNotInstantiable()"/>
        /// <seealso cref="ClassNotInstantiable(Type?)"/>
        public static TypeLoadException CouldntInstanceClass(Type t, Exception? inner = null)
        {
            return new TypeLoadException(string.Format(Ers.ClassNotInstantiableWIthArgs, t.NameOf()), inner);
        }
    }
}
