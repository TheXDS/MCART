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

using System.Reflection;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.ViewModel;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contiene extensiones para la clase <see cref="TypeFactory"/>.
/// </summary>
public static class TypeFactoryVmExtensions
{
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
    public static ITypeBuilder<EntityViewModel<TModel>> CreateViewModelClass<TModel>(this TypeFactory factory, IEnumerable<Type>? interfaces)
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
                    .This()
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
    public static ITypeBuilder<EntityViewModel<TModel>> CreateViewModelClass<TModel>(this TypeFactory factory)
        where TModel : notnull, new()
    {
        return CreateViewModelClass<TModel>(factory, null);
    }

    private static MethodInfo GetEqualsMethod(Type type)
    {
        return type.GetMethod(nameof(object.Equals), BindingFlags.Public | BindingFlags.Instance, null, new[] { type }, null)
            ?? type.GetMethod(nameof(object.Equals), BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(object) }, null)!;
    }
}