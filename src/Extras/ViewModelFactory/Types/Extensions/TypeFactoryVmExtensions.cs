/*
TypeFactoryVmExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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
using System.Reflection;
using System.Reflection.Emit;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contiene extensiones para la clase <see cref="TypeFactory"/>.
/// </summary>
public static class TypeFactoryVmExtensions
{
    /// <summary>
    /// Crea una nueva clase pública que implementa el patrón ViewModel por
    /// medio de una clase base <see cref="NotifyPropertyChanged"/>, y que
    /// incluirá todas las propiedades del tipo <typeparamref name="TModel"/>
    /// como propiedades con notificación de cambio de valor.
    /// </summary>
    /// <typeparam name="TModel">
    /// Modelo para el cual crear la clase con patrón ViewModel.
    /// </typeparam>
    /// <param name="factory">
    /// Fábrica de tipos a utilizar para crear la nueva clase.
    /// </param>
    /// <param name="interfaces">
    /// Colección de interfaces adicionales a implementar por el tipo final.
    /// </param>
    /// <returns>
    /// Un <see cref="TypeBuilder"/> por medio del cual se podrá definir a
    /// los miembros de la nueva clase.
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
    /// Crea una nueva clase pública que implementa el patrón ViewModel por
    /// medio de una clase base <see cref="NotifyPropertyChanged"/>, y que
    /// incluirá todas las propiedades del tipo <typeparamref name="TModel"/>
    /// como propiedades con notificación de cambio de valor.
    /// </summary>
    /// <typeparam name="TModel">
    /// Modelo para el cual crear la clase con patrón ViewModel.
    /// </typeparam>
    /// <param name="factory">
    /// Fábrica de tipos a utilizar para crear la nueva clase.
    /// </param>
    /// <returns>
    /// Un <see cref="TypeBuilder"/> por medio del cual se podrá definir a
    /// los miembros de la nueva clase.
    /// </returns>
    public static ITypeBuilder<INotifyPropertyChanged> CreateNpcClass<TModel>(this TypeFactory factory)
        where TModel : notnull, new()
    {
        return CreateNpcClass<TModel>(factory, null);
    }

    /// <summary>
    /// Compila un nuevo <see cref="EntityViewModel{T}"/> definiendo un campo de
    /// entidad de tipo <typeparamref name="TModel"/>.
    /// </summary>
    /// <typeparam name="TModel">
    /// Tipo de entidad para la cual compilar un nuevo
    /// <see cref="EntityViewModel{T}"/>.
    /// </typeparam>
    /// <param name="factory">
    /// Fábrica de tipos a utilizar para crear el nuevo tipo.
    /// </param>
    /// <param name="interfaces">
    /// Interfaces opcionales a incluir en la implementación del tipo. Puede
    /// establecerse en <see langword="null"/> si no se necesitan implementar
    /// interfaces adicionales.
    /// </param>
    /// <returns>
    /// Un objeto que puede utilizarse para construir un nuevo tipo.
    /// </returns>
    /// <remarks>
    /// El nuevo tipo generado contendrá propiedades con notificación de cambio
    /// de valor que utilizarán como campo de almacenamiento a la entidad en
    /// cuestión.
    /// </remarks>
    public static ITypeBuilder<EntityViewModel<TModel>> CreateEntityViewModelClass<TModel>(this TypeFactory factory, IEnumerable<Type>? interfaces)
        where TModel : notnull, new()
    {
        ITypeBuilder<EntityViewModel<TModel>> t = factory.NewType<EntityViewModel<TModel>>($"{typeof(TModel).Name}ViewModel", interfaces);
        PropertyInfo e = ReflectionHelpers.GetProperty<EntityViewModel<TModel>>(p => p.Entity);
        MethodInfo nm = ReflectionHelpers.GetMethod<EntityViewModel<TModel>, Action<string>>(p => p.Notify);

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
                    .Call(nm),
                p.PropertyType);
        }
        return t;
    }

    /// <summary>
    /// Compila un nuevo <see cref="EntityViewModel{T}"/> definiendo un campo de
    /// entidad de tipo <typeparamref name="TModel"/>.
    /// </summary>
    /// <typeparam name="TModel">
    /// Tipo de entidad para la cual compilar un nuevo
    /// <see cref="EntityViewModel{T}"/>.
    /// </typeparam>
    /// <param name="factory">
    /// Fábrica de tipos a utilizar para crear el nuevo tipo.
    /// </param>
    /// <returns>
    /// Un objeto que puede utilizarse para construir un nuevo tipo.
    /// </returns>
    public static ITypeBuilder<EntityViewModel<TModel>> CreateEntityViewModelClass<TModel>(this TypeFactory factory)
        where TModel : notnull, new()
    {
        return CreateEntityViewModelClass<TModel>(factory, null);
    }

    /// <summary>
    /// Agrega una propiedad pública con soporte para notificación de
    /// cambios de valor.
    /// </summary>
    /// <typeparam name="T">Tipo de la nueva propiedad.</typeparam>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad con
    /// soporte para notificación de cambios de valor.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty<T>(this ITypeBuilder<NotifyPropertyChangeBase> tb, string name)
    {
        return AddNpcProperty(tb, name, typeof(T));
    }

    /// <summary>
    /// Agrega una propiedad pública con soporte para notificación de
    /// cambios de valor.
    /// </summary>
    /// <typeparam name="T">Tipo de la nueva propiedad.</typeparam>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad con
    /// soporte para notificación de cambios de valor.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    [Sugar]
    public static PropertyBuildInfo AddNpcProperty<T>(this ITypeBuilder<NotifyPropertyChanged> tb, string name)
    {
        return AddNpcProperty((ITypeBuilder<NotifyPropertyChangeBase>)tb, name, typeof(T));
    }

    /// <summary>
    /// Agrega una propiedad pública con soporte para notificación de
    /// cambios de valor.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad con
    /// soporte para notificación de cambios de valor.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty(this ITypeBuilder<NotifyPropertyChangeBase> tb, string name, Type type)
    {
        return AddNpcProperty(tb, name, type, MemberAccess.Public);
    }

    /// <summary>
    /// Agrega una propiedad con soporte para notificación de cambios de
    /// valor.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad con
    /// soporte para notificación de cambios de valor.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <param name="access">Nivel de acceso de la nueva propiedad.</param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty(this ITypeBuilder<NotifyPropertyChangeBase> tb, string name, Type type, MemberAccess access)
    {
        return AddNpcProperty(tb, name, type, access, false);
    }

    /// <summary>
    /// Agrega una propiedad con soporte para notificación de cambios de
    /// valor.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad con
    /// soporte para notificación de cambios de valor.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <param name="access">Nivel de acceso de la nueva propiedad.</param>
    /// <param name="virtual">
    /// Si se establece en <see langword="true"/>, la propiedad será
    /// definida como virtual, por lo que podrá ser reemplazada en una
    /// clase derivada. 
    /// </param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty(this ITypeBuilder<NotifyPropertyChangeBase> tb, string name, Type type, MemberAccess access, bool @virtual)
    {
        CheckImplements<NotifyPropertyChangeBase>(tb.SpecificBaseType);
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
    /// Agrega una propiedad pública con soporte para notificación de
    /// cambios de valor.
    /// </summary>
    /// <typeparam name="T">Tipo de la nueva propiedad.</typeparam>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad con
    /// soporte para notificación de cambios de valor.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty<T>(this ITypeBuilder<INotifyPropertyChanged> tb, string name)
    {
        return AddNpcProperty(tb, name, typeof(T));
    }

    /// <summary>
    /// Agrega una propiedad pública con soporte para notificación de
    /// cambios de valor.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad con
    /// soporte para notificación de cambios de valor.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty(this ITypeBuilder<INotifyPropertyChanged> tb, string name, Type type)
    {
        return AddNpcProperty(tb, name, type, MemberAccess.Public);
    }

    /// <summary>
    /// Agrega una propiedad con soporte para notificación de cambios de
    /// valor.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad con
    /// soporte para notificación de cambios de valor.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <param name="access">Nivel de acceso de la nueva propiedad.</param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty(this ITypeBuilder<INotifyPropertyChanged> tb, string name, Type type, MemberAccess access)
    {
        return AddNpcProperty(tb, name, type, access, false);
    }

    /// <summary>
    /// Agrega una propiedad con soporte para notificación de cambios de
    /// valor.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad con
    /// soporte para notificación de cambios de valor.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <param name="access">Nivel de acceso de la nueva propiedad.</param>
    /// <param name="virtual">
    /// Si se establece en <see langword="true"/>, la propiedad será
    /// definida como virtual, por lo que podrá ser reemplazada en una
    /// clase derivada. 
    /// </param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty(this ITypeBuilder<INotifyPropertyChanged> tb, string name, Type type, MemberAccess access, bool @virtual)
    {
        return AddNpcProperty(tb, name, type, access, @virtual, (MethodInfo?)null);
    }

    /// <summary>
    /// Agrega una propiedad con soporte para notificación de cambios de
    /// valor.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad con
    /// soporte para notificación de cambios de valor.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <param name="access">Nivel de acceso de la nueva propiedad.</param>
    /// <param name="virtual">
    /// Si se establece en <see langword="true"/>, la propiedad será
    /// definida como virtual, por lo que podrá ser reemplazada en una
    /// clase derivada. 
    /// </param>
    /// <param name="evtHandler">
    /// Campo que contiene una referencia al manejador de eventos a llamar
    /// para los tipos construidos que implementan directamente
    /// <see cref="INotifyPropertyChanged"/>. Si se omite o se establece en
    /// <see langword="null"/> (<see langword="Nothing"/> en Visual Basic),
    /// se buscará un campo que contenga una referencia a un
    /// <see cref="PropertyChangedEventHandler"/> en el tipo base del tipo
    /// construido por medio de  <paramref name="tb"/>.
    /// </param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty(this ITypeBuilder<INotifyPropertyChanged> tb, string name, Type type, MemberAccess access, bool @virtual, FieldInfo? evtHandler)
    {
        evtHandler ??= tb.SpecificBaseType.GetFields().FirstOrDefault(p => p.FieldType.Implements<PropertyChangedEventHandler>()) ?? throw new MissingFieldException();
        return BuildNpcProp(tb.Builder, name, type, access, @virtual, (retLabel, setter) => setter
             .LoadField(evtHandler)
             .Duplicate()
             .BranchTrueNewLabel(out Label notify)
             .Pop()
             .Branch(retLabel)
             .PutLabel(notify)
             .LoadArg0()
             .LoadConstant(name)
             .NewObj<PropertyChangedEventArgs>()
        , ReflectionHelpers.GetMethod<PropertyChangedEventHandler, Action<object, PropertyChangedEventArgs>>(p => p.Invoke));
    }

    /// <summary>
    /// Agrega una propiedad con soporte para notificación de cambios de
    /// valor.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad con
    /// soporte para notificación de cambios de valor.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <param name="access">Nivel de acceso de la nueva propiedad.</param>
    /// <param name="virtual">
    /// Si se establece en <see langword="true"/>, la propiedad será
    /// definida como virtual, por lo que podrá ser reemplazada en una
    /// clase derivada. 
    /// </param>
    /// <param name="npcInvocator">
    /// Método que invoca al manejador de eventos para los tipos construidos
    /// que implementan directamente <see cref="INotifyPropertyChanged"/>.
    /// Si se omite o se establece en <see langword="null"/>
    /// (<see langword="Nothing"/> en Visual Basic), se buscará un campo
    /// que contenga una referencia a un
    /// <see cref="PropertyChangedEventHandler"/> en el tipo base del tipo
    /// construido por medio de  <paramref name="tb"/>.
    /// </param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty(this ITypeBuilder<INotifyPropertyChanged> tb, string name, Type type, MemberAccess access, bool @virtual, MethodInfo? npcInvocator)
    {
        npcInvocator ??= tb.SpecificBaseType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).First(p => p.IsVoid() && p.GetParameters().Length == 1 && p.GetParameters()[0].ParameterType == typeof(string) && p.HasAttribute<NpcChangeInvocatorAttribute>());
        return BuildNpcProp(tb.Builder, name, type, access, @virtual, (_, setter) => setter.LoadArg0().LoadConstant(name), npcInvocator);
    }

    /// <summary>
    /// Agrega una propiedad con soporte para notificación de cambios de
    /// valor.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad con
    /// soporte para notificación de cambios de valor.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <param name="access">Nivel de acceso de la nueva propiedad.</param>
    /// <param name="evtHandler">
    /// Campo que contiene una referencia al manejador de eventos a llamar
    /// para los tipos construidos que implementan directamente
    /// <see cref="INotifyPropertyChanged"/>. Si se omite o se establece en
    /// <see langword="null"/> (<see langword="Nothing"/> en Visual Basic),
    /// se buscará un campo que contenga una referencia a un
    /// <see cref="PropertyChangedEventHandler"/> en el tipo base del tipo
    /// construido por medio de  <paramref name="tb"/>.
    /// </param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty(this ITypeBuilder<INotifyPropertyChanged> tb, string name, Type type, MemberAccess access, FieldInfo? evtHandler)
    {
        return AddNpcProperty(tb, name, type, access, false, evtHandler);
    }

    /// <summary>
    /// Agrega una propiedad con soporte para notificación de cambios de
    /// valor.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad con
    /// soporte para notificación de cambios de valor.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <param name="access">Nivel de acceso de la nueva propiedad.</param>
    /// <param name="npcInvocator">
    /// Método que invoca al manejador de eventos para los tipos construidos
    /// que implementan directamente <see cref="INotifyPropertyChanged"/>.
    /// Si se omite o se establece en <see langword="null"/>
    /// (<see langword="Nothing"/> en Visual Basic), se buscará un campo
    /// que contenga una referencia a un
    /// <see cref="PropertyChangedEventHandler"/> en el tipo base del tipo
    /// construido por medio de  <paramref name="tb"/>.
    /// </param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty(this ITypeBuilder<INotifyPropertyChanged> tb, string name, Type type, MemberAccess access, MethodInfo? npcInvocator)
    {
        return AddNpcProperty(tb, name, type, access, false, npcInvocator);
    }

    /// <summary>
    /// Agrega una propiedad con soporte para notificación de cambios de
    /// valor.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad con
    /// soporte para notificación de cambios de valor.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <param name="evtHandler">
    /// Campo que contiene una referencia al manejador de eventos a llamar
    /// para los tipos construidos que implementan directamente
    /// <see cref="INotifyPropertyChanged"/>. Si se omite o se establece en
    /// <see langword="null"/> (<see langword="Nothing"/> en Visual Basic),
    /// se buscará un campo que contenga una referencia a un
    /// <see cref="PropertyChangedEventHandler"/> en el tipo base del tipo
    /// construido por medio de  <paramref name="tb"/>.
    /// </param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty(this ITypeBuilder<INotifyPropertyChanged> tb, string name, Type type, FieldInfo? evtHandler)
    {
        return AddNpcProperty(tb, name, type, MemberAccess.Public, false, evtHandler);
    }

    /// <summary>
    /// Agrega una propiedad con soporte para notificación de cambios de
    /// valor.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad con
    /// soporte para notificación de cambios de valor.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <param name="npcInvocator">
    /// Método que invoca al manejador de eventos para los tipos construidos
    /// que implementan directamente <see cref="INotifyPropertyChanged"/>.
    /// Si se omite o se establece en <see langword="null"/>
    /// (<see langword="Nothing"/> en Visual Basic), se buscará un campo
    /// que contenga una referencia a un
    /// <see cref="PropertyChangedEventHandler"/> en el tipo base del tipo
    /// construido por medio de  <paramref name="tb"/>.
    /// </param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty(this ITypeBuilder<INotifyPropertyChanged> tb, string name, Type type, MethodInfo? npcInvocator)
    {
        return AddNpcProperty(tb, name, type, MemberAccess.Public, false, npcInvocator);
    }

    /// <summary>
    /// Agrega una propiedad con soporte para notificación de cambios de
    /// valor.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad con
    /// soporte para notificación de cambios de valor.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="evtHandler">
    /// Campo que contiene una referencia al manejador de eventos a llamar
    /// para los tipos construidos que implementan directamente
    /// <see cref="INotifyPropertyChanged"/>. Si se omite o se establece en
    /// <see langword="null"/> (<see langword="Nothing"/> en Visual Basic),
    /// se buscará un campo que contenga una referencia a un
    /// <see cref="PropertyChangedEventHandler"/> en el tipo base del tipo
    /// construido por medio de  <paramref name="tb"/>.
    /// </param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty<T>(this ITypeBuilder<INotifyPropertyChanged> tb, string name, FieldInfo? evtHandler)
    {
        return AddNpcProperty(tb, name, typeof(T), MemberAccess.Public, false, evtHandler);
    }

    /// <summary>
    /// Agrega una propiedad con soporte para notificación de cambios de
    /// valor.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad con
    /// soporte para notificación de cambios de valor.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="npcInvocator">
    /// Método que invoca al manejador de eventos para los tipos construidos
    /// que implementan directamente <see cref="INotifyPropertyChanged"/>.
    /// Si se omite o se establece en <see langword="null"/>
    /// (<see langword="Nothing"/> en Visual Basic), se buscará un campo
    /// que contenga una referencia a un
    /// <see cref="PropertyChangedEventHandler"/> en el tipo base del tipo
    /// construido por medio de  <paramref name="tb"/>.
    /// </param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddNpcProperty<T>(this ITypeBuilder<INotifyPropertyChanged> tb, string name, MethodInfo? npcInvocator)
    {
        return AddNpcProperty(tb, name, typeof(T), MemberAccess.Public, false, npcInvocator);
    }

    private static PropertyBuildInfo BuildNpcProp(TypeBuilder tb, string name, Type t, MemberAccess access, bool @virtual, Action<Label, ILGenerator> evtHandler, MethodInfo method)
    {
        CheckImplements<INotifyPropertyChanged>(tb.BaseType!);
        PropertyBuildInfo? p = tb.AddProperty(name, t, true, access, @virtual).WithBackingField(out FieldBuilder? field);
        p.BuildNpcPropSetterSkeleton(
            (_, v) => v.LoadField(field),
            (l, v) =>
            {
                v.LoadArg0()
                .LoadArg1()
                .StoreField(field);
                evtHandler(l, p.Setter!);
                v.Call(method);
            }, t);
        return new PropertyBuildInfo(tb, p.Member, field);
    }

    [DebuggerNonUserCode]
    private static void CheckImplements<T>(Type t)
    {
        if (!t.Implements<T>()) throw Errors.InterfaceNotImplemented<T>();
    }

    private static MethodInfo GetEqualsMethod(Type type)
    {
        return type.GetMethod(nameof(object.Equals), BindingFlags.Public | BindingFlags.Instance, null, new[] { type }, null)
            ?? type.GetMethod(nameof(object.Equals), BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(object) }, null)!;
    }

    private static MethodInfo GetNpcChangeMethod(ITypeBuilder<NotifyPropertyChangeBase> tb, Type t)
    {
        return tb.ActualBaseType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(MemberInfoExtensions.HasAttribute<NpcChangeInvocatorAttribute>)?.MakeGenericMethod(new[] { t }) ?? throw new MissingMethodException();
    }
}
