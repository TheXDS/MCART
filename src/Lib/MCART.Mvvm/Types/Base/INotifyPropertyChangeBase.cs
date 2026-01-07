/*
INotifyPropertyChangeBase.cs

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

using System.Linq.Expressions;
using System.Reflection;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Defines members to be implemented by a type that can notify changes
/// in property values.
/// </summary>
public interface INotifyPropertyChangeBase : IRefreshable
{
    /// <summary>
    /// Subscribes a delegate to be executed when a specific property
    /// changes its value.
    /// </summary>
    /// <param name="property">Property to subscribe the action for.</param>
    /// <param name="callback">Action to execute when the property changes.</param>
    public void Subscribe(PropertyInfo? property, PropertyChangeObserver callback);

    /// <summary>
    /// Subscribes a delegate to be executed when any property changes
    /// its value.
    /// </summary>
    /// <param name="callback">Action to execute when any property changes.</param>
    public void Subscribe(PropertyChangeObserver callback);

    /// <summary>
    /// Subscribes a delegate to be executed when a specific property
    /// changes its value.
    /// </summary>
    /// <param name="propertySelector">
    /// Expression that selects the property to subscribe the action for.
    /// </param>
    /// <param name="callback">Action to execute when the property changes.</param>
    public void Subscribe(Expression<Func<object?>> propertySelector, PropertyChangeObserver callback);

    /// <summary>
    /// Removes the previously subscribed property change observer.
    /// </summary>
    /// <param name="callback">Delegate to unsubscribe.</param>
    public void Unsubscribe(PropertyChangeObserver callback);

    /// <summary>
    /// Removes all previously subscribed actions for the specified
    /// property.
    /// </summary>
    /// <param name="property">
    /// Property for which to remove all subscribed observers. If
    /// <see langword="null"/> is passed, all global subscribers for this
    /// instance will be removed.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if there was a previously registered
    /// callback for the specified property and it was removed;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public bool Unsubscribe(PropertyInfo? property);

    /// <summary>
    /// Removes all previously subscribed actions for the specified
    /// property.
    /// </summary>
    /// <param name="propertySelector">
    /// Expression that selects the property to unsubscribe the previously
    /// subscribed actions for.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if there was a previously registered
    /// callback for the selected property and it was removed;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public bool Unsubscribe(Expression<Func<object?>> propertySelector);
}

/// <summary>
/// Defines the signature of a method that executes an action when a
/// property changes its value.
/// </summary>
/// <param name="instance">Instance where the property changed.</param>
/// <param name="property">Property that changed.</param>
/// <param name="notificationType">
/// Indicates the type of notification that the observer is receiving.
/// </param>
public delegate void PropertyChangeObserver(object instance, PropertyInfo property, PropertyChangeNotificationType notificationType);
