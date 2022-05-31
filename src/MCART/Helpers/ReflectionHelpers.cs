/*
ReflectionHelpers.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de manipulación de objetos, 

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

namespace TheXDS.MCART.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Extensions;

/// <summary>
/// Funciones auxiliares de reflexión.
/// </summary>
public static partial class ReflectionHelpers
{
    /// <summary>
    /// Obtiene una referencia al método que ha llamado al método
    /// actualmente en ejecución.
    /// </summary>
    /// <returns>
    /// El método que ha llamado al método actual en donde se usa esta
    /// función. Se devolverá <see langword="null"/> si se llama a este
    /// método desde el punto de entrada de la aplicación (generalmente
    /// la función <c>Main()</c>).
    /// </returns>
    public static MethodBase? GetCallingMethod()
    {
        return GetCallingMethod(3);
    }

    /// <summary>
    /// Obtiene una referencia al método que ha llamado al método actual.
    /// </summary>
    /// <param name="nCaller">
    /// Número de iteraciones padre del método a devolver. Debe ser un
    /// valor mayor o igual a 1.
    /// </param>
    /// <returns>
    /// El método que ha llamado al método actual en donde se usa esta
    /// función. Se devolverá <see langword="null"/> si al analizar la
    /// pila de llamadas se alcanza el punto de entrada de la
    /// aplicación (generalmente la función <c>Main()</c>).
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Se produce si <paramref name="nCaller"/> es menor a 1.
    /// </exception>
    /// <exception cref="OverflowException">
    /// Se produce si <paramref name="nCaller"/> + 1 produce un error
    /// de sobreflujo.
    /// </exception>
    public static MethodBase? GetCallingMethod(int nCaller)
    {
        GetCallingMethod_Contract(nCaller);
        StackFrame[]? frames = new StackTrace().GetFrames();
        return frames.Length > nCaller ? frames[nCaller].GetMethod() : null;
    }

    /// <summary>
    /// Obtiene el punto de entrada de la aplicación en ejecución.
    /// </summary>
    /// <returns>
    /// El método del punto de entrada de la aplicación actual.
    /// </returns>
    [Sugar]
    public static MethodInfo? GetEntryPoint()
    {
        return new StackTrace().GetFrames().Last().GetMethod() as MethodInfo;
    }

    /// <summary>
    /// Obtiene el ensamblado que contiene el punto de entrada de la
    /// aplicación en ejecución.
    /// </summary>
    /// <returns>
    /// El ensamblado donde se define el punto de entrada de la aplicación
    /// actual.
    /// </returns>
    [Sugar]
    public static Assembly? GetEntryAssembly()
    {
        return GetEntryPoint()?.DeclaringType?.Assembly;
    }

    /// <summary>
    /// Enumera el valor de todas los campos que devuelvan valores de tipo
    /// <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">Tipo de campos a obtener.</typeparam>
    /// <param name="fields">
    /// Colección de campos a analizar.
    /// </param>
    /// <param name="instance">
    /// Instancia desde la cual obtener los campos.
    /// </param>
    /// <returns>
    /// Una enumeración de todos los valores de tipo
    /// <typeparamref name="T" /> de la instancia.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="fields"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="NullItemException">
    /// Se produce si cualquier elemento de <paramref name="fields"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="MissingFieldException">
    /// Cuando <paramref name="instance"/> no es <see langword="null"/>, se
    /// produce si cualquier elemento de <paramref name="fields"/> no forma
    /// parte del tipo de <paramref name="instance"/>.
    /// </exception>
    /// <exception cref="MemberAccessException">
    /// Cuando <paramref name="instance"/> es <see langword="null"/>, se
    /// produce si cualquier elemento de <paramref name="fields"/> no es un
    /// campo estático.
    /// </exception>
    public static IEnumerable<T> FieldsOf<T>(IEnumerable<FieldInfo> fields, object? instance)
    {
        FieldsOf_Contract(fields, instance);
        return
            from j in fields.Where(p => p.IsPublic)
            where j.FieldType == typeof(T)
            where j.IsStatic == instance is null
            select (T)j.GetValue(instance)!;
    }

    /// <summary>
    /// Enumera el valor de todas los campos estáticos que devuelvan
    /// valores de tipo <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">Tipo de campos a obtener.</typeparam>
    /// <param name="fields">
    /// Colección de campos a analizar.
    /// </param>
    /// <returns>
    /// Una enumeración de todos los valores de tipo
    /// <typeparamref name="T" />.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="fields"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static IEnumerable<T> FieldsOf<T>(IEnumerable<FieldInfo> fields)
    {
        return FieldsOf<T>(fields, null);
    }

    /// <summary>
    /// Obtiene un miembro de una clase a partir de una expresión.
    /// </summary>
    /// <typeparam name="T">
    /// Clase desde la cual obtener al miembro.
    /// </typeparam>
    /// <param name="memberSelector">
    /// Expresión que indica qué miembro de la clase debe devolverse.
    /// </param>
    /// <returns>
    /// Un <see cref="MemberInfo"/> que representa al miembro
    /// seleccionado en la expresión.
    /// </returns>
    public static MemberInfo GetMember<T>(Expression<Func<T, object?>> memberSelector)
    {
        return GetMemberInternal(memberSelector);
    }

    /// <summary>
    /// Obtiene un miembro a partir de una expresión.
    /// </summary>
    /// <param name="memberSelector">
    /// Expresión que indica qué miembro debe devolverse.
    /// </param>
    /// <returns>
    /// Un <see cref="MemberInfo"/> que representa al miembro
    /// seleccionado en la expresión.
    /// </returns>
    public static MemberInfo GetMember(Expression<Func<object?>> memberSelector)
    {
        return GetMemberInternal(memberSelector);
    }

    /// <summary>
    /// Obtiene un miembro de instancia de una clase a partir de una
    /// expresión.
    /// </summary>
    /// <typeparam name="T">
    /// Clase desde la cual obtener al miembro.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Tipo del miembro obtenido.
    /// </typeparam>
    /// <param name="memberSelector">
    /// Expresión que indica qué miembro de la clase debe devolverse.
    /// </param>
    /// <returns>
    /// Un <see cref="MemberInfo"/> que representa al miembro
    /// seleccionado en la expresión.
    /// </returns>
    public static MemberInfo GetMember<T, TValue>(Expression<Func<T, TValue>> memberSelector)
    {
        return GetMemberInternal(memberSelector);
    }

    /// <summary>
    /// Obtiene un miembro de clase a partir de una expresión.
    /// </summary>
    /// <typeparam name="TValue">
    /// Tipo del miembro obtenido.
    /// </typeparam>
    /// <param name="memberSelector">
    /// Expresión que indica qué miembro de la clase debe devolverse.
    /// </param>
    /// <returns>
    /// Un <see cref="MemberInfo"/> que representa al miembro
    /// seleccionado en la expresión.
    /// </returns>
    public static MemberInfo GetMember<TValue>(Expression<Func<TValue>> memberSelector)
    {
        return GetMemberInternal(memberSelector);
    }

    /// <summary>
    /// Obtiene una referencia al miembro seleccionado por medio de una
    /// expresión.
    /// </summary>
    /// <typeparam name="TMember">
    /// Tipo del miembro a obtener.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Tipo a partir del cual obtener al miembro.
    /// </typeparam>
    /// <param name="memberSelector">
    /// Expresión que indica qué miembro de la clase debe devolverse.
    /// </param>
    /// <returns>
    /// Un <see cref="MemberInfo"/> que representa al miembro
    /// seleccionado en la expresión.
    /// </returns>
    public static TMember GetMember<TMember, TValue>(Expression<Func<TValue>> memberSelector) where TMember : MemberInfo
    {
        return GetMemberInternal(memberSelector) as TMember ?? throw new InvalidArgumentException(nameof(memberSelector));
    }

    /// <summary>
    /// Obtiene una referencia al miembro seleccionado por medio de una
    /// expresión.
    /// </summary>
    /// <typeparam name="TMember">
    /// Tipo del miembro a obtener.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Tipo devuelto por el miembro a obtener.
    /// </typeparam>
    /// <typeparam name="T">
    /// Tipo a partir del cual obtener al miembro.
    /// </typeparam>
    /// <param name="memberSelector">
    /// Expresión que indica qué miembro de la clase debe devolverse.
    /// </param>
    /// <returns>
    /// Un <see cref="MemberInfo"/> que representa al miembro
    /// seleccionado en la expresión.
    /// </returns>
    public static TMember GetMember<TMember, T, TValue>(Expression<Func<T, TValue>> memberSelector) where TMember : MemberInfo
    {
        return GetMemberInternal(memberSelector) as TMember ?? throw new InvalidArgumentException(nameof(memberSelector));
    }

    /// <summary>
    /// Obtiene una referencia al método seleccionado por medio de una
    /// expresión.
    /// </summary>
    /// <param name="methodSelector">
    /// Expresión que indica qué método del tipo debe devolverse.
    /// </param>
    /// <typeparam name="TMethod">
    /// Tipo delegado del método a obtener.
    /// </typeparam>
    /// <returns>
    /// Un <see cref="MethodInfo"/> que representa al método
    /// seleccionado en la expresión.
    /// </returns>
    public static MethodInfo GetMethod<TMethod>(Expression<Func<TMethod>> methodSelector) where TMethod : Delegate
    {
        return GetMember<MethodInfo, TMethod>(methodSelector);
    }

    /// <summary>
    /// Obtiene una referencia al método seleccionado por medio de una
    /// expresión.
    /// </summary>
    /// <param name="methodSelector">
    /// Expresión que indica qué método del tipo debe devolverse.
    /// </param>
    /// <typeparam name="T">
    /// Tipo desde el cual seleccionar al método a obtener.
    /// </typeparam>
    /// <typeparam name="TMethod">
    /// Tipo delegado del método a obtener.
    /// </typeparam>
    /// <returns>
    /// Un <see cref="MethodInfo"/> que representa al método
    /// seleccionado en la expresión.
    /// </returns>
    public static MethodInfo GetMethod<T, TMethod>(Expression<Func<T, TMethod>> methodSelector) where TMethod : Delegate
    {
        MethodInfo? m = GetMember<MethodInfo, T, TMethod>(methodSelector);

        /* HACK
         * Las expresiones de Linq podrían no detectar correctamente el
         * tipo de origen de un método que es una sobrecarga en una clase
         * derivada.
         */
        return m.DeclaringType == typeof(T) ? m
            : typeof(T).GetMethod(m.Name, m.GetBindingFlags(), null, m.GetParameters().Select(p => p.ParameterType).ToArray(), null) ?? throw new TamperException();
    }

    /// <summary>
    /// Obtiene una referencia al método seleccionado por medio de una
    /// expresión.
    /// </summary>
    /// <param name="methodSelector">
    /// Expresión que indica qué método del tipo debe devolverse.
    /// </param>
    /// <typeparam name="T">
    /// Tipo desde el cual seleccionar al método a obtener.
    /// </typeparam>
    /// <returns>
    /// Un <see cref="MethodInfo"/> que representa al método
    /// seleccionado en la expresión.
    /// </returns>
    public static MethodInfo GetMethod<T>(Expression<Func<T, Delegate>> methodSelector)
    {
        return GetMember<MethodInfo, T, Delegate>(methodSelector);
    }

    /// <summary>
    /// Obtiene una referencia a la propiedad seleccionada por medio de una
    /// expresión.
    /// </summary>
    /// <param name="propertySelector">
    /// Expresión que indica qué propiedad del tipo debe devolverse.
    /// </param>
    /// <typeparam name="T">
    /// Tipo desde el cual seleccionar a la propiedad a obtener.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Tipo devuelto por la propiedad a obtener.
    /// </typeparam>
    /// <returns>
    /// Un <see cref="PropertyInfo"/> que representa a la propiedad
    /// seleccionada en la expresión.
    /// </returns>
    public static PropertyInfo GetProperty<T, TValue>(Expression<Func<T, TValue>> propertySelector)
    {
        return GetMember<PropertyInfo, T, TValue>(propertySelector);
    }

    /// <summary>
    /// Obtiene una referencia a la propiedad seleccionada por medio de una
    /// expresión.
    /// </summary>
    /// <param name="propertySelector">
    /// Expresión que indica qué propiedad del tipo debe devolverse.
    /// </param>
    /// <typeparam name="TValue">
    /// Tipo devuelto por la propiedad a obtener.
    /// </typeparam>
    /// <returns>
    /// Un <see cref="PropertyInfo"/> que representa a la propiedad
    /// seleccionada en la expresión.
    /// </returns>
    public static PropertyInfo GetProperty<TValue>(Expression<Func<TValue>> propertySelector)
    {
        return GetMember<PropertyInfo, TValue>(propertySelector);
    }

    /// <summary>
    /// Obtiene una referencia a la propiedad seleccionada por medio de una
    /// expresión.
    /// </summary>
    /// <param name="propertySelector">
    /// Expresión que indica qué propiedad del tipo debe devolverse.
    /// </param>
    /// <typeparam name="T">
    /// Tipo desde el cual seleccionar a la propiedad a obtener.
    /// </typeparam>
    /// <returns>
    /// Un <see cref="PropertyInfo"/> que representa a la propiedad
    /// seleccionada en la expresión.
    /// </returns>
    public static PropertyInfo GetProperty<T>(Expression<Func<T, object?>> propertySelector)
    {
        return GetMember<PropertyInfo, T, object?>(propertySelector);
    }

    /// <summary>
    /// Obtiene una referencia al campo seleccionado por medio de una
    /// expresión.
    /// </summary>
    /// <param name="fieldSelector">
    /// Expresión que indica qué campo del tipo debe devolverse.
    /// </param>
    /// <typeparam name="T">
    /// Tipo desde el cual seleccionar al campo a obtener.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Tipo devuelto por el campo a obtener.
    /// </typeparam>
    /// <returns>
    /// Un <see cref="FieldInfo"/> que representa al campo seleccionado en
    /// la expresión.
    /// </returns>
    public static FieldInfo GetField<T, TValue>(Expression<Func<T, TValue>> fieldSelector)
    {
        return GetMember<FieldInfo, T, TValue>(fieldSelector);
    }

    /// <summary>
    /// Obtiene una referencia al campo seleccionado por medio de una
    /// expresión.
    /// </summary>
    /// <param name="fieldSelector">
    /// Expresión que indica qué campo del tipo debe devolverse.
    /// </param>
    /// <typeparam name="TValue">
    /// Tipo devuelto por el campo a obtener.
    /// </typeparam>
    /// <returns>
    /// Un <see cref="FieldInfo"/> que representa al campo seleccionado en
    /// la expresión.
    /// </returns>
    public static FieldInfo GetField<TValue>(Expression<Func<TValue>> fieldSelector)
    {
        return GetMember<FieldInfo, TValue>(fieldSelector);
    }

    /// <summary>
    /// Obtiene una referencia al campo seleccionado por medio de una
    /// expresión.
    /// </summary>
    /// <param name="fieldSelector">
    /// Expresión que indica qué campo del tipo debe devolverse.
    /// </param>
    /// <typeparam name="T">
    /// Tipo desde el cual seleccionar al campo a obtener.
    /// </typeparam>
    /// <returns>
    /// Un <see cref="FieldInfo"/> que representa al campo seleccionado en
    /// la expresión.
    /// </returns>
    public static FieldInfo GetField<T>(Expression<Func<T, object?>> fieldSelector)
    {
        return GetMember<FieldInfo, T, object?>(fieldSelector);
    }


    private static MemberInfo GetMemberInternal(LambdaExpression memberSelector)
    {
        return memberSelector.Body switch
        {
            UnaryExpression { Operand: MethodCallExpression { Object: ConstantExpression { Value: MethodInfo m } } } => m,
            UnaryExpression { Operand: MethodCallExpression { Method: { } m } } => m,
            MethodCallExpression { Object: ConstantExpression { Value: MethodInfo m } } => m,
            MethodCallExpression { Method: { } m } => m,
            UnaryExpression { Operand: MemberExpression m } => m.Member,
            MemberExpression m => m.Member,
            _ => throw Errors.InvalidSelectorExpression()
        };
    }
}
