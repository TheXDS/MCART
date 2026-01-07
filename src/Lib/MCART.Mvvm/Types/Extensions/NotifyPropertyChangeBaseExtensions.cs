// NotifyPropertyChangeBaseExtensions.cs
// 
// This file is part of Morgan's CLR Advanced Runtime (MCART)
// 
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
// 
// Released under the MIT License (MIT)
// Copyright © 2011 - 2026 César Andrés Morgan
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the “Software”), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Linq.Expressions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Includes extensions for any object that inherits the
/// <see cref="NotifyPropertyChangeBase"/> class.
/// </summary>
public static class NotifyPropertyChangeBaseExtensions
{
    /// <summary>
    /// Subscribes a delegate to be executed when a specific property changes
    /// its value.
    /// </summary>
    /// <typeparam name="T">
    /// Type of object to select the observed property from.
    /// </typeparam>
    /// <param name="instance">
    /// Instance onto which to add the subscription.
    /// </param>
    /// <param name="propertySelector">
    /// Expression that selects the property to subscribe the action for.
    /// </param>
    /// <param name="callback">
    /// Action to execute when the property changes its value.
    /// </param>
    public static void Subscribe<T>(this T instance, Expression<Func<T, object?>> propertySelector, PropertyChangeObserver callback)
        where T : NotifyPropertyChangeBase
    {
        instance.Subscribe(ReflectionHelpers.GetProperty(propertySelector), callback);
    }

    /// <summary>
    /// Removes all previously subscribed actions for the specified property.
    /// </summary>
    /// <param name="instance">
    /// Instance from which to remove the subscription.
    /// </param>
    /// <param name="propertySelector">
    /// Expression that selects the property to unsubscribe the previously
    /// subscribed actions for.
    /// </param>
    public static bool Unsubscribe<T>(this T instance, Expression<Func<T, object?>> propertySelector)
        where T : NotifyPropertyChangeBase
    {
        return instance.Unsubscribe(ReflectionHelpers.GetProperty(propertySelector));
    }
}