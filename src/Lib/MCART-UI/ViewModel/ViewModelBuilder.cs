/*
ViewModelBuilder.cs

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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.Annotations;
using static System.Reflection.BindingFlags;
using TheXDS.MCART.Exceptions;
using System.Collections.Specialized;
using static System.Reflection.Emit.OpCodes;
using System.Collections.ObjectModel;
using TheXDS.MCART.Types.Base;
using System.Collections;

// ReSharper disable StaticMemberInGenericType

namespace TheXDS.MCART.ViewModel
{
    /// <summary>
    ///     Constructor dinámico de tipos ViewModel en Runtime.
    /// </summary>
    /// <typeparam name="T">
    ///     Tipo de modelo a utilizar como campo de almacenamiento y a partir
    ///     del cual se diseñará un nuevo <see cref="ViewModel{T}"/>.
    /// </typeparam>
    public static class ViewModelBuilder<T> where T : new()
    {
        private const string _namespace = "TheXDS.MCART.ViewModel._Generated";
        private static readonly ModuleBuilder _mBuilder =
            AssemblyBuilder.DefineDynamicAssembly(
                new AssemblyName(_namespace),
                AssemblyBuilderAccess.Run)
            .DefineDynamicModule(_namespace);


        private static readonly Dictionary<Type, Type> _builtTypes = new Dictionary<Type, Type>();
        
        /// <summary>
        ///     Construye un nuevo ViewModel sin ninguna clase que herede
        ///     <see cref="ViewModel{TModel}"/> como base.
        /// </summary>
        /// <returns>
        ///     Un nuevo ViewModel con las propiedades originales del modelo
        ///     implementadas como llamadas con notificación de cambio de
        ///     propiedades.
        /// </returns>
        public static ViewModel<T> New()
        {
            return Build().New<ViewModel<T>>();
        }

        /// <summary>
        ///     Construye un nuevo ViewModel utilizando una clase base que
        ///     hereda de <see cref="ViewModel{T}"/> como base.
        /// </summary>
        /// <typeparam name="TViewModel">
        ///     Clase de ViewModel a utilizar como tipo base.
        /// </typeparam>
        /// <returns>
        ///     Un nuevo ViewModel con las propiedades originales del modelo
        ///     implementadas como llamadas con notificación de cambio de
        ///     propiedades.
        /// </returns>
        public static TViewModel New<TViewModel>() where TViewModel : class, IEntityViewModel<T>, new()
        {
            return Activator.CreateInstance(Build<TViewModel>()) as TViewModel;
        }

        /// <summary>
        ///     Construye un tipo de ViewModel sin ninguna clase que herede
        ///     <see cref="ViewModel{T}"/> como base.
        /// </summary>
        /// <returns></returns>
        public static Type Build() => Build<ViewModel<T>>();

        /// <summary>
        ///     Construye un tipo de ViewModel utilizando una clase base que
        ///     hereda de <see cref="ViewModel{TModel}"/> como base.
        /// </summary>
        /// <typeparam name="TViewModel">
        ///     Clase de ViewModel a utilizar como tipo base.
        /// </typeparam>
        /// <returns></returns>
        public static Type Build<TViewModel>() where TViewModel : IEntityViewModel<T>
        {
            if (_builtTypes.ContainsKey(typeof(TViewModel)))
                return _builtTypes[typeof(TViewModel)];

            var tb = _mBuilder.DefineType(
                $"{typeof(TViewModel).Name}_{Guid.NewGuid().ToString().Replace("-","")}",
                TypeAttributes.Public,
                typeof(TViewModel)
            );

            var ctor = tb.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.Standard,
                Type.EmptyTypes);
            var ctorIl = ctor.GetILGenerator();
            ctorIl.Emit(Ldarg_0);
            ctorIl.Emit(Call, typeof(TViewModel).GetConstructor(Type.EmptyTypes) ?? throw new InvalidOperationException());

            var modelProps = typeof(T).GetProperties(Public | Instance).Where(p => p.CanRead).Distinct();

            foreach (var j in modelProps.Where(p => p.CanWrite))
            {
                AddProp(typeof(TViewModel), tb, j);
            }
            foreach (var j in modelProps.Where(p => typeof(IEnumerable).IsAssignableFrom(p.PropertyType) && p.PropertyType != typeof(string)))
            {
                ProcessCollection(typeof(TViewModel), tb, ctorIl, j);
            }

            ctorIl.Emit(Ret);

            var retVal = tb.CreateType();
            _builtTypes.Add(typeof(TViewModel), retVal);
            return retVal;
        }

        private static PropertyBuilder AddProp([NotNull]Type baseType, [NotNull]TypeBuilder tb, [NotNull] PropertyInfo property)
        {
            const MethodAttributes gsArgs = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;
            //var entity = baseType.GetProperty("Entity", Public | Instance)?.GetMethod?.DeclaringType?.GetMethod("get_Entity") ?? throw new TamperException();
            var entity = typeof(IEntityViewModel<T>).GetMethod("get_Entity") ?? throw new TamperException();

            var prop = tb.DefineProperty(
                property.Name,
                PropertyAttributes.HasDefault,
                property.PropertyType,
                null);

            var getM = tb.DefineMethod(
                $"get_{property.Name}",
                gsArgs,
                property.PropertyType,
                null
            );
            var getIl = getM.GetILGenerator();
            getIl.Emit(Ldarg_0);
            getIl.Emit(Call, entity);
            getIl.Emit(Callvirt, property.GetMethod);
            getIl.Emit(Ret);
            prop.SetGetMethod(getM);

            if (!property.CanWrite) return prop;
            var setM = tb.DefineMethod(
                $"set_{property.Name}",
                gsArgs,
                null,
                new[] { property.PropertyType }
            );
            var setIl = setM.GetILGenerator();
            //var eqM = property.PropertyType.GetMethod("Equals", new[] { property.PropertyType }) ??
            //            property.PropertyType.GetMethod("Equals", new[] { typeof(object) });
            //if (!(eqM is null))
            //{
            //    var lbl1 = setIl.DefineLabel();
            //    setIl.Emit(Ldarg_0);
            //    setIl.Emit(Call, entity);
            //    setIl.Emit(Callvirt, property.GetMethod);
            //    setIl.Emit(Ldarg_1);
            //    setIl.Emit(Callvirt, eqM);
            //    setIl.Emit(Brfalse, lbl1);
            //    setIl.Emit(Ret);
            //    setIl.MarkLabel(lbl1);
            //}
            setIl.Emit(Ldarg_0);
            setIl.Emit(Call, entity);
            setIl.Emit(Ldarg_1);
            setIl.Emit(Callvirt, property.SetMethod);
            setIl.Emit(Ldarg_0);
            setIl.Emit(Ldstr, property.Name);
            setIl.Emit(Call, baseType.GetMethod("OnPropertyChanged", NonPublic | Instance) ?? throw new TamperException());
            setIl.Emit(Ret);
            prop.SetSetMethod(setM);
            return prop;
        }

        private static void ProcessCollection([NotNull]Type baseType, [NotNull]TypeBuilder tb, [NotNull]ILGenerator ctorIl, [NotNull]PropertyInfo modelProperty)
        {
            // Variables necesarias de estado
            var entity = baseType.GetProperty("Entity", Public | Instance)?.GetMethod ?? throw new TamperException();
            var labels = new Dictionary<string, Label>();
            var listType = modelProperty.PropertyType.GenericTypeArguments?.Count() == 1 ? modelProperty.PropertyType.GenericTypeArguments.Single() : typeof(object);
            var enumerable = typeof(IEnumerable<>).MakeGenericType(listType);
            var enumerator = typeof(IEnumerator<>).MakeGenericType(listType);
            var thisType = typeof(ObservableCollection<>).MakeGenericType(listType);
            var collectionType = typeof(ICollection<>).MakeGenericType(listType);

            // Funciones locales útiles
            Label NewLabel(string id, ILGenerator generator)
            {
                var l = generator.DefineLabel();
                labels.Add(id,l);
                return l;
            }

            var thisField = tb.DefineField($"_{modelProperty.Name.Substring(0, 1).ToLower()}{modelProperty.Name.Substring(1)}",thisType,FieldAttributes.Private| FieldAttributes.InitOnly);

            var thisProp = tb.DefineProperty(
                modelProperty.Name,
                PropertyAttributes.HasDefault,
                typeof(ObservableCollection<>).MakeGenericType(listType),
                null);

            var getM = tb.DefineMethod(
                $"get_{modelProperty.Name}",
                MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig,
                thisType,
                null);
            var getIl = getM.GetILGenerator();
            getIl.Emit(Ldarg_0);
            getIl.Emit(Ldfld, thisField);
            getIl.Emit(Ret);
            thisProp.SetGetMethod(getM);

            var handler = tb.DefineMethod(
                $"{modelProperty.Name}_CollectionChanged",
                MethodAttributes.Private,
                null,
                new[] {typeof(object), typeof(NotifyCollectionChangedEventArgs) });

            var handlerIl = handler.GetILGenerator();

            handlerIl.Emit(Ldarg_2);
            handlerIl.Emit(Callvirt, typeof(NotifyCollectionChangedEventArgs).GetMethod("get_NewItems"));
            handlerIl.Emit(Dup);
            handlerIl.Emit(Brtrue_S, NewLabel("0x000f", handlerIl));
            handlerIl.Emit(Pop);
            handlerIl.Emit(Ldnull);
            handlerIl.Emit(Br_S, NewLabel("0x0014", handlerIl));
            handlerIl.MarkLabel(labels["0x000f"]);
            handlerIl.Emit(Call,typeof(Enumerable).GetMethod("OfType").MakeGenericMethod(listType));
            handlerIl.MarkLabel(labels["0x0014"]);
            handlerIl.Emit(Dup);
            handlerIl.Emit(Brtrue_S, NewLabel("0x001e", handlerIl));
            handlerIl.Emit(Pop);
            handlerIl.Emit(Ldc_I4_0);
            handlerIl.Emit(Newarr, listType);
            handlerIl.MarkLabel(labels["0x001e"]);
            handlerIl.Emit(Callvirt, enumerable.GetMethod("GetEnumerator"));
            handlerIl.Emit(Stloc_0);

            handlerIl.BeginExceptionBlock();
            handlerIl.Emit(Br_S, NewLabel("0x003f", handlerIl));
            handlerIl.MarkLabel(NewLabel("0x0026", handlerIl));
            handlerIl.Emit(Ldloc_0);
            handlerIl.Emit(Callvirt, enumerator.GetMethod("get_Current"));
            handlerIl.Emit(Stloc_1);
            handlerIl.Emit(Ldarg_0);
            handlerIl.Emit(Call,modelProperty.GetMethod);
            handlerIl.Emit(Callvirt, entity);
            handlerIl.Emit(Ldloc_1);
            handlerIl.Emit(Callvirt, collectionType.GetMethod("Add"));
            handlerIl.MarkLabel(labels["0x003f"]);
            handlerIl.Emit(Ldloc_0);
            handlerIl.Emit(Callvirt, typeof(IEnumerator).GetMethod("MoveNext"));
            handlerIl.Emit(Brtrue_S,labels["0x0026"]);
            handlerIl.Emit(Leave_S, NewLabel("0x0054", handlerIl));
            handlerIl.BeginFinallyBlock();            
            handlerIl.Emit(Ldloc_0);
            handlerIl.Emit(Brfalse_S,NewLabel("0x0053", handlerIl));
            handlerIl.Emit(Ldloc_0);
            handlerIl.Emit(Callvirt, typeof(IDisposable).GetMethod("Dispose"));
            handlerIl.MarkLabel(labels["0x0053"]);
            handlerIl.Emit(Endfinally);
            handlerIl.EndExceptionBlock();
            handlerIl.MarkLabel(labels["0x0054"]);

            handlerIl.Emit(Ldarg_2);
            handlerIl.Emit(Callvirt, typeof(NotifyCollectionChangedEventArgs).GetMethod("get_OldItems"));
            handlerIl.Emit(Dup);
            handlerIl.Emit(Brtrue_S, NewLabel("0x0062", handlerIl));
            handlerIl.Emit(Pop);
            handlerIl.Emit(Ldnull);
            handlerIl.Emit(Br_S, NewLabel("0x0067", handlerIl));
            handlerIl.MarkLabel(labels["0x0062"]);
            handlerIl.Emit(Call, typeof(Enumerable).GetMethod("OfType").MakeGenericMethod(listType));
            handlerIl.MarkLabel(labels["0x0067"]);
            handlerIl.Emit(Dup);
            handlerIl.Emit(Brtrue_S, NewLabel("0x0071", handlerIl));
            handlerIl.Emit(Pop);
            handlerIl.Emit(Ldc_I4_0);
            handlerIl.Emit(Newarr, listType);
            handlerIl.MarkLabel(labels["0x0071"]);
            handlerIl.Emit(Callvirt, enumerable.GetMethod("GetEnumerator"));
            handlerIl.Emit(Stloc_0);

            handlerIl.BeginExceptionBlock();
            handlerIl.Emit(Br_S, NewLabel("0x013f", handlerIl));
            handlerIl.MarkLabel(NewLabel("0x0126", handlerIl));
            handlerIl.Emit(Ldloc_0);
            handlerIl.Emit(Callvirt, enumerator.GetMethod("get_Current"));
            handlerIl.Emit(Stloc_1);
            handlerIl.Emit(Ldarg_0);
            handlerIl.Emit(Call, modelProperty.GetMethod);
            handlerIl.Emit(Callvirt, entity);
            handlerIl.Emit(Ldloc_1);
            handlerIl.Emit(Callvirt, collectionType.GetMethod("Add"));
            handlerIl.MarkLabel(labels["0x013f"]);
            handlerIl.Emit(Ldloc_0);
            handlerIl.Emit(Callvirt, typeof(IEnumerator).GetMethod("MoveNext"));
            handlerIl.Emit(Brtrue_S, labels["0x0126"]);
            handlerIl.Emit(Leave_S, NewLabel("0x0154", handlerIl));
            handlerIl.BeginFinallyBlock();
            handlerIl.Emit(Ldloc_0);
            handlerIl.Emit(Brfalse_S, NewLabel("0x0153", handlerIl));
            handlerIl.Emit(Ldloc_0);
            handlerIl.Emit(Callvirt, typeof(IDisposable).GetMethod("Dispose"));
            handlerIl.MarkLabel(labels["0x0153"]);
            handlerIl.Emit(Endfinally);
            handlerIl.EndExceptionBlock();
            handlerIl.MarkLabel(labels["0x0154"]);
            handlerIl.Emit(Ldarg_0);
            handlerIl.Emit(Ldstr, modelProperty.Name);
            handlerIl.Emit(Callvirt, typeof(NotifyPropertyChanged).GetMethod("OnPropertyChanged", NonPublic|Instance));
            handlerIl.Emit(Ret);

            ctorIl.Emit(Ldarg_0);
            ctorIl.Emit(Newobj, thisType.GetConstructor(Type.EmptyTypes));
            ctorIl.Emit(Stfld, thisField);
            ctorIl.Emit(Ldarg_0);
            ctorIl.Emit(Ldfld,thisField);
            ctorIl.Emit(Ldarg_0);
            ctorIl.Emit(Ldftn, handler);
            ctorIl.Emit(Newobj, typeof(NotifyCollectionChangedEventHandler).GetConstructors().Single());
            ctorIl.Emit(Callvirt, typeof(INotifyCollectionChanged).GetMethod("add_CollectionChanged"));
        }
    }
}