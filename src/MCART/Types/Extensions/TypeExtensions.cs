/*
TypeExtensions.cs

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

namespace TheXDS.MCART.Types.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Resources;
using System.Linq.Expressions;
using TheXDS.MCART.Helpers;

/// <summary>
/// Extensiones para todos los elementos de tipo <see cref="Type"/>.
/// </summary>
public static partial class TypeExtensions
{
    /// <summary>
    /// Comprueba si todos los tipos son asignables a partir del tipo
    /// <paramref name="source" />.
    /// </summary>
    /// <param name="types">Lista de tipos a comprobar.</param>
    /// <param name="source">Tipo que desea asignarse.</param>
    /// <returns>
    /// <see langword="true" /> si todos los tipos son asignables a partir de
    /// <paramref name="source" />, <see langword="false" /> en caso contrario.
    /// </returns>
    public static bool AreAllAssignable(this Type source, IEnumerable<Type> types)
    {
        return types.All(p => p.IsAssignableFrom(source));
    }

    /// <summary>
    /// Comprueba si todos los tipos son asignables a partir del tipo
    /// <paramref name="source" />.
    /// </summary>
    /// <param name="types">Lista de tipos a comprobar.</param>
    /// <param name="source">Tipo que desea asignarse.</param>
    /// <returns>
    /// <see langword="true" /> si todos los tipos son asignables a partir de
    /// <paramref name="source" />, <see langword="false" /> en caso contrario.
    /// </returns>
    public static bool AreAllAssignable(this Type source, params Type[] types)
    {
        return source.AreAllAssignable(types.AsEnumerable());
    }

    /// <summary>
    /// Enumera los tipos asignables a partir de <paramref name="source" />.
    /// </summary>
    /// <param name="types">Lista de tipos a comprobar.</param>
    /// <param name="source">Tipo que desea asignarse.</param>
    /// <returns>
    /// Un enumerador con los tipos que pueden ser asignados a partir de
    /// <paramref name="source" />.
    /// </returns>
    public static IEnumerable<Type> Assignables(this Type source, IEnumerable<Type> types)
    {
        return types.Where(p => p.IsAssignableFrom(source));
    }

    /// <summary>
    /// Enumera los tipos asignables a partir de <paramref name="source" />.
    /// </summary>
    /// <param name="types">Lista de tipos a comprobar.</param>
    /// <param name="source">Tipo que desea asignarse.</param>
    /// <returns>
    /// Un enumerador con los tipos que pueden ser asignados a partir de
    /// <paramref name="source" />.
    /// </returns>
    public static IEnumerable<Type> Assignables(this Type source, params Type[] types)
    {
        return source.Assignables(types.AsEnumerable());
    }

    /// <summary>
    /// Equivalente programático de <see langword="default" />, obtiene
    /// el valor predeterminado del tipo.
    /// </summary>
    /// <param name="t">
    /// <see cref="Type" /> del cual obtener el valor predeterminado.
    /// </param>
    /// <returns>
    /// Una nueva instancia del tipo si el mismo es un
    /// <see langword="struct" />, o <see langword="null" /> si es una
    /// <see langword="class" />.
    /// </returns>
    public static object? Default(this Type t)
    {
        return t.IsValueType ? Activator.CreateInstance(t) : null;
    }

    /// <summary>
    /// Determina si el tipo implementa a <paramref name="baseType" />.
    /// </summary>
    /// <param name="type">Tipo a comprobar</param>
    /// <param name="baseType">Herencia de tipo a verificar.</param>
    /// <returns>
    /// <see langword="true" /> si <paramref name="type" /> implementa a <paramref name="baseType" />,
    /// <see langword="false" /> en caso contrario.
    /// </returns>
    public static bool Implements(this Type type, Type baseType)
    {
        if (!baseType.ContainsGenericParameters) return baseType.IsAssignableFrom(type);

        if (!baseType.GenericTypeArguments.Any())
            return (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == baseType) || type.GetInterfaces().Any(p => p.Implements(baseType));

        Type? gt = baseType.MakeGenericType(type);
        return !gt.ContainsGenericParameters && gt.IsAssignableFrom(type);
    }

    /// <summary>
    /// Determina si el tipo implementa a <paramref name="baseType" /> con los argumentos de tipo genérico especificados.
    /// </summary>
    /// <param name="type">Tipo a comprobar</param>
    /// <param name="baseType">Herencia de tipo a verificar.</param>
    /// <param name="typeArgs">Tipos de argumentos genéricos a utilizar para crear el tipo genérico a comprobar.</param>
    /// <returns>
    /// <see langword="true" /> si <paramref name="type"/> implementa a <paramref name="baseType" />,
    /// <see langword="false" /> en caso contrario.
    /// </returns>
    public static bool Implements(this Type type, Type baseType, params Type[] typeArgs)
    {
        if (!baseType.ContainsGenericParameters) return baseType.IsAssignableFrom(type);
        Type? gt = baseType.MakeGenericType(typeArgs);
        return !gt.ContainsGenericParameters && gt.IsAssignableFrom(type);
    }

    /// <summary>
    /// Determina si el tipo implementa a <typeparamref name="T" />.
    /// </summary>
    /// <param name="type">Tipo a comprobar</param>
    /// <typeparam name="T">Herencia de tipo a verificar.</typeparam>
    /// <returns>
    /// <see langword="true" /> si <paramref name="type" /> implementa a <typeparamref name="T" />,
    /// <see langword="false" /> en caso contrario.
    /// </returns>
    public static bool Implements<T>(this Type type)
    {
        return Implements(type, typeof(T));
    }

    /// <summary>
    /// Determina si el tipo implementa a todos los tipos especificados.
    /// </summary>
    /// <param name="type">Tipo a comprobar</param>
    /// <param name="baseTypes">
    /// Colección de tipos a comprobar que <paramref name="type"/>
    /// herede.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si <paramref name="type" /> implementa
    /// a todos los tipos especificados, <see langword="false" /> en
    /// caso contrario.
    /// </returns>
    public static bool Implements(this Type type, IEnumerable<Type> baseTypes)
    {
        return baseTypes.Select(type.Implements).And();
    }

    /// <summary>
    /// Comprueba si un tipo implementa un operador especificado por la
    /// expresión.
    /// </summary>
    /// <param name="type">Tipo a comprobar</param>
    /// <param name="operator">Operador a buscar en el tipo.</param>
    /// <returns>
    /// <see langword="true"/> si el operador existe en el tipo,
    /// <see langword="false"/> en caso contrario.
    /// </returns>
    public static bool ImplementsOperator(this Type type, Func<Expression, Expression, BinaryExpression> @operator)
    {
        ConstantExpression? c = Expression.Constant(type.Default(), type);
        try
        {
            _ = Expression.Lambda(
                Expression.TryCatch(
                    @operator(c, c),
                    Expression.Catch(Expression.Parameter(typeof(DivideByZeroException)), c)
                )).Compile().DynamicInvoke();
            return true;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }

    /// <summary>
    /// Comprueba si alguno de los tipos especificados es asignable a partir
    /// del tipo <paramref name="source" />.
    /// </summary>
    /// <param name="types">Lista de tipos a comprobar.</param>
    /// <param name="source">Tipo que desea asignarse.</param>
    /// <returns>
    /// <see langword="true" /> si el tipo <paramref name="source" /> puede ser asignado
    /// a uno de los tipos especificados, <see langword="false" /> en caso contrario.
    /// </returns>
    public static bool IsAnyAssignable(this Type source, IEnumerable<Type> types)
    {
        return types.Any(source.IsAssignableFrom);
    }

    /// <summary>
    /// Comprueba si alguno de los tipos especificados es asignable a partir
    /// del tipo <paramref name="source" />.
    /// </summary>
    /// <param name="types">Lista de tipos a comprobar.</param>
    /// <param name="source">Tipo que desea asignarse.</param>
    /// <returns>
    /// <see langword="true" /> si el tipo <paramref name="source" /> puede ser asignado
    /// a uno de los tipos especificados, <see langword="false" /> en caso contrario.
    /// </returns>
    public static bool IsAnyAssignable(this Type source, params Type[] types)
    {
        return source.IsAnyAssignable(types.AsEnumerable());
    }

    /// <summary>
    /// Obtiene un valor que determina si el tipo es instanciable.
    /// </summary>
    /// <param name="type">Tipo a comprobar.</param>
    /// <returns>
    /// <see langword="true" /> si el tipo es instanciable por medio de
    /// un constructor sin parámetros, <see langword="false" /> en caso
    /// contrario.
    /// </returns>
    public static bool IsInstantiable(this Type type)
    {
        return IsInstantiable(type, Array.Empty<Type>());
    }

    /// <summary>
    /// Obtiene un valor que determina si el tipo es instanciable.
    /// </summary>
    /// <param name="type">Tipo a comprobar.</param>
    /// <param name="constructorArgs">
    /// Colección con los tipos de argumentos que el constructor a
    /// buscar debe contener.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si el tipo es instanciable por medio de
    /// un constructor con los parámetros del tipo especificado,
    /// <see langword="false" /> en caso contrario.
    /// </returns>
    public static bool IsInstantiable(this Type type, IEnumerable<Type>? constructorArgs)
    {
        if (constructorArgs is null) return !(type.IsAbstract || type.IsInterface) && type.GetConstructors().Any();
        return !(type.IsAbstract || type.IsInterface) && type.GetConstructor(constructorArgs.ToArray()) is not null;
    }

    /// <summary>
    /// Obtiene un valor que determina si el tipo es un tipo de valor
    /// no primitivo.
    /// </summary>
    /// <param name="type">
    /// Tipo a comprobar.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si el tipo es un tipo de valor no 
    /// primitivo, <see langword="false"/> en caso contrario.
    /// </returns>
    [Sugar]
    public static bool IsStruct(this Type type)
    {
        return type.IsValueType && !type.IsPrimitive;
    }

    /// <summary>
    /// Obtiene un valor que determina si el tipo es instanciable
    /// utilizando un contrustor que acepte los parámetros
    /// especificados.
    /// </summary>
    /// <param name="type">Tipo a comprobar.</param>
    /// <param name="constructorArgs">
    /// Colección con los tipos de argumentos que el constructor a
    /// buscar debe contener.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si el tipo es instanciable por medio de
    /// un constructor con los parámetros del tipo especificado,
    /// <see langword="false" /> en caso contrario.
    /// </returns>
    [DebuggerStepThrough]
    [Sugar]
    public static bool IsInstantiable(this Type type, params Type[] constructorArgs)
    {
        return IsInstantiable(type, constructorArgs.AsEnumerable());
    }

    /// <summary>
    /// Inicializa una nueva instancia del tipo en runtime especificado.
    /// </summary>
    /// <returns>La nueva instancia del tipo especificado.</returns>
    /// <param name="type">Tipo a instanciar.</param>
    [DebuggerStepThrough]
    [Sugar]
    public static object New(this Type type)
    {
        return type.New<object>(Array.Empty<object>());
    }

    /// <summary>
    /// Inicializa una nueva instancia del tipo dinámico especificado,
    /// devolviéndola como un <typeparamref name="T" />.
    /// </summary>
    /// <returns>La nueva instancia del tipo especificado.</returns>
    /// <param name="type">
    /// Tipo a instanciar. Debe ser, heredar o implementar
    /// el tipo especificado en <typeparamref name="T" />
    /// </param>
    /// <typeparam name="T">Tipo de instancia a devolver.</typeparam>
    [DebuggerStepThrough]
    [Sugar]
    public static T New<T>(this Type type)
    {
        return type.New<T>(Array.Empty<object>());
    }

    /// <summary>
    /// Inicializa una nueva instancia de un objeto con un constructor que
    /// acepte los argumentos provistos.
    /// </summary>
    /// <returns>La nueva instancia del tipo especificado.</returns>
    /// <param name="type">Tipo a instanciar.</param>
    /// <param name="parameters">
    /// Parámetros a pasar al constructor. Se buscará
    /// un constructor compatible para poder crear la instancia.
    /// </param>
    [DebuggerStepThrough]
    [Sugar]
    public static object New(this Type type, params object?[] parameters)
    {
        return type.New<object>(parameters);
    }

    /// <summary>
    /// Inicializa una nueva instancia de un objeto con un constructor que
    /// acepte los argumentos provistos.
    /// </summary>
    /// <typeparam name="T">Tipo de instancia a devolver.</typeparam>
    /// <param name="type">
    /// Tipo a instanciar. Debe ser, heredar o implementar
    /// el tipo especificado en <typeparamref name="T" />.
    /// </param>
    /// <param name="parameters">
    /// Parámetros a pasar al constructor. Se buscará
    /// un constructor compatible para poder crear la instancia.
    /// </param>
    /// <returns>Una nueva instancia del tipo especificado.</returns>
    /// <exception cref="TypeLoadException">
    /// Se produce si no es posible instanciar una clase del tipo
    /// solicitado.
    /// </exception>
    [DebuggerStepThrough]
    public static T New<T>(this Type type, params object?[] parameters)
    {
        return New<T>(type, true, parameters)!;
    }

    /// <summary>
    /// Inicializa una nueva instancia de un objeto con un constructor que
    /// acepte los argumentos provistos.
    /// </summary>
    /// <typeparam name="T">Tipo de instancia a devolver.</typeparam>
    /// <param name="type">
    /// Tipo a instanciar. Debe ser, heredar o implementar
    /// el tipo especificado en <typeparamref name="T" />.
    /// </param>
    /// <param name="throwOnFail">
    /// Si se establece en <see langword="true"/>, se producirá una
    /// excepción en caso que el tipo no pueda instanciarse con la
    /// información provista, o se devolverá <see langword="null"/> si
    /// se establece en <see langword="false"/>
    /// </param>
    /// <param name="parameters">
    /// Parámetros a pasar al constructor. Se buscará
    /// un constructor compatible para poder crear la instancia.
    /// </param>
    /// <returns>
    /// Una nueva instancia del tipo especificado, o
    /// <see langword="null"/> si ocurre un problema al instanciar el
    /// tipo y <paramref name="throwOnFail"/> es
    /// <see langword="false"/>.
    /// </returns>
    /// <exception cref="TypeLoadException">
    /// Se produce si no es posible instanciar una clase del tipo
    /// solicitado.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="type"/> es <see langword="null"/>
    /// y <paramref name="throwOnFail"/> es <see langword="true"/>.
    /// </exception>
    /// <exception cref="ClassNotInstantiableException">
    /// Se produce si el tipo <paramref name="type"/> no puede ser
    /// instanciado utilizando un constructor público que acepte los
    /// parámetros especificados en <paramref name="parameters"/> y
    /// <paramref name="throwOnFail"/> es <see langword="true"/>.
    /// </exception>
    [DebuggerStepThrough]
    public static T? New<T>(this Type type, bool throwOnFail, IEnumerable? parameters)
    {
        object?[]? p = parameters?.ToGeneric().ToArray() ?? Array.Empty<object?>();
        try
        {
            New_Contract(type, p);
            return (T)type.GetConstructor(p.ToTypes().ToArray())!.Invoke(p);
        }
        catch
        {
            if (throwOnFail) throw;
            return default;
        }
    }

    /// <summary>
    /// Intenta instanciar el tipo con los argumentos de constructor
    /// especificados, devolviéndolo como un objeto de tipo
    /// <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto a devolver.</typeparam>
    /// <param name="t">Tipo que de intentará instanciar.</param>
    /// <param name="instance">
    /// Parámetro de salida. Instancia que ha sido creada, o 
    /// <see langword="null"/> si no se ha podido crear una instancia del
    /// tipo especificado.
    /// </param>
    /// <param name="args">
    /// Argumentos a pasar al constructor. Puede omitirse o establecerse en
    /// <see langword="null"/> para constructores sin argumentos.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si fue posible instanciar el tipo y si el
    /// mismo se ha instanciado de forma correcta, <see langword="false"/>
    /// en caso contrario.
    /// </returns>
    public static bool TryInstance<T>(this Type t, [MaybeNullWhen(false)] out T instance, params object[]? args)
    {
        TryInstance_Contract(t, args);
        if (!t.IsAbstract && !t.IsInterface && typeof(T).IsAssignableFrom(t) && t.GetConstructor(args?.ToTypes().ToArray() ?? Type.EmptyTypes) is { } ctor)
        {
            try
            {
                instance = (T)ctor.Invoke(args);
                return true;
            }
            catch
            {
                instance = default;
                return false;
            }
        }
        instance = default!;
        return t.IsStruct();
    }

    /// <summary>
    /// Intenta instanciar el tipo con los argumentos de constructor
    /// especificados.
    /// </summary>
    /// <param name="t">Tipo que de intentará instanciar.</param>
    /// <param name="instance">
    /// Parámetro de salida. Instancia que ha sido creada, o 
    /// <see langword="null"/> si no se ha podido crear una instancia del
    /// tipo especificado.
    /// </param>
    /// <param name="args">
    /// Argumentos a pasar al constructor. Puede omitirse o establecerse en
    /// <see langword="null"/> para constructores sin argumentos.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si fue posible instanciar el tipo y si el
    /// mismo se ha instanciado de forma correcta, <see langword="false"/>
    /// en caso contrario.
    /// </returns>
    [Sugar]
    public static bool TryInstance(this Type t, [MaybeNullWhen(false)] out object instance, params object[]? args)
    {
        return TryInstance<object>(t, out instance, args);
    }

    /// <summary>
    /// Inicializa una nueva instancia de un objeto de forma asíncrona 
    /// con un constructor que acepte los argumentos provistos.
    /// </summary>
    /// <param name="type">Tipo a instanciar.</param>
    /// <param name="throwOnFail">
    /// Si se establece en <see langword="true"/>, se producirá una
    /// excepción en caso que el tipo no pueda instanciarse con la
    /// información provista, o se devolverá <see langword="null"/> si
    /// se establece en <see langword="false"/>
    /// </param>
    /// <param name="async">
    /// Si se establece en <see langword="true"/>, el constructor
    /// también se ejecutará asíncronamente. Esto puede ser
    /// problemático si la ejecución normal del programa depende de qué
    /// hilo es el propietario del objeto, por ejemplo al instanciar
    /// elementos de UI.
    /// </param>
    /// <param name="parameters">
    /// Parámetros a pasar al constructor. Se buscará
    /// un constructor compatible para poder crear la instancia.
    /// </param>
    /// <returns>
    /// Una nueva instancia del tipo especificado, o
    /// <see langword="null"/> si ocurre un problema al instanciar el
    /// tipo y <paramref name="throwOnFail"/> es
    /// <see langword="false"/>.
    /// </returns>
    /// <exception cref="TypeLoadException">
    /// Se produce si no es posible instanciar una clase del tipo
    /// solicitado y <paramref name="throwOnFail"/> se establece en
    /// <see langword="true"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="type"/> es <see langword="null"/>
    /// y <paramref name="throwOnFail"/> se establece en
    /// <see langword="true"/>.
    /// </exception>
    /// <exception cref="ClassNotInstantiableException">
    /// Se produce si el tipo <paramref name="type"/> no puede ser
    /// instanciado utilizando un constructor público que acepte los
    /// parámetros especificados en <paramref name="parameters"/> y
    /// <paramref name="throwOnFail"/> se establece en
    /// <see langword="true"/>.
    /// </exception>
    [DebuggerStepThrough]
    public static async Task<object?> NewAsync(this Type type, bool throwOnFail, bool @async, IEnumerable parameters)
    {
        object?[]? p = parameters?.ToGeneric().ToArray() ?? Array.Empty<object?>();
        New_Contract(type, p);
        try
        {
            ConstructorInfo? ctor = type.GetConstructor(p.ToTypes().ToArray());
            return (@async ? await Task.Run(() => ctor?.Invoke(p)) : ctor?.Invoke(p)) ?? Errors.ClassNotInstantiable();
        }
        catch (Exception e)
        {
            return throwOnFail ? throw Errors.CouldntInstanceClass(type, e) : null;
        }
    }

    /// <summary>
    /// Inicializa una nueva instancia de un objeto de forma asíncrona 
    /// con un constructor que acepte los argumentos provistos.
    /// </summary>
    /// <typeparam name="T">Tipo de instancia a devolver.</typeparam>
    /// <param name="type">
    /// Tipo a instanciar. Debe ser, heredar o implementar
    /// el tipo especificado en <typeparamref name="T" />.
    /// </param>
    /// <param name="throwOnFail">
    /// Si se establece en <see langword="true"/>, se producirá una
    /// excepción en caso que el tipo no pueda instanciarse con la
    /// información provista, o se devolverá <see langword="null"/> si
    /// se establece en <see langword="false"/>
    /// </param>
    /// <param name="async">
    /// Si se establece en <see langword="true"/>, el constructor
    /// también se ejecutará asíncronamente. Esto puede ser
    /// problemático si la ejecución normal del programa depende de qué
    /// hilo es el propietario del objeto, por ejemplo al instanciar
    /// elementos de UI.
    /// </param>
    /// <param name="parameters">
    /// Parámetros a pasar al constructor. Se buscará
    /// un constructor compatible para poder crear la instancia.
    /// </param>
    /// <returns>
    /// Una nueva instancia del tipo especificado, o
    /// <see langword="null"/> si ocurre un problema al instanciar el
    /// tipo y <paramref name="throwOnFail"/> es
    /// <see langword="false"/>.
    /// </returns>
    /// <exception cref="TypeLoadException">
    /// Se produce si no es posible instanciar una clase del tipo
    /// solicitado y <paramref name="throwOnFail"/> se establece en
    /// <see langword="true"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="type"/> es <see langword="null"/>
    /// y <paramref name="throwOnFail"/> se establece en
    /// <see langword="true"/>.
    /// </exception>
    /// <exception cref="ClassNotInstantiableException">
    /// Se produce si el tipo <paramref name="type"/> no puede ser
    /// instanciado utilizando un constructor público que acepte los
    /// parámetros especificados en <paramref name="parameters"/> y
    /// <paramref name="throwOnFail"/> se establece en
    /// <see langword="true"/>.
    /// </exception>
    [DebuggerStepThrough]
    public static async Task<T> NewAsync<T>(this Type type, bool throwOnFail, bool @async, IEnumerable parameters)
    {
        object? r = await NewAsync(type, throwOnFail, async, parameters);
        if (r is T v) return v;
        return throwOnFail ? throw new InvalidCastException() : (T)default!;
    }

    /// <summary>
    /// Se asegura de devolver un tipo no nulable para las estructuras.
    /// </summary>
    /// <param name="t">Tipo a devolver</param>
    /// <returns>
    /// El tipo subyacente de un <see cref="Nullable{T}"/>, o
    /// <paramref name="t"/> si el tipo no es nulable.
    /// </returns>
    [DebuggerStepThrough]
    public static Type NotNullable(this Type t)
    {
        NotNullable_Contract(t);
        return Nullable.GetUnderlyingType(t) ?? t;
    }

    /// <summary>
    /// Se asegura de devolver un tipo definido en tiempo de
    /// compilación.
    /// </summary>
    /// <param name="t">
    /// Tipo a comprobar.
    /// </param>
    /// <returns>
    /// <paramref name="t"/>, si se trata de un tipo definido en tiempo
    /// de compilación, o un tipo base que lo sea. Se devolverá
    /// <see langword="null"/> si no hay un tipo base definido, como en
    /// las interfaces.
    /// </returns>
    public static Type? ResolveToDefinedType(this Type t)
    {
        return (t.Assembly?.IsDynamic ?? false) ? ResolveToDefinedType(t.BaseType!) : t;
    }

    /// <summary>
    /// Determina si el tipo hace referencia a un tipo de colección.
    /// </summary>
    /// <param name="type">Tipo a comprobar.</param>
    /// <returns>
    /// <see langword="true"/> si el tipo es un tipo de colección,
    /// <see langword="false"/> en caso contrario.
    /// </returns>
    [Sugar]
    public static bool IsCollectionType(this Type type) => type.Implements<IEnumerable>();

    /// <summary>
    /// Obtiene el tipo de elementos contenidos por el tipo de
    /// colección.
    /// </summary>
    /// <param name="collectionType">
    /// Tipo de colección del cual obtener el tipo de elementos.
    /// </param>
    /// <returns>
    /// El tipo de elementos contenidos por la colección.
    /// </returns>
    /// <remarks>
    /// Por convención, se asume que el tipo de elementos de una colección
    /// está basado en los argumentos de tipo genéricos utilizados en la
    /// definición del tipo, utilizando una concención común de colocar el
    /// tipo de elementos al final de los argumentos de tipo.
    /// </remarks>
    public static Type GetCollectionType(this Type collectionType)
    {
        GetCollectionType_Contract(collectionType);
        if (collectionType.IsArray) return collectionType.GetElementType()!;
        return collectionType.GenericTypeArguments.Count() switch
        {
            0 => typeof(object),
            1 => collectionType.GenericTypeArguments.Single(),
            { } => collectionType.GenericTypeArguments.Last(),
        };
    }

    /// <summary>
    /// Resuelve un tipo de colección al tipo de sus elementos.
    /// </summary>
    /// <param name="type">
    /// Tipo a comprobar.
    /// </param>
    /// <returns>
    /// El tipo de elementos de la colección del tipo
    /// <paramref name="type"/>, o <paramref name="type"/> si el mismo
    /// no es un tipo de colección.
    /// </returns>
    [Sugar]
    public static Type ResolveCollectionType(this Type type) => type.IsCollectionType() ? type.GetCollectionType() : type;

    /// <summary>
    /// Enumera a los tipos descendientes del tipo especificado.
    /// </summary>
    /// <param name="type">
    /// Tipo del cual buscar descendientes.
    /// </param>
    /// <returns>
    /// Una secuencia con todos los tipos descendientes del tipo
    /// especificado.
    /// </returns>
    public static IEnumerable<Type> Derivates(this Type type)
    {
        return Derivates(type, AppDomain.CurrentDomain);
    }

    /// <summary>
    /// Enumera a los tipos descendientes del tipo dentro del dominio
    /// especificado.
    /// </summary>
    /// <param name="type">
    /// Tipo del cual buscar descendientes.
    /// </param>
    /// <param name="domain">
    /// Dominio sobre el cual realizar la búsqueda.
    /// </param>
    /// <returns>
    /// Una secuencia con todos los tipos descendientes del tipo
    /// especificado.
    /// </returns>
    public static IEnumerable<Type> Derivates(this Type type, AppDomain domain)
    {
        Derivates_Contract(domain);
        return Derivates(type, domain.GetAssemblies());
    }

    /// <summary>
    /// Enumera a los tipos descendientes del tipo dentro de los
    /// ensamblados especificados.
    /// </summary>
    /// <param name="type">
    /// Tipo del cual buscar descendientes.
    /// </param>
    /// <param name="assemblies">
    /// Secuencia que contiene un listado de los ensamblados en los
    /// cuales realizar la búsqueda.
    /// </param>
    /// <returns>
    /// Una secuencia con todos los tipos descendientes del tipo
    /// especificado.
    /// </returns>
    public static IEnumerable<Type> Derivates(this Type type, params Assembly[] assemblies)
    {
        return Derivates(type, assemblies.AsEnumerable());
    }

    /// <summary>
    /// Enumera a los tipos descendientes del tipo dentro de los
    /// ensamblados especificados.
    /// </summary>
    /// <param name="type">
    /// Tipo del cual buscar descendientes.
    /// </param>
    /// <param name="assemblies">
    /// Secuencia que contiene un listado de los ensamblados en los
    /// cuales realizar la búsqueda.
    /// </param>
    /// <returns>
    /// Una secuencia con todos los tipos descendientes del tipo
    /// especificado.
    /// </returns>
    public static IEnumerable<Type> Derivates(this Type type, IEnumerable<Assembly> assemblies)
    {
        Derivates_Contract(assemblies);
        List<Type>? retval = new();
        foreach (Assembly? j in assemblies)
        {
            IEnumerable<Type> types;
            try
            {
                types = j.GetTypes();
            }
            catch (ReflectionTypeLoadException rex)
            {
                types = rex.Types.NotNull();
            }
            retval.AddRange(Derivates(type, types));
        }
        return retval;
    }

    /// <summary>
    /// Enumera a los tipos descendientes del tipo dentro de la colección
    /// de tipos especificada.
    /// </summary>
    /// <param name="type">
    /// Tipo del cual buscar descendientes.
    /// </param>
    /// <param name="types">
    /// Secuencia que contiene un listado de los tipos en los cuales se
    /// debe realizar la búsqueda.
    /// </param>
    /// <returns>
    /// Una secuencia con todos los tipos descendientes del tipo
    /// especificado.
    /// </returns>
    public static IEnumerable<Type> Derivates(this Type type, IEnumerable<Type> types)
    {
        Derivates_Contract(type, types);
        foreach (Type? k in types)
        {
            if (type.IsAssignableFrom(k ?? throw new NullItemException())) yield return k;
        }
    }

    /// <summary>
    /// Obtiene el nombre del tipo tal cual se declaría en C#.
    /// </summary>
    /// <param name="type">
    /// Tipo del cual obtener la cadena de declaración.
    /// </param>
    /// <returns>
    /// Una cadena que representa la declaración del tipo utilizando 
    /// sintaxis de C#.
    /// </returns>
    public static string CSharpName(this Type type)
    {
        return new StringBuilder()
            .Append(CleanFullName(type))
            .Append(string.Join(", ", type.GenericTypeArguments.Select(CSharpName)).OrNull("<{0}>"))
            .ToString();
    }

    /// <summary>
    /// Obtiene el nombre completo del tipo, sin incluir la anotación
    /// de cantidad de argumentos genéricos.
    /// </summary>
    /// <param name="type">
    /// Tipo del cual obtener el nombre limpio.
    /// </param>
    /// <returns>
    /// El nombre completo de tipo, incluyendo su espacio de nombres,
    /// pero no su ensamblado ni su anotación de cantidad de argumentos
    /// genéricos en caso de poseer una.
    /// </returns>
    public static string CleanFullName(this Type type)
    {
        if (type.FullName is null) return type.Name;
        return type.FullName.Contains('`') ? type.FullName[..type.FullName.IndexOf('`')] : type.FullName;
    }

    /// <summary>
    /// convierte los valores de un tipo de enumeración en una colección de
    /// <see cref="NamedObject{T}"/>.
    /// </summary>
    /// <param name="type">Tipo de enumeración a convertir.</param>
    /// <returns>
    /// Una enumeración de todos los <see cref="NamedObject{T}"/> creados a
    /// partir de los valores del tipo de enumeración especificado.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="type"/> es <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidTypeException">
    /// Se produce si <paramref name="type"/> no es un tipo de enumeración.
    /// </exception>
    public static IEnumerable<NamedObject<Enum>> ToNamedEnum(this Type type)
    {
        ToNamedEnum_Contract(type);
        return type.GetEnumValues().Cast<Enum>().Select(j => new NamedObject<Enum>(j, j.NameOf()));
    }

    /// <summary>
    /// Determina si un miembro o su ensamblado contenedor posee un atributo definido.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="type">
    /// Miembro del cual se extraerá el atributo.
    /// </param>
    /// <param name="attribute">
    /// Parámetro de salida. Si un atributo de tipo
    /// <typeparamref name="T" /> ha sido encontrado, el mismo es devuelto.
    /// Se devolverá <see langword="null" /> si el miembro no posee el atributo
    /// especificado.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
    /// en caso contrario.
    /// </returns>
    public static bool HasAttrAlt<T>(this Type type, [MaybeNullWhen(false)] out T attribute) where T : Attribute
    {
        attribute = (Attribute.GetCustomAttributes(type, typeof(T)).FirstOrDefault()
                     ?? Attribute.GetCustomAttributes(type.Assembly, typeof(T)).FirstOrDefault()) as T;
        return attribute is not null;
    }

    /// <summary>
    /// Determina si un miembro o su ensamblado contenedor posee un atributo definido.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="type">
    /// Miembro del cual se extraerá el atributo.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
    /// en caso contrario.
    /// </returns>
    public static bool HasAttrAlt<T>(this Type type) where T : Attribute
    {
        return HasAttrAlt<T>(type, out _);
    }

    /// <summary>
    /// Devuelve el atributo asociado a la declaración del tipo
    /// especificado, o en su defecto, del ensamblado que lo contiene.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="type">Objeto del cual se extraerá el atributo.</param>
    /// <returns>
    /// Un atributo del tipo <typeparamref name="T" /> con los datos
    /// asociados en la declaración del tipo; o <see langword="null" /> en caso de no
    /// encontrarse el atributo especificado.
    /// </returns>
    public static T? GetAttrAlt<T>(this Type type) where T : Attribute
    {
        return (Attribute.GetCustomAttribute(type, typeof(T))
                ?? Attribute.GetCustomAttribute(type.Assembly, typeof(T))) as T;
    }

    /// <summary>
    /// Determina si el tipo <paramref name="t" /> es de un tipo numérico
    /// </summary>
    /// <param name="t">Tipo a comprobar</param>
    /// <returns>
    /// <see langword="true" /> si <paramref name="t" /> es un tipo numérico; de
    /// lo contrario, <see langword="false" />.
    /// </returns>
    public static bool IsNumericType(this Type? t)
    {
        return new[]
        {
            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(decimal),
            typeof(float),
            typeof(double)
        }.Contains(t);
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
        return ReflectionHelpers.FieldsOf<T>(type.GetFields(BindingFlags.Static | BindingFlags.Public), null);
    }
}
