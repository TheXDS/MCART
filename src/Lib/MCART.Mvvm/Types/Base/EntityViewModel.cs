/*
EntityViewModel.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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
/// Base class for a <see cref="ViewModelBase"/> whose backing fields
/// are part of an entity model.
/// </summary>
/// <typeparam name="T">
/// The entity type used as the internal backing store for this
/// ViewModel.
/// </typeparam>
public class EntityViewModel<T> : ViewModelBase, IEntityViewModel<T>
{
    private static readonly HashSet<PropertyInfo> _modelProperties = [.. typeof(T).GetProperties(Public | Instance).Where(p => p.CanRead)];
    private static IEnumerable<PropertyInfo> WritableProperties => _modelProperties.Where(p => p.CanWrite);

    private T _entity = default!;

    /// <summary>
    /// The entity instance managed by this ViewModel.
    /// </summary>
    public virtual T Entity
    {
        get => _entity;
        set => Change(ref _entity, value);
    }

    /// <summary>
    /// Copies values from the provided <typeparamref name="T"/> into the
    /// entity instance held by this ViewModel.
    /// </summary>
    /// <param name="entity">
    /// The entity containing new values to apply to the current
    /// <see cref="Entity"/> instance.
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
    /// Notifies that properties of this
    /// <see cref="EntityViewModel{T}"/> have changed.
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
    /// Implicitly converts an <see cref="EntityViewModel{T}"/> to
    /// <typeparamref name="T"/>.
    /// </summary>
    /// <param name="vm">The <see cref="EntityViewModel{T}"/> to convert.</param>
    public static implicit operator T(EntityViewModel<T> vm)
    {
        return vm.Entity;
    }
}
