/*
TypeBuilderExtensions.cs

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
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types.Base;
using static System.Reflection.MethodAttributes;
using static TheXDS.MCART.Types.TypeBuilderHelpers;
using Errors = TheXDS.MCART.Resources.TypeFactoryErrors;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Contiene extensiones útiles para la generación de miembros por medio
    /// de la clase <see cref="TypeBuilder"/>.
    /// </summary>
    public static class TypeBuilderExtensions
    {
        /// <summary>
        /// Inicializa una nueva instancia del tipo en runtime especificado.
        /// </summary>
        /// <returns>La nueva instancia del tipo especificado.</returns>
        /// <param name="tb">
        /// <see cref="TypeBuilder"/> desde el cual instanciar un nuevo objeto.
        /// </param>
        [DebuggerStepThrough]
        [Sugar]
        public static object New(this TypeBuilder tb)
        {
            if (!tb.IsCreated()) tb.CreateType();
            return TypeExtensions.New(tb);
        }

        /// <summary>
        /// Determina si el método es reemplazable.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> si el método es reemplazable
        /// <see langword="false"/> en caso contrario, o
        /// <see langword="null"/> si el método no existe en la clase base
        /// del constructor de tipos.
        /// </returns>
        /// <param name="tb">
        /// Constructor de tipo sobre el cual ejecutar la consulta.
        /// </param>
        /// <param name="method">Nombre del método a buscar.</param>
        /// <param name="args">Tipo de argumentos del método a buscar.</param>
        public static bool? Overridable(this TypeBuilder tb, string method, params Type[] args)
        {
            var bm = tb.BaseType?.GetMethod(method, args);
            if (bm is null) return null;
            return bm.IsVirtual || bm.IsAbstract;
        }

        /// <summary>
        /// Agrega una propiedad automática al tipo.
        /// </summary>
        /// <param name="tb">
        /// Constructor del tipo en el cual crear la nueva propiedad
        /// automática.
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
        public static PropertyBuildInfo AddAutoProperty(this TypeBuilder tb, string name, Type type, MemberAccess access, bool @virtual)
        {
            var p = AddProperty(tb, name, type, true, access, @virtual).WithBackingField(out var field);
            p.Setter!
                .This()
                .LoadArg1()
                .StoreField(field)
                .Return();
            return new PropertyBuildInfo(tb, p.Member, field);
        }

        /// <summary>
        /// Agrega una propiedad automática al tipo.
        /// </summary>
        /// <param name="tb">
        /// Constructor del tipo en el cual crear la nueva propiedad
        /// automática.
        /// </param>
        /// <param name="name">Nombre de la nueva propiedad.</param>
        /// <param name="type">Tipo de la nueva propiedad.</param>
        /// <param name="access">Nivel de acceso de la nueva propiedad.</param>
        /// <returns>
        /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
        /// la propiedad que ha sido construida.
        /// </returns>
        public static PropertyBuildInfo AddAutoProperty(this TypeBuilder tb, string name, Type type, MemberAccess access)
        {
            return AddAutoProperty(tb, name, type, access, false);
        }

        /// <summary>
        /// Agrega una propiedad automática pública al tipo.
        /// </summary>
        /// <param name="tb">
        /// Constructor del tipo en el cual crear la nueva propiedad
        /// automática.
        /// </param>
        /// <param name="name">Nombre de la nueva propiedad.</param>
        /// <param name="type">Tipo de la nueva propiedad.</param>
        /// <returns>
        /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
        /// la propiedad que ha sido construida.
        /// </returns>
        public static PropertyBuildInfo AddAutoProperty(this TypeBuilder tb, string name, Type type)
        {
            return AddAutoProperty(tb, name, type, MemberAccess.Public);
        }

        /// <summary>
        /// Agrega una propiedad automática pública al tipo.
        /// </summary>
        /// <typeparam name="T">Tipo de la nueva propiedad.</typeparam>
        /// <param name="tb">
        /// Constructor del tipo en el cual crear la nueva propiedad
        /// automática.
        /// </param>
        /// <param name="name">Nombre de la nueva propiedad.</param>
        /// <returns>
        /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
        /// la propiedad que ha sido construida.
        /// </returns>
        public static PropertyBuildInfo AddAutoProperty<T>(this TypeBuilder tb, string name)
        {
            return AddAutoProperty(tb, name, typeof(T));
        }

        /// <summary>
        /// Agrega una sustitución para el método especificado.
        /// </summary>
        /// <param name="tb">
        /// Constructor del tipo en el cual crear la nueva propiedad
        /// automática.
        /// </param>
        /// <param name="method">
        /// Método a sustituir. Debe existir en el tipo base.
        /// </param>
        /// <returns></returns>
        public static MethodBuildInfo AddOverride(this TypeBuilder tb, MethodInfo method)
        {
            var newMethod = tb.DefineMethod(method.Name, GetNonAbstract(method), method.IsVoid() ? null : method.ReturnType, method.GetParameters().Select(p => p.ParameterType).ToArray());
            tb.DefineMethodOverride(newMethod, method);
            return new MethodBuildInfo(tb, newMethod);
        }

        private static MethodAttributes GetNonAbstract(MethodInfo m)
        {
            var a = (int)m.Attributes;
            a &= ~(int)Abstract;
            a |= (int)Virtual;

            return (MethodAttributes)a;
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
            var p = AddProperty(tb.Builder, name, type, true, access, @virtual);
            var field = tb.Builder.DefineField(UndName(name), type, FieldAttributes.Private | FieldAttributes.PrivateScope);
            p.Getter!.LoadField(field).Return();
            p.Setter!
                .This()
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
            return BuildNpcProp(tb.Builder, name, type, access,@virtual, (retLabel, setter) => setter
                .LoadField(evtHandler)
                .Duplicate()
                .BranchTrueNewLabel(out var notify)
                .Pop()
                .Branch(retLabel)
                .PutLabel(notify)
                .This()
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
        /// Métod que invoca al manejador de eventos para los tipos construidos
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
            npcInvocator ??= tb.SpecificBaseType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).First(p => p.IsVoid() && p.GetParameters().Length == 1 && p.GetParameters()[0].ParameterType == typeof(string) && p.HasAttr<NpcChangeInvocatorAttribute>());
            return BuildNpcProp(tb.Builder, name, type, access, @virtual, (_, setter) => setter.This().LoadConstant(name), npcInvocator);
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
        /// Métod que invoca al manejador de eventos para los tipos construidos
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
        /// Métod que invoca al manejador de eventos para los tipos construidos
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
        /// Métod que invoca al manejador de eventos para los tipos construidos
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

        /// <summary>
        /// Agrega una propiedad al tipo sin implementaciones de
        /// <see langword="get"/> ni <see langword="set"/> establecidas.
        /// </summary>
        /// <param name="tb">
        /// Constructor del tipo en el cual crear la nueva propiedad.
        /// </param>
        /// <param name="name">Nombre de la nueva propiedad.</param>
        /// <param name="type">Tipo de la nueva propiedad.</param>
        /// <param name="writtable">
        /// <see langword="true"/> para crear una propiedad que contiene
        /// accesor de escritura (accesor <see langword="set"/>),
        /// <see langword="false"/> para no incluir un accesor de escritura en
        /// la propiedad.
        /// </param>
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
        /// <remarks>
        /// La propiedad generada requerirá que se implementen los accesores
        /// antes de construir el tipo.
        /// </remarks>
        public static PropertyBuildInfo AddProperty(this TypeBuilder tb, string name, Type type, bool writtable, MemberAccess access, bool @virtual)
        {
            ILGenerator? setIl = null;

            var prop = tb.DefineProperty(name, PropertyAttributes.HasDefault, type, null);
            var getM = MkGet(tb, name, type, access, @virtual);
            var getIl = getM.GetILGenerator();
            prop.SetGetMethod(getM);

            if (writtable)
            {
                var setM = MkSet(tb, name, type, access, @virtual);
                setIl = setM.GetILGenerator();
                prop.SetSetMethod(setM);
            }

            return new PropertyBuildInfo(tb, prop, getIl, setIl);
        }

        /// <summary>
        /// Agrega una propiedad al tipo sin implementaciones de
        /// <see langword="get"/> ni <see langword="set"/> establecidas.
        /// </summary>
        /// <typeparam name="T">Tipo de la nueva propiedad.</typeparam>
        /// <param name="tb">
        /// Constructor del tipo en el cual crear la nueva propiedad.
        /// </param>
        /// <param name="name">Nombre de la nueva propiedad.</param>
        /// <param name="writtable">
        /// <see langword="true"/> para crear una propiedad que contiene
        /// accesor de escritura (accesor <see langword="set"/>),
        /// <see langword="false"/> para no incluir un accesor de escritura en
        /// la propiedad.
        /// </param>
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
        /// <remarks>
        /// La propiedad generada requerirá que se implementen los accesores
        /// antes de construir el tipo.
        /// </remarks>
        public static PropertyBuildInfo AddProperty<T>(this TypeBuilder tb, string name, bool writtable, MemberAccess access, bool @virtual)
        {
            return AddProperty(tb, name, typeof(T), writtable, access, @virtual);
        }

        /// <summary>
        /// Agrega una propiedad al tipo sin implementaciones de
        /// <see langword="get"/> ni <see langword="set"/> establecidas.
        /// </summary>
        /// <typeparam name="T">Tipo de la nueva propiedad.</typeparam>
        /// <param name="tb">
        /// Constructor del tipo en el cual crear la nueva propiedad.
        /// </param>
        /// <param name="name">Nombre de la nueva propiedad.</param>
        /// <param name="writtable">
        /// <see langword="true"/> para crear una propiedad que contiene
        /// accesor de escritura (accesor <see langword="set"/>),
        /// <see langword="false"/> para no incluir un accesor de escritura en
        /// la propiedad.
        /// </param>
        /// <param name="access">Nivel de acceso de la nueva propiedad.</param>
        /// <returns>
        /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
        /// la propiedad que ha sido construida.
        /// </returns>
        /// <remarks>
        /// La propiedad generada requerirá que se implementen los accesores
        /// antes de construir el tipo.
        /// </remarks>
        public static PropertyBuildInfo AddProperty<T>(this TypeBuilder tb, string name, bool writtable, MemberAccess access)
        {
            return AddProperty(tb, name, typeof(T), writtable, access, false);
        }

        /// <summary>
        /// Agrega una propiedad al tipo sin implementaciones de
        /// <see langword="get"/> ni <see langword="set"/> establecidas.
        /// </summary>
        /// <typeparam name="T">Tipo de la nueva propiedad.</typeparam>
        /// <param name="tb">
        /// Constructor del tipo en el cual crear la nueva propiedad.
        /// </param>
        /// <param name="name">Nombre de la nueva propiedad.</param>
        /// <param name="writtable">
        /// <see langword="true"/> para crear una propiedad que contiene
        /// accesor de escritura (accesor <see langword="set"/>),
        /// <see langword="false"/> para no incluir un accesor de escritura en
        /// la propiedad.
        /// </param>
        /// <returns>
        /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
        /// la propiedad que ha sido construida.
        /// </returns>
        /// <remarks>
        /// La propiedad generada requerirá que se implementen los accesores
        /// antes de construir el tipo.
        /// </remarks>
        public static PropertyBuildInfo AddProperty<T>(this TypeBuilder tb, string name, bool writtable)
        {
            return AddProperty(tb, name, typeof(T), writtable, MemberAccess.Public, false);
        }
        
        /// <summary>
        /// Agrega una propiedad al tipo sin implementaciones de
        /// <see langword="get"/> ni <see langword="set"/> establecidas.
        /// </summary>
        /// <typeparam name="T">Tipo de la nueva propiedad.</typeparam>
        /// <param name="tb">
        /// Constructor del tipo en el cual crear la nueva propiedad.
        /// </param>
        /// <param name="name">Nombre de la nueva propiedad.</param>
        /// <returns>
        /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
        /// la propiedad que ha sido construida.
        /// </returns>
        /// <remarks>
        /// La propiedad generada requerirá que se implementen los accesores
        /// antes de construir el tipo.
        /// </remarks>
        public static PropertyBuildInfo AddProperty<T>(this TypeBuilder tb, string name)
        {
            return AddProperty(tb, name, typeof(T), true, MemberAccess.Public, false);
        }

        /// <summary>
        /// Agrega una propiedad computada al tipo.
        /// </summary>
        /// <typeparam name="T">Tipo de la nueva propiedad.</typeparam>
        /// <param name="tb">
        /// Constructor del tipo en el cual crear la nueva propiedad.
        /// </param>
        /// <param name="name">Nombre de la nueva propiedad.</param>
        /// <param name="getterDefinition">
        /// Acción que implementará las instrucciones a ejecutar dentro del
        /// método asociado al accesor <see langword="get"/> de la propiedad 
        /// computada.
        /// </param>
        /// <returns>
        /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
        /// la propiedad que ha sido construida.
        /// </returns>
        public static PropertyBuildInfo AddComputedProperty<T>(this TypeBuilder tb, string name, Action<ILGenerator> getterDefinition)
        {
            return AddComputedProperty(tb, name, typeof(T), getterDefinition);
        }

        /// <summary>
        /// Agrega una propiedad computada al tipo.
        /// </summary>
        /// <param name="tb">
        /// Constructor del tipo en el cual crear la nueva propiedad.
        /// </param>
        /// <param name="name">Nombre de la nueva propiedad.</param>
        /// <param name="type">Tipo de la nueva propiedad.</param>
        /// <param name="getterDefinition">
        /// Acción que implementará las instrucciones a ejecutar dentro del
        /// método asociado al accesor <see langword="get"/> de la propiedad 
        /// computada.
        /// </param>
        /// <returns>
        /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
        /// la propiedad que ha sido construida.
        /// </returns>
        public static PropertyBuildInfo AddComputedProperty(this TypeBuilder tb, string name, Type type, Action<ILGenerator> getterDefinition)
        {
            var prop = AddProperty(tb, name, type, false, MemberAccess.Public, false);
            getterDefinition(prop.Getter!);
            return prop;
        }

        /// <summary>
        /// Agrega una propiedad con un valor constante.
        /// </summary>
        /// <typeparam name="T">Tipo de la nueva propiedad.</typeparam>
        /// <param name="tb">
        /// Constructor del tipo en el cual crear la nueva propiedad.
        /// </param>
        /// <param name="name">Nombre de la nueva propiedad.</param>
        /// <param name="value">Valor constante a asignar.</param>
        /// <returns>
        /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
        /// la propiedad que ha sido construida.
        /// </returns>
        public static PropertyBuildInfo AddConstantProperty<T>(this TypeBuilder tb, string name, T value)
        {
            return AddConstantProperty(tb, name, typeof(T), value);
        }

        /// <summary>
        /// Agrega una propiedad con un valor constante.
        /// </summary>
        /// <param name="tb">
        /// Constructor del tipo en el cual crear la nueva propiedad.
        /// </param>
        /// <param name="name">Nombre de la nueva propiedad.</param>
        /// <param name="type">Tipo de la nueva propiedad.</param>
        /// <param name="value">Valor constante a asignar.</param>
        /// <returns>
        /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
        /// la propiedad que ha sido construida.
        /// </returns>
        public static PropertyBuildInfo AddConstantProperty(this TypeBuilder tb, string name, Type type, object? value)
        {
            var prop = AddProperty(tb, name, type, false, MemberAccess.Public, false);
            prop.Getter!.LoadConstant(type, value).Return();
            return prop;
        }

        /// <summary>
        /// Agrega una propiedad pública de solo escritura al tipo.
        /// </summary>
        /// <param name="tb">
        /// Constructor del tipo en el cual crear la nueva propiedad.
        /// </param>
        /// <param name="name">Nombre de la nueva propiedad.</param>
        /// <param name="type">Tipo de la nueva propiedad.</param>
        /// <returns>
        /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
        /// la propiedad que ha sido construida.
        /// </returns>
        public static PropertyBuildInfo AddWriteOnlyProperty(this TypeBuilder tb, string name, Type type)
        {
            return AddWriteOnlyProperty(tb, name, type, MemberAccess.Public, false);
        }

        /// <summary>
        /// Agrega una propiedad de solo escritura al tipo.
        /// </summary>
        /// <param name="tb">
        /// Constructor del tipo en el cual crear la nueva propiedad.
        /// </param>
        /// <param name="name">Nombre de la nueva propiedad.</param>
        /// <param name="type">Tipo de la nueva propiedad.</param>
        /// <param name="access">Nivel de acceso de la nueva propiedad.</param>
        /// <returns>
        /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
        /// la propiedad que ha sido construida.
        /// </returns>
        public static PropertyBuildInfo AddWriteOnlyProperty(this TypeBuilder tb, string name, Type type, MemberAccess access)
        {
            return AddWriteOnlyProperty(tb, name, type, access, false);
        }

        /// <summary>
        /// Agrega una propiedad pública de solo escritura al tipo.
        /// </summary>
        /// <param name="tb">
        /// Constructor del tipo en el cual crear la nueva propiedad.
        /// </param>
        /// <param name="name">Nombre de la nueva propiedad.</param>
        /// <param name="type">Tipo de la nueva propiedad.</param>
        /// <param name="virtual">
        /// Si se establece en <see langword="true"/>, la propiedad será
        /// definida como virtual, por lo que podrá ser reemplazada en una
        /// clase derivada. 
        /// </param>
        /// <returns>
        /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
        /// la propiedad que ha sido construida.
        /// </returns>
        public static PropertyBuildInfo AddWriteOnlyProperty(this TypeBuilder tb, string name, Type type, bool @virtual)
        {
            return AddWriteOnlyProperty(tb, name, type, MemberAccess.Public, @virtual);
        }

        /// <summary>
        /// Agrega una propiedad de solo escritura al tipo.
        /// </summary>
        /// <param name="tb">
        /// Constructor del tipo en el cual crear la nueva propiedad.
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
        public static PropertyBuildInfo AddWriteOnlyProperty(this TypeBuilder tb, string name, Type type, MemberAccess access, bool @virtual)
        {
            var prop = tb.DefineProperty(name, PropertyAttributes.HasDefault, type, null);
            var setM = MkSet(tb, name, type, access, @virtual);
            var setIl = setM.GetILGenerator();
            prop.SetSetMethod(setM);
            return new PropertyBuildInfo(tb, prop, null, setIl);
        }

        /// <summary>
        /// Inserta explícitamente un constructor público sin argumentos al
        /// tipo.
        /// </summary>
        /// <param name="tb">
        /// <see cref="TypeBuilder"/> en el cual se definirá el nuevo constructor.
        /// </param>
        /// <returns>
        /// Un <see cref="ILGenerator"/> que permite definir las instrucciones
        /// del constructor.
        /// </returns>
        public static ILGenerator AddPublicConstructor(this TypeBuilder tb) => AddPublicConstructor(tb, Type.EmptyTypes);

        /// <summary>
        /// Inserta explícitamente un constructor público al tipo,
        /// especificando los argumentos requeridos por el mismo.
        /// </summary>
        /// <param name="tb">
        /// <see cref="TypeBuilder"/> en el cual se definirá el nuevo
        /// constructor.
        /// </param>
        /// <param name="arguments">
        /// Arreglo con los tipos de argumentos aceptados por el nuevo
        /// constructor.
        /// </param>
        /// <returns>
        /// Un <see cref="ILGenerator"/> que permite definir las instrucciones
        /// del constructor.
        /// </returns>
        public static ILGenerator AddPublicConstructor(this TypeBuilder tb, Type[] arguments)
        {
            return tb.DefineConstructor(Public | SpecialName | HideBySig | RTSpecialName, CallingConventions.HasThis, arguments).GetILGenerator();
        }

        /// <summary>
        /// Implementa explícitamente un método abstracto.
        /// </summary>
        /// <param name="tb">
        /// <see cref="TypeBuilder"/> en el cual se implementará de forma
        /// explícita el método abstracto.
        /// </param>
        /// <param name="method">
        /// Método abstracto a implementar. El <see cref="TypeBuilder"/>
        /// especificado debe implementar la interfaz en la cual está definido
        /// el método.
        /// </param>
        /// <returns>
        /// Un <see cref="MethodBuildInfo"/> que contiene información sobre
        /// el método que ha sido implementado de forma explícita.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Se produce si <paramref name="tb"/> no implementa o hereda la
        /// interfaz en la cual se ha definido el método
        /// <paramref name="method"/>, o si <paramref name="method"/> no es un
        /// miembro definido en una interfaz.
        /// </exception>
        public static MethodBuildInfo ExplicitImplementMethod(this TypeBuilder tb, MethodInfo method)
        {
            if (!method.DeclaringType?.IsInterface ?? true) throw Errors.IFaceMethodExpected();
            if (!tb.GetInterfaces().Contains(method.DeclaringType!)) throw Errors.IfaceNotImpl(method.DeclaringType!);

            var m = tb.DefineMethod($"{method.DeclaringType!.Name}.{method.Name}",
                Private | HideBySig | NewSlot | Virtual | Final,
                method.IsVoid() ? null : method.ReturnType,
                method.GetParameters().Select(p => p.ParameterType).ToArray());
            tb.DefineMethodOverride(m, method);

            return new MethodBuildInfo(tb, m);
        }

        #region Helpers privados

        [DebuggerNonUserCode]
        private static void CheckImplements<T>(Type t)
        {
            if (!t.Implements<T>()) throw Errors.IfaceNotImpl<T>();
        }

        private static PropertyBuildInfo BuildNpcProp(TypeBuilder tb, string name, Type t, MemberAccess access, bool @virtual, Action<Label,ILGenerator> evtHandler, MethodInfo method)
        {
            CheckImplements<INotifyPropertyChanged>(tb.BaseType!);

            var p = AddProperty(tb, name, t, true, access, @virtual).WithBackingField(out var field);
            var setRet = p.Setter!.DefineLabel();

            p.Setter!.LoadField(field);
            if (t.IsValueType)
            {
                p.Setter!.LoadArg1()
                    .CompareEqual()
                    .BranchTrue(setRet);
            }
            else
            {
                p.Setter!
                    .BranchFalseNewLabel(out var fieldIsNull)
                    .LoadField(field)
                    .LoadArg1()
                    .Call(GetEqualsMethod(t))
                    .BranchTrue(setRet)
                    .BranchNewLabel(out var doSet)
                    .PutLabel(fieldIsNull)
                    .LoadArg1()
                    .BranchFalse(setRet)
                    .PutLabel(doSet);
            }
            p.Setter!
                .This()
                .LoadArg1()
                .StoreField(field);

            evtHandler(setRet, p.Setter!);
            p.Setter!
                .Call(method)
                .Return(setRet);
            return new PropertyBuildInfo(tb, p.Member, field);
        }

        private static MethodInfo GetEqualsMethod(Type type)
        {
            return type.GetMethod(nameof(object.Equals), BindingFlags.Public | BindingFlags.Instance, null, new[] { type }, null)
                ?? type.GetMethod(nameof(object.Equals), BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(object) }, null)!;
        }

        private static MethodInfo GetNpcChangeMethod(ITypeBuilder<NotifyPropertyChangeBase> tb, Type t)
        {
            return tb.ActualBaseType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(Objects.HasAttr<NpcChangeInvocatorAttribute>)?.MakeGenericMethod(new[] { t }) ?? throw new MissingMethodException();
        }

        private static MethodBuilder MkGet(TypeBuilder tb, string name, Type t, MemberAccess a, bool v)
        {
            var n = $"get_{name}";
            return tb.DefineMethod(n, MkPFlags(tb, n, a, v), t, null);
        }
        
        private static MethodBuilder MkSet(TypeBuilder tb, string name, Type t, MemberAccess a, bool v)
        {
            var n = $"set_{name}";
            return tb.DefineMethod(n, MkPFlags(tb, n, a, v), null, new[] { t });
        }
        
        private static MethodAttributes MkPFlags(TypeBuilder tb, string n, MemberAccess a, bool v)
        {
            var f = Access(a) | SpecialName | HideBySig | ReuseSlot;
            if (tb.Overridable(n) ?? v) f |= Virtual;
            return f;
        }

        #endregion
    }
}