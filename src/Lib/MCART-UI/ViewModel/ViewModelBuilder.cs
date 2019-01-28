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
        private const string Namespace = "TheXDS.MCART.ViewModel._Generated";
        private static readonly ModuleBuilder MBuilder =
            AppDomain.CurrentDomain.DefineDynamicAssembly(
                    new AssemblyName(Namespace),
                    AssemblyBuilderAccess.Run)
                .DefineDynamicModule(Namespace);

        private static readonly Dictionary<Type, Type> BuiltTypes = new Dictionary<Type, Type>();
        
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
        public static TViewModel New<TViewModel>() where TViewModel : ViewModel<T>
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
        ///     hereda de <see cref="ViewModel{TModel,TKey}"/> como base.
        /// </summary>
        /// <typeparam name="TViewModel">
        ///     Clase de ViewModel a utilizar como tipo base.
        /// </typeparam>
        /// <returns></returns>
        public static Type Build<TViewModel>() where TViewModel : ViewModel<T>
        {
            if (BuiltTypes.ContainsKey(typeof(TViewModel)))
                return BuiltTypes[typeof(TViewModel)];

            var tb = MBuilder.DefineType(
                $"{typeof(T).Name}_{Guid.NewGuid()}",
                TypeAttributes.Public,
                typeof(TViewModel)
            );

            var ctor = tb.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.Standard,
                Type.EmptyTypes);
            var ctorIl = ctor.GetILGenerator();
            ctorIl.Emit(OpCodes.Ldarg_0);
            ctorIl.Emit(OpCodes.Call, typeof(TViewModel).GetConstructor(Type.EmptyTypes) ?? throw new InvalidOperationException());
            ctorIl.Emit(OpCodes.Ret);

            var addedProps = new HashSet<string>(typeof(TViewModel).GetProperties().Select(p => p.Name));
            var modelProps = typeof(T).GetProperties(Public | Instance).Where(p => p.CanRead);
            
            foreach (var j in modelProps.Where(p => p.CanWrite && addedProps.All(q => q != p.Name)))
            {
                addedProps.Add(j.Name);
                AddProp(tb, j);
            }

            var retVal = tb.CreateType();
            BuiltTypes.Add(typeof(TViewModel), retVal);
            return retVal;
        }

        private static void AddProp([NotNull]TypeBuilder tb, [NotNull] PropertyInfo property)
        {
            const MethodAttributes gsArgs = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;
            var t = typeof(ViewModel<T>);
            var entity = t.GetProperty("Entity", NonPublic | Instance)?.GetMethod ?? throw new TamperException();

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
            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Call, entity);
            getIl.Emit(OpCodes.Callvirt, property.GetMethod);
            getIl.Emit(OpCodes.Ret);
            prop.SetGetMethod(getM);

            if (!property.CanWrite) return;
            var setM = tb.DefineMethod(
                $"set_{property.Name}",
                gsArgs,
                null,
                new[] { property.PropertyType }
            );
            var setIl = setM.GetILGenerator();
            var eqM = property.PropertyType.GetMethod("Equals", new[] { property.PropertyType }) ??
                        property.PropertyType.GetMethod("Equals", new[] { typeof(object) });
            if (!(eqM is null))
            {
                var lbl1 = setIl.DefineLabel();
                setIl.Emit(OpCodes.Ldarg_0);
                setIl.Emit(OpCodes.Call, entity);
                setIl.Emit(OpCodes.Callvirt, property.GetMethod);
                setIl.Emit(OpCodes.Ldarg_1);
                setIl.Emit(OpCodes.Callvirt, eqM);
                setIl.Emit(OpCodes.Brfalse, lbl1);
                setIl.Emit(OpCodes.Ret);
                setIl.MarkLabel(lbl1);
            }
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Call, entity);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Callvirt, property.SetMethod);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldstr, property.Name);
            setIl.Emit(OpCodes.Call, t.GetMethod("OnPropertyChanged", NonPublic | Instance) ?? throw new TamperException());
            setIl.Emit(OpCodes.Ret);
            prop.SetSetMethod(setM);
        }
    }
}
