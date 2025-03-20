/*
NotifyPropertyChangeBase.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Base class for all types that implement a property change notification
/// pattern via either <see cref="INotifyPropertyChanged"/> or
/// <see cref="INotifyPropertyChanging"/>.
/// </summary>
public abstract partial class NotifyPropertyChangeBase : INotifyPropertyChangeBase
{
    /// <summary>
    /// Defines the available configuration methods for setting up property
    /// change notification broadcast and triggers.
    /// </summary>
    protected interface IPropertyBroadcastSetup
    {
        /// <summary>
        /// Registers the broadcast of a change notification whenever the specified
        /// property changes its value.
        /// </summary>
        /// <param name="propertySelector">
        /// Property whose change notification will be broadcasted.
        /// </param>
        /// <param name="affectedProperties">
        /// Properties that will be triggered as changed when the specified
        /// property in <paramref name="propertySelector"/> changes its value.
        /// </param>
        /// <returns>
        /// The same configurator instance, allowing the use of Fluent syntax.
        /// </returns>
        IPropertyBroadcastSetup RegisterPropertyChangeBroadcast(Expression<Func<object?>> propertySelector, params Expression<Func<object?>>[] affectedProperties);

        /// <summary>
        /// Registers a property to be triggered as changed whenever any of the
        /// specified properties changes their value.
        /// </summary>
        /// <param name="property">
        /// Property to be triggered as changed.
        /// </param>
        /// <param name="listenedProperties">
        /// Properties to listen for change notification events.
        /// </param>
        /// <returns>
        /// The same configurator instance, allowing the use of Fluent syntax.
        /// </returns>
        IPropertyBroadcastSetup RegisterPropertyChangeTrigger(Expression<Func<object?>> property, params Expression<Func<object?>>[] listenedProperties);
    }

    private class PropertyBroadcastSetup(Dictionary<string, HashSet<string>> NotifyTree, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] Type OwnerType) : IPropertyBroadcastSetup
    {
        private readonly Dictionary<string, HashSet<string>> notifyTree = NotifyTree;
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
        private readonly Type ownerType = OwnerType;

        private (string, string[]) RegisterPropertyChangeBroadcast_contract(Expression<Func<object?>> propertySelector, Expression<Func<object?>>[] affectedProperties)
        {
            ArgumentNullException.ThrowIfNull(propertySelector);
            ArgumentNullException.ThrowIfNull(affectedProperties);

            var prop = ReflectionHelpers.GetProperty(propertySelector);
            ValidateProperty(prop, ownerType);

            if (affectedProperties.Length == 0) throw Errors.EmptyCollection(affectedProperties);
            var props = affectedProperties.Select(ReflectionHelpers.GetProperty).ToArray();
            foreach (var j in props) ValidateProperty(j, ownerType);

            var propName = prop.Name;
            var propNames = props.Select(p => p.Name).ToArray();
            if (propNames.Where(j => notifyTree.ContainsKey(j)).Any(j => BranchScanFails(propName, j, notifyTree, [])))
            {
                throw Errors.CircularOpDetected();
            }
            return (propName, propNames);
        }

        public IPropertyBroadcastSetup RegisterPropertyChangeBroadcast(Expression<Func<object?>> propertySelector, Expression<Func<object?>>[] affectedProperties)
        {
            (var propName, var propNames) = RegisterPropertyChangeBroadcast_contract(propertySelector, affectedProperties);
            if (notifyTree.TryGetValue(propName, out var affectedPropsRegistry))
            {
                foreach (string? j in propNames)
                {
                    affectedPropsRegistry.Add(j);
                }
            }
            else
            {
                notifyTree.Add(propName, [.. propNames]);
            }
            return this;
        }

        public IPropertyBroadcastSetup RegisterPropertyChangeTrigger(Expression<Func<object?>> property, Expression<Func<object?>>[] listenedProperties)
        {
            foreach (var j in listenedProperties) RegisterPropertyChangeBroadcast(j, [property]);
            return this;
        }

        private static bool BranchScanFails<T>(T a, T b, Dictionary<T, HashSet<T>> tree, List<T> keysChecked) where T : notnull
        {
            if (!tree.TryGetValue(b, out var bCollection)) return false;
            foreach (T? j in bCollection)
            {
                if (keysChecked.Contains(j)) return false;
                keysChecked.Add(j);
                if (j.Equals(a)) return true;
                if (BranchScanFails(a, j, tree, keysChecked)) return true;
            }
            return false;
        }
    }

    private record SubscriptionEntry(PropertyInfo? Property, PropertyChangeObserver Observer);

    private readonly HashSet<SubscriptionEntry> _observeSubscriptions = [];

    private readonly Dictionary<string, HashSet<string>> _notifyTree = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="NotifyPropertyChangeBase"/>
    /// </summary>
    protected NotifyPropertyChangeBase()
    {
        OnInitialize(new PropertyBroadcastSetup(_notifyTree, GetType()));
    }

    /// <summary>
    /// When overriden in a derived class, allows the configuration of property
    /// change broadcast and notification triggers
    /// </summary>
    /// <param name="broadcastSetup">
    /// Internal instance used to configure the property change broadcast and
    /// notification triggers.
    /// </param>
    protected virtual void OnInitialize(IPropertyBroadcastSetup broadcastSetup)
    {
    }

    /// <summary>
    /// Raises the appropriate property change notification event.
    /// </summary>
    /// <param name="propertyName">
    /// Property name for the event to be triggered.
    /// </param>
    /// <param name="notificationType">
    /// Type of property change notification.
    /// </param>
    protected abstract void RaisePropertyChangeEvent(in string propertyName, in PropertyChangeNotificationType notificationType);

    /// <inheritdoc/>
    public virtual void Refresh()
    {
        foreach (var j in GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead))
        {
            RaisePropertyChangeEvent(j.Name, PropertyChangeNotificationType.NoChange);
        }
    }

    /// <inheritdoc/>
    public void Subscribe(PropertyInfo? property, PropertyChangeObserver callback)
    {
        if (property is not null) ValidateProperty(property, GetType());
        _observeSubscriptions.Add(new(property, callback));
    }

    /// <inheritdoc/>
    public void Subscribe(PropertyChangeObserver callback)
    {
        Subscribe((PropertyInfo?)null, callback);
    }

    /// <inheritdoc/>
    public void Subscribe(Expression<Func<object?>> propertySelector, PropertyChangeObserver callback)
    {
        Subscribe(ReflectionHelpers.GetProperty(propertySelector), callback);
    }

    /// <inheritdoc/>
    public void Unsubscribe(PropertyChangeObserver callback)
    {
        SubscriptionEntry[] entries = _observeSubscriptions.Where(p => p.Observer == callback).ToArray();
        foreach (var j in entries) _observeSubscriptions.Remove(j);
    }

    /// <inheritdoc/>
    public bool Unsubscribe(PropertyInfo? property)
    {
        if (property is not null) ValidateProperty(property, GetType());
        SubscriptionEntry[] entries = _observeSubscriptions.Where(p => p.Property == property).ToArray();
        foreach (var j in entries) _observeSubscriptions.Remove(j);
        return entries.Length > 0;
    }

    /// <inheritdoc/>
    public bool Unsubscribe(Expression<Func<object?>> propertySelector)
    {
        return Unsubscribe(ReflectionHelpers.GetProperty(propertySelector));
    }

    /// <summary>
    /// Changes the value of a property's backing field, invoking a property
    /// change notification.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the backing field for the property.
    /// </typeparam>
    /// <param name="field">Backing field to be updated.</param>
    /// <param name="value">New value to be set onto the backing field.</param>
    /// <param name="propertyName">
    /// Property name. Must be ommitted, as it's determined at compile time.
    /// Implementors of this class must decorate this parameter with the
    /// <see cref="CallerMemberNameAttribute"/> attribute.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the value of the backing field has changed;
    /// that is, if the original and the new values are considered different.
    /// <see langword="false"/> is returned otherwise.
    /// </returns>
    [NpcChangeInvocator]
    protected bool Change<T>(ref T field, T value, [CallerMemberName]string propertyName = null!)
    {
        if (Change_CheckFails(in field, value, propertyName)) return false;
        OnDoChange(ref field, value, propertyName);
        return true;
    }

    /// <summary>
    /// Performs the actual value change on a backing field for a property.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the backing field for the property.
    /// </typeparam>
    /// <param name="field">Backing field to be updated.</param>
    /// <param name="value">New value to be set onto the backing field.</param>
    /// <param name="propertyName">Name of the property.</param>
    protected abstract void OnDoChange<T>(ref T field, T value, string propertyName);

    private static bool Change_CheckFails<T>(in T field, T value, string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);
        return field?.Equals(value) ?? Objects.AreAllNull(field, value);
    }

    private protected void Change_Notify(in string propertyName, PropertyChangeNotificationType notificationType)
    {
        PropertyInfo prop = GetType().GetProperty(propertyName)!;
        SubscriptionEntry[] entries = _observeSubscriptions.Where(p => p.Property is null || p.Property == prop).ToArray();
        foreach (var entry in entries)
        {
            entry.Observer.Invoke(this, prop, notificationType);
        }
        RaisePropertyChangeEvent(propertyName, notificationType);
        if (_notifyTree.TryGetValue(propertyName, out var broadcast))
        {
            foreach (var j in broadcast) RaisePropertyChangeEvent(j, notificationType);
        }
    }

    private static void ValidateProperty(PropertyInfo property, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]Type ownerType)
    {
        if (!(property.DeclaringType?.IsAssignableFrom(ownerType) ?? false)) throw Errors.MissingMember(ownerType, property);
    }
}
