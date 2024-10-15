/*
ReflectionHelpers.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de manipulación de objetos, 

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

using System.Collections;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Funciones auxiliares de reflexión.
/// </summary>
public static partial class ReflectionHelpers
{
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
    /// Enumera el valor de todas los campos estáticos que devuelvan
    /// valores de tipo <typeparamref name="T" /> en el tipo especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de campos a obtener.</typeparam>
    /// <param name="type">
    /// Tipo desde el cual obtener los campos.
    /// </param>
    /// <returns>
    /// Una enumeración de todos los valores de tipo
    /// <typeparamref name="T" /> del tipo.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="type"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static IEnumerable<T> FieldsOf<T>(this Type type)
    {
        FieldsOf_Contract(type);
        return FieldsOf<T>(type.GetFields(BindingFlags.Static | BindingFlags.Public), null);
    }

    /// <summary>
    /// Instancia todos los objetos del tipo especificado,
    /// devolviéndolos en una enumeración.
    /// </summary>
    /// <typeparam name="T">Tipo de objetos a buscar.</typeparam>
    /// <returns>
    /// Una enumeración de todas las instancias de objeto de tipo
    /// <typeparamref name="T"/> encontradas.
    /// </returns>
    public static IEnumerable<T> FindAllObjects<T>() where T : notnull
    {
        return FindAllObjects<T>((IEnumerable?)null);
    }

    /// <summary>
    /// Instancia todos los objetos del tipo especificado,
    /// devolviéndolos en una enumeración.
    /// </summary>
    /// <typeparam name="T">Tipo de objetos a buscar.</typeparam>
    /// <param name="typeFilter">
    /// Función de filtro a aplicar a los tipos coincidentes.
    /// </param>
    /// <returns>
    /// Una enumeración de todas las instancias de objeto de tipo
    /// <typeparamref name="T"/> encontradas.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="typeFilter"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static IEnumerable<T> FindAllObjects<T>(Func<Type, bool> typeFilter) where T : notnull
    {
        return FindAllObjects<T>(null, typeFilter);
    }

    /// <summary>
    /// Instancia todos los objetos del tipo especificado,
    /// devolviéndolos en una enumeración.
    /// </summary>
    /// <typeparam name="T">Tipo de objetos a buscar.</typeparam>
    /// <param name="ctorArgs">
    /// Argumentos a pasar al constructor de instancia de la clase.
    /// </param>
    /// <param name="typeFilter">
    /// Función de filtro a aplicar a los tipos coincidentes.
    /// </param>
    /// <returns>
    /// Una enumeración de todas las instancias de objeto de tipo
    /// <typeparamref name="T"/> encontradas.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="typeFilter"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static IEnumerable<T> FindAllObjects<T>(IEnumerable? ctorArgs, Func<Type, bool> typeFilter) where T : notnull
    {
        FindAllObjects_Contract(ctorArgs, typeFilter);
        return GetTypes<T>(true).NotNull().Where(typeFilter).Select(p => p.New<T>(false, ctorArgs)).Where(p => p is not null)!;
    }

    /// <summary>
    /// Instancia todos los objetos del tipo especificado,
    /// devolviéndolos en una enumeración.
    /// </summary>
    /// <typeparam name="T">Tipo de objetos a buscar.</typeparam>
    /// <param name="ctorArgs">
    /// Argumentos a pasar al constructor de instancia de la clase.
    /// </param>
    /// <returns>
    /// Una enumeración de todas las instancias de objeto de tipo
    /// <typeparamref name="T"/> encontradas.
    /// </returns>
    public static IEnumerable<T> FindAllObjects<T>(IEnumerable? ctorArgs) where T : notnull
    {
        return GetTypes<T>(true).NotNull().Select(p => p.New<T>(false, ctorArgs)).Where(p => p is not null)!;
    }

    /// <summary>
    /// Obtiene al primer objeto que coincida con el tipo base
    /// especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto a buscar.</typeparam>
    /// <returns>
    /// Una nueva instancia del objeto solicitado, o
    /// <see langword="null"/> si no se encuentra ningún tipo
    /// coincidente.
    /// </returns>
    public static T? FindFirstObject<T>() where T : notnull
    {
        return FindFirstObject<T>((IEnumerable?)null);
    }

    /// <summary>
    /// Obtiene al primer objeto que coincida con el tipo base
    /// especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto a buscar.</typeparam>
    /// <param name="typeFilter">
    /// Función de filtro a aplicar a los tipos coincidentes.
    /// </param>
    /// <returns>
    /// Una nueva instancia del objeto solicitado, o
    /// <see langword="null"/> si no se encuentra ningún tipo
    /// coincidente.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="typeFilter"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static T? FindFirstObject<T>(Func<Type, bool> typeFilter) where T : notnull
    {
        return FindFirstObject<T>(null, typeFilter);
    }

    /// <summary>
    /// Obtiene al primer objeto que coincida con el tipo base
    /// especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto a buscar.</typeparam>
    /// <param name="ctorArgs">
    /// Argumentos a pasar al constructor de instancia de la clase.
    /// </param>
    /// <param name="typeFilter">
    /// Función de filtro a aplicar a los tipos coincidentes.
    /// </param>
    /// <returns>
    /// Una nueva instancia del objeto solicitado, o
    /// <see langword="null"/> si no se encuentra ningún tipo
    /// coincidente.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="typeFilter"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static T? FindFirstObject<T>(IEnumerable? ctorArgs, Func<Type, bool> typeFilter) where T : notnull
    {
        FindFirstObject_Contract(ctorArgs, typeFilter);
        Type? t = GetTypes<T>(true).NotNull().FirstOrDefault(typeFilter);
        return t is not null ? t.New<T>(false, ctorArgs) : default;
    }

    /// <summary>
    /// Obtiene al primer objeto que coincida con el tipo base
    /// especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto a buscar.</typeparam>
    /// <param name="ctorArgs">
    /// Argumentos a pasar al constructor de instancia de la clase.
    /// </param>
    /// <returns>
    /// Una nueva instancia del objeto solicitado, o
    /// <see langword="null"/> si no se encuentra ningún tipo
    /// coincidente.
    /// </returns>
    public static T? FindFirstObject<T>(IEnumerable? ctorArgs) where T : notnull
    {
        Type? t = GetTypes<T>().FirstOrDefault(p => p.IsInstantiable(ctorArgs?.ToTypes().ToArray() ?? Type.EmptyTypes));
        return t is not null ? t.New<T>(false, ctorArgs) : default;
    }

    /// <summary>
    /// Obtiene un único objeto que coincida con el tipo base
    /// especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto a buscar.</typeparam>
    /// <returns>
    /// Una nueva instancia del objeto solicitado, o
    /// <see langword="null"/> si no se encuentra ningún tipo
    /// coincidente.
    /// </returns>
    public static T? FindSingleObject<T>() where T : notnull
    {
        return FindSingleObject<T>((IEnumerable?)null);
    }

    /// <summary>
    /// Obtiene un único objeto que coincida con el tipo base
    /// especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto a buscar.</typeparam>
    /// <param name="typeFilter">
    /// Función de filtro a aplicar a los tipos coincidentes.
    /// </param>
    /// <returns>
    /// Una nueva instancia del objeto solicitado, o
    /// <see langword="null"/> si no se encuentra ningún tipo
    /// coincidente.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="typeFilter"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static T? FindSingleObject<T>(Func<Type, bool> typeFilter) where T : notnull
    {
        return FindSingleObject<T>(null, typeFilter);
    }

    /// <summary>
    /// Obtiene un único objeto que coincida con el tipo base
    /// especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto a buscar.</typeparam>
    /// <param name="ctorArgs">
    /// Argumentos a pasar al constructor de instancia de la clase.
    /// </param>
    /// <param name="typeFilter">
    /// Función de filtro a aplicar a los tipos coincidentes.
    /// </param>
    /// <returns>
    /// Una nueva instancia del objeto solicitado, o
    /// <see langword="null"/> si no se encuentra ningún tipo
    /// coincidente.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="typeFilter"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static T? FindSingleObject<T>(IEnumerable? ctorArgs, Func<Type, bool> typeFilter) where T : notnull
    {
        FindSingleObject_Contract(ctorArgs, typeFilter);
        Type? t = GetTypes<T>(true).NotNull().SingleOrDefault(typeFilter);
        return t is not null ? t.New<T>(false, ctorArgs) : default;
    }

    /// <summary>
    /// Obtiene un único objeto que coincida con el tipo base
    /// especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto a buscar.</typeparam>
    /// <param name="ctorArgs">
    /// Argumentos a pasar al constructor de instancia de la clase.
    /// </param>
    /// <returns>
    /// Una nueva instancia del objeto solicitado, o
    /// <see langword="null"/> si no se encuentra ningún tipo
    /// coincidente.
    /// </returns>
    public static T? FindSingleObject<T>(IEnumerable? ctorArgs) where T : notnull
    {
        Type? t = GetTypes<T>(true).NotNull().SingleOrDefault();
        return t is not null ? t.New<T>(false, ctorArgs) : default;
    }

    /// <summary>
    /// Busca en el <see cref="AppDomain" /> especificado un tipo que
    /// contenga el <see cref="IdentifierAttribute" /> especificado.
    /// </summary>
    /// <param name="identifier">Identificador a buscar.</param>
    /// <param name="domain">Dominio en el cual buscar.</param>
    /// <returns>
    /// Un tipo que ha sido etiquetado con el identificador especificado,
    /// o <see langword="null" /> si ningún tipo contiene el identificador.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="identifier"/> o
    /// <paramref name="domain"/> son <see langword="null"/>.
    /// </exception>
    [Sugar]
    public static Type? FindType(string identifier, AppDomain domain)
    {
        return FindType<object>(identifier, domain);
    }

    /// <summary>
    /// Busca en el <see cref="AppDomain" /> actual un tipo que contenga el
    /// <see cref="IdentifierAttribute" /> especificado.
    /// </summary>
    /// <param name="identifier">Identificador a buscar.</param>
    /// <returns>
    /// Un tipo que ha sido etiquetado con el identificador especificado,
    /// o <see langword="null" /> si ningún tipo contiene el identificador.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="identifier"/> es
    /// <see langword="null"/>.
    /// </exception>
    [Sugar]
    public static Type? FindType(string identifier)
    {
        return FindType<object>(identifier);
    }

    /// <summary>
    /// Busca en el <see cref="AppDomain" /> especificado un tipo que
    /// contenga el <see cref="IdentifierAttribute" /> especificado.
    /// </summary>
    /// <typeparam name="T">Restringir búsqueda a estos tipos.</typeparam>
    /// <param name="identifier">Identificador a buscar.</param>
    /// <param name="domain">Dominio en el cual buscar.</param>
    /// <returns>
    /// Un tipo que ha sido etiquetado con el identificador especificado,
    /// o <see langword="null" /> si ningún tipo contiene el identificador.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="identifier"/> o
    /// <paramref name="domain"/> son <see langword="null"/>.
    /// </exception>
    public static Type? FindType<T>(string identifier, AppDomain domain)
    {
        FindType_Contract(identifier, domain);
        return GetTypes<T>(domain)
            .FirstOrDefault(j => j.GetCustomAttributes(typeof(IdentifierAttribute), false)
                .Cast<IdentifierAttribute>()
                .Any(k => k.Value == identifier));
    }

    /// <summary>
    /// Busca en el <see cref="AppDomain" /> actual un tipo que contenga el
    /// <see cref="IdentifierAttribute" /> especificado.
    /// </summary>
    /// <typeparam name="T">Restringir búsqueda a estos tipos.</typeparam>
    /// <param name="identifier">Identificador a buscar.</param>
    /// <returns>
    /// Un tipo que ha sido etiquetado con el identificador especificado,
    /// o <see langword="null" /> si ningún tipo contiene el identificador.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="identifier"/> es
    /// <see langword="null"/>.
    /// </exception>
    [Sugar]
    public static Type? FindType<T>(string identifier)
    {
        return FindType<T>(identifier, AppDomain.CurrentDomain);
    }

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
    /// <remarks>Debido a algunas optimizaciones realizadas por el compilador,
    /// es recomendable que en los métodos donde se llame a
    /// <see cref="GetCallingMethod()"/> sean anotados con el atributo
    /// <see cref="MethodImplAttribute"/> con el valor
    /// <see cref="MethodImplOptions.NoInlining"/>.
    /// </remarks>
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
    /// <remarks>Debido a algunas optimizaciones realizadas por el compilador,
    /// es recomendable que en los métodos donde se llame a
    /// <see cref="GetCallingMethod(int)"/> sean anotados con el atributo
    /// <see cref="MethodImplAttribute"/> con el valor
    /// <see cref="MethodImplOptions.NoInlining"/>.
    /// </remarks>
    public static MethodBase? GetCallingMethod(int nCaller)
    {
        GetCallingMethod_Contract(nCaller);
        StackFrame[]? frames = new StackTrace().GetFrames();
        return frames.Length > nCaller ? frames[nCaller].GetMethod() : null;
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
    /// Enumera las propiedades del tipo especificado cuyo tipo de valor sea
    /// <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de las propiedades a obtener.</typeparam>
    /// <param name="t">Tipo del cual enumerar las propiedades.</param>
    /// <param name="flags">
    /// Banderas de declaración a utilizar para filtrar los miembros a obtener.
    /// </param>
    /// <returns>
    /// Una enumeración de las propiedades del tipo deseado contenidas dentro
    /// del tipo especificado.
    /// </returns>
    public static IEnumerable<PropertyInfo> GetPropertiesOf<T>(this Type t, BindingFlags flags)
    {
        return t.GetProperties(flags).Where(p => p.PropertyType.Implements<T>());
    }

    /// <summary>
    /// Enumera las propiedades del tipo especificado cuyo tipo de valor sea
    /// <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de las propiedades a obtener.</typeparam>
    /// <param name="t">Tipo del cual enumerar las propiedades.</param>
    /// <returns>
    /// Una enumeración de las propiedades del tipo deseado contenidas dentro
    /// del tipo especificado.
    /// </returns>
    public static IEnumerable<PropertyInfo> GetPropertiesOf<T>(this Type t)
    {
        return t.GetProperties().Where(p => p.PropertyType.Implements<T>());
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
    /// Obtiene una lista de tipos asignables a partir de la interfaz o clase base
    /// especificada.
    /// </summary>
    /// <typeparam name="T">Interfaz o clase base a buscar.</typeparam>
    /// <returns>
    /// Una lista de tipos de las clases que implementan a la interfaz o que heredan a la clase base
    /// <typeparamref name="T" /> dentro de <see cref="AppDomain.CurrentDomain" />.
    /// </returns>
    /// <remarks>
    /// Esta función obtiene todos los tipos (privados y públicos)
    /// definidos dentro de todos ls ensamblados dentro del dominio
    /// actual. Para obtener únicamente aquellos tipos exportados
    /// públicamente, utilice <see cref="PublicTypes(Type)"/>,
    /// <see cref="PublicTypes(Type, AppDomain)"/>,
    /// <see cref="PublicTypes{T}()"/> o
    /// <see cref="PublicTypes{T}(AppDomain)"/>.
    /// </remarks>
    public static IEnumerable<Type> GetTypes<T>()
    {
        return typeof(T).Derivates();
    }

    /// <summary>
    /// Obtiene una lista de tipos asignables a partir de la interfaz o clase base
    /// especificada dentro del <see cref="AppDomain" /> especificado.
    /// </summary>
    /// <typeparam name="T">Interfaz o clase base a buscar.</typeparam>
    /// <param name="domain">
    /// <see cref="AppDomain" /> en el cual realizar la búsqueda.
    /// </param>
    /// <param name="instantiablesOnly">
    /// Si se establece en <see langword="true" />, únicamente se incluirán aquellos tipos instanciables.
    /// <see langword="false" /> hará que se devuelvan todos los tipos coincidentes.
    /// </param>
    /// <returns>
    /// Una lista de tipos de las clases que implementan a la interfaz o que heredan a la clase base
    /// <typeparamref name="T" /> dentro del <paramref name="domain" />.
    /// </returns>
    /// <remarks>
    /// Esta función obtiene todos los tipos (privados y públicos)
    /// definidos dentro de todos los ensamblados dentro del dominio
    /// especificado. Para obtener únicamente aquellos tipos exportados
    /// públicamente, utilice <see cref="PublicTypes(Type)"/>,
    /// <see cref="PublicTypes(Type, AppDomain)"/>,
    /// <see cref="PublicTypes{T}()"/> o
    /// <see cref="PublicTypes{T}(AppDomain)"/>.
    /// </remarks>
    public static IEnumerable<Type> GetTypes<T>(AppDomain domain, in bool instantiablesOnly)
    {
        return domain.GetAssemblies().GetTypes<T>(instantiablesOnly);
    }

    /// <summary>
    /// Obtiene una lista de tipos asignables a partir de la interfaz o clase base
    /// especificada dentro del <see cref="AppDomain" /> especificado.
    /// </summary>
    /// <typeparam name="T">Interfaz o clase base a buscar.</typeparam>
    /// <param name="domain">
    /// <see cref="AppDomain" /> en el cual realizar la búsqueda.
    /// </param>
    /// <returns>
    /// Una lista de tipos de las clases que implementan a la interfaz o que heredan a la clase base
    /// <typeparamref name="T" /> dentro del <paramref name="domain" />.
    /// </returns>
    /// <remarks>
    /// Esta función obtiene todos los tipos (privados y públicos)
    /// definidos dentro de todos ls ensamblados dentro del dominio
    /// especificado. Para obtener únicamente aquellos tipos exportados
    /// públicamente, utilice <see cref="PublicTypes(Type)"/>,
    /// <see cref="PublicTypes(Type, AppDomain)"/>,
    /// <see cref="PublicTypes{T}()"/> o
    /// <see cref="PublicTypes{T}(AppDomain)"/>.
    /// </remarks>
    [Sugar]
    public static IEnumerable<Type> GetTypes<T>(AppDomain domain)
    {
        return typeof(T).Derivates(domain);
    }

    /// <summary>
    /// Obtiene una lista de tipos asignables a partir de la interfaz o clase base
    /// especificada.
    /// </summary>
    /// <typeparam name="T">Interfaz o clase base a buscar.</typeparam>
    /// <param name="instantiablesOnly">
    /// Si se establece en <see langword="true" />, únicamente se incluirán aquellos tipos instanciables.
    /// <see langword="false" /> hará que se devuelvan todos los tipos coincidentes.
    /// </param>
    /// <returns>
    /// Una lista de tipos de las clases que implementan a la interfaz o que heredan a la clase base
    /// <typeparamref name="T" /> dentro de <see cref="AppDomain.CurrentDomain" />.
    /// </returns>
    /// <remarks>
    /// Esta función obtiene todos los tipos (privados y públicos)
    /// definidos dentro de todos ls ensamblados dentro del dominio
    /// actual. Para obtener únicamente aquellos tipos exportados
    /// públicamente, utilice <see cref="PublicTypes(Type)"/>,
    /// <see cref="PublicTypes(Type, AppDomain)"/>,
    /// <see cref="PublicTypes{T}()"/> o
    /// <see cref="PublicTypes{T}(AppDomain)"/>.
    /// </remarks>
    public static IEnumerable<Type> GetTypes<T>(bool instantiablesOnly)
    {
        return GetTypes<T>(AppDomain.CurrentDomain, instantiablesOnly);
    }

    /// <summary>
    /// Obtiene una lista de tipos asignables a partir de la interfaz o clase base
    /// especificada dentro del <see cref="AppDomain" /> especificado.
    /// </summary>
    /// <typeparam name="T">Interfaz o clase base a buscar.</typeparam>
    /// <param name="assemblies">
    /// Colección de ensamblados en la cual realizar la búsqueda.
    /// </param>
    /// <param name="instantiablesOnly">
    /// Si se establece en <see langword="true" />, únicamente se incluirán aquellos tipos instanciables.
    /// <see langword="false" /> hará que se devuelvan todos los tipos coincidentes.
    /// </param>
    /// <returns>
    /// Una lista de tipos de las clases que implementan a la interfaz o que heredan a la clase base
    /// <typeparamref name="T" /> dentro del dominio predeterminado.
    /// </returns>
    /// <remarks>
    /// Esta función obtiene todos los tipos (privados y públicos)
    /// definidos dentro de todos los ensamblados dentro de la
    /// colección especificada. Para obtener únicamente aquellos tipos
    /// exportados públicamente, utilice
    /// <see cref="PublicTypes(Type)"/>,
    /// <see cref="PublicTypes(Type, AppDomain)"/>,
    /// <see cref="PublicTypes{T}()"/> o
    /// <see cref="PublicTypes{T}(AppDomain)"/>.
    /// </remarks>
    public static IEnumerable<Type> GetTypes<T>(this IEnumerable<Assembly> assemblies, bool instantiablesOnly)
    {
        Type? TryType(Type k)
        {
            try
            {
                return typeof(T).IsAssignableFrom(k)
                    && (!instantiablesOnly || !(k.IsInterface || k.IsAbstract || k.GetConstructors().Length == 0))
                    ? k : null;
            }
            catch { return null; }
        }
        IEnumerable<Type?> TryAssembly(Assembly j)
        {
            try
            {
                return j.GetTypes().Select(TryType);
            }
            catch
            {
                return [];
            }
        }
        return assemblies.SelectMany(TryAssembly).NotNull();
    }

    /// <summary>
    /// Obtiene una lista de tipos asignables a partir de la interfaz o clase base
    /// especificada dentro del <see cref="AppDomain" /> especificado.
    /// </summary>
    /// <typeparam name="T">Interfaz o clase base a buscar.</typeparam>
    /// <param name="assemblies">
    /// Colección de ensamblados en la cual realizar la búsqueda.
    /// </param>
    /// <returns>
    /// Una lista de tipos de las clases que implementan a la interfaz
    /// o que heredan a la clase base <typeparamref name="T" /> dentro
    /// de <paramref name="assemblies" />.
    /// </returns>
    /// <remarks>
    /// Esta función obtiene todos los tipos (privados y públicos)
    /// definidos dentro de todos los ensamblados dentro de la
    /// colección especificada. Para obtener únicamente aquellos tipos
    /// exportados públicamente, utilice
    /// <see cref="PublicTypes(Type)"/>,
    /// <see cref="PublicTypes(Type, AppDomain)"/>,
    /// <see cref="PublicTypes{T}()"/> o
    /// <see cref="PublicTypes{T}(AppDomain)"/>.
    /// </remarks>
    [Sugar]
    public static IEnumerable<Type> GetTypes<T>(this IEnumerable<Assembly> assemblies)
    {
        return typeof(T).Derivates(assemblies);
    }

    /// <summary>
    /// Enumera el valor de todas las propiedades que devuelvan valores de
    /// tipo <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">Tipo de propiedades a obtener.</typeparam>
    /// <param name="properties">
    /// Colección de propiedades a analizar.
    /// </param>
    /// <param name="instance">
    /// Instancia desde la cual obtener las propiedades.
    /// </param>
    /// <returns>
    /// Una enumeración de todos los valores de tipo
    /// <typeparamref name="T" /> de la instancia.
    /// </returns>
    public static IEnumerable<T> PropertiesOf<T>(this IEnumerable<PropertyInfo> properties, object? instance)
    {
        return
            from j in properties.Where(p => p.CanRead)
            where j.PropertyType.Implements(typeof(T))
            select (T)j.GetMethod!.Invoke(instance, [])!;
    }

    /// <summary>
    /// Enumera el valor de todas las propiedades estáticas que devuelvan
    /// valores de tipo <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">Tipo de propiedades a obtener.</typeparam>
    /// <param name="properties">
    /// Colección de propiedades a analizar.
    /// </param>
    /// <returns>
    /// Una enumeración de todos los valores de tipo
    /// <typeparamref name="T" />.
    /// </returns>
    public static IEnumerable<T> PropertiesOf<T>(this IEnumerable<PropertyInfo> properties)
    {
        return PropertiesOf<T>(properties, null);
    }

    /// <summary>
    /// Obtiene todos los tipos públicos que estén dentro del
    /// <see cref="AppDomain"/> actual.
    /// </summary>
    /// <returns>
    /// Una enumeración con todos los tipos públicos encontrados en el
    /// dominio actual.
    /// </returns>
    /// <remarks>
    /// Esta función obtiene todos los tipos públicos exportados del
    /// dominio actual, obviando los ensamblados dinámicos (generados
    /// por medio del espacio de nombres
    /// <see cref="System.Reflection.Emit"/>). Para obtener una lista
    /// indiscriminada de tipos, utilice <see cref="GetTypes{T}()"/>.
    /// </remarks>
    public static IEnumerable<Type> PublicTypes()
    {
        return PublicTypes(AppDomain.CurrentDomain);
    }

    /// <summary>
    /// Obtiene todos los tipos públicos que estén dentro del
    /// <see cref="AppDomain"/> especificado.
    /// </summary>
    /// <param name="domain">
    /// Dominio de aplicación dentro del cual buscar tipos.
    /// </param>
    /// <returns>
    /// Una enumeración con todos los tipos públicos encontrados en el
    /// dominio especificado.
    /// </returns>
    /// <remarks>
    /// Esta función obtiene todos los tipos públicos exportados del
    /// dominio especificado, obviando los ensamblados dinámicos
    /// (generados por medio del espacio de nombres
    /// <see cref="System.Reflection.Emit"/>). Para obtener una lista
    /// indiscriminada de tipos, utilice <see cref="GetTypes{T}()"/>.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="domain"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static IEnumerable<Type> PublicTypes(AppDomain domain)
    {
        PublicTypes_Contract(domain);
        return domain.GetAssemblies()
            .Where(p => !p.IsDynamic)
            .SelectMany(SafeGetExportedTypes);
    }

    /// <summary>
    /// Obtiene todos los tipos públicos que implementan al tipo especificado.
    /// </summary>
    /// <param name="type">Tipo a obtener.</param>
    /// <param name="domain">Dominio de aplicación dentro del cual buscar tipos.</param>
    /// <returns>
    /// Una enumeración con todos los tipos que heredan o implementan el
    /// tipo especificado.
    /// </returns>
    /// <remarks>
    /// Esta función obtiene todos los tipos públicos exportados del
    /// dominio especificado, obviando los ensamblados dinámicos
    /// (generados por medio del espacio de nombres
    /// <see cref="System.Reflection.Emit"/>). Para obtener una lista
    /// indiscriminada de tipos, utilice <see cref="GetTypes{T}()"/>.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="domain"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static IEnumerable<Type> PublicTypes(Type type, AppDomain domain)
    {
        PublicTypes_Contract(type, domain);
        return PublicTypes(domain).Where(type.IsAssignableFrom);
    }

    /// <summary>
    /// Obtiene todos los tipos públicos que implementan al tipo especificado.
    /// </summary>
    /// <param name="type">Tipo a obtener.</param>
    /// <returns>
    /// Una enumeración con todos los tipos que heredan o implementan el
    /// tipo especificado.
    /// </returns>
    /// <remarks>
    /// Esta función obtiene todos los tipos públicos exportados del
    /// dominio actual, obviando los ensamblados dinámicos (generados
    /// por medio del espacio de nombres
    /// <see cref="System.Reflection.Emit"/>). Para obtener una lista
    /// indiscriminada de tipos, utilice <see cref="GetTypes{T}()"/>.
    /// </remarks>
    public static IEnumerable<Type> PublicTypes(Type type)
    {
        return PublicTypes(type, AppDomain.CurrentDomain);
    }

    /// <summary>
    /// Obtiene todos los tipos públicos que implementan al tipo especificado.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de objetos a obtener.
    /// </typeparam>
    /// <returns>
    /// Una enumeración con todos los tipos que heredan o implementan el
    /// tipo especificado.
    /// </returns>
    /// <remarks>
    /// Esta función obtiene todos los tipos públicos exportados del
    /// dominio especificado, obviando los ensamblados dinámicos
    /// (generados por medio del espacio de nombres
    /// <see cref="System.Reflection.Emit"/>). Para obtener una lista
    /// indiscriminada de tipos, utilice <see cref="GetTypes{T}()"/>.
    /// </remarks>
    public static IEnumerable<Type> PublicTypes<T>()
    {
        return PublicTypes(typeof(T));
    }

    /// <summary>
    /// Obtiene todos los tipos públicos que implementan al tipo especificado.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de objetos a obtener.
    /// </typeparam>
    /// <param name="domain">Dominio de aplicación dentro del cual buscar tipos.</param>
    /// <returns>
    /// Una enumeración con todos los tipos que heredan o implementan el
    /// tipo especificado.
    /// </returns>
    /// <remarks>
    /// Esta función obtiene todos los tipos públicos exportados del
    /// dominio especificado, obviando los ensamblados dinámicos
    /// (generados por medio del espacio de nombres
    /// <see cref="System.Reflection.Emit"/>). Para obtener una lista
    /// indiscriminada de tipos, utilice <see cref="GetTypes{T}()"/>.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="domain"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static IEnumerable<Type> PublicTypes<T>(AppDomain domain)
    {
        return PublicTypes(typeof(T), domain);
    }

    /// <summary>
    /// Obtiene todos los métodos de instancia con firma compatible con el
    /// delegado especificado.
    /// </summary>
    /// <typeparam name="T">
    /// Delegado a utilizar como firma a comprobar.
    /// </typeparam>
    /// <param name="methods">
    /// Colección de métodos en la cual realizar la búsqueda.
    /// </param>
    /// <param name="instance">
    /// Instancia del objeto sobre el cual construir los delegados.
    /// </param>
    /// <returns>
    /// Una enumeración de todos los métodos que tienen una firma
    /// compatible con <typeparamref name="T" />.
    /// </returns>
    public static IEnumerable<T> WithSignature<T>(this IEnumerable<MethodInfo> methods, object instance) where T : Delegate
    {
        foreach (MethodInfo? j in methods)
        {
            if (Objects.TryCreateDelegate(j, instance, out T? d))
            {
                yield return d ?? throw new TamperException();
            }
        }
    }

    /// <summary>
    /// Obtiene todos los métodos estáticos con firma compatible con el
    /// delegado especificado.
    /// </summary>
    /// <typeparam name="T">
    /// Delegado a utilizar como firma a comprobar.
    /// </typeparam>
    /// <param name="methods">
    /// Colección de métodos en la cual realizar la búsqueda.
    /// </param>
    /// <returns>
    /// Una enumeración de todos los métodos que tienen una firma
    /// compatible con <typeparamref name="T" />.
    /// </returns>
    public static IEnumerable<T> WithSignature<T>(this IEnumerable<MethodInfo> methods) where T : Delegate
    {
        foreach (MethodInfo? j in methods)
        {
            if (Objects.TryCreateDelegate(j, out T? d))
            {
                yield return d ?? throw new TamperException();
            }
        }
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

    private static IEnumerable<Type> SafeGetExportedTypes(Assembly arg)
    {
        try
        {
            return arg.GetExportedTypes();
        }
        catch
        {
            return [];
        }        
    }
}
