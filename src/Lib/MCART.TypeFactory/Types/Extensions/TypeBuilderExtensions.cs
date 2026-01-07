/*
TypeBuilderExtensions.cs

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

using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using TheXDS.MCART.Attributes;
using static System.Reflection.MethodAttributes;
using Errors = TheXDS.MCART.Resources.TypeFactoryErrors;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Provides useful extensions for generating members using the
/// <see cref="TypeBuilder"/> class.
/// </summary>
public static class TypeBuilderExtensions
{
    /// <summary>
    /// Initializes a new instance of the specified runtime type.
    /// </summary>
    /// <returns>The new instance of the specified type.</returns>
    /// <param name="tb">
    /// <see cref="TypeBuilder"/> from which to instantiate a new object.
    /// </param>
    [DebuggerStepThrough]
    [Sugar]
    public static object New(this TypeBuilder tb)
    {
        if (!tb.IsCreated()) tb.CreateType();
        return TypeExtensions.New(tb);
    }

    /// <summary>
    /// Determines whether the specified method can be overridden.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the method can be overridden; 
    /// <see langword="false"/> otherwise; or <see langword="null"/> if the
    /// method does not exist in the base class.
    /// </returns>
    /// <param name="tb">
    /// Type constructor on which to perform the query.
    /// </param>
    /// <param name="method">Name of the method to search for.</param>
    /// <param name="args">Types of the method's arguments.</param>
    public static bool? Overridable(this TypeBuilder tb, string method, params Type[] args)
    {
        MethodInfo? bm = tb.BaseType?.GetMethod(method, args);
        if (bm is null) return null;
        return bm.IsVirtual || bm.IsAbstract;
    }

    /// <summary>
    /// Adds an auto‑implemented property to the type.
    /// </summary>
    /// <param name="tb">
    /// <see cref="TypeBuilder"/> in which to create the new auto‑implemented
    /// property.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="type">Type of the new property.</param>
    /// <param name="access">Access level of the new property.</param>
    /// <param name="virtual">
    /// If <see langword="true"/>, the property will be defined as virtual and
    /// can be overridden in a derived class.
    /// </param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about the
    /// constructed property.
    /// </returns>
    public static PropertyBuildInfo AddAutoProperty(this TypeBuilder tb, string name, Type type, MemberAccess access, bool @virtual)
    {
        PropertyBuildInfo p = AddProperty(tb, name, type, true, access, @virtual).WithBackingField(out FieldBuilder? field);
        p.Setter!
            .SetField(field, il => il.LoadArg1())
            .Return();
        return new PropertyBuildInfo(tb, p.Member, field);
    }

    /// <summary>
    /// Adds an auto‑implemented property to the type.
    /// </summary>
    /// <param name="tb">
    /// <see cref="TypeBuilder"/> in which to create the new auto‑implemented
    /// property.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="type">Type of the new property.</param>
    /// <param name="access">Access level of the new property.</param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about the
    /// constructed property.
    /// </returns>
    public static PropertyBuildInfo AddAutoProperty(this TypeBuilder tb, string name, Type type, MemberAccess access)
    {
        return AddAutoProperty(tb, name, type, access, false);
    }

    /// <summary>
    /// Adds a public auto‑implemented property to the type.
    /// </summary>
    /// <param name="tb">
    /// <see cref="TypeBuilder"/> in which to create the new auto‑implemented
    /// property.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="type">Type of the new property.</param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about the
    /// constructed property.
    /// </returns>
    public static PropertyBuildInfo AddAutoProperty(this TypeBuilder tb, string name, Type type)
    {
        return AddAutoProperty(tb, name, type, MemberAccess.Public);
    }

    /// <summary>
    /// Adds a public auto‑implemented property to the type.
    /// </summary>
    /// <typeparam name="T">Type of the new property.</typeparam>
    /// <param name="tb">
    /// Type constructor in which to create the new auto‑implemented property.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about the
    /// constructed property.
    /// </returns>
    public static PropertyBuildInfo AddAutoProperty<T>(this TypeBuilder tb, string name)
    {
        return AddAutoProperty(tb, name, typeof(T));
    }

    /// <summary>
    /// Adds an override for the specified method.
    /// </summary>
    /// <param name="tb">
    /// Type constructor in which to create the override.
    /// </param>
    /// <param name="method">
    /// Method to override. Must exist in the base type.
    /// </param>
    /// <returns>
    /// A <see cref="MethodBuildInfo"/> representing the new method.
    /// </returns>
    public static MethodBuildInfo AddOverride(this TypeBuilder tb, MethodInfo method)
    {
        MethodBuilder newMethod = tb.DefineMethod(method.Name, GetNonAbstract(method), method.IsVoid() ? null : method.ReturnType, [.. method.GetParameters().Select(p => p.ParameterType)]);
        tb.DefineMethodOverride(newMethod, method);
        return new MethodBuildInfo(tb, newMethod);
    }

    /// <summary>
    /// Adds a property to the type without <see langword="get"/> or
    /// <see langword="set"/> implementations.
    /// </summary>
    /// <param name="tb">
    /// Type constructor in which to create the property.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="type">Type of the new property.</param>
    /// <param name="writable">
    /// <see langword="true"/> to create a property that includes a
    /// write accessor (<see langword="set"/>); <see langword="false"/> to omit
    /// the write accessor.
    /// </param>
    /// <param name="access">Access level of the new property.</param>
    /// <param name="virtual">
    /// If <see langword="true"/>, the property will be virtual and can be
    /// overridden in derived classes.
    /// </param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about the
    /// constructed property.
    /// </returns>
    /// <remarks>
    /// The generated property requires that the accessors be
    /// implemented before the type is built.
    /// </remarks>
    public static PropertyBuildInfo AddProperty(this TypeBuilder tb, string name, Type type, bool writable, MemberAccess access, bool @virtual)
    {
        return PropertyBuildInfo.Create(tb, name, type, writable, access, @virtual);
    }

    /// <summary>
    /// Adds a property to the type without <see langword="get"/> or
    /// <see langword="set"/> implementations.
    /// </summary>
    /// <typeparam name="T">Type of the new property.</typeparam>
    /// <param name="tb">
    /// Type constructor in which to create the property.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="writable">
    /// <see langword="true"/> to include a write accessor
    /// (<see langword="set"/>); <see langword="false"/> to omit it.
    /// </param>
    /// <param name="access">Access level of the new property.</param>
    /// <param name="virtual">
    /// If <see langword="true"/>, the property will be virtual and can be
    /// overridden in derived classes.
    /// </param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about the
    /// constructed property.
    /// </returns>
    /// <remarks>
    /// The generated property requires that the accessors be implemented
    /// before building the type.
    /// </remarks>
    public static PropertyBuildInfo AddProperty<T>(this TypeBuilder tb, string name, bool writable, MemberAccess access, bool @virtual)
    {
        return AddProperty(tb, name, typeof(T), writable, access, @virtual);
    }

    /// <summary>
    /// Adds a property to the type without implementations of get or set.
    /// </summary>
    /// <typeparam name="T">Type of the new property.</typeparam>
    /// <param name="tb">
    /// Type builder in which to create the new property.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="writable">
    /// <see langword="true"/> to create a property that contains a write
    /// accessor (set); <see langword="false"/> to omit the write accessor.
    /// </param>
    /// <param name="access">Access level of the new property.</param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about the
    /// property that has been built.
    /// </returns>
    /// <remarks>
    /// The generated property requires that the accessors be implemented
    /// before the type is built.
    /// </remarks>
    public static PropertyBuildInfo AddProperty<T>(this TypeBuilder tb, string name, bool writable, MemberAccess access)
    {
        return AddProperty(tb, name, typeof(T), writable, access, false);
    }

    /// <summary>
    /// Adds a property to the type without implementations of get or set.
    /// </summary>
    /// <typeparam name="T">Type of the new property.</typeparam>
    /// <param name="tb">
    /// Type builder in which to create the new property.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="writable">
    /// <see langword="true"/> to create a property that contains a write
    /// accessor (set); <see langword="false"/> to omit the write accessor.
    /// </param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about the
    /// property that has been built.
    /// </returns>
    /// <remarks>
    /// The generated property requires that the accessors be implemented
    /// before the type is built.
    /// </remarks>
    public static PropertyBuildInfo AddProperty<T>(this TypeBuilder tb, string name, bool writable)
    {
        return AddProperty(tb, name, typeof(T), writable, MemberAccess.Public, false);
    }

    /// <summary>
    /// Adds a property to the type without implementations of get or set.
    /// </summary>
    /// <typeparam name="T">Type of the new property.</typeparam>
    /// <param name="tb">
    /// Type builder in which to create the new property.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about the
    /// property that has been built.
    /// </returns>
    /// <remarks>
    /// The generated property requires that the accessors be implemented
    /// before the type is built.
    /// </remarks>
    public static PropertyBuildInfo AddProperty<T>(this TypeBuilder tb, string name)
    {
        return AddProperty(tb, name, typeof(T), true, MemberAccess.Public, false);
    }

    /// <summary>
    /// Adds a computed property to the type.
    /// </summary>
    /// <typeparam name="T">Type of the new property.</typeparam>
    /// <param name="tb">
    /// Type builder in which to create the new property.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="getterDefinition">
    /// Action that implements the instructions to be executed inside the
    /// get accessor of the computed property.
    /// </param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about the
    /// property that has been built.
    /// </returns>
    public static PropertyBuildInfo AddComputedProperty<T>(this TypeBuilder tb, string name, Action<ILGenerator> getterDefinition)
    {
        return AddComputedProperty(tb, name, typeof(T), getterDefinition);
    }

    /// <summary>
    /// Adds a computed property to the type.
    /// </summary>
    /// <param name="tb">
    /// Type builder in which to create the new property.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="type">Type of the new property.</param>
    /// <param name="getterDefinition">
    /// Action that implements the instructions to be executed inside
    /// the get accessor of the computed property.
    /// </param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about
    /// the property that has been built.
    /// </returns>
    public static PropertyBuildInfo AddComputedProperty(this TypeBuilder tb, string name, Type type, Action<ILGenerator> getterDefinition)
    {
        PropertyBuildInfo prop = AddProperty(tb, name, type, false, MemberAccess.Public, false);
        getterDefinition(prop.Getter!);
        return prop;
    }

    /// <summary>
    /// Adds a property with a constant value.
    /// </summary>
    /// <typeparam name="T">Type of the new property.</typeparam>
    /// <param name="tb">
    /// Type builder in which to create the new property.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="value">Constant value to assign.</param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about
    /// the property that has been built.
    /// </returns>
    public static PropertyBuildInfo AddConstantProperty<T>(this TypeBuilder tb, string name, T value)
    {
        return AddConstantProperty(tb, name, typeof(T), value);
    }

    /// <summary>
    /// Adds a property with a constant value.
    /// </summary>
    /// <param name="tb">
    /// Type builder in which to create the new property.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="type">Type of the new property.</param>
    /// <param name="value">Constant value to assign.</param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about
    /// the property that has been built.
    /// </returns>
    public static PropertyBuildInfo AddConstantProperty(this TypeBuilder tb, string name, Type type, object? value)
    {
        PropertyBuildInfo prop = AddProperty(tb, name, type, false, MemberAccess.Public, false);
        prop.Getter!.LoadConstant(type, value).Return();
        return prop;
    }

    /// <summary>
    /// Adds a public write‑only property to the type.
    /// </summary>
    /// <param name="tb">
    /// Type builder in which to create the new property.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="type">Type of the new property.</param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about
    /// the property that has been built.
    /// </returns>
    public static PropertyBuildInfo AddWriteOnlyProperty(this TypeBuilder tb, string name, Type type)
    {
        return AddWriteOnlyProperty(tb, name, type, MemberAccess.Public, false);
    }

    /// <summary>
    /// Adds a write‑only property to the type.
    /// </summary>
    /// <param name="tb">
    /// Type builder in which to create the new property.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="type">Type of the new property.</param>
    /// <param name="access">
    /// Access level of the new property.
    /// </param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about
    /// the property that has been built.
    /// </returns>
    public static PropertyBuildInfo AddWriteOnlyProperty(this TypeBuilder tb, string name, Type type, MemberAccess access)
    {
        return AddWriteOnlyProperty(tb, name, type, access, false);
    }

    /// <summary>
    /// Adds a public write‑only property to the type.
    /// </summary>
    /// <param name="tb">
    /// Type builder in which to create the new property.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="type">Type of the new property.</param>
    /// <param name="virtual">
    /// If set to <see langword="true"/>, the property will be
    /// declared virtual, allowing it to be overridden in a derived
    /// class.
    /// </param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about
    /// the property that has been built.
    /// </returns>
    public static PropertyBuildInfo AddWriteOnlyProperty(this TypeBuilder tb, string name, Type type, bool @virtual)
    {
        return AddWriteOnlyProperty(tb, name, type, MemberAccess.Public, @virtual);
    }

    /// <summary>
    /// Adds a write‑only property to the type.
    /// </summary>
    /// <param name="tb">
    /// TypeBuilder in which the new property will be created.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="type">Type of the new property.</param>
    /// <param name="access">Access level of the new property.</param>
    /// <param name="virtual">
    /// If set to <see langword="true"/>, the property is defined as virtual,
    /// so it can be overridden in a derived class.
    /// </param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about
    /// the property that has been built.
    /// </returns>
    public static PropertyBuildInfo AddWriteOnlyProperty(this TypeBuilder tb, string name, Type type, MemberAccess access, bool @virtual)
    {
        return PropertyBuildInfo.CreateWriteOnly(tb, name, type, access, @virtual);
    }

    /// <summary>
    /// Explicitly inserts a public parameterless constructor into the type.
    /// </summary>
    /// <param name="tb">
    /// <see cref="TypeBuilder"/> in which the new constructor will be defined.
    /// </param>
    /// <returns>
    /// An <see cref="ILGenerator"/> that allows defining the constructor's instructions.
    /// </returns>
    public static ILGenerator AddPublicConstructor(this TypeBuilder tb) => AddPublicConstructor(tb, Type.EmptyTypes);

    /// <summary>
    /// Explicitly inserts a public constructor into the type,
    /// specifying the required arguments.
    /// </summary>
    /// <param name="tb">
    /// <see cref="TypeBuilder"/> in which the new constructor will be defined.
    /// </param>
    /// <param name="arguments">
    /// Array of types of arguments accepted by the new constructor.
    /// </param>
    /// <returns>
    /// An <see cref="ILGenerator"/> that allows defining the constructor's instructions.
    /// </returns>
    public static ILGenerator AddPublicConstructor(this TypeBuilder tb, params Type[] arguments)
    {
        return tb.DefineConstructor(Public | SpecialName | HideBySig | RTSpecialName, CallingConventions.HasThis, arguments).GetILGenerator();
    }

    /// <summary>
    /// Explicitly implements an abstract method.
    /// </summary>
    /// <param name="tb">
    /// <see cref="TypeBuilder"/> in which the abstract method will be explicitly implemented.
    /// </param>
    /// <param name="method">
    /// Abstract method to implement. The specified TypeBuilder must
    /// implement the interface in which the method is defined.
    /// </param>
    /// <returns>
    /// A <see cref="MethodBuildInfo"/> that contains information about
    /// the method that has been explicitly implemented.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="tb"/> does not implement or inherit the interface
    /// in which <paramref name="method"/> is defined, or if <paramref name="method"/>
    /// is not a member defined in an interface.
    /// </exception>
    public static MethodBuildInfo ExplicitImplementMethod(this TypeBuilder tb, MethodInfo method)
    {
        if (!method.DeclaringType?.IsInterface ?? true) throw Errors.IFaceMethodExpected();
        if (!tb.GetInterfaces().Contains(method.DeclaringType!)) throw Errors.InterfaceNotImplemented(method.DeclaringType!);

        MethodBuilder m = tb.DefineMethod($"{method.DeclaringType!.Name}.{method.Name}",
            Private | HideBySig | NewSlot | Virtual | Final,
            method.IsVoid() ? null : method.ReturnType,
            method.GetParameters().Select(p => p.ParameterType).ToArray());
        tb.DefineMethodOverride(m, method);

        return new MethodBuildInfo(tb, m);
    }

    /// <summary>
    /// Defines a method that returns no value.
    /// </summary>
    /// <param name="tb">
    /// <see cref="TypeBuilder"/> in which the new method will be created.
    /// </param>
    /// <param name="name">Name of the new method.</param>
    /// <param name="parameterTypes">
    /// Type of the parameters accepted by the method.
    /// </param>
    /// <returns>
    /// A <see cref="MethodBuilder"/> with which the method can be defined.
    /// </returns>
    [Sugar]
    public static MethodBuilder DefineVoidMethod(this TypeBuilder tb, string name, params Type[] parameterTypes)
    {
        return tb.DefineMethod(name, Public, typeof(void), parameterTypes);
    }

    /// <summary>
    /// Defines a method that returns a specified type.
    /// </summary>
    /// <typeparam name="TResult">
    /// Result type returned by the method.
    /// </typeparam>
    /// <param name="tb">
    /// <see cref="TypeBuilder"/> in which the new method will be created.
    /// </param>
    /// <param name="name">Name of the new method.</param>
    /// <param name="parameterTypes">
    /// Types of the parameters accepted by the method.
    /// </param>
    /// <returns>
    /// A <see cref="MethodBuilder"/> with which the method can be defined.
    /// </returns>
    [Sugar]
    public static MethodBuilder DefineMethod<TResult>(this TypeBuilder tb, string name, params Type[] parameterTypes)
    {
        return tb.DefineMethod(name, Public, typeof(TResult), parameterTypes);
    }

    /// <summary>
    /// Adds an event to the type.
    /// </summary>
    /// <param name="builder">
    /// <see cref="TypeBuilder"/> in which the new event and its required helper
    /// methods will be created.
    /// </param>
    /// <param name="name">Name of the new event.</param>
    /// <returns>
    /// An <see cref="EventBuildInfo"/> that contains information about the event
    /// that has been defined.
    /// </returns>
    public static EventBuildInfo AddEvent(this TypeBuilder builder, string name)
    {
        return AddEvent<EventHandler, EventArgs>(builder, name);
    }

    /// <summary>
    /// Adds an event to the type.
    /// </summary>
    /// <typeparam name="TEventArgs">
    /// Type of event arguments to pass when the event occurs.
    /// </typeparam>
    /// <param name="builder">
    /// <see cref="TypeBuilder"/> in which the new event and its required helper
    /// methods will be created.
    /// </param>
    /// <param name="name">Name of the new event.</param>
    /// <returns>
    /// An <see cref="EventBuildInfo"/> that contains information about the event
    /// that has been defined.
    /// </returns>
    public static EventBuildInfo AddEvent<TEventArgs>(this TypeBuilder builder, string name) where TEventArgs : EventArgs
    {
        return AddEvent<EventHandler<TEventArgs>, TEventArgs>(builder, name);
    }

    /// <summary>
    /// Adds an event to the type.
    /// </summary>
    /// <typeparam name="TEventHandler">
    /// Event handler delegate. Must follow the standard event handler signature,
    /// i.e. a <see langword="void"/>-returning method that takes an
    /// <see cref="object"/> sender and a <typeparamref name="TEventArgs"/> argument.
    /// </typeparam>
    /// <typeparam name="TEventArgs">
    /// Type of event arguments to pass when the event occurs.
    /// </typeparam>
    /// <param name="builder">
    /// <see cref="TypeBuilder"/> in which the new event and its required helper
    /// methods will be created.
    /// </param>
    /// <param name="name">Name of the new event.</param>
    /// <returns>
    /// An <see cref="EventBuildInfo"/> that contains information about the event
    /// that has been defined.
    /// </returns>
    public static EventBuildInfo AddEvent<TEventHandler, TEventArgs>(this TypeBuilder builder, string name) where TEventHandler : Delegate where TEventArgs : EventArgs
    {
        return AddEvent<TEventHandler, object, TEventArgs>(builder, name);
    }

    /// <summary>
    /// Adds an event to the type.
    /// </summary>
    /// <typeparam name="TEventHandler">
    /// Event handler delegate. Must follow the standard event handler signature,
    /// i.e. a <see langword="void"/>-returning method that takes a <typeparamref
    /// name="TSender"/> (typically <see cref="object"/>) and a
    /// <typeparamref name="TEventArgs"/> argument.
    /// </typeparam>
    /// <typeparam name="TSender">
    /// Type of the object that raises the event. Defaults to <see cref="object"/>.
    /// </typeparam>
    /// <typeparam name="TEventArgs">
    /// Type of event arguments to pass when the event occurs.
    /// </typeparam>
    /// <param name="builder">
    /// <see cref="TypeBuilder"/> in which the new event and its required helper
    /// methods will be created.
    /// </param>
    /// <param name="name">Name of the new event.</param>
    /// <returns>
    /// An <see cref="EventBuildInfo"/> that contains information about the event
    /// that has been defined.
    /// </returns>
    public static EventBuildInfo AddEvent<TEventHandler, TSender, TEventArgs>(this TypeBuilder builder, string name) where TEventHandler : Delegate where TEventArgs : EventArgs
    {
        return EventBuildInfo.Create<TEventHandler, TSender, TEventArgs>(builder, name);
    }

    private static MethodAttributes GetNonAbstract(MethodInfo m)
    {
        int a = (int)m.Attributes;
        a &= ~(int)Abstract;
        a |= (int)Virtual;
        return (MethodAttributes)a;
    }
}
