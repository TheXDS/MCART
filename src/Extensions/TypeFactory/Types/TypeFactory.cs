/*
TypeFactory.cs

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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using static System.Reflection.BindingFlags;
using static System.Reflection.Emit.OpCodes;
using static System.Reflection.MethodAttributes;
using static TheXDS.MCART.Types.Extensions.EnumExtensions;
using static TheXDS.MCART.Types.Extensions.StringExtensions;
using static TheXDS.MCART.Types.PropertyBuilderInfo.PropertyFlags;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Define la estructura de una propiedad construida.
    /// </summary>
    public class PropertyBuilderInfo
    {
        private FieldBuilder? _backingField;
        private MethodBuilder? _getterBuilder;
        private MethodBuilder? _setterBuilder;

        private void Set<T>(ref T field, T value, PropertyFlags flags)
        {
            if ((field = value) is null)
            {
                Flags &= (PropertyFlags)(255 ^ (byte)flags);
            }
            else
            {
                Flags |= flags;
            }
        }

        /// <summary>
        /// Obtiene propiedades importantes de la propiedad.
        /// </summary>
        [Flags]
        public enum PropertyFlags : byte
        {
            /// <summary>
            /// La propiedad puede ser leída.
            /// </summary>
            Readable = 1,
            /// <summary>
            /// La propiedad puede ser escrita.
            /// </summary>
            Writtable = 2,
            /// <summary>
            /// La propiedad puede ser leída o escrita.
            /// </summary>
            ReadWrite = Readable | Writtable,
            /// <summary>
            /// La propiedad incorpora notificación de cambios.
            /// </summary>
            Notifies = 4 | Writtable,
            /// <summary>
            /// La propiedad es de lectura y escritura, además provee de
            /// notificación de cambio de valor.
            /// </summary>
            ReadWriteNotify,
            /// <summary>
            /// La propiedad utiliza un campo como almacenamiento
            /// del valor actual.
            /// </summary>
            BackingField = 8,
        }

        /// <summary>
        /// Obtiene una referencia al constructor del campo de almacenamiento de la propiedad.
        /// </summary>
        public FieldBuilder? BackingField
        {
            get => _backingField;
            internal set => Set(ref _backingField, value, PropertyFlags.BackingField);
        }

        /// <summary>
        /// Obtiene una referencia al generador de IL del método que 
        /// obtiene el valor de la propiedad.
        /// </summary>
        public ILGenerator? Getter { get; internal set; }

        /// <summary>
        /// Obtiene una referencia al generador de IL del método que 
        /// establece el valor de la propiedad.
        /// </summary>
        public ILGenerator? Setter { get; internal set; }

        /// <summary>
        /// Obtiene una referencia al constructor de método que obtiene
        /// el valor de la propiedad.
        /// </summary>
        public MethodBuilder? GetterBuilder
        {
            get => _getterBuilder;
            internal set => Set(ref _getterBuilder, value, Readable);
        }

        /// <summary>
        /// Obtiene una referencia al constructor de método que
        /// establece el valor de la propiedad.
        /// </summary>
        public MethodBuilder? SetterBuilder 
        { 
            get => _setterBuilder;
            internal set => Set(ref _setterBuilder, value, Writtable);
        }

        /// <summary>
        /// Obtiene las banderas definidas para esta propiedad.
        /// </summary>
        public PropertyFlags Flags { get; private set; }

        /// <summary>
        /// Obtiene una referencia al objeto que construye la propiedad.
        /// </summary>
        public PropertyBuilder Builder { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PropertyBuilderInfo"/>.
        /// </summary>
        /// <param name="builder">
        /// Constructor de propiedad.
        /// </param>
        /// <param name="flags">Banderas definidas para esta propiedad.</param>
        internal PropertyBuilderInfo(PropertyBuilder builder, PropertyFlags flags)
        {
            Builder = builder;
            Flags = flags;
        }

        internal PropertyBuilderInfo(PropertyBuilder builder) : this(builder, 0)
        {
        }
    }

    public class TypeGenerator
    {
        public TypeBuilder Builder { get; }
        public TypeFactory Factory { get; }

        internal TypeGenerator(TypeBuilder builder, TypeFactory factory)
        {
            Builder = builder;
            Factory = factory;
        }

        public PropertyBuilderInfo AddAutoProperty<T>(string name) => AddAutoProperty(name, typeof(T));

        public PropertyBuilderInfo AddAutoProperty(string name, Type propertyType)
        {
            var field = Builder.DefineField(TypeBuilderHelpers.UndName(name), propertyType, FieldAttributes.Private | FieldAttributes.PrivateScope);
            var prop = Builder.DefineProperty(name, PropertyAttributes.HasDefault, propertyType, null);

            var getM = Builder.DefineMethod($"get_{name}", MethodAttributes.Public | SpecialName | HideBySig | ReuseSlot, propertyType, null);
            var getIl = getM.GetILGenerator();
            getIl.Emit(Ldarg_0);
            getIl.Emit(Ldfld, field);
            getIl.Emit(Ret);
            prop.SetGetMethod(getM);

            var setM = Builder.DefineMethod($"set_{name}", MethodAttributes.Public | SpecialName | HideBySig | ReuseSlot, null, new[] { propertyType });
            var setIl = setM.GetILGenerator();
            setIl.Emit(Ldarg_0);
            setIl.Emit(Ldarg_1);
            setIl.Emit(Stfld, field);
            setIl.Emit(Ret);
            prop.SetSetMethod(setM);

            return new PropertyBuilderInfo(prop)
            {
                BackingField = field,
                GetterBuilder = getM,
                SetterBuilder = setM,
                Getter = getIl,
                Setter = setIl,
            };
        }













        private readonly HashSet<PropertyBuilder> _declaredProperties = new HashSet<PropertyBuilder>();
        private readonly HashSet<FieldBuilder> _declaredFields = new HashSet<FieldBuilder>();

    }

    /// <summary>
    /// Fábrica de tipos. Permite compilar nuevos tipos en Runtime.
    /// </summary>
    public class TypeFactory
    {
        private readonly string _namespace;
        private readonly bool _useGuid;
        private readonly ModuleBuilder _mBuilder;
        private readonly IDictionary<string, Type> _builtTypes = new Dictionary<string, Type>();
        
        public TypeFactory() : this("TheXDS.MCART.Types._Generated") { }

        public TypeFactory(string @namespace) : this(@namespace, true) { }

        public TypeFactory(string @namespace, bool useGuid)
        {
            _namespace = @namespace;
            _useGuid = useGuid;
            _mBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(_namespace), AssemblyBuilderAccess.Run).DefineDynamicModule(_namespace);
        }


        public TypeGenerator NewClass(string name)
        {
            return NewClass(name, typeof(object), Type.EmptyTypes);
        }

        public TypeGenerator NewClass(string name, IEnumerable<Type> interfaces)
        {
            return NewClass(name, typeof(object), interfaces);
        }

        public TypeGenerator NewClass(string name, Type baseType, IEnumerable<Type> interfaces)
        {
            var nme = new StringBuilder();
            nme.Append($"{_namespace}.{name}");
            if (_useGuid) nme.Append($"_{Guid.NewGuid().ToString().Replace("-", "")}");


            return new TypeGenerator(_mBuilder.DefineType(nme.ToString(), TypeAttributes.Public | TypeAttributes.Class, baseType, interfaces?.ToArray()), this);
        }







    }
}
