/*
NotifyPropertyChanged.cs

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

using System.ComponentModel;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Base class for any object that can generate notifications and events when
/// the value of a property has changed.
/// </summary>
public abstract partial class NotifyPropertyChanged : NotifyPropertyChangeBase, INotifyPropertyChanged
{
    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Manually raises a <see cref="PropertyChanged"/> event for a set of
    /// properties.
    /// </summary>
    /// <param name="propertyNames">
    /// Name of the properties that changed their value.
    /// </param>
    protected void Notify(params string[] propertyNames)
    {
        foreach (var j in propertyNames)
        {
            Change_Notify(j, PropertyChangeNotificationType.PropertyChanged);
        }
    }

    /// <summary>
    /// Manually raises a <see cref="PropertyChanged"/> event for a single
    /// property.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the property that changed its value.
    /// </param>
    protected void Notify(string propertyName)
    {
        Change_Notify(propertyName, PropertyChangeNotificationType.PropertyChanged);
    }
    
    /// <inheritdoc/>
    protected sealed override void RaisePropertyChangeEvent(in string propertyName, in PropertyChangeNotificationType _)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    /// <inheritdoc/>
    protected override void OnDoChange<T>(ref T field, T value, string propertyName)
    {
        field = value;
        Change_Notify(propertyName, PropertyChangeNotificationType.PropertyChanged);
    }
}
