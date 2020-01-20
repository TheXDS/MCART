﻿/*
TypeBuilderExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using TheXDS.MCART.Types.Base;
using static System.Reflection.Emit.OpCodes;
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
            var p = AddProperty(tb, name, type, true, access, @virtual);
            var field = tb.DefineField(UndName(name), type, FieldAttributes.Private | FieldAttributes.PrivateScope);

            p.Getter!.LoadField(field);
            p.Getter!.Emit(Ret);

            p.Setter!.Emit(Ldarg_0);
            p.Setter.Emit(Ldarg_1);
            p.Setter.StoreField(field);
            p.Setter.Emit(Ret);

            return new PropertyBuildInfo(p.Property, field);
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
            return AddAutoProperty(tb, name, typeof(T), MemberAccess.Public);
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
        public static PropertyBuildInfo AddNpcProperty<T>(this TypeBuilder tb, string name)
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
        public static PropertyBuildInfo AddNpcProperty(this TypeBuilder tb, string name, Type type)
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
        public static PropertyBuildInfo AddNpcProperty(this TypeBuilder tb, string name, Type type, MemberAccess access)
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
        public static PropertyBuildInfo AddNpcProperty(this TypeBuilder tb, string name, Type type, MemberAccess access, bool @virtual)
        {
            return AddNpcProperty(tb, name, type, access, @virtual, null);
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
        /// construido por medio de  <paramref name="tb"/>. Este argumento será
        /// ignorado si el tipo a construir por medio de <paramref name="tb"/>
        /// hereda de la clase <see cref="NotifyPropertyChangeBase"/> o una de
        /// sus clases derivadas.
        /// </param>
        /// <returns>
        /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
        /// la propiedad que ha sido construida.
        /// </returns>
        public static PropertyBuildInfo AddNpcProperty(this TypeBuilder tb, string name, Type type, MemberAccess access, bool @virtual, FieldInfo? evtHandler)
        {
            (PropertyBuilder prop, ILGenerator setter, FieldBuilder field) AddP()
            {
                var p = AddProperty(tb, name, type, true, access, @virtual);
                var field = tb.DefineField(UndName(name), type, FieldAttributes.Private | FieldAttributes.PrivateScope);
                p.Getter!.LoadField(field);
                p.Getter!.Emit(Ret);
                return (p.Property, p.Setter!, field);
            }

            PropertyBuilder prop;
            ILGenerator setter;
            FieldBuilder field;

            if (tb.Implements<NotifyPropertyChangeBase>())
            {
                (prop, setter, field) = AddP();
                AddNpcSetter(tb, name, type, setter, field);
            }
            else if (tb.Implements<INotifyPropertyChanged>())
            {
                evtHandler ??= tb.BaseType?.GetFields().FirstOrDefault(p => p.FieldType.Implements<PropertyChangedEventHandler>()) ?? throw new MissingFieldException();
                (prop, setter, field) = AddP();
                AddNpcSetter(tb, name, type, setter, field, evtHandler);
            }
            else
            {
                throw Errors.ErrIfaceNotImpl<INotifyPropertyChanged>();
            }
            return new PropertyBuildInfo(prop, field);
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

            return new PropertyBuildInfo(prop, getIl, setIl);
        }








        #region Helpers privados

        private static void AddNpcSetter(TypeBuilder tb, string name, Type t, ILGenerator setter, FieldBuilder field)
        {
            setter.Emit(Ldarg_0);
            setter.Emit(Dup);
            setter.Emit(Ldflda, field);
            setter.Emit(Ldarg_1);
            setter.Emit(Ldstr, name);
            setter.Emit(Callvirt, tb.BaseType!.GetMethod("Change", BindingFlags.NonPublic | BindingFlags.Instance)!.MakeGenericMethod(new[] { t })!);
            setter.Emit(Pop);
            setter.Emit(Ret);
        }
        private static void AddNpcSetter(TypeBuilder tb, string name, Type t, ILGenerator setter, FieldBuilder field, FieldInfo evtHandler)
        {
            var setRet = setter.DefineLabel();
            var cl = setter.DefineLabel();

            setter.Emit(Ldarg_0);
            setter.Emit(Ldfld, field);
            setter.Emit(Ldarg_1);
            if (!t.IsValueType)
            {
                setter.Emit(Ceq);
            }
            else
            {
                setter.Emit(Call, GetEqualsMethod(tb, t));
            }
            setter.Emit(Brtrue, setRet);
            setter.Emit(Ldarg_0);
            setter.Emit(Ldarg_1);
            setter.Emit(Stfld, field);
            setter.Emit(Ldarg_0);
            setter.Emit(Ldfld, evtHandler);
            setter.Emit(Dup);
            setter.Emit(Brtrue_S, cl);
            setter.Emit(Pop);
            setter.Emit(Br_S, setRet);
            setter.MarkLabel(cl);
            setter.Emit(Ldarg_0);
            setter.LoadConstant(name);
            setter.NewObject<PropertyChangedEventArgs>();
            setter.Emit(Callvirt, typeof(PropertyChangedEventHandler).GetMethod(nameof(PropertyChangedEventHandler.Invoke), BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(object), typeof(PropertyChangedEventArgs) }, null)!);
            setter.MarkLabel(setRet);
            setter.Emit(Ret);
        }
        private static MethodInfo GetEqualsMethod(TypeBuilder tb, Type type)
        {
            return tb.GetMethod(nameof(object.Equals), BindingFlags.Public | BindingFlags.Instance, null, new[] { type }, null)
                ?? tb.GetMethod(nameof(object.Equals), BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(object) }, null)!;
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