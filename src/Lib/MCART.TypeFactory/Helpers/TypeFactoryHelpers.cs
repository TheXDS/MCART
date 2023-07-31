/*
TypeFactoryHelpers.cs

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

using System.Reflection;
using System.Reflection.Emit;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Helpers;

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
            ctorIl.StoreField(field, il => il.LoadArg(c));
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
            AddProperty(field, j, tb);
        }
        foreach (var j in type.GetDefinedMethods())
        {
            AddMethod(field, j, tb);
        }
    }

    private static void AddProperty(FieldInfo field, PropertyInfo property, TypeBuilder t)
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
