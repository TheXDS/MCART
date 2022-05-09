/*
TypeFactoryVmExtensions.cs

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
    /// Compila un nuevo <see cref="ViewModel{T}"/> definiendo un campo de
    /// entidad de tipo <typeparamref name="TModel"/>.
    /// </summary>
    /// <typeparam name="TModel">
    /// Tipo de entidad para la cual compilar un nuevo
    /// <see cref="ViewModel{T}"/>.
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
    public static ITypeBuilder<ViewModel<TModel>> CreateViewModelClass<TModel>(this TypeFactory factory, IEnumerable<Type>? interfaces)
        where TModel : notnull, new()
    {
        ITypeBuilder<ViewModel<TModel>> t = factory.NewType<ViewModel<TModel>>($"{typeof(TModel).Name}ViewModel", interfaces);
        PropertyInfo e = ReflectionHelpers.GetProperty<ViewModel<TModel>>(p => p.Entity);
        MethodInfo nm = ReflectionHelpers.GetMethod<ViewModel<TModel>, Action<string>>(p => p.Notify);

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
    /// Compila un nuevo <see cref="ViewModel{T}"/> definiendo un campo de
    /// entidad de tipo <typeparamref name="TModel"/>.
    /// </summary>
    /// <typeparam name="TModel">
    /// Tipo de entidad para la cual compilar un nuevo
    /// <see cref="ViewModel{T}"/>.
    /// </typeparam>
    /// <param name="factory">
    /// Fábrica de tipos a utilizar para crear el nuevo tipo.
    /// </param>
    /// <returns>
    /// Un objeto que puede utilizarse para construir un nuevo tipo.
    /// </returns>
    public static ITypeBuilder<ViewModel<TModel>> CreateViewModelClass<TModel>(this TypeFactory factory)
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