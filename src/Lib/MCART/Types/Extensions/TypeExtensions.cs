/*
TypeExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Resources;
using static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes;
using static TheXDS.MCART.Misc.AttributeErrorMessages;

namespace TheXDS.MCART.Types.Extensions;

public static partial class TypeExtensions
{
    /// <summary>
    /// Checks if all types are assignable from the <paramref name="source"/> type.
    /// </summary>
    /// <param name="types">List of types to check.</param>
    /// <param name="source">Type that is being assigned.</param>
    /// <returns>
    /// <see langword="true" /> if all types are assignable from
    /// <paramref name="source" />, <see langword="false" /> otherwise.
    /// </returns>
    public static bool AreAllAssignable(this Type source, IEnumerable<Type> types)
    {
        return types.All(p => p.IsAssignableFrom(source));
    }

    /// <summary>
    /// Checks if all types are assignable from the <paramref name="source"/> type.
    /// </summary>
    /// <param name="types">List of types to check.</param>
    /// <param name="source">Type that is being assigned.</param>
    /// <returns>
    /// <see langword="true" /> if all types are assignable from
    /// <paramref name="source" />, <see langword="false" /> otherwise.
    /// </returns>
    public static bool AreAllAssignable(this Type source, params Type[] types)
    {
        return source.AreAllAssignable(types.AsEnumerable());
    }

    /// <summary>
    /// Enumerates the types that are assignable from <paramref name="source"/>.
    /// </summary>
    /// <param name="types">List of types to check.</param>
    /// <param name="source">Type that is being assigned.</param>
    /// <returns>
    /// An enumerator with the types that can be assigned from
    /// <paramref name="source" />.
    /// </returns>
    public static IEnumerable<Type> Assignables(this Type source, IEnumerable<Type> types)
    {
        return types.Where(p => p.IsAssignableFrom(source));
    }

    /// <summary>
    /// Enumerates the types that are assignable from <paramref name="source"/>.
    /// </summary>
    /// <param name="types">List of types to check.</param>
    /// <param name="source">Type that is being assigned.</param>
    /// <returns>
    /// An enumerator with the types that can be assigned from
    /// <paramref name="source" />.
    /// </returns>
    public static IEnumerable<Type> Assignables(this Type source, params Type[] types)
    {
        return source.Assignables(types.AsEnumerable());
    }

    /// <summary>
    /// Gets the full name of the type without including the generic argument count annotation.
    /// </summary>
    /// <param name="type">
    /// Type from which to obtain the clean name.
    /// </param>
    /// <returns>
    /// The full type name, including its namespace,
    /// but not its assembly or its generic argument count annotation if present.
    /// </returns>
    public static string CleanFullName(this Type type)
    {
        var name = type.FullName ?? type.Name;
        if (name.Contains('`')) name = name[..name.IndexOf('`')];
        name = name.TrimEnd("&*".ToCharArray());
        if (name.EndsWith("[]")) name = name[..^2];
        return name;
    }

    /// <summary>
    /// Gets the type name as it would be declared in C#.
    /// </summary>
    /// <param name="type">
    /// Type from which to obtain the declaration string.
    /// </param>
    /// <returns>
    /// A string representing the type's declaration using C# syntax.
    /// </returns>
    /// <remarks>
    /// The types will use their full name, including their namespace. Types
    /// with reserved keyword aliases (like <see langword="int"/> or
    /// <see langword="string"/>) will use their type name instead.
    /// </remarks>
    public static string CSharpName(this Type type)
    {
        return new StringBuilder()
            .Append(CleanFullName(type))
            .Append(string.Join(", ", type.GenericTypeArguments.Select(CSharpName)).OrNull("<{0}>"))
            .ToString();
    }

    /// <summary>
    /// Programmatic equivalent of <see langword="default" />, gets the default value of a type.
    /// </summary>
    /// <param name="t">
    /// <see cref="Type" /> from which to obtain the default value.
    /// </param>
    /// <returns>
    /// A new instance of the type if it is a
    /// <see langword="struct" />, or <see langword="null" /> if it's a
    /// <see langword="class" />.
    /// </returns>
    public static object? Default([DynamicallyAccessedMembers(PublicParameterlessConstructor)] this Type t)
    {
        return t.IsValueType ? Activator.CreateInstance(t) : null;
    }

    /// <summary>
    /// Enumerates the descendant types of a type within the specified domain.
    /// </summary>
    /// <param name="type">
    /// Type for which to find descendants.
    /// </param>
    /// <param name="domain">
    /// Domain on which to perform the search.
    /// </param>
    /// <returns>
    /// A sequence with all descendant types of the specified type.
    /// </returns>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static IEnumerable<Type> FindDerivedTypes(this Type type, AppDomain domain)
    {
        Derivates_Contract(domain);
        return FindDerivedTypes(type, domain.GetAssemblies());
    }

    /// <summary>
    /// Enumerates the descendant types of a type within the specified assemblies.
    /// </summary>
    /// <param name="type">
    /// Type for which to find descendants.
    /// </param>
    /// <param name="assemblies">
    /// Sequence containing a list of assemblies in which to perform the search.
    /// </param>
    /// <returns>
    /// A sequence with all descendant types of the specified type.
    /// </returns>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static IEnumerable<Type> Derivates(this Type type, IEnumerable<Assembly> assemblies)
    {
        Derivates_Contract(assemblies);
        List<Type> returnValue = [];
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
            returnValue.AddRange(Derivates(type, types));
        }
        return returnValue;
    }

    /// <summary>
    /// Enumerates the descendant types of a type within the specified collection of types.
    /// </summary>
    /// <param name="type">
    /// Type for which to find descendants.
    /// </param>
    /// <param name="types">
    /// Sequence containing a list of types in which to perform the search.
    /// </param>
    /// <returns>
    /// A sequence with all descendant types of the specified type.
    /// </returns>
    public static IEnumerable<Type> Derivates(this Type type, IEnumerable<Type> types)
    {
        Derivates_Contract(type, types);
        foreach (Type k in types)
        {
            if (type.IsAssignableFrom(k ?? throw new NullItemException())) yield return k;
        }
    }

    /// <summary>
    /// Enumerates the descendant types of a specified type within given assemblies.
    /// </summary>
    /// <param name="type">
    /// Type for which to find descendants.
    /// </param>
    /// <param name="assemblies">
    /// Sequence containing a list of assemblies in which to perform the search.
    /// </param>
    /// <returns>
    /// A sequence with all descendant types of the specified type.
    /// </returns>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static IEnumerable<Type> FindDerivedTypes(this Type type, params Assembly[] assemblies)
    {
        return Derivates(type, assemblies.AsEnumerable());
    }

    /// <summary>
    /// Enumerates the descendant types of a specified type.
    /// </summary>
    /// <param name="type">
    /// Type for which to find descendants.
    /// </param>
    /// <returns>
    /// A sequence with all descendant types of the specified type.
    /// </returns>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static IEnumerable<Type> GetDerivedTypes(this Type type)
    {
        return FindDerivedTypes(type, AppDomain.CurrentDomain);
    }

    /// <summary>
    /// Returns the attribute associated with the declaration of a specified type,
    /// or otherwise, from the assembly that contains it.
    /// </summary>
    /// <typeparam name="T">
    /// Attribute type to retrieve. Must inherit <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="type">Object from which to extract the attribute.</param>
    /// <returns>
    /// An attribute of type <typeparamref name="T" /> with associated data in the
    /// type's declaration; or <see langword="null" /> if the specified attribute is not found.
    /// </returns>
    public static T? GetAttrAlt<T>(this Type type) where T : Attribute
    {
        return (Attribute.GetCustomAttribute(type, typeof(T))
                ?? Attribute.GetCustomAttribute(type.Assembly, typeof(T))) as T;
    }

    /// <summary>
    /// Gets the type of elements contained by a collection type.
    /// </summary>
    /// <param name="collectionType">
    /// Collection type from which to obtain the element type.
    /// </param>
    /// <returns>
    /// The type of elements contained by the collection.
    /// </returns>
    /// <remarks>
    /// By convention, it is assumed that the element type of a collection
    /// is based on the generic type arguments used in its definition,
    /// with a common convention of placing the element type at the end of the type arguments.
    /// </remarks>
#if EnforceContracts
    [RequiresDynamicCode(MethodCallsDynamicCode)]
    public static Type GetCollectionType([DynamicallyAccessedMembers(Interfaces)] this Type collectionType)
    {
        GetCollectionType_Contract(collectionType);
#else
    public static Type GetCollectionType(this Type collectionType)
    {
#endif
        if (collectionType.IsArray) return collectionType.GetElementType()!;
        return collectionType.GenericTypeArguments.Count() switch
        {
            0 => typeof(object),
            1 => collectionType.GenericTypeArguments.Single(),
            { } => collectionType.GenericTypeArguments.Last(),
        };
    }

    /// <summary>
    /// Gets a collection of methods defined directly in the specified type.
    /// </summary>
    /// <param name="type">Type for which to list the methods.</param>
    /// <param name="flags">
    /// Flags to use for filtering the methods to retrieve.
    /// </param>
    /// <returns>
    /// An enumeration of methods defined directly in the type.
    /// </returns>
    public static IEnumerable<MethodInfo> GetDefinedMethods([DynamicallyAccessedMembers(PublicMethods | NonPublicMethods)] this Type type, BindingFlags flags)
    {
        return type.GetMethods(flags).Where(p => p.DeclaringType == type && !p.IsSpecialName);
    }

    /// <summary>
    /// Gets a collection of publicly defined instance methods directly in the type.
    /// </summary>
    /// <param name="type">Type for which to list the methods.</param>
    /// <returns>
    /// An enumeration of methods defined directly in the type.
    /// </returns>
    public static IEnumerable<MethodInfo> GetDefinedMethods([DynamicallyAccessedMembers(PublicMethods | NonPublicMethods)] this Type type)
    {
        GetDefinedMethods_Contract(type);
        return GetDefinedMethods(type, BindingFlags.Public | BindingFlags.Instance);
    }

    /// <summary>
    /// Enumerates all public instance properties of the type.
    /// </summary>
    /// <param name="type">
    /// Type for which to enumerate the public instance properties.
    /// </param>
    /// <returns>
    /// An enumeration with all public instance properties of the type.
    /// </returns>
    [Sugar]
    public static IEnumerable<PropertyInfo> GetPublicProperties([DynamicallyAccessedMembers(PublicProperties)] this Type type)
    {
        GetPublicProperties_Contract(type);
        return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
    }

    /// <summary>
    /// Determines if a member or its containing assembly has a defined attribute.
    /// </summary>
    /// <typeparam name="T">
    /// Attribute type to retrieve. Must inherit <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="type">
    /// Member from which to extract the attribute.
    /// </param>
    /// <param name="attribute">
    /// Output parameter. If an attribute of type
    /// <typeparamref name="T" /> has been found, it is returned.
    /// It will return <see langword="null" /> if the member does not have the specified attribute.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the member has the attribute, <see langword="false" />
    /// otherwise.
    /// </returns>
    public static bool HasAttrAlt<T>(this Type type, [MaybeNullWhen(false)] out T attribute) where T : Attribute
    {
        attribute = (Attribute.GetCustomAttributes(type, typeof(T)).FirstOrDefault()
                     ?? Attribute.GetCustomAttributes(type.Assembly, typeof(T)).FirstOrDefault()) as T;
        return attribute is not null;
    }

    /// <summary>
    /// Determines if a member or its containing assembly has a defined attribute.
    /// </summary>
    /// <typeparam name="T">
    /// Attribute type to retrieve. Must inherit <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="type">
    /// Member from which to extract the attribute.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the member has the attribute, <see langword="false" />
    /// otherwise.
    /// </returns>
    public static bool HasAttrAlt<T>(this Type type) where T : Attribute
    {
        return HasAttrAlt<T>(type, out _);
    }

    /// <summary>
    /// Determines if a type implements all specified types.
    /// </summary>
    /// <param name="type">Type to check</param>
    /// <param name="baseTypes">
    /// Collection of types to verify that <paramref name="type"/> inherits from.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="type" /> implements
    /// all specified types, <see langword="false" /> otherwise.
    /// </returns>
    [RequiresDynamicCode(MethodCreatesNewTypes)]
    [RequiresUnreferencedCode(MethodCreatesNewTypes)]
    public static bool Implements(this Type type, IEnumerable<Type> baseTypes)
    {
        return baseTypes.Select(type.Implements).All(p => p);
    }

    /// <summary>
    /// Determines if a type implements <paramref name="baseType"/> with the specified generic type arguments.
    /// </summary>
    /// <param name="type">Type to check</param>
    /// <param name="baseType">Base type inheritance to verify.</param>
    /// <param name="typeArgs">Generic type arguments to use for creating the generic type to check.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="type"/> implements <paramref name="baseType"/>,
    /// <see langword="false"/> otherwise.
    /// </returns>
    [RequiresDynamicCode(MethodCreatesNewTypes)]
    [RequiresUnreferencedCode(MethodCreatesNewTypes)]
    public static bool Implements(this Type type, Type baseType, params Type[] typeArgs)
    {
        if (!baseType.ContainsGenericParameters) return baseType.IsAssignableFrom(type);
        Type gt = baseType.MakeGenericType(typeArgs);
        return !gt.ContainsGenericParameters && gt.IsAssignableFrom(type);
    }

    /// <summary>
    /// Determines if a type implements <paramref name="baseType"/>.
    /// </summary>
    /// <param name="type">Type to check</param>
    /// <param name="baseType">Base type inheritance to verify.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="type" /> implements <paramref name="baseType" />,
    /// <see langword="false" /> otherwise.
    /// </returns>
    [RequiresDynamicCode(MethodCreatesNewTypes)]
    [RequiresUnreferencedCode(MethodCreatesNewTypes)]
    public static bool Implements([DynamicallyAccessedMembers(Interfaces)] this Type type, Type baseType)
    {
        if (!baseType.ContainsGenericParameters) return baseType.IsAssignableFrom(type);
        if (baseType.GenericTypeArguments.Length == 0)
            return (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == baseType) || type.GetInterfaces().Any(([DynamicallyAccessedMembers(Interfaces)] p) => p.Implements(baseType));
        Type gt = baseType.MakeGenericType(type);
        return !gt.ContainsGenericParameters && gt.IsAssignableFrom(type);
    }

    /// <summary>
    /// Determines if a type implements <typeparamref name="T"/>.
    /// </summary>
    /// <param name="type">Type to check</param>
    /// <typeparam name="T">Base type inheritance to verify.</typeparam>
    /// <returns>
    /// <see langword="true" /> if <paramref name="type" /> implements <typeparamref name="T" />,
    /// <see langword="false" /> otherwise.
    /// </returns>
    public static bool Implements<T>(this Type type)
    {
        return type.IsAssignableTo(typeof(T));
    }

    /// <summary>
    /// Checks if a type implements an operator specified by the expression.
    /// </summary>
    /// <param name="type">Type to check</param>
    /// <param name="operator">Operator to search in the type.</param>
    /// <returns>
    /// <see langword="true"/> if the operator exists in the type,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool ImplementsOperator([DynamicallyAccessedMembers(PublicParameterlessConstructor)] this Type type, Func<Expression, Expression, BinaryExpression> @operator)
    {
        ConstantExpression c = Expression.Constant((Nullable.GetUnderlyingType(type) is { } t ? t : type).Default(), type);
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
    /// Checks if any of the specified types are assignable from the type <paramref name="source"/>.
    /// </summary>
    /// <param name="types">List of types to check.</param>
    /// <param name="source">Type that is desired to be assigned.</param>
    /// <returns>
    /// <see langword="true" /> if the type <paramref name="source" /> can be assigned
    /// to one of the specified types, <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsAnyAssignable(this Type source, IEnumerable<Type> types)
    {
        return types.Any(source.IsAssignableFrom);
    }

    /// <summary>
    /// Checks if any of the specified types are assignable from the type <paramref name="source" />.
    /// </summary>
    /// <param name="types">List of types to check.</param>
    /// <param name="source">Type that is desired to be assigned.</param>
    /// <returns>
    /// <see langword="true" /> if the type <paramref name="source" /> can be assigned
    /// to one of the specified types, <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsAnyAssignable(this Type source, params Type[] types)
    {
        return source.IsAnyAssignable(types.AsEnumerable());
    }

    /// <summary>
    /// Determines if the type refers to a collection type.
    /// </summary>
    /// <param name="type">Type to check.</param>
    /// <returns>
    /// <see langword="true" /> if the type is a collection type,
    /// <see langword="false" /> otherwise.
    /// </returns>
    [Sugar]
    [RequiresDynamicCode(MethodCreatesNewTypes)]
    public static bool IsCollectionType([DynamicallyAccessedMembers(Interfaces)] this Type type) => type.Implements<IEnumerable>();

    /// <summary>
    /// Gets a value that determines if the type is instantiable.
    /// </summary>
    /// <param name="type">Type to check.</param>
    /// <param name="constructorArgs">
    /// Collection with the argument types that the constructor to
    /// search for must contain.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the type is instantiable by means of
    /// a constructor with parameters of the specified type,
    /// <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsInstantiable([DynamicallyAccessedMembers(PublicConstructors)] this Type type, IEnumerable<Type>? constructorArgs)
    {
        return !(type.IsAbstract || type.IsInterface) && type.GetConstructor(constructorArgs?.ToArray() ?? Type.EmptyTypes) is not null;
    }

    /// <summary>
    /// Gets a value that determines if the type is instantiable
    /// using a constructor that accepts the specified parameters.
    /// </summary>
    /// <param name="type">Type to check.</param>
    /// <param name="constructorArgs">
    /// Collection with the argument types that the constructor to
    /// search for must contain.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the type is instantiable by means of
    /// a constructor with parameters of the specified type,
    /// <see langword="false" /> otherwise.
    /// </returns>
    [DebuggerStepThrough]
    [Sugar]
    public static bool IsInstantiable([DynamicallyAccessedMembers(PublicConstructors)] this Type type, params Type[]? constructorArgs)
    {
        return IsInstantiable(type, constructorArgs?.AsEnumerable());
    }

    /// <summary>
    /// Gets a value that determines if the type is instantiable.
    /// </summary>
    /// <param name="type">Type to check.</param>
    /// <returns>
    /// <see langword="true" /> if the type is instantiable by means of
    /// a parameterless constructor, <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsInstantiable([DynamicallyAccessedMembers(PublicConstructors)] this Type type)
    {
        return IsInstantiable(type, []);
    }

    /// <summary>
    /// Determines if the type <paramref name="t" /> is a numeric type
    /// </summary>
    /// <param name="t">Type to check</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="t" /> is a numeric type; otherwise, <see langword="false" />.
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
    /// Gets a value that determines if the type is a non-primitive value type.
    /// </summary>
    /// <param name="type">
    /// Type to check.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the type is a non-primitive value type,
    /// <see langword="false"/> otherwise.
    /// </returns>
    [Sugar]
    public static bool IsStruct(this Type type)
    {
        return type.IsValueType && !type.IsPrimitive;
    }

    /// <summary>
    /// Initializes a new instance of an object with a constructor that
    /// accepts the provided arguments.
    /// </summary>
    /// <returns>The new instance of the specified type.</returns>
    /// <param name="type">Type to instantiate.</param>
    /// <param name="parameters">
    /// Parameters to pass to the constructor. A compatible
    /// constructor will be searched for to create the instance.
    /// </param>
    [DebuggerStepThrough]
    [Sugar]
    public static object New([DynamicallyAccessedMembers(PublicConstructors)] this Type type, params object?[] parameters)
    {
        return type.New<object>(parameters);
    }

    /// <summary>
    /// Initializes a new instance of the runtime specified type.
    /// </summary>
    /// <returns>The new instance of the specified type.</returns>
    /// <param name="type">Type to instantiate.</param>
    [DebuggerStepThrough]
    [Sugar]
    public static object New([DynamicallyAccessedMembers(PublicConstructors)] this Type type)
    {
        return type.New<object>([]);
    }

    /// <summary>
    /// Initializes a new instance of an object with a constructor that
    /// accepts the provided arguments.
    /// </summary>
    /// <typeparam name="T">Type of instance to return.</typeparam>
    /// <param name="type">
    /// Type to instantiate. Must be, inherit from or implement
    /// the type specified in <typeparamref name="T"/>.
    /// </param>
    /// <param name="throwOnFail">
    /// If set to <see langword="true"/>, an exception will be thrown
    /// if the type cannot be instantiated with the provided information,
    /// otherwise returns <see langword="null"/> if set to <see langword="false"/>.
    /// </param>
    /// <param name="parameters">
    /// Parameters to pass to the constructor. A compatible
    /// constructor will be searched for to create the instance.
    /// </param>
    /// <returns>
    /// A new instance of the specified type, or
    /// <see langword="null"/> if a problem occurs while instantiating the
    /// type and <paramref name="throwOnFail"/> is
    /// <see langword="false"/>.
    /// </returns>
    /// <exception cref="TypeLoadException">
    /// Occurs if it's not possible to instantiate a class of the requested type.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Occurs if <paramref name="type"/> is <see langword="null"/>
    /// and <paramref name="throwOnFail"/> is <see langword="true"/>.
    /// </exception>
    /// <exception cref="ClassNotInstantiableException">
    /// Occurs if the type <paramref name="type"/> cannot be
    /// instantiated using a public constructor that accepts the parameters
    /// specified in <paramref name="parameters"/> and
    /// <paramref name="throwOnFail"/> is <see langword="true"/>.
    /// </exception>
    [DebuggerStepThrough]
    public static T? New<T>([DynamicallyAccessedMembers(PublicConstructors)] this Type type, bool throwOnFail, IEnumerable? parameters)
    {
        object?[] p = parameters?.ToGeneric().ToArray() ?? [];
        try
        {
            New_Contract(type, throwOnFail, p);
            if (type.ContainsGenericParameters) return default;
            return (T)type.GetConstructor([.. p.ToTypes()])!.Invoke(p);
        }
        catch
        {
            if (throwOnFail) throw;
            return default;
        }
    }

    /// <summary>
    /// Initializes a new instance of an object with a constructor that
    /// accepts the provided arguments.
    /// </summary>
    /// <typeparam name="T">Type of instance to return.</typeparam>
    /// <param name="type">
    /// Type to instantiate. Must be, inherit from or implement
    /// the type specified in <typeparamref name="T"/>.
    /// </param>
    /// <param name="parameters">
    /// Parameters to pass to the constructor. A compatible
    /// constructor will be searched for to create the instance.
    /// </param>
    /// <returns>A new instance of the specified type.</returns>
    /// <exception cref="TypeLoadException">
    /// Occurs if it's not possible to instantiate a class of the requested type.
    /// </exception>
    [DebuggerStepThrough]
    public static T New<T>([DynamicallyAccessedMembers(PublicConstructors)] this Type type, params object?[] parameters)
    {
        return New<T>(type, true, parameters)!;
    }

    /// <summary>
    /// Initializes a new instance of the dynamic type specified,
    /// returning it as a <typeparamref name="T"/>.
    /// </summary>
    /// <returns>The new instance of the specified type.</returns>
    /// <param name="type">
    /// Type to instantiate. Must be, inherit from or implement
    /// the type specified in <typeparamref name="T"/>
    /// </param>
    /// <typeparam name="T">Type of instance to return.</typeparam>
    [DebuggerStepThrough]
    [Sugar]
    public static T New<T>([DynamicallyAccessedMembers(PublicConstructors)] this Type type)
    {
        return type.New<T>([]);
    }

    /// <summary>
    /// Initializes a new instance of an object asynchronously
    /// with a constructor that accepts the provided arguments.
    /// </summary>
    /// <param name="type">Type to instantiate.</param>
    /// <param name="throwOnFail">
    /// If set to <see langword="true"/>, an exception will be thrown
    /// if the type cannot be instantiated with the provided information,
    /// otherwise returns <see langword="null"/> if set to <see langword="false"/>.
    /// </param>
    /// <param name="parameters">
    /// Parameters to pass to the constructor. A compatible
    /// constructor will be searched for to create the instance.
    /// </param>
    /// <returns>
    /// A new instance of the specified type, or
    /// <see langword="null"/> if a problem occurs while instantiating the
    /// type and <paramref name="throwOnFail"/> is
    /// <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Using this method to create an instance asynchronously can be problematic
    /// if normal program execution depends on which thread owns the object,
    /// for example when instantiating UI elements.
    /// </remarks>
    /// <exception cref="TypeLoadException">
    /// Occurs if it's not possible to instantiate a class of the requested type and
    /// <paramref name="throwOnFail"/> is set to <see langword="true"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Occurs if <paramref name="type"/> is <see langword="null"/>
    /// and <paramref name="throwOnFail"/> is set to <see langword="true"/>.
    /// </exception>
    /// <exception cref="ClassNotInstantiableException">
    /// Occurs if the type <paramref name="type"/> cannot be
    /// instantiated using a public constructor that accepts the parameters
    /// specified in <paramref name="parameters"/> and
    /// <paramref name="throwOnFail"/> is set to <see langword="true"/>.
    /// </exception>
    [DebuggerStepThrough]
    public static async Task<object?> NewAsync([DynamicallyAccessedMembers(PublicConstructors)] this Type type, bool throwOnFail, IEnumerable? parameters)
    {
        object?[] p = parameters?.ToGeneric().ToArray() ?? [];
        New_Contract(type, throwOnFail, p);
        try
        {
            ConstructorInfo? ctor = type.GetConstructor([.. p.ToTypes()]);
            return await Task.Run(() => ctor?.Invoke(p) ?? throw Errors.ClassNotInstantiable());
        }
        catch (Exception e)
        {
            return throwOnFail ? throw Errors.CannotInstanceClass(type, e) : null;
        }
    }

    /// <summary>
    /// Initializes a new instance of an object asynchronously
    /// with a parameterless constructor.
    /// </summary>
    /// <param name="type">Type to instantiate.</param>
    /// <param name="throwOnFail">
    /// If set to <see langword="true"/>, an exception will be thrown
    /// if the type cannot be instantiated with the provided information,
    /// otherwise returns <see langword="null"/> if set to <see langword="false"/>
    /// </param>
    /// <returns>
    /// A new instance of the specified type, or
    /// <see langword="null"/> if a problem occurs while instantiating the
    /// type and <paramref name="throwOnFail"/> is
    /// <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Using this method to create an instance asynchronously can be problematic
    /// if normal program execution depends on which thread owns the object,
    /// for example when instantiating UI elements.
    /// </remarks>
    /// <exception cref="TypeLoadException">
    /// Occurs if it's not possible to instantiate a class of the requested type and
    /// <paramref name="throwOnFail"/> is set to <see langword="true"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Occurs if <paramref name="type"/> is <see langword="null"/>
    /// and <paramref name="throwOnFail"/> is set to <see langword="true"/>.
    /// </exception>
    /// <exception cref="ClassNotInstantiableException">
    /// Occurs if the type <paramref name="type"/> cannot be
    /// instantiated using a public parameterless constructor and
    /// <paramref name="throwOnFail"/> is set to <see langword="true"/>.
    /// </exception>
    public static Task<object?> NewAsync([DynamicallyAccessedMembers(PublicConstructors)] this Type type, bool throwOnFail)
    {
        return NewAsync(type, throwOnFail, null);
    }

    /// <summary>
    /// Initializes a new instance of an object asynchronously
    /// with a constructor that accepts the provided arguments.
    /// </summary>
    /// <param name="type">Type to instantiate.</param>
    /// <param name="parameters">
    /// Parameters to pass to the constructor. A compatible
    /// constructor will be searched for to create the instance.
    /// </param>
    /// <returns>
    /// A new instance of the specified type, or
    /// <see langword="null"/> if a problem occurs while instantiating the
    /// type.
    /// </returns>
    /// <remarks>
    /// Using this method to create an instance asynchronously can be problematic
    /// if normal program execution depends on which thread owns the object,
    /// for example when instantiating UI elements.
    /// </remarks>
    /// <exception cref="TypeLoadException">
    /// Occurs if it's not possible to instantiate a class of the requested type.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Occurs if <paramref name="type"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ClassNotInstantiableException">
    /// Occurs if the type <paramref name="type"/> cannot be
    /// instantiated using a public constructor that accepts the parameters
    /// specified in <paramref name="parameters"/>.
    /// </exception>
    public static Task<object?> NewAsync([DynamicallyAccessedMembers(PublicConstructors)] this Type type, IEnumerable? parameters)
    {
        return NewAsync(type, true, parameters);
    }

    /// <summary>
    /// Initializes a new instance of an object asynchronously
    /// with a parameterless constructor.
    /// </summary>
    /// <param name="type">Type to instantiate.</param>
    /// <returns>
    /// A new instance of the specified type, or
    /// <see langword="null"/> if a problem occurs while instantiating the
    /// type.
    /// </returns>
    /// <remarks>
    /// Using this method to create an instance asynchronously can be problematic
    /// if normal program execution depends on which thread owns the object,
    /// for example when instantiating UI elements.
    /// </remarks>
    /// <exception cref="TypeLoadException">
    /// Occurs if it's not possible to instantiate a class of the requested type.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Occurs if <paramref name="type"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ClassNotInstantiableException">
    /// Occurs if the type <paramref name="type"/> cannot be
    /// instantiated using a public parameterless constructor.
    /// </exception>
    public static Task<object?> NewAsync([DynamicallyAccessedMembers(PublicConstructors)] this Type type)
    {
        return NewAsync(type, true);
    }

    /// <summary>
    /// Initializes a new instance of an object asynchronously
    /// with a constructor that accepts the provided arguments.
    /// </summary>
    /// <typeparam name="T">Type of instance to return.</typeparam>
    /// <param name="type">
    /// Type to instantiate. Must be, inherit from or implement
    /// the type specified in <typeparamref name="T" />.
    /// </param>
    /// <param name="throwOnFail">
    /// If set to <see langword="true"/>, an exception will be thrown
    /// if the type cannot be instantiated with the provided information,
    /// otherwise returns <see langword="null"/> if set to <see langword="false"/>
    /// </param>
    /// <param name="parameters">
    /// Parameters to pass to the constructor. A compatible
    /// constructor will be searched for to create the instance.
    /// </param>
    /// <returns>
    /// A new instance of the specified type, or
    /// <see langword="null"/> if a problem occurs while instantiating the
    /// type and <paramref name="throwOnFail"/> is
    /// <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Using this method to create an instance asynchronously can be
    /// problematic if normal program execution depends on which thread
    /// owns the object, for example when instantiating UI elements.
    /// </remarks>
    /// <exception cref="TypeLoadException">
    /// Occurs if it's not possible to instantiate a class of the requested type and
    /// <paramref name="throwOnFail"/> is set to <see langword="true"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Occurs if <paramref name="type"/> is <see langword="null"/>
    /// and <paramref name="throwOnFail"/> is set to <see langword="true"/>.
    /// </exception>
    /// <exception cref="ClassNotInstantiableException">
    /// Occurs if the type <paramref name="type"/> cannot be
    /// instantiated using a public constructor that accepts the parameters
    /// specified in <paramref name="parameters"/> and
    /// <paramref name="throwOnFail"/> is set to <see langword="true"/>.
    /// </exception>
    [DebuggerStepThrough]
    public static async Task<T> NewAsync<T>([DynamicallyAccessedMembers(PublicConstructors)] this Type type, bool throwOnFail, IEnumerable? parameters)
    {
        object? r = await NewAsync(type, throwOnFail, parameters);
        if (r is T v) return v;
        return throwOnFail ? throw new InvalidCastException() : default!;
    }

    /// <summary>
    /// Initializes a new instance of an object asynchronously
    /// with a constructor that accepts the provided arguments.
    /// </summary>
    /// <typeparam name="T">Type of instance to return.</typeparam>
    /// <param name="type">
    /// Type to instantiate. Must be, inherit from or implement
    /// the type specified in <typeparamref name="T" />.
    /// </param>
    /// <param name="throwOnFail">
    /// If set to <see langword="true"/>, an exception will be thrown
    /// if the type cannot be instantiated with the provided information,
    /// otherwise returns <see langword="null"/> if set to <see langword="false"/>
    /// </param>
    /// <returns>
    /// A new instance of the specified type, or
    /// <see langword="null"/> if a problem occurs while instantiating the
    /// type and <paramref name="throwOnFail"/> is
    /// <see langword="false"/>.
    /// </returns>
    /// <exception cref="TypeLoadException">
    /// Occurs if it's not possible to instantiate a class of the requested type and
    /// <paramref name="throwOnFail"/> is set to <see langword="true"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Occurs if <paramref name="type"/> is <see langword="null"/>
    /// and <paramref name="throwOnFail"/> is set to <see langword="true"/>.
    /// </exception>
    /// <exception cref="ClassNotInstantiableException">
    /// Occurs if the type <paramref name="type"/> cannot be
    /// instantiated using a public constructor and
    /// <paramref name="throwOnFail"/> is set to <see langword="true"/>.
    /// </exception>
    public static Task<T> NewAsync<T>([DynamicallyAccessedMembers(PublicConstructors)] this Type type, bool throwOnFail)
    {
        return NewAsync<T>(type, throwOnFail, Array.Empty<object?>());
    }

    /// <summary>
    /// Initializes a new instance of an object asynchronously
    /// with a constructor that accepts the provided arguments.
    /// </summary>
    /// <typeparam name="T">Type of instance to return.</typeparam>
    /// <param name="type">
    /// Type to instantiate. Must be, inherit from or implement
    /// the type specified in <typeparamref name="T" />.
    /// </param>
    /// <param name="parameters">
    /// Parameters to pass to the constructor. A compatible
    /// constructor will be searched for to create the instance.
    /// </param>
    /// <returns>
    /// A new instance of the specified type, or
    /// <see langword="null"/> if a problem occurs while instantiating the
    /// type.
    /// </returns>
    /// <exception cref="TypeLoadException">
    /// Occurs if it's not possible to instantiate a class of the requested type.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Occurs if <paramref name="type"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ClassNotInstantiableException">
    /// Occurs if the type <paramref name="type"/> cannot be
    /// instantiated using a public constructor that accepts the parameters
    /// specified in <paramref name="parameters"/>.
    /// </exception>
    public static Task<T> NewAsync<T>([DynamicallyAccessedMembers(PublicConstructors)] this Type type, IEnumerable? parameters)
    {
        return NewAsync<T>(type, true, parameters);
    }

    /// <summary>
    /// Initializes a new instance of an object asynchronously
    /// with a constructor that accepts the provided arguments.
    /// </summary>
    /// <typeparam name="T">Type of instance to return.</typeparam>
    /// <param name="type">
    /// Type to instantiate. Must be, inherit from or implement
    /// the type specified in <typeparamref name="T" />.
    /// </param>
    /// <returns>
    /// A new instance of the specified type.
    /// </returns>
    /// <exception cref="TypeLoadException">
    /// Occurs if it's not possible to instantiate a class of the requested type.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Occurs if <paramref name="type"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ClassNotInstantiableException">
    /// Occurs if the type <paramref name="type"/> cannot be
    /// instantiated using a public constructor without parameters.
    /// </exception>
    public static Task<T> NewAsync<T>([DynamicallyAccessedMembers(PublicConstructors)] this Type type)
    {
        return NewAsync<T>(type, true);
    }

    /// <summary>
    /// Ensures to return a non-nullable type for structures.
    /// </summary>
    /// <param name="t">Type to return</param>
    /// <returns>
    /// The underlying type of a <see cref="Nullable{T}"/>, or
    /// <paramref name="t"/> if the type is not nullable.
    /// </returns>
    [DebuggerStepThrough, Sugar]
    public static Type NotNullable(this Type t)
    {
        NotNullable_Contract(t);
        return Nullable.GetUnderlyingType(t) ?? t;
    }

    /// <summary>
    /// Resolves a collection type to the type of its elements.
    /// </summary>
    /// <param name="type">
    /// Type to check.
    /// </param>
    /// <returns>
    /// The element type of the collection for the type
    /// <paramref name="type"/>, or <paramref name="type"/> if it is not a
    /// collection type.
    /// </returns>
    [Sugar]
    [RequiresDynamicCode(MethodCreatesNewTypes)]
    public static Type ResolveCollectionType([DynamicallyAccessedMembers(Interfaces)] this Type type) => type.IsCollectionType() ? type.GetCollectionType() : type;

    /// <summary>
    /// Ensures a type defined at compile time is returned.
    /// </summary>
    /// <param name="t">
    /// Type to check.
    /// </param>
    /// <returns>
    /// <paramref name="t"/>, if it's a type defined at compile time, or a base type that is. It will return
    /// <see langword="null"/> if there is no base type defined, like in interfaces.
    /// </returns>
    public static Type? ResolveToDefinedType(this Type t)
    {
        return t.Assembly.IsDynamic ? ResolveToDefinedType(t.BaseType!) : t;
    }

    /// <summary>
    /// Converts the values of an enumeration type to a collection of
    /// <see cref="NamedObject{T}"/>.
    /// </summary>
    /// <param name="type">Enumeration type to convert.</param>
    /// <returns>
    /// An enumeration of all <see cref="NamedObject{T}"/> created from the values of the specified enumeration type.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Occurs if <paramref name="type"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidTypeException">
    /// Occurs if <paramref name="type"/> is not an enumeration type.
    /// </exception>
    [RequiresDynamicCode(MethodCallsDynamicCode)]
    public static IEnumerable<NamedObject<Enum>> ToNamedEnum(this Type type)
    {
        ToNamedEnum_Contract(type);
        return type.GetEnumValues().Cast<Enum>().Select(j => new NamedObject<Enum>(j.NameOf(), j));
    }

    /// <summary>
    /// Attempts to instantiate the type with the specified constructor arguments.
    /// </summary>
    /// <param name="t">Type that will be attempted to instantiate.</param>
    /// <param name="instance">
    /// Output parameter. Instance created or
    /// <see langword="null"/> if it was not possible to create an instance of the specified type.
    /// </param>
    /// <param name="args">
    /// Arguments to pass to the constructor. Can be omitted or set to
    /// <see langword="null"/> for constructors with no arguments.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if it was possible to instantiate the type and do so correctly,
    /// <see langword="false"/> otherwise.
    /// </returns>
    [Sugar]
    public static bool TryInstance([DynamicallyAccessedMembers(PublicConstructors)] this Type t, [MaybeNullWhen(false)] out object instance, params object[]? args)
    {
        return TryInstance<object>(t, out instance, args);
    }

    /// <summary>
    /// Attempts to instantiate the type with the specified constructor arguments,
    /// returning it as an object of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of object to return.</typeparam>
    /// <param name="t">Type that will be attempted to instantiate.</param>
    /// <param name="instance">
    /// Output parameter. Instance created or
    /// <see langword="null"/> if it was not possible to create an instance of the specified type.
    /// </param>
    /// <param name="args">
    /// Arguments to pass to the constructor. Can be omitted or set to
    /// <see langword="null"/> for constructors with no arguments.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if it was possible to instantiate the type and do so correctly,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool TryInstance<T>([DynamicallyAccessedMembers(PublicConstructors)] this Type t, [MaybeNullWhen(false)] out T instance, params object[]? args)
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
}
