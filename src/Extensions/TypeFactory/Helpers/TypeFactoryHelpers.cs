using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Helpers
{
    /// <summary>
    /// Contiene métodos auxiliares útiles para la construcción de tipos.
    /// </summary>
    public static class TypeFactoryHelpers
    {
        private static readonly TypeFactory _factory = new(true);

        /// <summary>
        /// Combina las propiedades y métodos públicos de una colección de
        /// objetos.
        /// </summary>
        /// <param name="instances">Instancias a combinar.</param>
        /// <returns>
        /// Un nuevo objeto recompilado que incluye todas las propiedades y
        /// métodos públicos de todos los tipos especificados.
        /// </returns>
        public static object Merge(params object[] instances)
        {
            var types = instances.Select(p => p.GetType()).ToArray();
            if (CheckPropsCollision(types) || CheckMethodCollision(types))
            {
                throw TypeFactoryErrors.CannotJoinObjects();
            }
            var typeBuilder = _factory.NewClass($"MergeWrap_{string.Join('_', types.Select(p => p.ToString()))}", types.SelectMany(p => p.GetInterfaces()).Distinct());
            var fields = types.Select(p => typeBuilder.DefineField($"_{Guid.NewGuid().ToString().Replace('-', '_')}", p, FieldAttributes.PrivateScope | FieldAttributes.InitOnly)).ToArray();
            var ctorIl = typeBuilder.AddPublicConstructor(types).CallBaseCtor<object>();
            short c = 1;
            foreach (var (field, type) in fields.Zip(types))
            {
                ctorIl.This().LoadArg(c).StoreField(field);
                c++;
                AddMembers(type, field, typeBuilder);
            }
            ctorIl.Return();
            return typeBuilder.CreateType()!.New(instances);
        }

        private static void AddMembers(Type type, FieldInfo field, TypeBuilder tb)
        {
            foreach (var j in type.GetProperties())
            {
                AddProp(field, j, tb);
            }
            foreach (var j in type.GetDefinedMethods())
            {
                AddMethod(field, j, tb);
            }
        }

        private static void AddProp(FieldInfo field, PropertyInfo property, TypeBuilder t)
        {
            var prop = t.AddProperty(property.Name, property.PropertyType, property.CanWrite, MemberAccess.Public, false);
            prop.Getter!.LoadField(field).Call(property.GetMethod!).Return();
            if (property.CanWrite)
            {
                prop.Setter!.LoadField(field).LoadArg1().Call(property.SetMethod!).Return();
            }
        }

        private static void AddMethod(FieldInfo field, MethodInfo method, TypeBuilder t)
        {
            var @params = method.GetParameterTypes();
            var m = t.DefineMethod(method.Name, method.Attributes, method.ReturnType, @params);
            var il = m.GetILGenerator().LoadField(field);
            for (short j = 1; j <= @params.Length; j++)
            {
                il.LoadArg(j);
            }
            il.Call(method).Return();
        }

        private static bool CheckMethodCollision(Type[] types)
        {
            var methods = types.SelectMany(p => p.GetDefinedMethods()).Select(p => $"{p.Name}{string.Join(null, p.GetParameterTypes().Select(q => q.GetHashCode()))}");
            return methods.Count() != methods.Distinct().Count();
        }

        private static bool CheckPropsCollision(Type[] types)
        {
            var props = types.SelectMany(p => p.GetPublicProperties()).Select(p => p.Name);
            return props.Count() != props.Distinct().Count();
        }
    }
}
