/*
TypeFactoryVmExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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

using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Includes a set of extensions for the <see cref="TypeFactory"/> class.
/// </summary>
[RequiresDynamicCode(AttributeErrorMessages.ClassHeavilyUsesReflection), RequiresUnreferencedCode(AttributeErrorMessages.ClassHeavilyUsesReflection)]
public static class TypeFactoryVmExtensions
{
    /// <summary>
    /// Generates a new public class that implements the ViewModel pattern
    /// through the <see cref="NotifyPropertyChanged"/> base class, and
    /// includes all public properties from the specified
    /// <typeparamref name="TModel"/> type as properties with change
    /// notification support.
    /// </summary>
    /// <typeparam name="TModel">
    /// Model template type to create a ViewModel for.
    /// </typeparam>
    /// <param name="factory">
    /// Type factory to use when creating the new type.
    /// </param>
    /// <param name="interfaces">
    /// Collection of additional interfaces to be implemented by the new type.
    /// Can be set to <see langword="null"/> or an empty collection to denote
    /// that the new type does not require to implement any additional
    /// interfaces.
    /// </param>
    /// <returns>
    /// A new <see cref="TypeBuilder"/> that can be used to define any
    /// additional members of the newly created type.
    /// </returns>
    public static ITypeBuilder<INotifyPropertyChanged> CreateNpcClass<TModel>(this TypeFactory factory, IEnumerable<Type>? interfaces)
        where TModel : notnull, new()
    {
        ITypeBuilder<NotifyPropertyChanged> t = factory.NewType<NotifyPropertyChanged>($"{typeof(TModel).Name}Npc", interfaces);
        foreach (var p in typeof(TModel).GetProperties().Where(p => p.CanRead && p.CanWrite))
        {
            ((ITypeBuilder<NotifyPropertyChangeBase>)t).AddNpcProperty(p.Name, p.PropertyType);
        }
        return t;
    }

    /// <summary>
    /// Generates a new public class that implements the ViewModel pattern
    /// through the <see cref="NotifyPropertyChanged"/> base class, and
    /// includes all public properties from the specified
    /// <typeparamref name="TModel"/> type as properties with change
    /// notification support.
    /// </summary>
    /// <typeparam name="TModel">
    /// Model template type to create a ViewModel for.
    /// </typeparam>
    /// <param name="factory">
    /// Type factory to use when creating the new type.
    /// </param>
    /// <returns>
    /// A new <see cref="TypeBuilder"/> that can be used to define any
    /// additional members of the newly created type.
    /// </returns>
    public static ITypeBuilder<INotifyPropertyChanged> CreateNpcClass<TModel>(this TypeFactory factory)
        where TModel : notnull, new()
    {
        return CreateNpcClass<TModel>(factory, null);
    }

    /// <summary>
    /// Generates a new <see cref="EntityViewModel{T}"/> implementation
    /// defining the entity property type as <typeparamref name="TModel"/>.
    /// </summary>
    /// <typeparam name="TModel">
    /// Type of the <see cref="EntityViewModel{T}.Entity"/> property.
    /// </typeparam>
    /// <param name="factory">
    /// Type factory to use when creating the new type.
    /// </param>
    /// <param name="interfaces">
    /// Collection of additional interfaces to be implemented by the new type.
    /// Can be set to <see langword="null"/> or an empty collection to denote
    /// that the new type does not require to implement any additional
    /// interfaces.
    /// </param>
    /// <returns>
    /// A new <see cref="ITypeBuilder{T}"/> that can be used to define any
    /// additional members of the newly created type.
    /// </returns>
    /// <remarks>
    /// The exposed properties will write back their values to the entity
    /// directly, and then trigger the
    /// <see cref="INotifyPropertyChanged.PropertyChanged"/> event with the
    /// appropriate event args.
    /// </remarks>
    public static ITypeBuilder<EntityViewModel<TModel>> CreateEntityViewModelClass<TModel>(this TypeFactory factory, IEnumerable<Type>? interfaces)
        where TModel : notnull, new()
    {
        ITypeBuilder<EntityViewModel<TModel>> t = factory.NewType<EntityViewModel<TModel>>($"{typeof(TModel).Name}ViewModel", interfaces);
        PropertyInfo e = ReflectionHelpers.GetProperty<EntityViewModel<TModel>>(p => p.Entity);
        MethodInfo nm = typeof(EntityViewModel<TModel>).GetMethod("RaisePropertyChangeEvent", BindingFlags.NonPublic | BindingFlags.Instance, [typeof(string), typeof(PropertyChangeNotificationType)]) ?? throw new MissingMemberException();

        foreach (var p in typeof(TModel).GetProperties().Where(p => p.CanRead && p.CanWrite))
        {
            var prop = t.Builder.AddProperty(p.Name, p.PropertyType, true, MemberAccess.Public, false);
            prop.Getter!
                .LoadProperty(e)
                .Call(p.GetMethod!)
                .Return();
            prop.BuildNpcPropSetterSkeleton(
                (l, v) => v
                    .LoadProperty(e)
                    .BranchFalse(l)
                    .LoadProperty(e)
                    .Call(p.GetMethod!),
                (l, v) => v
                    .LoadProperty(e)
                    .BranchFalse(l)
                    .LoadProperty(e)
                    .LoadArg1()
                    .Call(p.SetMethod!)
                    .LoadArg0()
                    .LoadConstant(p.Name)
                    .LoadConstant(PropertyChangeNotificationType.PropertyChanged)
                    .Call(nm),
                p.PropertyType);
        }
        return t;
    }

    /// <summary>
    /// Generates a new <see cref="EntityViewModel{T}"/> implementation
    /// defining the entity property type as <typeparamref name="TModel"/>.
    /// </summary>
    /// <typeparam name="TModel">
    /// Type of the <see cref="EntityViewModel{T}.Entity"/> property.
    /// </typeparam>
    /// <param name="factory">
    /// Type factory to use when creating the new type.
    /// </param>
    /// <returns>
    /// A new <see cref="ITypeBuilder{T}"/> that can be used to define any
    /// additional members of the newly created type.
    /// </returns>
    /// <remarks>
    /// The exposed properties will write back their values to the entity
    /// directly, and then trigger the
    /// <see cref="INotifyPropertyChanged.PropertyChanged"/> event with the
    /// appropriate event args.
    /// </remarks>
    public static ITypeBuilder<EntityViewModel<TModel>> CreateEntityViewModelClass<TModel>(this TypeFactory factory)
        where TModel : notnull, new()
    {
        return CreateEntityViewModelClass<TModel>(factory, null);
    }

    /// <summary>
    /// Adds a new public property with change notification support to the
    /// type.
    /// </summary>
    /// <typeparam name="T">
    /// Type of value stored/exposed by the new property.
    /// </typeparam>
    /// <param name="tb">
    /// Type builder onto which to create the new property.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <returns>
    /// A new <see cref="PropertyBuildInfo"/> that includes information on the
    /// newly created property.
    /// </returns>
    /// <remarks>
    /// The properties generated by this method will include a public getter
    /// and setter.
    /// </remarks>
    public static PropertyBuildInfo AddNpcProperty<T>(this ITypeBuilder<NotifyPropertyChangeBase> tb, string name)
    {
        return AddNpcProperty(tb, name, typeof(T));
    }

    /// <summary>
    /// Adds a new public property with change notification support to the
    /// type.
    /// </summary>
    /// <typeparam name="T">
    /// Type of value stored/exposed by the new property.
    /// </typeparam>
    /// <param name="tb">
    /// Type builder onto which to create the new property.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <returns>
    /// A new <see cref="PropertyBuildInfo"/> that includes information on the
    /// newly created property.
    /// </returns>
    /// <remarks>
    /// The properties generated by this method will include a public getter
    /// and setter.
    /// </remarks>
    [Sugar]
    public static PropertyBuildInfo AddNpcProperty<T>(this ITypeBuilder<NotifyPropertyChanged> tb, string name)
    {
        return AddNpcProperty((ITypeBuilder<NotifyPropertyChangeBase>)tb, name, typeof(T));
    }

    /// <summary>
    /// Adds a new public property with change notification support to the
    /// type.
    /// </summary>
    /// <param name="tb">
    /// Type builder onto which to create the new property.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="type">
    /// Type of value stored/exposed by the new property.
    /// </param>
    /// <returns>
    /// A new <see cref="PropertyBuildInfo"/> that includes information on the
    /// newly created property.
    /// </returns>
    /// <remarks>
    /// The properties generated by this method will include a public getter
    /// and setter.
    /// </remarks>
    public static PropertyBuildInfo AddNpcProperty(this ITypeBuilder<NotifyPropertyChangeBase> tb, string name, Type type)
    {
        return AddNpcProperty(tb, name, type, MemberAccess.Public);
    }

    /// <summary>
    /// Adds a new public property with change notification support to the
    /// type.
    /// </summary>
    /// <param name="tb">
    /// Type builder onto which to create the new property.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="type">
    /// Type of value stored/exposed by the new property.
    /// </param>
    /// <param name="access">Level of access for the getter and setter of the property.</param>
    /// <returns>
    /// A new <see cref="PropertyBuildInfo"/> that includes information on the
    /// newly created property.
    /// </returns>
    /// <remarks>
    /// The properties generated by this method will include a public getter
    /// and setter.
    /// </remarks>
    public static PropertyBuildInfo AddNpcProperty(this ITypeBuilder<NotifyPropertyChangeBase> tb, string name, Type type, MemberAccess access)
    {
        return AddNpcProperty(tb, name, type, access, false);
    }

    /// <summary>
    /// Adds a property with support for change notification of
    /// value.
    /// </summary>
    /// <param name="tb">
    /// The type builder in which to create the new property with
    /// support for change notification of value.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="type">Type of the new property.</param>
    /// <param name="access">Access level of the new property.</param>
    /// <param name="virtual">
    /// If set to <see langword="true"/>, the property will be
    /// defined as virtual, allowing it to be overridden in a
    /// derived class.
    /// </param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about
    /// the property that has been built.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty(this ITypeBuilder<NotifyPropertyChangeBase> tb, string name, Type type, MemberAccess access, bool @virtual)
    {
        CheckImplements<NotifyPropertyChangeBase>(tb);
        PropertyBuildInfo? p = tb.Builder.AddProperty(name, type, true, access, @virtual).WithBackingField(out var field);
        p.Setter!
            .LoadArg0()
            .LoadFieldAddress(field)
            .LoadArg1()
            .LoadConstant(name)
            .Call(GetNpcChangeMethod(tb, type))
            .Pop()
            .Return();
        return new PropertyBuildInfo(tb.Builder, p.Member, field);
    }

    /// <summary>
    /// Adds a public property with support for change notification of
    /// value.
    /// </summary>
    /// <typeparam name="T">Type of the new property.</typeparam>
    /// <param name="tb">
    /// The type builder in which to create the new property with
    /// support for change notification of value.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about
    /// the property that has been built.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty<T>(this ITypeBuilder<INotifyPropertyChanged> tb, string name)
    {
        return AddNpcProperty(tb, name, typeof(T));
    }

    /// <summary>
    /// Adds a public property with support for change notification of
    /// value.
    /// </summary>
    /// <param name="tb">
    /// The type builder in which to create the new property with
    /// support for change notification of value.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="type">Type of the new property.</param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about
    /// the property that has been built.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty(this ITypeBuilder<INotifyPropertyChanged> tb, string name, Type type)
    {
        return AddNpcProperty(tb, name, type, MemberAccess.Public);
    }

    /// <summary>
    /// Adds a property with support for change notification of
    /// value.
    /// </summary>
    /// <param name="tb">
    /// The type builder in which to create the new property with
    /// support for change notification of value.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="type">Type of the new property.</param>
    /// <param name="access">Access level of the new property.</param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about
    /// the property that has been built.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty(this ITypeBuilder<INotifyPropertyChanged> tb, string name, Type type, MemberAccess access)
    {
        return AddNpcProperty(tb, name, type, access, false);
    }

    /// <summary>
    /// Adds a property with support for change notification of
    /// value.
    /// </summary>
    /// <param name="tb">
    /// The type builder in which to create the new property with
    /// support for change notification of value.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="type">Type of the new property.</param>
    /// <param name="access">Access level of the new property.</param>
    /// <param name="virtual">
    /// If set to <see langword="true"/>, the property will be
    /// defined as virtual, allowing it to be overridden in a
    /// derived class.
    /// </param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about
    /// the property that has been built.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty(this ITypeBuilder<INotifyPropertyChanged> tb, string name, Type type, MemberAccess access, bool @virtual)
    {
        return AddNpcProperty(tb, name, type, access, @virtual, null);
    }

    /// <summary>
    /// Adds a property with support for change notification of
    /// value.
    /// </summary>
    /// <param name="tb">
    /// The type builder in which to create the new property with
    /// support for change notification of value.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="type">Type of the new property.</param>
    /// <param name="access">Access level of the new property.</param>
    /// <param name="virtual">
    /// If set to <see langword="true"/>, the property will be
    /// defined as virtual, allowing it to be overridden in a
    /// derived class.
    /// </param>
    /// <param name="npcInvocator">
    /// Method that invokes the event handler for types built
    /// that directly implement <see cref="INotifyPropertyChanged"/>.
    /// If omitted or set to <see langword="null"/>
    /// (<see langword="Nothing"/> in Visual Basic), a field
    /// containing a reference to a
    /// <see cref="PropertyChangedEventHandler"/> will be searched
    /// for in the base type of the type
    /// constructed through <paramref name="tb"/>.
    /// </param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about
    /// the property that has been built.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty(this ITypeBuilder<INotifyPropertyChanged> tb, string name, Type type, MemberAccess access, bool @virtual, MethodInfo? npcInvocator)
    {
        npcInvocator ??= tb.SpecificBaseType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).First(p => p.IsVoid() && p.GetParameters().Length == 1 && p.GetParameters()[0].ParameterType == typeof(string) && p.HasAttribute<NpcChangeInvocatorAttribute>());
        return BuildNpcProp(tb.Builder, name, type, access, @virtual, (_, setter) => setter.LoadArg0().LoadConstant(name), npcInvocator);
    }

    /// <summary>
    /// Adds a property with support for change notification of
    /// value.
    /// </summary>
    /// <param name="tb">
    /// The type builder in which to create the new property with
    /// support for change notification of value.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="type">Type of the new property.</param>
    /// <param name="access">Access level of the new property.</param>
    /// <param name="npcInvocator">
    /// Method that invokes the event handler for types built
    /// that directly implement <see cref="INotifyPropertyChanged"/>.
    /// If omitted or set to <see langword="null"/>
    /// (<see langword="Nothing"/> in Visual Basic), a field
    /// containing a reference to a
    /// <see cref="PropertyChangedEventHandler"/> will be searched
    /// for in the base type of the type
    /// constructed through <paramref name="tb"/>.
    /// </param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about
    /// the property that has been built.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty(this ITypeBuilder<INotifyPropertyChanged> tb, string name, Type type, MemberAccess access, MethodInfo? npcInvocator)
    {
        return AddNpcProperty(tb, name, type, access, false, npcInvocator);
    }

    /// <summary>
    /// Adds a property with support for change notification of
    /// value.
    /// </summary>
    /// <param name="tb">
    /// The type builder in which to create the new property with
    /// support for change notification of value.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="type">Type of the new property.</param>
    /// <param name="npcInvocator">
    /// Method that invokes the event handler for types built
    /// that directly implement <see cref="INotifyPropertyChanged"/>.
    /// If omitted or set to <see langword="null"/>
    /// (<see langword="Nothing"/> in Visual Basic), a field
    /// containing a reference to a
    /// <see cref="PropertyChangedEventHandler"/> will be searched
    /// for in the base type of the type
    /// constructed through <paramref name="tb"/>.
    /// </param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about
    /// the property that has been built.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty(this ITypeBuilder<INotifyPropertyChanged> tb, string name, Type type, MethodInfo? npcInvocator)
    {
        return AddNpcProperty(tb, name, type, MemberAccess.Public, false, npcInvocator);
    }

    /// <summary>
    /// Adds a property with support for change notification of
    /// value.
    /// </summary>
    /// <param name="tb">
    /// The type builder in which to create the new property with
    /// support for change notification of value.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="npcInvocator">
    /// Method that invokes the event handler for types built
    /// that directly implement <see cref="INotifyPropertyChanged"/>.
    /// If omitted or set to <see langword="null"/>
    /// (<see langword="Nothing"/> in Visual Basic), a field
    /// containing a reference to a
    /// <see cref="PropertyChangedEventHandler"/> will be searched
    /// for in the base type of the type
    /// constructed through <paramref name="tb"/>.
    /// </param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information about
    /// the property that has been built.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty<T>(this ITypeBuilder<INotifyPropertyChanged> tb, string name, MethodInfo? npcInvocator)
    {
        return AddNpcProperty(tb, name, typeof(T), MemberAccess.Public, false, npcInvocator);
    }

    private static readonly Func<Type, TypeBuilder, bool>[] CheckImplementsConditions =
    [
        (t, tb) => tb.BaseType?.Implements(t)?? false,
        (t, tb) => tb.GetInterfaces().Any(i => i == t || i.Implements(t)),
    ];

    private static PropertyBuildInfo BuildNpcProp(TypeBuilder tb, string name, Type t, MemberAccess access, bool @virtual, Action<Label, ILGenerator> evtHandler, MethodInfo method)
    {
        CheckImplements<INotifyPropertyChanged>(tb);
        PropertyBuildInfo? p = tb.AddProperty(name, t, true, access, @virtual).WithBackingField(out FieldBuilder? field);
        p.BuildNpcPropSetterSkeleton(
            (_, v) => v.GetField(field),
            (l, v) =>
            {
                v.SetField(field, il => il.LoadArg1());
                evtHandler(l, p.Setter!);
                v.Call(method);
            }, t);
        return new PropertyBuildInfo(tb, p.Member, field);
    }

    [DebuggerNonUserCode]
    private static void CheckImplements<T>(TypeBuilder t)
    {
        if (!CheckImplementsConditions.Any(p => p.Invoke(typeof(T), t))) throw Errors.InterfaceNotImplemented<T>();
    }

    [DebuggerNonUserCode]
    private static void CheckImplements<T>(ITypeBuilder<object> t)
    {
        if (!t.SpecificBaseType.Implements(typeof(T))) throw Errors.InterfaceNotImplemented<T>();
    }

    private static MethodInfo GetNpcChangeMethod(ITypeBuilder<NotifyPropertyChangeBase> tb, Type t)
    {
        return tb.ActualBaseType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(MemberInfoExtensions.HasAttribute<NpcChangeInvocatorAttribute>)?.MakeGenericMethod([t]) ?? throw new MissingMethodException();
    }
}
