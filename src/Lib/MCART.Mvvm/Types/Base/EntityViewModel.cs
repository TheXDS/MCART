/*
EntityViewModel.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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
using static System.Reflection.BindingFlags;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Clase base para un <see cref="ViewModelBase"/> cuyos campos de
/// almacenamiento sean parte de un modelo de entidad.
/// </summary>
/// <typeparam name="T">
/// Tipo de entidad a utilizar como almacenamiento interno de este
/// ViewModel.
/// </typeparam>
public class EntityViewModel<T> : ViewModelBase, IEntityViewModel<T>
{
    private static readonly HashSet<PropertyInfo> _modelProperties = new(typeof(T).GetProperties(Public | Instance).Where(p => p.CanRead));
    private static IEnumerable<PropertyInfo> WritableProperties => _modelProperties.Where(p => p.CanWrite);

    private T _entity = default!;

    /// <summary>
    /// Instancia de la entidad controlada por este ViewModel.
    /// </summary>
    public virtual T Entity
    {
        get => _entity;
        set => Change(ref _entity, value);
    }

    /// <summary>
    /// Edita la instancia de <typeparamref name="T"/> dentro de este
    /// ViewModel.
    /// </summary>
    /// <param name="entity">
    /// Entidad con los nuevos valores a establecer en la entidad
    /// actualmente establecida en la propiedad <see cref="Entity"/>.
    /// </param>
    public virtual void Update(T entity)
    {
        foreach (PropertyInfo? j in WritableProperties)
        {
            j.SetValue(Entity, j.GetValue(entity));
        }
        Refresh();
    }

    /// <summary>
    /// Notifica al sistema que las propiedades de este
    /// <see cref="EntityViewModel{T}"/> han cambiado.
    /// </summary>
    public override void Refresh()
    {
        if (Entity is null) return;
        lock (Entity)
        {
            Notify(nameof(Entity));
        }
    }

    /// <summary>
    /// Convierte implícitamente un <see cref="EntityViewModel{T}"/>
    /// en un <typeparamref name="T"/>.
    /// </summary>
    /// <param name="vm">
    /// <see cref="EntityViewModel{T}"/> a convertir.
    /// </param>
    public static implicit operator T(EntityViewModel<T> vm)
    {
        return vm.Entity;
    }
}
