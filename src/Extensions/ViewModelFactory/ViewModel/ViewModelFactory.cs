/*
ViewModelFactory.cs

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

//#define IncludeLockBlock
#define AltMethods

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TheXDS.MCART.ViewModel
{
//    /// <summary>
//    /// Marca una propiedad para ser excluída a la hora de generar un
//    /// <see cref="DynamicViewModel{T}"/>.
//    /// </summary>
//    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
//    public sealed class ExcludeAttribute : Attribute { }

//    /// <summary>
//    /// Fábrica de tipos para implementaciones ViewModel dinámicas.
//    /// </summary>
//    public static class ViewModelFactory
//    {
//        /// <summary>
//        /// Contiene una lista de atributos de exclusión a la hora de
//        /// generar ViewModels.
//        /// </summary>
//        public static HashSet<Type> AttributeExclusionList { get; } = new HashSet<Type>();

//        private const MethodAttributes _gsArgs = MethodAttributes.Public | SpecialName | HideBySig;
//        private const string _namespace = "TheXDS.MCART.ViewModel._Generated";
//        private static readonly ModuleBuilder _mBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(_namespace), AssemblyBuilderAccess.Run).DefineDynamicModule(_namespace);
//        private static readonly Dictionary<Type, Type> _builtViewModels = new Dictionary<Type, Type>();
//        private static readonly Dictionary<Type, Type> _builtModels = new Dictionary<Type, Type>();
//        private static readonly Dictionary<Type, Type> _builtDynViewModels = new Dictionary<Type, Type>();

//        #region Helpers

//        private static void InitField(ILGenerator ilGen, FieldBuilder field, object? value)
//        {
//            ilGen.Emit(Ldarg_0);
//            LoadConstant(ilGen, value);
//            ilGen.Emit(Stfld, field);
//        }
//        private static void InitField(ILGenerator ilGen, FieldBuilder field, Type instanceType, params object?[] args)
//        {
//            if (instanceType.IsAbstract) throw new InvalidTypeException(instanceType);
//            var c = instanceType.GetConstructor(args.ToTypes().ToArray()) ?? throw new TypeLoadException();
//            ilGen.Emit(Ldarg_0);
//            foreach (var j in args) LoadConstant(ilGen, j);
//            ilGen.Emit(Newobj, c);
//            ilGen.Emit(Stfld, field);
//        }
//        private static void LoadConstant(this ILGenerator ilGen, object? value)
//        {
//            switch (value)
//            {
//                case Enum e:
//                    LoadConstant(ilGen, e.ToUnderlyingType());
//                    break;
//                case byte b:
//                    ilGen.Emit(Ldc_I4_S, b);
//                    break;
//                case sbyte sb:
//                    ilGen.Emit(Ldc_I4_S, sb);
//                    break;
//                case bool b:
//                    ilGen.Emit(b ? Ldc_I4_1 : Ldc_I4_0);
//                    break;
//                case char ch:
//                    ilGen.Emit(Ldc_I4, unchecked(ch));
//                    break;
//                case short sh:
//                    ilGen.Emit(Ldc_I4, unchecked((int)sh));
//                    break;
//                case ushort ush:
//                    ilGen.Emit(Ldc_I4, unchecked(ush));
//                    break;
//                case int i:
//                    ilGen.Emit(Ldc_I4, i);
//                    break;
//                case uint ui:
//                    ilGen.Emit(Ldc_I4, unchecked((int)ui));
//                    break;
//                case long l:
//                    ilGen.Emit(Ldc_I8, l);
//                    break;
//                case ulong ul:
//                    ilGen.Emit(Ldc_I8, unchecked((long)ul));
//                    break;
//                case float f:
//                    ilGen.Emit(Ldc_R4, f);
//                    break;
//                case double d:
//                    ilGen.Emit(Ldc_R8, d);
//                    break;
//                case decimal de:
//                    foreach (var j in decimal.GetBits(de))
//                    {
//                        ilGen.Emit(Ldc_I4, j);
//                    }
//                    ilGen.Emit(Newobj, typeof(decimal).GetConstructor(new Type[]
//                    {
//                        typeof(int),
//                        typeof(int),
//                        typeof(int),
//                        typeof(bool),
//                        typeof(byte)
//                    })!);
//                    break;
//                case string s:
//                    if (s is null) ilGen.Emit(Ldnull);                    
//                    else ilGen.Emit(Ldstr, s);
//                    break;
//                case Type t:
//                    ilGen.Emit(Ldtoken, t);
//                    ilGen.Emit(Call, typeof(Type).GetMethod("GetTypeFromHandle")!);
//                    break;
//                case null:
//                    ilGen.Emit(Ldnull);
//                    break;
//                default:
//                    if (value.GetType().IsValueType)
//                    {
//                        ilGen.Emit(Newobj, value.GetType().GetConstructor(Type.EmptyTypes)!);
//                    }
//                    else
//                    {
//                        ilGen.Emit(Ldnull);
//                    }
//                    break;
//            }
//        }
//        private static string UndName(string name)
//        {
//            if (name.IsEmpty()) throw new ArgumentNullException(name);
//            return name.Length > 1
//                ? $"_{name.Substring(0, 1).ToLower()}{name.Substring(1)}"
//                : $"_{name.ToLower()}";
//        }
//        private static string NoIfaceName(string name)
//        {
//            if (name.IsEmpty()) throw new ArgumentNullException(name);
//            return name[0] != 'I' ? $"{name}Implementation" : name.Substring(1);
//        }
        
//        private static TypeBuilder NewType(string name, Type baseType, params Type[] interfaces) => _mBuilder.DefineType($"{_namespace}.{name}_{Guid.NewGuid().ToString().Replace("-", "")}", TypeAttributes.Public | TypeAttributes.Class, baseType, interfaces);
//        private static Type GetListType(Type listType) => listType.GenericTypeArguments?.Count() == 1 ? listType.GenericTypeArguments.Single() : typeof(object);
//        private static bool CanMap(PropertyInfo p)
//        {
//            return p.CanRead && !p.HasAttr<ExcludeAttribute>() && AttributeExclusionList.All(q => p.GetCustomAttribute(q) is null);
//        }

//        #endregion

//        #region Construcción de campos

//        private static void AddAutoProp(TypeBuilder tb, PropertyInfo property, out PropertyBuilder prop, out FieldBuilder field)
//        {
//            AddFieldProp(tb, property, (setIl, f) =>
//             {
//                 setIl.Emit(Ldarg_0);
//                 setIl.Emit(Ldarg_1);
//                 setIl.Emit(Stfld, f);
//             }, out prop, out field);
//        }
//        private static void AddFieldProp(TypeBuilder tb, PropertyInfo property, Action<ILGenerator, FieldBuilder> setMethodBuilder, out PropertyBuilder prop, out FieldBuilder field) => AddFieldProp(tb, property, property.PropertyType, ReuseSlot, setMethodBuilder, out prop, out field);
//        private static void AddFieldProp(TypeBuilder tb, PropertyInfo property, Type fieldType, MethodAttributes morFlags, Action<ILGenerator, FieldBuilder> setMethodBuilder, out PropertyBuilder prop, out FieldBuilder field)
//        {
//            field = tb.DefineField(UndName(property.Name), fieldType, FieldAttributes.Private | (!property.CanWrite ? FieldAttributes.InitOnly : FieldAttributes.PrivateScope));
//            prop = tb.DefineProperty(property.Name, PropertyAttributes.HasDefault, property.PropertyType, null);
//            var getM = tb.DefineMethod($"get_{property.Name}", _gsArgs | morFlags, property.PropertyType, null);
//            var getIl = getM.GetILGenerator();
//            getIl.Emit(Ldarg_0);
//            getIl.Emit(Ldfld, field);
//            getIl.Emit(Ret);
//            prop.SetGetMethod(getM);

//            if (property.CanWrite)
//            {
//                var setM = tb.DefineMethod($"set_{property.Name}", _gsArgs | morFlags, null, new[] { property.PropertyType });
//                var setIl = setM.GetILGenerator();
//                setMethodBuilder?.Invoke(setIl, field);
//                setIl.Emit(Ret);
//                prop.SetSetMethod(setM);
//            }
//        }

//        #endregion

//        #region Operaciones atómicas

//        private static ILGenerator MakeCtor(TypeBuilder tb)
//        {
//            var ctor= tb.DefineConstructor(_gsArgs | RTSpecialName, CallingConventions.HasThis, Type.EmptyTypes).GetILGenerator();
//            ctor.Emit(Ldarg_0);
//            ctor.Emit(Call, tb.BaseType.GetConstructor(Type.EmptyTypes) ?? tb.BaseType.GetConstructors(NonPublic | Instance).First(p=>p.GetParameters().Length==0) ?? throw new InvalidOperationException());
//            return ctor;
//        }

//        private static PropertyBuilder MakeProp(this TypeBuilder tb, string name, Type type, out ILGenerator getter, out ILGenerator setter)
//        {
//            return MakeProp(tb, name, type, _gsArgs, out getter, out setter);
//        }

//        private static PropertyBuilder MakeProp(this TypeBuilder tb, string name, Type type, MethodAttributes attrs, out ILGenerator getter, out ILGenerator setter)
//        {
//            var getProp = tb.DefineMethod($"get_{name}", attrs, type, null);
//            var setProp = tb.DefineMethod($"set_{name}", attrs, null, new[] { type });
//            var prop = tb.DefineProperty(name, PropertyAttributes.HasDefault, type, null);
//            getter = getProp.GetILGenerator();
//            setter = setProp.GetILGenerator();
//            prop.SetGetMethod(getProp);
//            prop.SetSetMethod(setProp);
//            return prop;
//        }
//        private static void MakeProp(this TypeBuilder tb, string name, Type type, out ILGenerator getter)
//        {
//            var getProp = tb.DefineMethod($"get_{name}", _gsArgs, type, null);
//            var prop = tb.DefineProperty(name, PropertyAttributes.HasDefault, type, null);
//            getter = getProp.GetILGenerator();
//            prop.SetGetMethod(getProp);
//        }

//        private static FieldBuilder MakeEntityProp(this TypeBuilder tb, Type modelType, out ILGenerator setter, out Label ret)
//        {
//            var entity = tb.DefineField("_entity", modelType, FieldAttributes.Private);
//            tb.MakeProp("Entity", modelType, _gsArgs | Virtual, out var getter, out setter);
//            getter.Emit(Ldarg_0);
//            getter.Emit(Ldfld, entity);
//            getter.Emit(Ret);

//            ret = setter.DefineLabel();
//            setter.Emit(Ldarg_0);
//            setter.Emit(Ldarg_0);
//            setter.Emit(Ldflda, entity);
//            setter.Emit(Ldarg_1);
//            setter.Emit(Ldstr, "Entity");
//            setter.Emit(Call, typeof(NotifyPropertyChanged).GetMethod("Change", Instance | NonPublic).MakeGenericMethod(modelType));
//            setter.Emit(Brfalse, ret);
//            return entity;
//        }

//        private static void AddEntityProp(this TypeBuilder tb, PropertyInfo prop, FieldInfo entity, ILGenerator edit, ILGenerator refresh)
//        {
//            var p = tb.MakeProp(prop.Name, prop.PropertyType, out var getter, out var setter);

//            var L000e = getter.DefineLabel();
//            var LgetRet = getter.DefineLabel();
//            getter.Emit(Ldarg_0);
//            getter.Emit(Ldfld, entity);
//            getter.Emit(Dup);
//            getter.Emit(Brtrue, L000e);
//            getter.Emit(Pop);



//            if (prop.PropertyType.IsValueType)
//            {
//                getter.LoadConstant(prop.PropertyType.Default());
//            }
//            else
//            {
//                getter.Emit(Ldnull);
//            }



//            getter.Emit(Br_S, LgetRet);
//            getter.MarkLabel(L000e);
//            getter.Emit(Call,prop.GetMethod);
//            getter.MarkLabel(LgetRet);
//            getter.Emit(Ret);

//            var LsetRet = setter.DefineLabel();
//            setter.Emit(Ldarg_0);
//            setter.Emit(Ldfld, entity);
//            setter.Emit(Brfalse, LsetRet);
//            setter.Emit(Ldarg_0);
//            setter.Emit(Ldfld, entity);
//            setter.Emit(Callvirt, prop.GetMethod);
//            setter.Emit(Ldarg_1);
//            setter.Emit(Call, typeof(object).GetMethod("Equals", new Type[] { typeof(object), typeof(object) }));
//            setter.Emit(Brtrue, LsetRet);
//            setter.Emit(Ldarg_0);
//            setter.Emit(Ldfld, entity);
//            setter.Emit(Ldarg_1);

//            setter.Emit(Callvirt, prop.SetMethod);
//            setter.Emit(Ldarg_0);
//            setter.Emit(Ldstr, prop.Name);
//            setter.Emit(Call, tb.BaseType.GetMethod("Notify", NonPublic | Instance, null, new Type[] { typeof(string) }, null));
//            setter.MarkLabel(LsetRet);
//            setter.Emit(Ret);

//            edit.Emit(Ldarg_0);
//            edit.Emit(Ldarg_1);
//            edit.Emit(Callvirt, prop.GetMethod);
//            edit.Emit(Call, p.SetMethod);

//            refresh.Emit(Ldarg_0);
//            refresh.Emit(Ldstr, prop.Name);
//            refresh.Emit(Callvirt, tb.BaseType.GetMethod("Notify", NonPublic | Instance, null, new Type[] { typeof(string) }, null));
//        }

//        private static void AddEntityCollection(this TypeBuilder tb, PropertyInfo prop, ILGenerator ctor, ILGenerator edit, ILGenerator refresh, ILGenerator entitySetter)
//        {
//            var it = prop.PropertyType.ResolveCollectionType();
//            var ct = typeof(ICollection<>).MakeGenericType(it);
//            var ot = typeof(Types.ObservableCollectionWrap<>).MakeGenericType(it);
//            var fld = tb.DefineField($"_{prop.Name}", ot, FieldAttributes.Private);
//            tb.MakeProp(prop.Name, ct, out var getter);

//            getter.Emit(Ldarg_0);
//            getter.Emit(Ldfld, fld);
//            getter.Emit(Ret);

//            ctor.Emit(Ldarg_0);
//            ctor.Emit(Newobj, ot.GetConstructor(Type.EmptyTypes));
//            ctor.Emit(Stfld, fld);

//            edit.Emit(Ldarg_0);
//            edit.Emit(Ldfld, fld);
//            edit.Emit(Ldarg_1);
//            edit.Emit(Callvirt, prop.GetMethod);
//            edit.Emit(Callvirt, ot.GetMethod("Replace", new Type[] { ct }));

//            refresh.Emit(Ldarg_0);
//            refresh.Emit(Ldfld, fld);
//            refresh.Emit(Callvirt, ot.GetMethod("Refresh", Type.EmptyTypes));

//            var L003c = entitySetter.DefineLabel();
//            var L0042 = entitySetter.DefineLabel();
//            entitySetter.Emit(Ldarg_0);
//            entitySetter.Emit(Ldfld, fld);
//            entitySetter.Emit(Ldarg_1);            
//            entitySetter.Emit(Brtrue_S,L003c);
//            entitySetter.Emit(Ldnull);
//            entitySetter.Emit(Br_S,L0042);
//            entitySetter.MarkLabel(L003c);
//            entitySetter.Emit(Ldarg_1);
//            entitySetter.Emit(Callvirt, prop.GetMethod);
//            entitySetter.MarkLabel(L0042);
//            entitySetter.Emit(Callvirt, ot.GetMethod("Substitute", new Type[] { ct }));
//        }

//        #endregion

//        #region Constructores de ViewModels

//        private static void BuildViewModel(TypeBuilder tb, Type modelType)
//        {
//            var modelProps = modelType.GetProperties(BindingFlags.Public | Instance).Where(CanMap);
//            var ctor = MakeCtor(tb);
//            var entityFld = tb.MakeEntityProp(modelType, out var setter, out var setterRet);
//            var editMethod = tb.DefineMethod($"Edit", (tb.BaseType.GetMethod("Edit").IsAbstract || tb.BaseType.GetMethod("Edit").IsVirtual ? Virtual : 0) | MethodAttributes.Public | HideBySig, null, new[] { modelType });
//            var editIl = editMethod.GetILGenerator();
//            var refreshMethod = tb.DefineMethod($"Refresh", Virtual | MethodAttributes.Public | HideBySig, null, null);
//            var refreshIl = refreshMethod.GetILGenerator();
//            foreach (var j in modelProps)
//            {
//                if (typeof(ICollection<>).MakeGenericType(j.PropertyType.ResolveCollectionType()).IsAssignableFrom(j.PropertyType))
//                    tb.AddEntityCollection(j, ctor, editIl, refreshIl, setter);
//                else if (j.CanWrite)
//                    tb.AddEntityProp(j, entityFld, editIl, refreshIl);
//            }
//            ctor.Emit(Ret);
//            setter.Emit(Ldarg_0);
//            setter.Emit(Callvirt, tb.BaseType.GetMethod("Refresh"));
//            setter.MarkLabel(setterRet);
//            setter.Emit(Ret);
//            if (!tb.BaseType.GetMethod("Edit").IsAbstract)
//            {
//                editIl.Emit(Ldarg_0);
//                editIl.Emit(Ldarg_1);
//                editIl.Emit(Call, tb.BaseType.GetMethod("Edit"));
//            }
//            editIl.Emit(Ret);
//            refreshIl.Emit(Ret);
//        }

//        /// <summary>
//        /// Compila y genera un nuevo ViewModel con los tipos de herencia y
//        /// de interfaz especificados, y devuelve una nueva instancia del
//        /// mismo.
//        /// </summary>
//        /// <typeparam name="TViewModel">
//        /// Tipo base del ViewModel. Este tipo podrá contener todas las
//        /// definiciones adicionales necesarias que no son auto-generadas,
//        /// como ser los campos calculados, comandos, y campos auxiliares.
//        /// Si se omite, se creará un ViewModel que hereda de la clase
//        /// <see cref="GeneratedViewModel{T}"/>.
//        /// </typeparam>
//        /// <typeparam name="TInterface">
//        /// Interfaz que describe los campos expuestos por este ViewModel.
//        /// </typeparam>
//        /// <returns>
//        /// Una nueva instancia del ViewModel solicitado.
//        /// </returns>
//        [Sugar]
//        public static TViewModel NewSelfViewModel<TViewModel, TInterface>() where TViewModel : IGeneratedViewModel<TInterface> where TInterface : class => BuildSelfViewModel(typeof(TViewModel), typeof(TInterface)).New<TViewModel>();

//        /// <summary>
//        /// Compila y genera un nuevo ViewModel con el tipo de interfaz
//        /// especificado, y devuelve una nueva instancia del mismo.
//        /// </summary>
//        /// <typeparam name="TInterface">
//        /// Interfaz que describe los campos expuestos por este ViewModel.
//        /// </typeparam>
//        /// <returns>
//        /// Una nueva instancia del ViewModel solicitado.
//        /// </returns>
//        [Sugar]
//        public static GeneratedViewModel<TInterface> NewSelfViewModel<TInterface>() where TInterface : class => BuildSelfViewModel(typeof(TInterface)).New<GeneratedViewModel<TInterface>>();

//        /// <summary>
//        /// Compila y genera un nuevo tipo que puede utilizarse como un
//        /// ViewModel.
//        /// </summary>
//        /// <typeparam name="TViewModel">
//        /// Tipo base del ViewModel. Este tipo podrá contener todas las
//        /// definiciones adicionales necesarias que no son auto-generadas,
//        /// como ser los campos calculados, comandos, y campos auxiliares.
//        /// Si se omite, se creará un ViewModel que hereda de la clase
//        /// <see cref="GeneratedViewModel{T}"/>.
//        /// </typeparam>
//        /// <typeparam name="TInterface">
//        /// Interfaz que describe los campos expuestos por este ViewModel.
//        /// </typeparam>
//        /// <returns></returns>
//        public static Type BuildSelfViewModel<TViewModel, TInterface>() where TViewModel : IGeneratedViewModel<TInterface> where TInterface : class => BuildSelfViewModel(typeof(TViewModel), typeof(TInterface));

//        /// <summary>
//        /// Compila y genera un nuevo tipo que puede utilizarse como un
//        /// ViewModel.
//        /// </summary>
//        /// <typeparam name="T">
//        /// Interfaz que describe los campos expuestos por este ViewModel.
//        /// </typeparam>
//        /// <returns>
//        /// Un nuevo tipo compilado en runtimeque expone propiedades para
//        /// cada una de mas mismas definidas en la interfaz especificada.
//        /// </returns>
//        public static Type BuildSelfViewModel<T>() where T : class => BuildSelfViewModel(typeof(T));

//        /// <summary>
//        /// Compila y genera un nuevo tipo que puede utilizarse como un
//        /// ViewModel.
//        /// </summary>
//        /// <param name="interfaceType">
//        /// Interfaz que describe los campos expuestos por este ViewModel.
//        /// </param>
//        /// <returns>
//        /// Un nuevo tipo compilado en runtimeque expone propiedades para
//        /// cada una de mas mismas definidas en la interfaz especificada.
//        /// </returns>
//        public static Type BuildSelfViewModel(Type interfaceType) => BuildSelfViewModel(typeof(GeneratedViewModel<>).MakeGenericType(interfaceType), interfaceType);

//        /// <summary>
//        /// Compila y genera un nuevo tipo que puede utilizarse como un
//        /// ViewModel.
//        /// </summary>
//        /// <param name="baseType">
//        /// Tipo base del ViewModel. Este tipo podrá contener todas las
//        /// definiciones adicionales necesarias que no son auto-generadas,
//        /// como ser los campos calculados, comandos, y campos auxiliares.
//        /// Si se omite, se creará un ViewModel que hereda de la clase
//        /// <see cref="GeneratedViewModel{T}"/>.
//        /// </param>
//        /// <param name="interfaceType">
//        /// Interfaz que describe los campos expuestos por este ViewModel.
//        /// </param>
//        /// <returns>
//        /// Un nuevo tipo compilado en runtime que expone propiedades para
//        /// cada una de mas mismas definidas en la interfaz especificada.
//        /// </returns>
//        public static Type BuildSelfViewModel(Type baseType, Type interfaceType)
//        {
//            if (!interfaceType.IsInterface) throw new InvalidTypeException(interfaceType);
//            if (!typeof(IGeneratedViewModel<>).MakeGenericType(interfaceType).IsAssignableFrom(baseType)) throw new InvalidTypeException(baseType);
//            if (_builtViewModels.ContainsKey(interfaceType)) return _builtViewModels[interfaceType];
//            var tb = NewType(NoIfaceName($"{interfaceType.Name}ViewModel"), baseType, new[]
//            {
//                typeof(IGeneratedViewModel<>).MakeGenericType(interfaceType),
//                interfaceType
//            });
//            BuildViewModel(tb, interfaceType);

//            var selfProp = tb.DefineProperty("Self", PropertyAttributes.None, interfaceType, null);
//            var getSelf = tb.DefineMethod($"get_Self", _gsArgs | Virtual, interfaceType, null);
//            var getSelfIl = getSelf.GetILGenerator();

//            getSelfIl.Emit(Ldarg_0);
//            getSelfIl.Emit(Ret);
//            selfProp.SetGetMethod(getSelf);

//            var retVal = tb.CreateType();
//            _builtViewModels.Add(interfaceType, retVal);
//            return retVal;
//        }

//        /// <summary>
//        /// Compila y genera un nuevo ViewModel con los tipos de herencia y
//        /// de modelo especificados, y devuelve una nueva instancia del
//        /// mismo.
//        /// </summary>
//        /// <typeparam name="TModel">
//        /// Modelo de datos que será editable por medio de este ViewModel.
//        /// </typeparam>
//        /// <returns>
//        /// Una nueva instancia del ViewModel solicitado.
//        /// </returns>
//        public static DynamicViewModel<TModel> NewViewModel<TModel>() where TModel : class, new() => NewViewModel<DynamicViewModel<TModel>,TModel>();

//        /// <summary>
//        /// Compila y genera un nuevo ViewModel con los tipos de herencia y
//        /// de modelo especificados, y devuelve una nueva instancia del
//        /// mismo.
//        /// </summary>
//        /// <typeparam name="TViewModel">
//        /// Tipo base del ViewModel. Este tipo podrá contener todas las
//        /// definiciones adicionales necesarias que no son auto-generadas,
//        /// como ser los campos calculados, comandos, y campos auxiliares.
//        /// Si se omite, se creará un ViewModel que hereda de la clase
//        /// <see cref="DynamicViewModel{T}"/>.
//        /// </typeparam>
//        /// <typeparam name="TModel">
//        /// Modelo de datos que será editable por medio de este ViewModel.
//        /// </typeparam>
//        /// <returns>
//        /// Una nueva instancia del ViewModel solicitado.
//        /// </returns>
//        public static TViewModel NewViewModel<TViewModel, TModel>() where TViewModel : IDynamicViewModel<TModel> where TModel : class, new() => BuildViewModel<TViewModel,TModel>().New<TViewModel>();

//        /// <summary>
//        /// Compila y genera un nuevo tipo que puede utilizarse como un
//        /// ViewModel.
//        /// </summary>
//        /// <typeparam name="TModel">
//        /// Modelo de datos que será editable por medio de este ViewModel.
//        /// </typeparam>
//        /// <returns>
//        /// Un nuevo tipo compilado en runtime que permite editar una
//        /// entidad por medio de MVVM.
//        /// </returns>
//        public static Type BuildViewModel<TModel>() where TModel : class, new() => BuildViewModel<DynamicViewModel<TModel>, TModel>();

//        /// <summary>
//        /// Compila y genera un nuevo tipo que puede utilizarse como un
//        /// ViewModel.
//        /// </summary>
//        /// <typeparam name="TViewModel">
//        /// Tipo base del ViewModel. Este tipo podrá contener todas las
//        /// definiciones adicionales necesarias que no son auto-generadas,
//        /// como ser los campos calculados, comandos, y campos auxiliares.
//        /// Si se omite, se creará un ViewModel que hereda de la clase
//        /// <see cref="DynamicViewModel{T}"/>.
//        /// </typeparam>
//        /// <typeparam name="TModel">
//        /// Modelo de datos que será editable por medio de este ViewModel.
//        /// </typeparam>
//        /// <returns>
//        /// Un nuevo tipo compilado en runtime que permite editar una
//        /// entidad por medio de MVVM.
//        /// </returns>
//        public static Type BuildViewModel<TViewModel, TModel>() where TViewModel : IDynamicViewModel<TModel> where TModel : class,new() => BuildViewModel(typeof(TViewModel), typeof(TModel));
  
//        /// <summary>
//        /// Compila y genera un nuevo tipo que puede utilizarse como un
//        /// ViewModel.
//        /// </summary>
//        /// <param name="modelType">
//        /// Modelo de datos que será editable por medio de este ViewModel.
//        /// </param>
//        /// <returns>
//        /// Un nuevo tipo compilado en runtime que permite editar una
//        /// entidad por medio de MVVM.
//        /// </returns>
//        public static Type BuildViewModel(Type modelType) => BuildViewModel(typeof(DynamicViewModel<>).MakeGenericType(modelType), modelType);

//        /// <summary>
//        /// Compila y genera un nuevo tipo que puede utilizarse como un
//        /// ViewModel.
//        /// </summary>
//        /// <param name="baseType">
//        /// Tipo base del ViewModel. Este tipo podrá contener todas las
//        /// definiciones adicionales necesarias que no son auto-generadas,
//        /// como ser los campos calculados, comandos, y campos auxiliares.
//        /// Si se omite, se creará un ViewModel que hereda de la clase
//        /// <see cref="DynamicViewModel{T}"/>.
//        /// </param>
//        /// <param name="modelType">
//        /// Modelo de datos que será editable por medio de este ViewModel.
//        /// </param>
//        /// <returns>
//        /// Un nuevo tipo compilado en runtime que permite editar una
//        /// entidad por medio de MVVM.
//        /// </returns>
//        public static Type BuildViewModel(Type baseType,Type modelType)
//        {
//            if (!typeof(IDynamicViewModel<>).MakeGenericType(modelType).IsAssignableFrom(baseType)) throw new InvalidTypeException(baseType);
//            if (_builtDynViewModels.ContainsKey(modelType)) return _builtDynViewModels[modelType];
//            var modelProps = modelType.GetProperties(BindingFlags.Public | Instance).Where(p => p.CanRead);
//            var tb = NewType($"{modelType.Name}ViewModel", baseType, new[]
//            {
//                typeof(IDynamicViewModel<>).MakeGenericType(modelType)
//            });
//            BuildViewModel(tb, modelType);

//            var retVal = tb.CreateType();
//            _builtDynViewModels.Add(modelType, retVal);
//            return retVal;
//        }

//#endregion

//        #region Constructores de Modelos

//        /// <summary>
//        /// Compila y genera un nuevo tipo que puede utilizarse como modelo
//        /// simple de datos.
//        /// </summary>
//        /// <typeparam name="T">
//        /// Interfaz que describe los campos expuestos por este modelo.
//        /// </typeparam>
//        /// <returns>
//        /// Un nuevo tipo compilado en runtimeque expone propiedades para
//        /// cada una de mas mismas definidas en la interfaz especificada.
//        /// </returns>
//        public static Type BuildModel<T>() where T : class => BuildModel(typeof(T));

//        /// <summary>
//        /// Compila y genera un nuevo tipo que puede utilizarse como modelo
//        /// simple de datos.
//        /// </summary>
//        /// <param name="interfaceType">
//        /// Interfaz que describe los campos expuestos por este modelo.
//        /// </param>
//        /// <returns>
//        /// Un nuevo tipo compilado en runtimeque expone propiedades para
//        /// cada una de mas mismas definidas en la interfaz especificada.
//        /// </returns>
//        public static Type BuildModel(Type interfaceType)
//        {
//            if (!interfaceType.IsInterface) throw new InvalidTypeException(interfaceType);
//            if (_builtModels.ContainsKey(interfaceType)) return _builtModels[interfaceType];

//            var modelProps = interfaceType.GetProperties(BindingFlags.Public | Instance).Where(p => p.CanRead);
//            var tb = NewType(NoIfaceName(interfaceType.Name), interfaceType);
//            var ctor = tb.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes).GetILGenerator();

//            foreach (var j in modelProps)
//            {
//                AddAutoProp(tb, j, out var prop, out var field);
//                if (j.HasAttr(out DefaultValueAttribute? dva))
//                {
//                    InitField(ctor, field, dva!.Value!);
//                }
//                if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType) && prop.PropertyType != typeof(string))
//                {
//                    InitField(ctor, field, typeof(Collection<>).MakeGenericType(GetListType(prop.PropertyType)), Type.EmptyTypes);
//                }
//            }

//            ctor.Emit(Ldarg_0);
//            ctor.Emit(Call, typeof(object).GetConstructor(Type.EmptyTypes) ?? throw new TamperException());
//            ctor.Emit(Ret);
//            var retVal = tb.CreateType();
//            _builtModels.Add(interfaceType, retVal);
//            return retVal;
//        }

//        #endregion
//    }
}