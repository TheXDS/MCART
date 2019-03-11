﻿/*
ViewModelFactory.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using static System.Reflection.BindingFlags;
using static System.Reflection.Emit.OpCodes;
using static System.Reflection.MethodAttributes;
using static TheXDS.MCART.Types.Extensions.EnumExtensions;
using static TheXDS.MCART.Types.Extensions.StringExtensions;

// ReSharper disable StaticMemberInGenericType

namespace TheXDS.MCART.ViewModel
{
    /// <summary>
    ///     Fábrica de tipos para implementaciones ViewModel dinámicas.
    /// </summary>
    public static class ViewModelFactory
    {
        private const MethodAttributes _gsArgs = MethodAttributes.Public | SpecialName | HideBySig;
        private const string _namespace = "TheXDS.MCART.ViewModel._Generated";
        private static readonly ModuleBuilder _mBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(_namespace), AssemblyBuilderAccess.Run).DefineDynamicModule(_namespace);
        private static readonly Dictionary<Type, Type> _builtViewModels = new Dictionary<Type, Type>();
        private static readonly Dictionary<Type, Type> _builtModels = new Dictionary<Type, Type>();
        private static readonly Dictionary<Type, Type> _builtDynViewModels = new Dictionary<Type, Type>();

        #region Helpers

        private static void InitField(ILGenerator ilGen, FieldBuilder field, object value)
        {
            ilGen.Emit(Ldarg_0);
            LoadConstant(ilGen, value);
            ilGen.Emit(Stfld, field);
        }
        private static void InitField(ILGenerator ilGen, FieldBuilder field, Type instanceType, params object[] args)
        {
            if (instanceType.IsAbstract) throw new InvalidTypeException(instanceType);
            var c = instanceType.GetConstructor(args.ToTypes().ToArray()) ?? throw new TypeLoadException();
            ilGen.Emit(Ldarg_0);
            foreach (var j in args) LoadConstant(ilGen, j);
            ilGen.Emit(Newobj, c);
            ilGen.Emit(Stfld, field);
        }
        private static void LoadConstant(ILGenerator ilGen, object value)
        {
            switch (value)
            {
                case Enum _:
                    LoadConstant(ilGen, ((Enum)value).ToUnderlyingType());
                    break;
                case byte _:
                case sbyte _:
                case char _:
                    ilGen.Emit(Ldc_I4_S, unchecked((int)value));
                    break;
                case bool b:
                    ilGen.Emit(b ? Ldc_I4_1 : Ldc_I4_0);
                    break;
                case short _:
                case ushort _:
                case int _:
                case uint _:
                    ilGen.Emit(Ldc_I4, unchecked((int)value));
                    break;
                case long _:
                case ulong _:
                    ilGen.Emit(Ldc_I8, unchecked((long)value));
                    break;
                case float _:
                    ilGen.Emit(Ldc_R4, (float)value);
                    break;
                case double _:
                    ilGen.Emit(Ldc_R8, (double)value);
                    break;
                case decimal _:
                    foreach (var j in decimal.GetBits((decimal)value))
                    {
                        ilGen.Emit(Ldc_I4, j);
                    }
                    ilGen.Emit(Newobj, typeof(decimal).GetConstructor(new Type[]
                    {
                        typeof(int),
                        typeof(int),
                        typeof(int),
                        typeof(bool),
                        typeof(byte)
                    }));
                    break;
                case string s:
                    if (s is null) ilGen.Emit(Ldnull);                    
                    else ilGen.Emit(Ldstr, (string)value);
                    break;
                case Type _:
                    ilGen.Emit(Ldtoken, (Type)value);
                    ilGen.Emit(Call, typeof(Type).GetMethod("GetTypeFromHandle"));
                    break;
                default:
                    ilGen.Emit(Ldnull);
                    break;
            }
        }
        private static string UndName(string name)
        {
            if (name.IsEmpty()) throw new ArgumentNullException(name);
            return name.Length > 1
                ? $"_{name.Substring(0, 1).ToLower()}{name.Substring(1)}"
                : $"_{name.ToLower()}";
        }
        private static string NoIfaceName(string name)
        {
            if (name.IsEmpty()) throw new ArgumentNullException(name);
            return name[0] != 'I' ? $"{name}Implementation" : name.Substring(1);
        }
        private static TypeBuilder NewType(string name, Type baseType, params Type[] interfaces) => _mBuilder.DefineType($"{_namespace}.{name}_{Guid.NewGuid().ToString().Replace("-", "")}", TypeAttributes.Public | TypeAttributes.Class, baseType, interfaces);
        private static Type GetListType(Type listType) => listType.GenericTypeArguments?.Count() == 1 ? listType.GenericTypeArguments.Single() : typeof(object);
        
        #endregion

        #region Construcción de campos

        private static void AddAutoProp(TypeBuilder tb, PropertyInfo property, out PropertyBuilder prop, out FieldBuilder field)
        {
            AddFieldProp(tb, property, (setIl, f) =>
             {
                 setIl.Emit(Ldarg_0);
                 setIl.Emit(Ldarg_1);
                 setIl.Emit(Stfld, f);
             }, out prop, out field);
        }
        private static void AddNpcProp(TypeBuilder tb, PropertyInfo property, Type propType, MethodAttributes morFlags, out PropertyBuilder prop, out FieldBuilder field)
        {
            AddFieldProp(tb, property, propType, morFlags, (setIl, f) =>
            {
                setIl.Emit(Ldarg_0);
                setIl.Emit(Ldarg_1);
                setIl.Emit(Stfld, f);
                setIl.Emit(Ldarg_0);
                setIl.Emit(Ldstr, property.Name);
                setIl.Emit(Call, tb.BaseType.GetMethod("OnPropertyChanged", NonPublic | Instance) ?? throw new TamperException());
            }, out prop, out field);
        }
        private static void AddFieldProp(TypeBuilder tb, PropertyInfo property, Action<ILGenerator, FieldBuilder> setMethodBuilder, out PropertyBuilder prop, out FieldBuilder field) => AddFieldProp(tb, property, property.PropertyType, ReuseSlot, setMethodBuilder, out prop, out field);
        private static void AddFieldProp(TypeBuilder tb, PropertyInfo property, Type fieldType, MethodAttributes morFlags, Action<ILGenerator, FieldBuilder> setMethodBuilder, out PropertyBuilder prop, out FieldBuilder field)
        {
            field = tb.DefineField(UndName(property.Name), fieldType, FieldAttributes.Private | (!property.CanWrite ? FieldAttributes.InitOnly : FieldAttributes.PrivateScope));
            prop = tb.DefineProperty(property.Name, PropertyAttributes.HasDefault, property.PropertyType, null);
            var getM = tb.DefineMethod($"get_{property.Name}", _gsArgs | morFlags, property.PropertyType, null);
            var getIl = getM.GetILGenerator();
            getIl.Emit(Ldarg_0);
            getIl.Emit(Ldfld, field);
            getIl.Emit(Ret);
            prop.SetGetMethod(getM);

            if (property.CanWrite)
            {
                var setM = tb.DefineMethod($"set_{property.Name}", _gsArgs | morFlags, null, new[] { property.PropertyType });
                var setIl = setM.GetILGenerator();
                setMethodBuilder?.Invoke(setIl, field);
                setIl.Emit(Ret);
                prop.SetSetMethod(setM);
            }
        }
        private static void AddFieldCollection(TypeBuilder tb, ILGenerator ctorIl, PropertyInfo modelProperty)
        {
            var listType = GetListType(modelProperty.PropertyType);
            var enumerator = typeof(IEnumerator<>).MakeGenericType(listType);
            var thisType = typeof(ObservableCollection<>).MakeGenericType(listType);

            AddNpcProp(tb, modelProperty, thisType, Virtual | NewSlot, out _, out var field);

            var handler = tb.DefineMethod($"{modelProperty.Name}_CollectionChanged", Private | HideBySig, null, new[] { typeof(object), typeof(NotifyCollectionChangedEventArgs) });
            handler.InitLocals = true;
            var handlerIl = handler.GetILGenerator();
            handlerIl.DeclareLocal(enumerator);
            handlerIl.DeclareLocal(listType);

            // Notificación de evento
            handlerIl.Emit(Ldarg_0);
            handlerIl.Emit(Ldstr, modelProperty.Name);
            handlerIl.Emit(Callvirt, typeof(NotifyPropertyChanged).GetMethod("OnPropertyChanged", NonPublic | Instance));
            handlerIl.Emit(Ret);

            // Inicialización de campo
            ctorIl.Emit(Ldarg_0);
            ctorIl.Emit(Newobj, thisType.GetConstructor(Type.EmptyTypes));
            ctorIl.Emit(Stfld, field);
            ctorIl.Emit(Ldarg_0);
            ctorIl.Emit(Ldfld, field);
            ctorIl.Emit(Ldarg_0);
            ctorIl.Emit(Ldftn, handler);
            ctorIl.Emit(Newobj, typeof(NotifyCollectionChangedEventHandler).GetConstructors().Single());
            ctorIl.Emit(Callvirt, typeof(INotifyCollectionChanged).GetMethod("add_CollectionChanged"));
        }

        #endregion

        #region Construcción de entidades

        private static void AddEntityProp(TypeBuilder tb, ILGenerator editIl, PropertyInfo property, MethodAttributes morFlags, out PropertyBuilder prop)
        {
            prop = tb.DefineProperty(property.Name, PropertyAttributes.HasDefault, property.PropertyType, null);
            var getM = tb.DefineMethod($"get_{property.Name}", _gsArgs | morFlags, property.PropertyType, null);
            var getIl = getM.GetILGenerator();
            var il0e = getIl.DefineLabel();
            var il13 = getIl.DefineLabel();
            getIl.Emit(Ldarg_0);
            getIl.Emit(Callvirt, tb.BaseType.GetMethod("get_Entity"));
            getIl.Emit(Dup);
            getIl.Emit(Brtrue_S,il0e);
            getIl.Emit(Pop);
            LoadConstant(getIl, property.PropertyType.Default());
            getIl.Emit(Br_S, il13);
            getIl.MarkLabel(il0e);
            getIl.Emit(Callvirt, property.GetMethod);
            getIl.MarkLabel(il13);
            getIl.Emit(Ret);
            prop.SetGetMethod(getM);
            if (property.CanWrite)
            {
                var setM = tb.DefineMethod($"set_{property.Name}", _gsArgs | morFlags, null, new[] { property.PropertyType });
                var setIl = setM.GetILGenerator();
                var exitLabel = setIl.DefineLabel();
                setIl.Emit(Ldarg_0);
                setIl.Emit(Callvirt, tb.BaseType.GetMethod("get_Entity"));
                setIl.Emit(Brfalse_S, exitLabel);
                setIl.Emit(Ldarg_0);
                setIl.Emit(Callvirt, tb.BaseType.GetMethod("get_Entity"));
                setIl.Emit(Ldarg_1);
                setIl.Emit(Callvirt, property.SetMethod);
                setIl.Emit(Ldarg_0);
                setIl.Emit(Ldstr, property.Name);
                setIl.Emit(Call, tb.BaseType.GetMethod("OnPropertyChanged", NonPublic | Instance));
                setIl.MarkLabel(exitLabel);
                setIl.Emit(Ret);
                prop.SetSetMethod(setM);
                editIl.Emit(Ldarg_0);
                editIl.Emit(Ldarg_1);
                editIl.Emit(Callvirt, property.GetMethod);
                editIl.Emit(Call, prop.SetMethod);
            }
        }
        private static void AddEntityCollection(TypeBuilder tb, ILGenerator ctorIl, ILGenerator setEntityIl, PropertyInfo modelProperty, FieldBuilder checkField, out PropertyBuilder thisProp)
        {
            var entity = tb.BaseType.GetProperty("Entity", BindingFlags.Public | Instance)?.GetMethod;
            var labels = new Dictionary<string, Label>();
            var listType = GetListType(modelProperty.PropertyType);
            var collectionType = typeof(ICollection<>).MakeGenericType(listType);
            var thisType = typeof(ObservableCollection<>).MakeGenericType(listType);
            var thisField = tb.DefineField(UndName(modelProperty.Name), thisType, FieldAttributes.Private | FieldAttributes.InitOnly);
            thisProp = tb.DefineProperty(modelProperty.Name, PropertyAttributes.HasDefault, modelProperty.PropertyType, null);
            var handler = tb.DefineMethod($"{modelProperty.Name}_CollectionChanged", Private | HideBySig, null, new[] { typeof(object), typeof(NotifyCollectionChangedEventArgs) });
            var handlerIl = handler.GetILGenerator();
            var getM = tb.DefineMethod($"get_{modelProperty.Name}", MethodAttributes.Public | SpecialName | HideBySig | Virtual, modelProperty.PropertyType, null);
            var getIl = getM.GetILGenerator();
            Label NewLabel(string id)
            {
                var l = handlerIl.DefineLabel();
                labels.Add(id, l);
                return l;
            }

            // Getter
            {
                getIl.Emit(Ldarg_0);
                getIl.Emit(Ldfld, thisField);
                getIl.Emit(Ret);
                thisProp.SetGetMethod(getM);
            }

            // NotifyCollectionChangedHandler
            {
                var enumerable = typeof(IEnumerable);
                var enumerator = typeof(IEnumerator);
                handlerIl.DeclareLocal(enumerator);
                handlerIl.DeclareLocal(typeof(IDisposable));
                handlerIl.Emit(Ldarg_0);
                handlerIl.Emit(Ldfld, checkField);
                handlerIl.Emit(Brtrue, NewLabel("Exit"));
                handlerIl.Emit(Ldarg_0);
                handlerIl.Emit(Callvirt, entity);
                handlerIl.Emit(Brfalse, labels["Exit"]);
                handlerIl.Emit(Ldarg_2);
                handlerIl.Emit(Callvirt, typeof(NotifyCollectionChangedEventArgs).GetMethod("get_NewItems"));
                handlerIl.Emit(Dup);
                handlerIl.Emit(Brtrue_S, NewLabel("GetEnumerator"));
                handlerIl.Emit(Pop);
                handlerIl.Emit(Ldc_I4_0);
                handlerIl.Emit(Newarr, listType);
                handlerIl.MarkLabel(labels["GetEnumerator"]);
                handlerIl.Emit(Callvirt, enumerable.GetMethod("GetEnumerator"));
                handlerIl.Emit(Stloc_0);
                handlerIl.BeginExceptionBlock();
                handlerIl.Emit(Br_S, NewLabel("Next"));
                handlerIl.MarkLabel(NewLabel("Loop"));
                handlerIl.Emit(Ldarg_0);
                handlerIl.Emit(Callvirt, entity);
                handlerIl.Emit(Callvirt, modelProperty.GetMethod);
                handlerIl.Emit(Ldloc_0);
                handlerIl.Emit(Callvirt, enumerator.GetMethod("get_Current"));
                handlerIl.Emit(Callvirt, collectionType.GetMethod("Add"));
                handlerIl.MarkLabel(labels["Next"]);
                handlerIl.Emit(Ldloc_0);
                handlerIl.Emit(Callvirt, typeof(IEnumerator).GetMethod("MoveNext"));
                handlerIl.Emit(Brtrue_S, labels["Loop"]);
                handlerIl.BeginFinallyBlock();
                handlerIl.Emit(Ldloc_0);
                handlerIl.Emit(Isinst, typeof(IDisposable));
                handlerIl.Emit(Stloc_1);
                handlerIl.Emit(Ldloc_1);
                handlerIl.Emit(Brfalse_S, NewLabel("Disposed"));
                handlerIl.Emit(Ldloc_1);
                handlerIl.Emit(Callvirt, typeof(IDisposable).GetMethod("Dispose"));
                handlerIl.MarkLabel(labels["Disposed"]);
                handlerIl.EndExceptionBlock();
                handlerIl.Emit(Ldarg_2);
                handlerIl.Emit(Callvirt, typeof(NotifyCollectionChangedEventArgs).GetMethod("get_OldItems"));
                handlerIl.Emit(Dup);
                handlerIl.Emit(Brtrue_S, NewLabel("GetEnumerator2"));
                handlerIl.Emit(Pop);
                handlerIl.Emit(Ldc_I4_0);
                handlerIl.Emit(Newarr, listType);
                handlerIl.MarkLabel(labels["GetEnumerator2"]);
                handlerIl.Emit(Callvirt, enumerable.GetMethod("GetEnumerator"));
                handlerIl.Emit(Stloc_0);
                handlerIl.BeginExceptionBlock();
                handlerIl.Emit(Br_S, NewLabel("Next2"));
                handlerIl.MarkLabel(NewLabel("Loop2"));
                handlerIl.Emit(Ldarg_0);
                handlerIl.Emit(Callvirt, entity);
                handlerIl.Emit(Callvirt, modelProperty.GetMethod);
                handlerIl.Emit(Ldloc_0);
                handlerIl.Emit(Callvirt, enumerator.GetMethod("get_Current"));
                handlerIl.Emit(Callvirt, collectionType.GetMethod("Remove"));
                handlerIl.Emit(Pop);
                handlerIl.MarkLabel(labels["Next2"]);
                handlerIl.Emit(Ldloc_0);
                handlerIl.Emit(Callvirt, typeof(IEnumerator).GetMethod("MoveNext"));
                handlerIl.Emit(Brtrue_S, labels["Loop2"]);
                handlerIl.BeginFinallyBlock();
                handlerIl.Emit(Ldloc_0);
                handlerIl.Emit(Isinst, typeof(IDisposable));
                handlerIl.Emit(Stloc_1);
                handlerIl.Emit(Ldloc_1);
                handlerIl.Emit(Brfalse_S, NewLabel("Disposed2"));
                handlerIl.Emit(Ldloc_1);
                handlerIl.Emit(Callvirt, typeof(IDisposable).GetMethod("Dispose"));
                handlerIl.MarkLabel(labels["Disposed2"]);
                handlerIl.EndExceptionBlock();
                handlerIl.Emit(Ldarg_0);
                handlerIl.Emit(Ldstr, modelProperty.Name);
                handlerIl.Emit(Callvirt, typeof(NotifyPropertyChanged).GetMethod("OnPropertyChanged", NonPublic | Instance));
                handlerIl.MarkLabel(labels["Exit"]);
                handlerIl.Emit(Ret);
            }

            // Entity setter
            {
                var il32 = setEntityIl.DefineLabel();
                var il37 = setEntityIl.DefineLabel();
                var il41 = setEntityIl.DefineLabel();
                var il5d = setEntityIl.DefineLabel();
                var il49 = setEntityIl.DefineLabel();
                var il71 = setEntityIl.DefineLabel();
                var enumerator = setEntityIl.DeclareLocal(typeof(IEnumerator));
                var disposable = setEntityIl.DeclareLocal(typeof(IDisposable));

                //setEntityIl.BeginExceptionBlock();
                setEntityIl.Emit(Ldarg_0);
                setEntityIl.Emit(Call, thisProp.GetMethod);
                setEntityIl.Emit(Callvirt, thisProp.PropertyType.GetMethod("Clear"));
                //setEntityIl.BeginCatchBlock(typeof(Exception));
                //setEntityIl.Emit(Pop);
                //setEntityIl.EndExceptionBlock();

                setEntityIl.Emit(Ldarg_0);
                setEntityIl.Emit(Callvirt, entity);
                setEntityIl.Emit(Dup);
                setEntityIl.Emit(Brtrue_S, il32);
                setEntityIl.Emit(Pop);
                setEntityIl.Emit(Ldnull);
                setEntityIl.Emit(Br_S, il37);
                setEntityIl.MarkLabel(il32);
                setEntityIl.Emit(Callvirt, modelProperty.GetMethod);
                setEntityIl.MarkLabel(il37);
                setEntityIl.Emit(Dup);
                setEntityIl.Emit(Brtrue_S, il41);
                setEntityIl.Emit(Pop);
                setEntityIl.Emit(Ldc_I4_0);
                setEntityIl.Emit(Newarr, listType);
                setEntityIl.MarkLabel(il41);
                setEntityIl.Emit(Callvirt, typeof(IEnumerable).GetMethod("GetEnumerator"));
                setEntityIl.Emit(Stloc, enumerator);
                setEntityIl.BeginExceptionBlock();
                setEntityIl.Emit(Br_S, il5d);
                setEntityIl.MarkLabel(il49);

                setEntityIl.Emit(Ldarg_0);
                setEntityIl.Emit(Call, thisProp.GetMethod);
                setEntityIl.Emit(Ldloc, enumerator);
                setEntityIl.Emit(Callvirt, typeof(IEnumerator).GetMethod("get_Current"));
                setEntityIl.Emit(Callvirt, thisProp.PropertyType.GetMethod("Add"));

                setEntityIl.MarkLabel(il5d);
                setEntityIl.Emit(Ldloc, enumerator);
                setEntityIl.Emit(Callvirt, typeof(IEnumerator).GetMethod("MoveNext"));
                setEntityIl.Emit(Brtrue_S, il49);
                setEntityIl.BeginFinallyBlock();
                setEntityIl.Emit(Ldloc, enumerator);
                setEntityIl.Emit(Isinst, typeof(IDisposable));
                setEntityIl.Emit(Stloc, disposable);
                setEntityIl.Emit(Ldloc, disposable);
                setEntityIl.Emit(Brfalse_S, il71);
                setEntityIl.Emit(Ldloc, disposable);
                setEntityIl.Emit(Callvirt, typeof(IDisposable).GetMethod("Dispose"));
                setEntityIl.MarkLabel(il71);
                setEntityIl.EndExceptionBlock();
            }

            // Inicializaciones en el constructor
            { 
                ctorIl.Emit(Ldarg_0);
                ctorIl.Emit(Newobj, thisType.GetConstructor(Type.EmptyTypes));
                ctorIl.Emit(Stfld, thisField);
                ctorIl.Emit(Ldarg_0);
                ctorIl.Emit(Ldfld, thisField);
                ctorIl.Emit(Ldarg_0);
                ctorIl.Emit(Ldftn, handler);
                ctorIl.Emit(Newobj, typeof(NotifyCollectionChangedEventHandler).GetConstructors().Single());
                ctorIl.Emit(Callvirt, typeof(INotifyCollectionChanged).GetMethod("add_CollectionChanged"));
            }
        }

        #endregion

        #region Constructores de ViewModels

        private static void BuildViewModel(TypeBuilder tb, Type modelType)
        {
            var modelProps = modelType.GetProperties(BindingFlags.Public | Instance).Where(p => p.CanRead);
            var ctor = tb.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes).GetILGenerator();
            var updatingObservables = tb.DefineField("updatingObservables", typeof(bool), FieldAttributes.Private);
            var entity = tb.DefineField("_entity", modelType, FieldAttributes.Private);

            var entityProp = tb.DefineProperty("Entity", PropertyAttributes.HasDefault, modelType, null);
            //var entityProp2 = tb.DefineProperty("TheXDS.MCART.ViewModel.IDynamicViewModel.Entity", PropertyAttributes.HasDefault, typeof(object), null);
            var getEntity = tb.DefineMethod($"get_Entity", _gsArgs | Virtual, modelType, null);
            var getEntityIl = getEntity.GetILGenerator();
            //var getEntity2 = tb.DefineMethod($"TheXDS.MCART.ViewModel.IDynamicViewModel.get_Entity", Private | Final | HideBySig | SpecialName | NewSlot | Virtual, typeof(object), null);
            //var getEntity2Il = getEntity2.GetILGenerator();
            var setEntity = tb.DefineMethod($"set_Entity", _gsArgs | Virtual, null, new[] { modelType });
            var setEntityIl = setEntity.GetILGenerator();
            //var setEntity2 = tb.DefineMethod($"TheXDS.MCART.ViewModel.IDynamicViewModel.set_Entity", Private | Final | HideBySig | SpecialName | NewSlot | Virtual, null, new[] { typeof(object) });
            //var setEntity2Il = setEntity.GetILGenerator();

            var editMethod = tb.DefineMethod($"Edit", (tb.BaseType.GetMethod("Edit").IsAbstract || tb.BaseType.GetMethod("Edit").IsVirtual ? Virtual : 0) | MethodAttributes.Public | HideBySig, null, new[] { modelType });
            var editIl = editMethod.GetILGenerator();
            var refreshMethod = tb.DefineMethod($"Refresh", Virtual | MethodAttributes.Public | HideBySig, null, null);
            var refreshIl = refreshMethod.GetILGenerator();

            entityProp.SetGetMethod(getEntity);
            entityProp.SetSetMethod(setEntity);
            //entityProp2.SetGetMethod(getEntity2);
            //entityProp2.SetSetMethod(setEntity2);

            ctor.Emit(Ldarg_0);
            ctor.Emit(Call, tb.BaseType.GetConstructor(Type.EmptyTypes) ?? tb.BaseType.GetConstructors(NonPublic | Instance).First(p=>p.GetParameters().Length==0) ?? throw new InvalidOperationException());

            getEntityIl.Emit(Ldarg_0);
            getEntityIl.Emit(Ldfld, entity);
            getEntityIl.Emit(Ret);

            //getEntity2Il.Emit(Ldarg_0);
            ////getEntity2Il.Emit(Callvirt, getEntity);
            //getEntity2Il.Emit(Ldfld, entity);
            //getEntity2Il.Emit(Ret);

            setEntityIl.Emit(Ldarg_0);
            setEntityIl.Emit(Ldarg_0);
            setEntityIl.Emit(Ldflda, entity);
            setEntityIl.Emit(Ldarg_1);
            setEntityIl.Emit(Ldstr, "Entity");
            setEntityIl.Emit(Call, typeof(NotifyPropertyChanged).GetMethod("Change", Instance | NonPublic).MakeGenericMethod(modelType));
            var ret = setEntityIl.DefineLabel();
            setEntityIl.Emit(Brfalse, ret);
            setEntityIl.Emit(Ldarg_0);
            LoadConstant(setEntityIl, true);
            setEntityIl.Emit(Stfld,updatingObservables);

            //setEntity2Il.Emit(Ldarg_0);
            //setEntity2Il.Emit(Ldarg_1);
            //setEntity2Il.Emit(Isinst, modelType);
            //setEntity2Il.Emit(Callvirt, setEntity);
            //setEntity2Il.Emit(Ret);

            foreach (var j in modelProps)
            {
                refreshIl.Emit(Ldarg_0);
                refreshIl.Emit(Ldstr, j.Name);
                refreshIl.Emit(Callvirt, tb.BaseType.GetMethod("OnPropertyChanged", Instance | NonPublic));
                if (typeof(ICollection<>).MakeGenericType(GetListType(j.PropertyType)).IsAssignableFrom(j.PropertyType))                
                    AddEntityCollection(tb, ctor, setEntityIl, j,updatingObservables, out var prop);                
                else                
                    AddEntityProp(tb,editIl, j, Infer(j.PropertyType), out var prop);                
            }

            ctor.Emit(Ret);

            setEntityIl.Emit(Ldarg_0);
            LoadConstant(setEntityIl, false);
            setEntityIl.Emit(Stfld, updatingObservables);
            setEntityIl.Emit(Ldarg_0);
            setEntityIl.Emit(Callvirt, tb.BaseType.GetMethod("Refresh"));
            setEntityIl.MarkLabel(ret);
            setEntityIl.Emit(Ret);

            if (!tb.BaseType.GetMethod("Edit").IsAbstract)
            {
                editIl.Emit(Ldarg_0);
                editIl.Emit(Ldarg_1);
                editIl.Emit(Call, tb.BaseType.GetMethod("Edit"));
            }
            refreshIl.Emit(Ret);
            editIl.Emit(Ret);
                       
            MethodAttributes Infer(Type propType)
            {
                var rv = Virtual | NewSlot;
                if (!propType.IsClass || propType == typeof(string))
                {
                    rv |= Final;
                }
                return rv;
            }
        }

        /// <summary>
        ///     Compila y genera un nuevo ViewModel con los tipos de herencia y
        ///     de interfaz especificados, y devuelve una nueva instancia del
        ///     mismo.
        /// </summary>
        /// <typeparam name="TViewModel">
        ///     Tipo base del ViewModel. Este tipo podrá contener todas las
        ///     definiciones adicionales necesarias que no son auto-generadas,
        ///     como ser los campos calculados, comandos, y campos auxiliares.
        ///     Si se omite, se creará un ViewModel que hereda de la clase
        ///     <see cref="GeneratedViewModel{T}"/>.
        /// </typeparam>
        /// <typeparam name="TInterface">
        ///     Interfaz que describe los campos expuestos por este ViewModel.
        /// </typeparam>
        /// <returns>
        ///     Una nueva instancia del ViewModel solicitado.
        /// </returns>
        [Thunk]
        public static TViewModel NewSelfViewModel<TViewModel, TInterface>() where TViewModel : IGeneratedViewModel<TInterface> where TInterface : class => BuildSelfViewModel(typeof(TViewModel), typeof(TInterface)).New<TViewModel>();

        /// <summary>
        ///     Compila y genera un nuevo ViewModel con el tipo de interfaz
        ///     especificado, y devuelve una nueva instancia del mismo.
        /// </summary>
        /// <typeparam name="TInterface">
        ///     Interfaz que describe los campos expuestos por este ViewModel.
        /// </typeparam>
        /// <returns>
        ///     Una nueva instancia del ViewModel solicitado.
        /// </returns>
        [Thunk]
        public static GeneratedViewModel<TInterface> NewSelfViewModel<TInterface>() where TInterface : class => BuildSelfViewModel(typeof(TInterface)).New<GeneratedViewModel<TInterface>>();

        /// <summary>
        ///     Compila y genera un nuevo tipo que puede utilizarse como un
        ///     ViewModel.
        /// </summary>
        /// <typeparam name="TViewModel">
        ///     Tipo base del ViewModel. Este tipo podrá contener todas las
        ///     definiciones adicionales necesarias que no son auto-generadas,
        ///     como ser los campos calculados, comandos, y campos auxiliares.
        ///     Si se omite, se creará un ViewModel que hereda de la clase
        ///     <see cref="GeneratedViewModel{T}"/>.
        /// </typeparam>
        /// <typeparam name="TInterface">
        ///     Interfaz que describe los campos expuestos por este ViewModel.
        /// </typeparam>
        /// <returns></returns>
        public static Type BuildSelfViewModel<TViewModel, TInterface>() where TViewModel : IGeneratedViewModel<TInterface> where TInterface : class => BuildSelfViewModel(typeof(TViewModel), typeof(TInterface));

        /// <summary>
        ///     Compila y genera un nuevo tipo que puede utilizarse como un
        ///     ViewModel.
        /// </summary>
        /// <typeparam name="T">
        ///     Interfaz que describe los campos expuestos por este ViewModel.
        /// </typeparam>
        /// <returns>
        ///     Un nuevo tipo compilado en runtimeque expone propiedades para
        ///     cada una de mas mismas definidas en la interfaz especificada.
        /// </returns>
        public static Type BuildSelfViewModel<T>() where T : class => BuildSelfViewModel(typeof(T));

        /// <summary>
        ///     Compila y genera un nuevo tipo que puede utilizarse como un
        ///     ViewModel.
        /// </summary>
        /// <param name="interfaceType">
        ///     Interfaz que describe los campos expuestos por este ViewModel.
        /// </param>
        /// <returns>
        ///     Un nuevo tipo compilado en runtimeque expone propiedades para
        ///     cada una de mas mismas definidas en la interfaz especificada.
        /// </returns>
        public static Type BuildSelfViewModel(Type interfaceType) => BuildSelfViewModel(typeof(GeneratedViewModel<>).MakeGenericType(interfaceType), interfaceType);

        /// <summary>
        ///     Compila y genera un nuevo tipo que puede utilizarse como un
        ///     ViewModel.
        /// </summary>
        /// <param name="baseType">
        ///     Tipo base del ViewModel. Este tipo podrá contener todas las
        ///     definiciones adicionales necesarias que no son auto-generadas,
        ///     como ser los campos calculados, comandos, y campos auxiliares.
        ///     Si se omite, se creará un ViewModel que hereda de la clase
        ///     <see cref="GeneratedViewModel{T}"/>.
        /// </param>
        /// <param name="interfaceType">
        ///     Interfaz que describe los campos expuestos por este ViewModel.
        /// </param>
        /// <returns>
        ///     Un nuevo tipo compilado en runtime que expone propiedades para
        ///     cada una de mas mismas definidas en la interfaz especificada.
        /// </returns>
        public static Type BuildSelfViewModel(Type baseType, Type interfaceType)
        {
            if (!interfaceType.IsInterface) throw new InvalidTypeException(interfaceType);
            if (!typeof(IGeneratedViewModel<>).MakeGenericType(interfaceType).IsAssignableFrom(baseType)) throw new InvalidTypeException(baseType);
            if (_builtViewModels.ContainsKey(interfaceType)) return _builtViewModels[interfaceType];
            var tb = NewType(NoIfaceName($"{interfaceType.Name}ViewModel"), baseType, new[]
            {
                typeof(IGeneratedViewModel<>).MakeGenericType(interfaceType),
                interfaceType
            });
            BuildViewModel(tb, interfaceType);

            var selfProp = tb.DefineProperty("Self", PropertyAttributes.None, interfaceType, null);
            var getSelf = tb.DefineMethod($"get_Self", _gsArgs | Virtual, interfaceType, null);
            var getSelfIl = getSelf.GetILGenerator();

            getSelfIl.Emit(Ldarg_0);
            getSelfIl.Emit(Ret);
            selfProp.SetGetMethod(getSelf);

            var retVal = tb.CreateType();
            _builtViewModels.Add(interfaceType, retVal);
            return retVal;
        }

        /// <summary>
        ///     Compila y genera un nuevo ViewModel con los tipos de herencia y
        ///     de modelo especificados, y devuelve una nueva instancia del
        ///     mismo.
        /// </summary>
        /// <typeparam name="TModel">
        ///     Modelo de datos que será editable por medio de este ViewModel.
        /// </typeparam>
        /// <returns>
        ///     Una nueva instancia del ViewModel solicitado.
        /// </returns>
        public static DynamicViewModel<TModel> NewViewModel<TModel>() where TModel : class, new() => NewViewModel<DynamicViewModel<TModel>,TModel>();

        /// <summary>
        ///     Compila y genera un nuevo ViewModel con los tipos de herencia y
        ///     de modelo especificados, y devuelve una nueva instancia del
        ///     mismo.
        /// </summary>
        /// <typeparam name="TViewModel">
        ///     Tipo base del ViewModel. Este tipo podrá contener todas las
        ///     definiciones adicionales necesarias que no son auto-generadas,
        ///     como ser los campos calculados, comandos, y campos auxiliares.
        ///     Si se omite, se creará un ViewModel que hereda de la clase
        ///     <see cref="DynamicViewModel{T}"/>.
        /// </typeparam>
        /// <typeparam name="TModel">
        ///     Modelo de datos que será editable por medio de este ViewModel.
        /// </typeparam>
        /// <returns>
        ///     Una nueva instancia del ViewModel solicitado.
        /// </returns>
        public static TViewModel NewViewModel<TViewModel, TModel>() where TViewModel : IDynamicViewModel<TModel> where TModel : class, new() => BuildViewModel<TViewModel,TModel>().New<TViewModel>();

        /// <summary>
        ///     Compila y genera un nuevo tipo que puede utilizarse como un
        ///     ViewModel.
        /// </summary>
        /// <typeparam name="TModel">
        ///     Modelo de datos que será editable por medio de este ViewModel.
        /// </typeparam>
        /// <returns>
        ///     Un nuevo tipo compilado en runtime que permite editar una
        ///     entidad por medio de MVVM.
        /// </returns>
        public static Type BuildViewModel<TModel>() where TModel : class, new() => BuildViewModel<DynamicViewModel<TModel>, TModel>();

        /// <summary>
        ///     Compila y genera un nuevo tipo que puede utilizarse como un
        ///     ViewModel.
        /// </summary>
        /// <typeparam name="TViewModel">
        ///     Tipo base del ViewModel. Este tipo podrá contener todas las
        ///     definiciones adicionales necesarias que no son auto-generadas,
        ///     como ser los campos calculados, comandos, y campos auxiliares.
        ///     Si se omite, se creará un ViewModel que hereda de la clase
        ///     <see cref="DynamicViewModel{T}"/>.
        /// </typeparam>
        /// <typeparam name="TModel">
        ///     Modelo de datos que será editable por medio de este ViewModel.
        /// </typeparam>
        /// <returns>
        ///     Un nuevo tipo compilado en runtime que permite editar una
        ///     entidad por medio de MVVM.
        /// </returns>
        public static Type BuildViewModel<TViewModel, TModel>() where TViewModel : IDynamicViewModel<TModel> where TModel : class,new() => BuildViewModel(typeof(TViewModel), typeof(TModel));
  
        /// <summary>
        ///     Compila y genera un nuevo tipo que puede utilizarse como un
        ///     ViewModel.
        /// </summary>
        /// <param name="modelType">
        ///     Modelo de datos que será editable por medio de este ViewModel.
        /// </param>
        /// <returns>
        ///     Un nuevo tipo compilado en runtime que permite editar una
        ///     entidad por medio de MVVM.
        /// </returns>
        public static Type BuildViewModel(Type modelType) => BuildViewModel(typeof(DynamicViewModel<>).MakeGenericType(modelType), modelType);

        /// <summary>
        ///     Compila y genera un nuevo tipo que puede utilizarse como un
        ///     ViewModel.
        /// </summary>
        /// <param name="baseType">
        ///     Tipo base del ViewModel. Este tipo podrá contener todas las
        ///     definiciones adicionales necesarias que no son auto-generadas,
        ///     como ser los campos calculados, comandos, y campos auxiliares.
        ///     Si se omite, se creará un ViewModel que hereda de la clase
        ///     <see cref="DynamicViewModel{T}"/>.
        /// </param>
        /// <param name="modelType">
        ///     Modelo de datos que será editable por medio de este ViewModel.
        /// </param>
        /// <returns>
        ///     Un nuevo tipo compilado en runtime que permite editar una
        ///     entidad por medio de MVVM.
        /// </returns>
        public static Type BuildViewModel(Type baseType,Type modelType)
        {
            if (!typeof(IDynamicViewModel<>).MakeGenericType(modelType).IsAssignableFrom(baseType)) throw new InvalidTypeException(baseType);
            if (_builtDynViewModels.ContainsKey(modelType)) return _builtDynViewModels[modelType];
            var modelProps = modelType.GetProperties(BindingFlags.Public | Instance).Where(p => p.CanRead);
            var tb = NewType($"{modelType.Name}ViewModel", baseType, new[]
            {
                typeof(IDynamicViewModel<>).MakeGenericType(modelType)
            });
            BuildViewModel(tb, modelType);

            var retVal = tb.CreateType();
            _builtDynViewModels.Add(modelType, retVal);
            return retVal;
        }

        #endregion

        #region Constructores de Modelos

        /// <summary>
        ///     Compila y genera un nuevo tipo que puede utilizarse como modelo
        ///     simple de datos.
        /// </summary>
        /// <typeparam name="T">
        ///     Interfaz que describe los campos expuestos por este modelo.
        /// </typeparam>
        /// <returns>
        ///     Un nuevo tipo compilado en runtimeque expone propiedades para
        ///     cada una de mas mismas definidas en la interfaz especificada.
        /// </returns>
        public static Type BuildModel<T>() where T : class => BuildModel(typeof(T));

        /// <summary>
        ///     Compila y genera un nuevo tipo que puede utilizarse como modelo
        ///     simple de datos.
        /// </summary>
        /// <param name="interfaceType">
        ///     Interfaz que describe los campos expuestos por este modelo.
        /// </param>
        /// <returns>
        ///     Un nuevo tipo compilado en runtimeque expone propiedades para
        ///     cada una de mas mismas definidas en la interfaz especificada.
        /// </returns>
        public static Type BuildModel(Type interfaceType)
        {
            if (!interfaceType.IsInterface) throw new InvalidTypeException(interfaceType);
            if (_builtModels.ContainsKey(interfaceType)) return _builtModels[interfaceType];

            var modelProps = interfaceType.GetProperties(BindingFlags.Public | Instance).Where(p => p.CanRead);
            var tb = NewType(NoIfaceName(interfaceType.Name), interfaceType);
            var ctor = tb.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes).GetILGenerator();

            foreach (var j in modelProps)
            {
                AddAutoProp(tb, j, out var prop, out var field);
                if (j.HasAttr(out DefaultValueAttribute dva))
                {
                    InitField(ctor, field, dva.Value);
                }
                if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType) && prop.PropertyType != typeof(string))
                {
                    InitField(ctor, field, typeof(Collection<>).MakeGenericType(GetListType(prop.PropertyType)), Type.EmptyTypes);
                }
            }

            ctor.Emit(Ldarg_0);
            ctor.Emit(Call, typeof(object).GetConstructor(Type.EmptyTypes) ?? throw new TamperException());
            ctor.Emit(Ret);
            var retVal = tb.CreateType();
            _builtModels.Add(interfaceType, retVal);
            return retVal;
        }

        #endregion
    }
}