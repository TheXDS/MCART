/*
ReflectionHelpers.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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
using System.Runtime.CompilerServices;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Extensions;
using static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes;
using static TheXDS.MCART.Misc.AttributeErrorMessages;
using static TheXDS.MCART.Misc.PrivateInternals;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Auxiliary functions for reflection.
/// </summary>
public static partial class ReflectionHelpers
{
    /// <summary>
    /// Enumerates the values of all fields that return values of type
    /// <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">Type of fields to retrieve.</typeparam>
    /// <param name="fields">
    /// Collection of fields to analyze.
    /// </param>
    /// <param name="instance">
    /// Instance from which to retrieve fields.
    /// </param>
    /// <returns>
    /// An enumeration of all values of type
    /// <typeparamref name="T" /> from the instance.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="fields"/> is
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="NullItemException">
    /// Thrown if any element of <paramref name="fields"/> is
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="MissingFieldException">
    /// Thrown when <paramref name="instance"/> is not
    /// <see langword="null"/> and any element of 
    /// <paramref name="fields"/> is not part of 
    /// <paramref name="instance"/> type.
    /// </exception>
    /// <exception cref="MemberAccessException">
    /// Thrown when <paramref name="instance"/> is
    /// <see langword="null"/> and any element of 
    /// <paramref name="fields"/> is not a static field.
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
    /// Enumerates the values of all static fields that return
    /// values of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">Type of fields to retrieve.</typeparam>
    /// <param name="fields">
    /// Collection of fields to analyze.
    /// </param>
    /// <returns>
    /// An enumeration of all values of type
    /// <typeparamref name="T" />.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="fields"/> is
    /// <see langword="null"/>.
    /// </exception>
    public static IEnumerable<T> FieldsOf<T>(IEnumerable<FieldInfo> fields)
    {
        return FieldsOf<T>(fields, null);
    }

    /// <summary>
    /// Enumerates the values of all static fields that return
    /// values of type <typeparamref name="T" /> in the specified type.
    /// </summary>
    /// <typeparam name="T">Type of fields to retrieve.</typeparam>
    /// <param name="type">
    /// Type from which to retrieve fields.
    /// </param>
    /// <returns>
    /// An enumeration of all values of type
    /// <typeparamref name="T" /> from the type.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="type"/> is
    /// <see langword="null"/>.
    /// </exception>
    public static IEnumerable<T> FieldsOf<T>([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)] this Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return FieldsOf<T>(type.GetFields(BindingFlags.Static | BindingFlags.Public), null);
    }

    /// <summary>
    /// Instantiates all objects of the specified type,
    /// returning them in an enumeration.
    /// </summary>
    /// <typeparam name="T">Type of objects to find.</typeparam>
    /// <returns>
    /// An enumeration of all object instances of type
    /// <typeparamref name="T"/> found.
    /// </returns>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static IEnumerable<T> FindAllObjects<T>() where T : notnull
    {
        return FindAllObjects<T>((IEnumerable?)null);
    }

    /// <summary>
    /// Instantiates all objects of the specified type,
    /// returning them in an enumeration.
    /// </summary>
    /// <typeparam name="T">Type of objects to find.</typeparam>
    /// <param name="typeFilter">
    /// Filter function to apply to matching types.
    /// </param>
    /// <returns>
    /// An enumeration of all object instances of type
    /// <typeparamref name="T"/> found.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="typeFilter"/> is
    /// <see langword="null"/>.
    /// </exception>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static IEnumerable<T> FindAllObjects<T>(Func<Type, bool> typeFilter) where T : notnull
    {
        return FindAllObjects<T>(null, typeFilter);
    }

    /// <summary>
    /// Instantiates all objects of the specified type,
    /// returning them in an enumeration.
    /// </summary>
    /// <typeparam name="T">Type of objects to find.</typeparam>
    /// <param name="ctorArgs">
    /// Arguments to pass to the class instance constructor.
    /// </param>
    /// <param name="typeFilter">
    /// Filter function to apply to matching types.
    /// </param>
    /// <returns>
    /// An enumeration of all object instances of type
    /// <typeparamref name="T"/> found.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="typeFilter"/> is
    /// <see langword="null"/>.
    /// </exception>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static IEnumerable<T> FindAllObjects<T>(IEnumerable? ctorArgs, Func<Type, bool> typeFilter) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(typeFilter);
        return GetTypes<T>(true).NotNull().Where(typeFilter).Select(p => p.New<T>(false, ctorArgs)).Where(p => p is not null)!;
    }

    /// <summary>
    /// Instantiates all objects of the specified type,
    /// returning them in an enumeration.
    /// </summary>
    /// <typeparam name="T">Type of objects to find.</typeparam>
    /// <param name="ctorArgs">
    /// Arguments to pass to the class instance constructor.
    /// </param>
    /// <returns>
    /// An enumeration of all object instances of type
    /// <typeparamref name="T"/> found.
    /// </returns>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static IEnumerable<T> FindAllObjects<T>(IEnumerable? ctorArgs) where T : notnull
    {
        return GetTypes<T>(true).NotNull().Select(p => p.New<T>(false, ctorArgs)).Where(p => p is not null)!;
    }

    /// <summary>
    /// Retrieves the first object that matches the specified base type.
    /// </summary>
    /// <typeparam name="T">Type of object to find.</typeparam>
    /// <returns>
    /// A new instance of the requested object, or
    /// <see langword="null"/> if no matching type is found.
    /// </returns>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static T? FindFirstObject<T>() where T : notnull
    {
        return FindFirstObject<T>((IEnumerable?)null);
    }

    /// <summary>
    /// Retrieves the first object that matches the specified base type.
    /// </summary>
    /// <typeparam name="T">Type of object to find.</typeparam>
    /// <param name="typeFilter">
    /// Filter function to apply to matching types.
    /// </param>
    /// <returns>
    /// A new instance of the requested object, or
    /// <see langword="null"/> if no matching type is found.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="typeFilter"/> is
    /// <see langword="null"/>.
    /// </exception>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static T? FindFirstObject<T>(Func<Type, bool> typeFilter) where T : notnull
    {
        return FindFirstObject<T>(null, typeFilter);
    }

    /// <summary>
    /// Retrieves the first object that matches the specified base type.
    /// </summary>
    /// <typeparam name="T">Type of object to find.</typeparam>
    /// <param name="ctorArgs">
    /// Arguments to pass to the class instance constructor.
    /// </param>
    /// <param name="typeFilter">
    /// Filter function to apply to matching types.
    /// </param>
    /// <returns>
    /// A new instance of the requested object, or
    /// <see langword="null"/> if no matching type is found.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="typeFilter"/> is
    /// <see langword="null"/>.
    /// </exception>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static T? FindFirstObject<T>(IEnumerable? ctorArgs, Func<Type, bool> typeFilter) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(typeFilter);
        Type? t = GetTypes<T>(true).NotNull().FirstOrDefault(typeFilter);
        return t is not null ? t.New<T>(false, ctorArgs) : default;
    }

    /// <summary>
    /// Retrieves the first object that matches the specified base type.
    /// </summary>
    /// <typeparam name="T">Type of object to find.</typeparam>
    /// <param name="ctorArgs">
    /// Arguments to pass to the class instance constructor.
    /// </param>
    /// <returns>
    /// A new instance of the requested object, or
    /// <see langword="null"/> if no matching type is found.
    /// </returns>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static T? FindFirstObject<T>(IEnumerable? ctorArgs) where T : notnull
    {
        Type? t = GetTypes<T>().FirstOrDefault(p => p.IsInstantiable(ctorArgs?.ToTypes().ToArray() ?? Type.EmptyTypes));
        return t is not null ? t.New<T>(false, ctorArgs) : default;
    }

    /// <summary>
    /// Retrieves a single object that matches the specified base type.
    /// </summary>
    /// <typeparam name="T">Type of object to find.</typeparam>
    /// <returns>
    /// A new instance of the requested object, or
    /// <see langword="null"/> if no matching type is found.
    /// </returns>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static T? FindSingleObject<T>() where T : notnull
    {
        return FindSingleObject<T>((IEnumerable?)null);
    }

    /// <summary>
    /// Retrieves a single object that matches the specified base type.
    /// </summary>
    /// <typeparam name="T">Type of object to find.</typeparam>
    /// <param name="typeFilter">
    /// Filter function to apply to matching types.
    /// </param>
    /// <returns>
    /// A new instance of the requested object, or
    /// <see langword="null"/> if no matching type is found.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="typeFilter"/> is
    /// <see langword="null"/>.
    /// </exception>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static T? FindSingleObject<T>(Func<Type, bool> typeFilter) where T : notnull
    {
        return FindSingleObject<T>(null, typeFilter);
    }

    /// <summary>
    /// Retrieves a single object that matches the specified base type.
    /// </summary>
    /// <typeparam name="T">Type of object to find.</typeparam>
    /// <param name="ctorArgs">
    /// Arguments to pass to the class instance constructor.
    /// </param>
    /// <param name="typeFilter">
    /// Filter function to apply to matching types.
    /// </param>
    /// <returns>
    /// A new instance of the requested object, or
    /// <see langword="null"/> if no matching type is found.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="typeFilter"/> is
    /// <see langword="null"/>.
    /// </exception>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static T? FindSingleObject<T>(IEnumerable? ctorArgs, Func<Type, bool> typeFilter) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(typeFilter);
        Type? t = GetTypes<T>(true).NotNull().SingleOrDefault(typeFilter);
        return t is not null ? t.New<T>(false, ctorArgs) : default;
    }

    /// <summary>
    /// Retrieves a single object that matches the specified base type.
    /// </summary>
    /// <typeparam name="T">Type of object to find.</typeparam>
    /// <param name="ctorArgs">
    /// Arguments to pass to the class instance constructor.
    /// </param>
    /// <returns>
    /// A new instance of the requested object, or
    /// <see langword="null"/> if no matching type is found.
    /// </returns>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static T? FindSingleObject<T>(IEnumerable? ctorArgs) where T : notnull
    {
        Type? t = GetTypes<T>(true).NotNull().SingleOrDefault();
        return t is not null ? t.New<T>(false, ctorArgs) : default;
    }

    /// <summary>
    /// Searches the specified <see cref="AppDomain"/> for a type that
    /// contains the specified <see cref="TagAttribute"/>.
    /// </summary>
    /// <param name="identifier">Identifier to search for.</param>
    /// <param name="domain">Domain in which to search.</param>
    /// <returns>
    /// A type that has been tagged with the specified identifier, or
    /// <see langword="null"/> if no type contains the identifier.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="identifier"/> or
    /// <paramref name="domain"/> is <see langword="null"/>.
    /// </exception>
    [Sugar]
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static Type? FindType(string identifier, AppDomain domain)
    {
        return FindType<object>(identifier, domain);
    }

    /// <summary>
    /// Searches the current <see cref="AppDomain"/> for a type that
    /// contains the specified <see cref="TagAttribute"/>.
    /// </summary>
    /// <param name="identifier">Identifier to search for.</param>
    /// <returns>
    /// A type that has been tagged with the specified identifier, or
    /// <see langword="null"/> if no type contains the identifier.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="identifier"/> is
    /// <see langword="null"/>.
    /// </exception>
    [Sugar]
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static Type? FindType(string identifier)
    {
        return FindType<object>(identifier);
    }

    /// <summary>
    /// Searches the specified <see cref="AppDomain"/> for a type that
    /// contains the specified <see cref="TagAttribute"/>.
    /// </summary>
    /// <typeparam name="T">Restrict search to these types.</typeparam>
    /// <param name="identifier">Identifier to search for.</param>
    /// <param name="domain">Domain in which to search.</param>
    /// <returns>
    /// A type that has been tagged with the specified identifier, or
    /// <see langword="null"/> if no type contains the identifier.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="identifier"/> or
    /// <paramref name="domain"/> is <see langword="null"/>.
    /// </exception>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static Type? FindType<T>(string identifier, AppDomain domain)
    {
        FindType_Contract(identifier, domain);
        return GetTypes<T>(domain)
            .FirstOrDefault(j => j.GetCustomAttributes(typeof(TagAttribute), false)
                .Cast<TagAttribute>()
                .Any(k => k.Value == identifier));
    }

    /// <summary>
    /// Searches the current <see cref="AppDomain"/> for a type that
    /// contains the specified <see cref="TagAttribute"/>.
    /// </summary>
    /// <typeparam name="T">Restrict search to these types.</typeparam>
    /// <param name="identifier">Identifier to search for.</param>
    /// <returns>
    /// A type that has been tagged with the specified identifier, or
    /// <see langword="null"/> if no type contains the identifier.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="identifier"/> is
    /// <see langword="null"/>.
    /// </exception>
    [Sugar]
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static Type? FindType<T>(string identifier)
    {
        return FindType<T>(identifier, AppDomain.CurrentDomain);
    }

    /// <summary>
    /// Gets a reference to the method that called the currently executing
    /// method.
    /// </summary>
    /// <returns>
    /// The method that called the current method where this function is
    /// used. Will return <see langword="null"/> if called from the
    /// application entry point (generally the <c>Main()</c> function).
    /// </returns>
    /// <remarks>Due to some compiler optimizations, it is recommended
    /// that methods calling <see cref="GetCallingMethod()"/> be annotated
    /// with the <see cref="MethodImplAttribute"/> with the value
    /// <see cref="MethodImplOptions.NoInlining"/>.
    /// </remarks>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static MethodBase? GetCallingMethod()
    {
        return GetCallingMethod(3);
    }

    /// <summary>
    /// Gets a reference to the method that called the current method.
    /// </summary>
    /// <param name="nCaller">
    /// Number of parent method iterations to return. Must be a value
    /// greater than or equal to 1.
    /// </param>
    /// <returns>
    /// The method that called the current method where this function is
    /// used. Will return <see langword="null"/> if the call stack is
    /// analyzed and the application entry point is reached (generally the
    /// <c>Main()</c> function).
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="nCaller"/> is less than 1.
    /// </exception>
    /// <exception cref="OverflowException">
    /// Thrown if <paramref name="nCaller"/> + 1 causes an overflow.
    /// </exception>
    /// <remarks>Due to some compiler optimizations, it is recommended
    /// that methods calling <see cref="GetCallingMethod(int)"/> be
    /// annotated with the <see cref="MethodImplAttribute"/> with the
    /// value <see cref="MethodImplOptions.NoInlining"/>.
    /// </remarks>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static MethodBase? GetCallingMethod(int nCaller)
    {
        GetCallingMethod_Contract(nCaller);
        StackFrame[]? frames = new StackTrace().GetFrames();
        return frames.Length > nCaller ? frames[nCaller].GetMethod() : null;
    }

    /// <summary>
    /// Gets the assembly that contains the entry point of the currently
    /// executing application.
    /// </summary>
    /// <returns>
    /// The assembly where the entry point of the current application is
    /// defined.
    /// </returns>
    [Sugar]
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static Assembly? GetEntryAssembly()
    {
        return GetEntryPoint()?.DeclaringType?.Assembly;
    }

    /// <summary>
    /// Gets the entry point of the currently executing application.
    /// </summary>
    /// <returns>
    /// The method that is the entry point of the current application.
    /// </returns>
    [Sugar]
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static MethodInfo? GetEntryPoint()
    {
        return new StackTrace().GetFrames().Last().GetMethod() as MethodInfo;
    }

    /// <summary>
    /// Gets a reference to the field selected by an expression.
    /// </summary>
    /// <param name="fieldSelector">
    /// Expression that indicates which field of the type to return.
    /// </param>
    /// <typeparam name="T">
    /// Type from which to select the field to retrieve.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Type returned by the field to retrieve.
    /// </typeparam>
    /// <returns>
    /// A <see cref="FieldInfo"/> that represents the field selected in
    /// the expression.
    /// </returns>
    public static FieldInfo GetField<T, TValue>(Expression<Func<T, TValue>> fieldSelector)
    {
        return GetMember<FieldInfo, T, TValue>(fieldSelector);
    }

    /// <summary>
    /// Gets a reference to the field selected by an expression.
    /// </summary>
    /// <param name="fieldSelector">
    /// Expression that indicates which field of the type to return.
    /// </param>
    /// <typeparam name="T">
    /// Type from which to select the field to retrieve.
    /// </typeparam>
    /// <returns>
    /// A <see cref="FieldInfo"/> that represents the field selected in
    /// the expression.
    /// </returns>
    public static FieldInfo GetField<T>(Expression<Func<T, object?>> fieldSelector)
    {
        return GetMember<FieldInfo, T, object?>(fieldSelector);
    }

    /// <summary>
    /// Gets a reference to the selected field via an expression.
    /// </summary>
    /// <param name="fieldSelector">
    /// Expression indicating which field to return.
    /// </param>
    /// <typeparam name="TValue">
    /// Type returned by the field to get.
    /// </typeparam>
    /// <returns>
    /// A <see cref="FieldInfo"/> representing the selected field.
    /// </returns>
    public static FieldInfo GetField<TValue>(Expression<Func<TValue>> fieldSelector)
    {
        return GetMember<FieldInfo, TValue>(fieldSelector);
    }

    /// <summary>
    /// Gets a member from an expression.
    /// </summary>
    /// <param name="memberSelector">
    /// Expression indicating which member to return.
    /// </param>
    /// <returns>
    /// A <see cref="MemberInfo"/> representing the selected member.
    /// </returns>
    public static MemberInfo GetMember(Expression<Func<object?>> memberSelector)
    {
        return GetMemberInternal(memberSelector);
    }

    /// <summary>
    /// Gets an instance member of a class from an expression.
    /// </summary>
    /// <typeparam name="T">
    /// Class from which to get the member.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Type of the member to get.
    /// </typeparam>
    /// <param name="memberSelector">
    /// Expression indicating which member to return.
    /// </param>
    /// <returns>
    /// A <see cref="MemberInfo"/> representing the selected member.
    /// </returns>
    public static MemberInfo GetMember<T, TValue>(Expression<Func<T, TValue>> memberSelector)
    {
        return GetMemberInternal(memberSelector);
    }

    /// <summary>
    /// Gets a member of a class from an expression.
    /// </summary>
    /// <typeparam name="T">
    /// Class from which to get the member.
    /// </typeparam>
    /// <param name="memberSelector">
    /// Expression indicating which member to return.
    /// </param>
    /// <returns>
    /// A <see cref="MemberInfo"/> representing the selected member.
    /// </returns>
    public static MemberInfo GetMember<T>(Expression<Func<T, object?>> memberSelector)
    {
        return GetMemberInternal(memberSelector);
    }

    /// <summary>
    /// Gets a reference to the selected member via an expression.
    /// </summary>
    /// <typeparam name="TMember">
    /// Type of the member to get.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Type returned by the member to get.
    /// </typeparam>
    /// <typeparam name="T">
    /// Type from which to get the member.
    /// </typeparam>
    /// <param name="memberSelector">
    /// Expression indicating which member to return.
    /// </param>
    /// <returns>
    /// A <see cref="MemberInfo"/> representing the selected member.
    /// </returns>
    public static TMember GetMember<TMember, T, TValue>(Expression<Func<T, TValue>> memberSelector) where TMember : MemberInfo
    {
        return GetMemberInternal(memberSelector) as TMember ?? throw new InvalidArgumentException(nameof(memberSelector));
    }

    /// <summary>
    /// Gets a reference to the selected member via an expression.
    /// </summary>
    /// <typeparam name="TMember">
    /// Type of the member to get.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Type from which to get the member.
    /// </typeparam>
    /// <param name="memberSelector">
    /// Expression indicating which member to return.
    /// </param>
    /// <returns>
    /// A <see cref="MemberInfo"/> representing the selected member.
    /// </returns>
    public static TMember GetMember<TMember, TValue>(Expression<Func<TValue>> memberSelector) where TMember : MemberInfo
    {
        return GetMemberInternal(memberSelector) as TMember ?? throw new InvalidArgumentException(nameof(memberSelector));
    }

    /// <summary>
    /// Gets a member from an expression.
    /// </summary>
    /// <typeparam name="TValue">
    /// Type of the member to get.
    /// </typeparam>
    /// <param name="memberSelector">
    /// Expression indicating which member to return.
    /// </param>
    /// <returns>
    /// A <see cref="MemberInfo"/> representing the selected member.
    /// </returns>
    public static MemberInfo GetMember<TValue>(Expression<Func<TValue>> memberSelector)
    {
        return GetMemberInternal(memberSelector);
    }

    /// <summary>
    /// Gets a reference to the selected method via an expression.
    /// </summary>
    /// <param name="methodSelector">
    /// Expression indicating which method to return.
    /// </param>
    /// <typeparam name="T">
    /// Type from which to select the method.
    /// </typeparam>
    /// <typeparam name="TMethod">
    /// Delegate type of the method to get.
    /// </typeparam>
    /// <returns>
    /// A <see cref="MethodInfo"/> representing the selected method.
    /// </returns>
    public static MethodInfo GetMethod<[DynamicallyAccessedMembers(PublicMethods | NonPublicMethods)] T, TMethod>(Expression<Func<T, TMethod>> methodSelector) where TMethod : Delegate
    {
        MethodInfo? m = GetMember<MethodInfo, T, TMethod>(methodSelector);

        /* HACK
         * Linq expressions may not correctly detect the source type of a 
         * method that is an overload in a derived class.
         */
        return m.DeclaringType == typeof(T) ? m
            : typeof(T).GetMethod(m.Name, m.GetBindingFlags(), null, [.. m.GetParameters().Select(p => p.ParameterType)], null) ?? throw new TamperException();
    }

    /// <summary>
    /// Gets a reference to the selected method via an expression.
    /// </summary>
    /// <param name="methodSelector">
    /// Expression indicating which method to return.
    /// </param>
    /// <typeparam name="T">
    /// Type from which to select the method.
    /// </typeparam>
    /// <returns>
    /// A <see cref="MethodInfo"/> representing the selected method.
    /// </returns>
    public static MethodInfo GetMethod<T>(Expression<Func<T, Delegate>> methodSelector)
    {
        return GetMember<MethodInfo, T, Delegate>(methodSelector);
    }

    /// <summary>
    /// Gets a reference to the selected method via an expression.
    /// </summary>
    /// <param name="methodSelector">
    /// Expression indicating which method to return.
    /// </param>
    /// <typeparam name="TMethod">
    /// Delegate type of the method to get.
    /// </typeparam>
    /// <returns>
    /// A <see cref="MethodInfo"/> representing the selected method.
    /// </returns>
    public static MethodInfo GetMethod<TMethod>(Expression<Func<TMethod>> methodSelector) where TMethod : Delegate
    {
        return GetMember<MethodInfo, TMethod>(methodSelector);
    }

    /// <summary>
    /// Enumerates the properties of the specified type whose value type is
    /// <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of the properties to get.</typeparam>
    /// <param name="t">Type from which to enumerate the properties.</param>
    /// <param name="flags">
    /// Declaration flags to use for filtering the members to get.
    /// </param>
    /// <returns>
    /// An enumeration of the properties of the desired type contained within
    /// the specified type.
    /// </returns>
    public static IEnumerable<PropertyInfo> GetPropertiesOf<T>([DynamicallyAccessedMembers(PublicProperties | NonPublicProperties)] this Type t, BindingFlags flags)
    {
        return t.GetProperties(flags).Where(p => p.PropertyType.IsAssignableTo(typeof(T)));
    }

    /// <summary>
    /// Enumerates the properties of the specified type whose value type is
    /// <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of the properties to get.</typeparam>
    /// <param name="t">Type from which to enumerate the properties.</param>
    /// <returns>
    /// An enumeration of the properties of the desired type contained within
    /// the specified type.
    /// </returns>
    public static IEnumerable<PropertyInfo> GetPropertiesOf<T>([DynamicallyAccessedMembers(PublicProperties)] this Type t)
    {
        return t.GetProperties().Where(p => p.PropertyType.IsAssignableTo(typeof(T)));
    }

    /// <summary>
    /// Gets a reference to the property selected via an expression.
    /// </summary>
    /// <param name="propertySelector">
    /// Expression indicating which property to return.
    /// </param>
    /// <typeparam name="T">
    /// Type from which to select the property.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Type returned by the property.
    /// </typeparam>
    /// <returns>
    /// A <see cref="PropertyInfo"/> representing the selected property.
    /// </returns>
    public static PropertyInfo GetProperty<T, TValue>(Expression<Func<T, TValue>> propertySelector)
    {
        return GetMember<PropertyInfo, T, TValue>(propertySelector);
    }

    /// <summary>
    /// Gets a reference to the property selected via an expression.
    /// </summary>
    /// <param name="propertySelector">
    /// Expression indicating which property to return.
    /// </param>
    /// <typeparam name="T">
    /// Type from which to select the property.
    /// </typeparam>
    /// <returns>
    /// A <see cref="PropertyInfo"/> representing the selected property.
    /// </returns>
    public static PropertyInfo GetProperty<T>(Expression<Func<T, object?>> propertySelector)
    {
        return GetMember<PropertyInfo, T, object?>(propertySelector);
    }

    /// <summary>
    /// Gets a reference to the property selected via an expression.
    /// </summary>
    /// <param name="propertySelector">
    /// Expression indicating which property to return.
    /// </param>
    /// <typeparam name="TValue">
    /// Type returned by the property.
    /// </typeparam>
    /// <returns>
    /// A <see cref="PropertyInfo"/> representing the selected property.
    /// </returns>
    public static PropertyInfo GetProperty<TValue>(Expression<Func<TValue>> propertySelector)
    {
        return GetMember<PropertyInfo, TValue>(propertySelector);
    }

    /// <summary>
    /// Gets a list of types assignable from the specified interface or base class.
    /// </summary>
    /// <typeparam name="T">Interface or base class to search for.</typeparam>
    /// <returns>
    /// A list of types of classes that implement the interface or inherit from the base class
    /// <typeparamref name="T" /> within the current <see cref="AppDomain"/>.
    /// </returns>
    /// <remarks>
    /// This function gets all types (private and public) defined within all assemblies
    /// in the current domain. To get only publicly exported types, use
    /// <see cref="PublicTypes(Type)"/>, <see cref="PublicTypes(Type, AppDomain)"/>,
    /// <see cref="PublicTypes{T}()"/> or <see cref="PublicTypes{T}(AppDomain)"/>.
    /// </remarks>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static IEnumerable<Type> GetTypes<T>()
    {
        return typeof(T).GetDerivedTypes();
    }

    /// <summary>
    /// Gets a list of types assignable from the specified interface or base class
    /// within the specified <see cref="AppDomain"/>.
    /// </summary>
    /// <typeparam name="T">Interface or base class to search for.</typeparam>
    /// <param name="domain">
    /// <see cref="AppDomain"/> in which to perform the search.
    /// </param>
    /// <param name="instantiablesOnly">
    /// If set to <see langword="true"/>, only instantiable types will be included.
    /// <see langword="false"/> will return all matching types.
    /// </param>
    /// <returns>
    /// A list of types of classes that implement the interface or inherit from the base class
    /// <typeparamref name="T"/> within the <paramref name="domain"/>.
    /// </returns>
    /// <remarks>
    /// This function gets all types (private and public) defined within all assemblies
    /// in the specified domain. To get only publicly exported types, use
    /// <see cref="PublicTypes(Type)"/>, <see cref="PublicTypes(Type, AppDomain)"/>,
    /// <see cref="PublicTypes{T}()"/> or <see cref="PublicTypes{T}(AppDomain)"/>.
    /// </remarks>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static IEnumerable<Type> GetTypes<T>(AppDomain domain, in bool instantiablesOnly)
    {
        return domain.GetAssemblies().GetTypes<T>(instantiablesOnly);
    }

    /// <summary>
    /// Gets a list of types assignable from the specified interface or base class
    /// within the specified <see cref="AppDomain"/>.
    /// </summary>
    /// <typeparam name="T">Interface or base class to search for.</typeparam>
    /// <param name="domain">
    /// <see cref="AppDomain"/> in which to perform the search.
    /// </param>
    /// <returns>
    /// A list of types of classes that implement the interface or inherit from the base class
    /// <typeparamref name="T"/> within the <paramref name="domain"/>.
    /// </returns>
    /// <remarks>
    /// This function gets all types (private and public) defined within all assemblies
    /// in the specified domain. To get only publicly exported types, use
    /// <see cref="PublicTypes(Type)"/>, <see cref="PublicTypes(Type, AppDomain)"/>,
    /// <see cref="PublicTypes{T}()"/> or <see cref="PublicTypes{T}(AppDomain)"/>.
    /// </remarks>
    [Sugar]
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static IEnumerable<Type> GetTypes<T>(AppDomain domain)
    {
        return typeof(T).FindDerivedTypes(domain);
    }

    /// <summary>
    /// Gets a list of types assignable from the specified interface or base class.
    /// </summary>
    /// <typeparam name="T">Interface or base class to search for.</typeparam>
    /// <param name="instantiablesOnly">
    /// If set to <see langword="true"/>, only instantiable types will be included.
    /// <see langword="false"/> will return all matching types.
    /// </param>
    /// <returns>
    /// A list of types of classes that implement the interface or inherit from the base class
    /// <typeparamref name="T"/> within the current <see cref="AppDomain"/>.
    /// </returns>
    /// <remarks>
    /// This function gets all types (private and public) defined within all assemblies
    /// in the current domain. To get only publicly exported types, use
    /// <see cref="PublicTypes(Type)"/>, <see cref="PublicTypes(Type, AppDomain)"/>,
    /// <see cref="PublicTypes{T}()"/> or <see cref="PublicTypes{T}(AppDomain)"/>.
    /// </remarks>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static IEnumerable<Type> GetTypes<T>(bool instantiablesOnly)
    {
        return GetTypes<T>(AppDomain.CurrentDomain, instantiablesOnly);
    }

    /// <summary>
    /// Gets a list of assignable types from the specified interface or base class
    /// within the specified <see cref="AppDomain" />.
    /// </summary>
    /// <typeparam name="T">Interface or base class to search for.</typeparam>
    /// <param name="assemblies">
    /// Collection of assemblies to search in.
    /// </param>
    /// <param name="instantiablesOnly">
    /// If set to <see langword="true" />, only instantiable types are included.
    /// <see langword="false" /> returns all matching types.
    /// </param>
    /// <returns>
    /// A list of types of classes that implement the interface or inherit from
    /// the base class <typeparamref name="T" /> within the default domain.
    /// </returns>
    /// <remarks>
    /// This function gets all types (private and public) defined within all
    /// assemblies in the specified collection. To get only publicly exported
    /// types, use <see cref="PublicTypes(Type)"/>, 
    /// <see cref="PublicTypes(Type, AppDomain)"/>, 
    /// <see cref="PublicTypes{T}()"/> or 
    /// <see cref="PublicTypes{T}(AppDomain)"/>.
    /// </remarks>
    [RequiresUnreferencedCode(MethodScansForTypes)]
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
    /// Gets a list of assignable types from the specified interface or base class
    /// within the specified <see cref="AppDomain" />.
    /// </summary>
    /// <typeparam name="T">Interface or base class to search for.</typeparam>
    /// <param name="assemblies">
    /// Collection of assemblies to search in.
    /// </param>
    /// <returns>
    /// A list of types of classes that implement the interface or inherit from
    /// the base class <typeparamref name="T" /> within 
    /// <paramref name="assemblies" />.
    /// </returns>
    /// <remarks>
    /// This function gets all types (private and public) defined within all
    /// assemblies in the specified collection. To get only publicly exported
    /// types, use <see cref="PublicTypes(Type)"/>, 
    /// <see cref="PublicTypes(Type, AppDomain)"/>, 
    /// <see cref="PublicTypes{T}()"/> or 
    /// <see cref="PublicTypes{T}(AppDomain)"/>.
    /// </remarks>
    [Sugar]
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static IEnumerable<Type> GetTypes<T>(this IEnumerable<Assembly> assemblies)
    {
        return typeof(T).Derivates(assemblies);
    }

    /// <summary>
    /// Enumerates the value of all properties that return values of type
    /// <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">Type of properties to get.</typeparam>
    /// <param name="properties">
    /// Collection of properties to analyze.
    /// </param>
    /// <param name="instance">
    /// Instance from which to get the properties.
    /// </param>
    /// <returns>
    /// An enumeration of all values of type <typeparamref name="T" /> from the
    /// instance.
    /// </returns>
    public static IEnumerable<T> PropertiesOf<T>(this IEnumerable<PropertyInfo> properties, object? instance)
    {
        return properties
            .Where(p => p.CanRead && p.PropertyType.IsAssignableTo(typeof(T)))
            .Select(j => (T)j.GetMethod!.Invoke(instance, [])!);
    }

    /// <summary>
    /// Enumerates the value of all static properties that return values of type
    /// <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">Type of properties to get.</typeparam>
    /// <param name="properties">
    /// Collection of properties to analyze.
    /// </param>
    /// <returns>
    /// An enumeration of all values of type <typeparamref name="T" />.
    /// </returns>
    public static IEnumerable<T> PropertiesOf<T>(this IEnumerable<PropertyInfo> properties)
    {
        return PropertiesOf<T>(properties, null);
    }

    /// <summary>
    /// Gets all public types within the current <see cref="AppDomain"/>.
    /// </summary>
    /// <returns>
    /// An enumeration of all public types found in the current domain.
    /// </returns>
    /// <remarks>
    /// This function gets all public types exported from the current domain,
    /// excluding dynamic assemblies (generated using the 
    /// <see cref="System.Reflection.Emit"/> namespace). To get an 
    /// indiscriminate list of types, use <see cref="GetTypes{T}()"/>.
    /// </remarks>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static IEnumerable<Type> PublicTypes()
    {
        return PublicTypes(AppDomain.CurrentDomain);
    }

    /// <summary>
    /// Gets all public types within the specified <see cref="AppDomain"/>.
    /// </summary>
    /// <param name="domain">
    /// Application domain within which to search for types.
    /// </param>
    /// <returns>
    /// An enumeration of all public types found in the specified domain.
    /// </returns>
    /// <remarks>
    /// This function gets all public types exported from the specified domain,
    /// excluding dynamic assemblies (generated using the 
    /// <see cref="System.Reflection.Emit"/> namespace). To get an 
    /// indiscriminate list of types, use <see cref="GetTypes{T}()"/>.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="domain"/> is <see langword="null"/>.
    /// </exception>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static IEnumerable<Type> PublicTypes(AppDomain domain)
    {
        ArgumentNullException.ThrowIfNull(domain);
        return domain.GetAssemblies()
            .Where(p => !p.IsDynamic)
            .SelectMany(SafeGetExportedTypes);
    }

    /// <summary>
    /// Gets all public types that implement the specified type.
    /// </summary>
    /// <param name="type">Type to get.</param>
    /// <param name="domain">Application domain within which to search for types.</param>
    /// <returns>
    /// An enumeration of all types that inherit from or implement the specified type.
    /// </returns>
    /// <remarks>
    /// This function gets all public types exported from the specified domain,
    /// excluding dynamic assemblies (generated using the 
    /// <see cref="System.Reflection.Emit"/> namespace). To get an 
    /// indiscriminate list of types, use <see cref="GetTypes{T}()"/>.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="domain"/> is <see langword="null"/>.
    /// </exception>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static IEnumerable<Type> PublicTypes(Type type, AppDomain domain)
    {
        PublicTypes_Contract(type, domain);
        return PublicTypes(domain).Where(type.IsAssignableFrom);
    }

    /// <summary>
    /// Gets all public types that implement the specified type.
    /// </summary>
    /// <param name="type">Type to get.</param>
    /// <returns>
    /// An enumeration of all types that inherit from or implement the specified type.
    /// </returns>
    /// <remarks>
    /// This function gets all public types exported from the current domain,
    /// excluding dynamic assemblies (generated using the 
    /// <see cref="System.Reflection.Emit"/> namespace). To get an 
    /// indiscriminate list of types, use <see cref="GetTypes{T}()"/>.
    /// </remarks>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static IEnumerable<Type> PublicTypes(Type type)
    {
        return PublicTypes(type, AppDomain.CurrentDomain);
    }

    /// <summary>
    /// Gets all public types that implement the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// Type of objects to get.
    /// </typeparam>
    /// <returns>
    /// An enumeration of all types that inherit from or implement the specified type.
    /// </returns>
    /// <remarks>
    /// This function gets all public types exported from the current domain,
    /// excluding dynamic assemblies (generated using the 
    /// <see cref="System.Reflection.Emit"/> namespace). To get an 
    /// indiscriminate list of types, use <see cref="GetTypes{T}()"/>.
    /// </remarks>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static IEnumerable<Type> PublicTypes<T>()
    {
        return PublicTypes(typeof(T));
    }

    /// <summary>
    /// Gets all public types that implement the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// Type of objects to get.
    /// </typeparam>
    /// <param name="domain">Application domain within which to search for types.</param>
    /// <returns>
    /// An enumeration of all types that inherit from or implement the specified type.
    /// </returns>
    /// <remarks>
    /// This function gets all public types exported from the specified domain,
    /// excluding dynamic assemblies (generated using the 
    /// <see cref="System.Reflection.Emit"/> namespace). To get an 
    /// indiscriminate list of types, use <see cref="GetTypes{T}()"/>.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="domain"/> is <see langword="null"/>.
    /// </exception>
    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static IEnumerable<Type> PublicTypes<T>(AppDomain domain)
    {
        return PublicTypes(typeof(T), domain);
    }

    /// <summary>
    /// Gets all instance methods with a signature compatible with the specified delegate.
    /// </summary>
    /// <typeparam name="T">
    /// Delegate to use as the signature to check.
    /// </typeparam>
    /// <param name="methods">
    /// Collection of methods in which to search.
    /// </param>
    /// <param name="instance">
    /// Instance of the object on which to construct the delegates.
    /// </param>
    /// <returns>
    /// An enumeration of all methods that have a signature compatible with <typeparamref name="T" />.
    /// </returns>
    [RequiresUnreferencedCode(MethodCreatesDelegates)]
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
    /// Gets all static methods with a signature compatible with the specified delegate.
    /// </summary>
    /// <typeparam name="T">
    /// Delegate to use as the signature to check.
    /// </typeparam>
    /// <param name="methods">
    /// Collection of methods in which to search.
    /// </param>
    /// <returns>
    /// An enumeration of all methods that have a signature compatible with <typeparamref name="T" />.
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
}
