/*
PasswordBoxMonitor.cs

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

using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace TheXDS.MCART.Component;

/// <summary>
/// Contains a set of attached dependency properties used to monitor
/// and control a <see cref="PasswordBox"/>.
/// </summary>
public class PasswordBoxMonitor : DependencyObject
{
    /// <summary>
    /// Defines the different password monitoring levels.
    /// </summary>
    [Flags]
    public enum MonitorLevel : byte
    {
        /// <summary>
        /// None. The <see cref="PasswordBox"/> will not be monitored.
        /// </summary>
        None,

        /// <summary>
        /// Password length only. Only the
        /// <see cref="PasswordLengthProperty"/> is exposed.
        /// </summary>
        Length,

        /// <summary>
        /// Plain-text password. Monitors the
        /// <see cref="PasswordBox"/>'s plain-text password content.
        /// </summary>
        Password,

        /// <summary>
        /// Plain-text password and password length.
        /// </summary>
        PasswordAndLength,

        /// <summary>
        /// Secure password. Monitors the
        /// <see cref="PasswordBox"/>'s secure password content.
        /// </summary>
        SecurePassword,

        /// <summary>
        /// Secure password and password length.
        /// </summary>
        SecurePasswordAndLength,

        /// <summary>
        /// Secure password and plain-text password.
        /// </summary>
        SecurePasswordAndPassword,

        /// <summary>
        /// Secure password, plain-text password, and password length.
        /// </summary>
        All
    }

    private static readonly DependencyPropertyKey IsMonitoringDependencyPropertyKey = DependencyProperty.RegisterAttachedReadOnly(
        "IsMonitoring", typeof(bool), typeof(PasswordBoxMonitor), new PropertyMetadata(false));

    /// <summary>
    /// Enables monitoring of a <see cref="PasswordBox"/>.
    /// </summary>
    public static readonly DependencyProperty MonitoringProperty = DependencyProperty.RegisterAttached(
        "Monitoring", typeof(MonitorLevel), typeof(PasswordBoxMonitor), new PropertyMetadata(MonitorLevel.None, OnMonitoringChanged), ValidateMonitorValue);

    /// <summary>
    /// Gets the length of the password currently shown in the
    /// <see cref="PasswordBox"/>.
    /// </summary>
    public static readonly DependencyProperty PasswordLengthProperty = DependencyProperty.RegisterAttached(
        "PasswordLength", typeof(int), typeof(PasswordBoxMonitor), new PropertyMetadata(0));

    /// <summary>
    /// Emulates a bindable version of <see cref="PasswordBox.Password"/>.
    /// </summary>
    public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached(
        "Password", typeof(string), typeof(PasswordBoxMonitor), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    /// <summary>
    /// Emulates a bindable version of
    /// <see cref="PasswordBox.SecurePassword"/>.
    /// </summary>
    public static readonly DependencyProperty SecurePasswordProperty = DependencyProperty.RegisterAttached(
        "SecurePassword", typeof(SecureString), typeof(PasswordBoxMonitor), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    /// <summary>
    /// Identifies a property that indicates whether the
    /// <see cref="PasswordBox"/> is being monitored.
    /// </summary>
    public static readonly DependencyProperty IsMonitoringDependencyProperty = IsMonitoringDependencyPropertyKey.DependencyProperty;

    /// <summary>
    /// Gets whether the specified <see cref="PasswordBox"/> is being
    /// monitored.
    /// </summary>
    /// <param name="obj">
    /// The instance to read the attached dependency property's value
    /// from.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the instance monitors the specified
    /// <see cref="PasswordBox"/>, otherwise <see langword="false"/>.
    /// </returns>
    public static bool GetIsMonitoring(DependencyObject obj)
    {
        return GetMonitoring(obj) != MonitorLevel.None;
    }

    /// <summary>
    /// Gets the value of the attached dependency property
    /// <see cref="MonitoringProperty"/>.
    /// </summary>
    /// <param name="obj">
    /// The instance to read the attached dependency property's value
    /// from.
    /// </param>
    /// <returns>
    /// The active monitoring level for the <see cref="PasswordBox"/>.
    /// </returns>
    public static MonitorLevel GetMonitoring(DependencyObject obj)
    {
        return (MonitorLevel)obj.GetValue(MonitoringProperty);
    }

    /// <summary>
    /// Sets the value of the attached dependency property
    /// <see cref="MonitoringProperty"/> on the specified object.
    /// </summary>
    /// <param name="obj">
    /// The object on which to set the attached dependency property
    /// value.
    /// </param>
    /// <param name="value">The value to set for the attached property.</param>
    public static void SetMonitoring(DependencyObject obj, MonitorLevel value)
    {
        obj.SetValue(MonitoringProperty, value);
    }

    /// <summary>
    /// Gets the value of the attached dependency property
    /// <see cref="PasswordLengthProperty"/>.
    /// </summary>
    /// <param name="obj">
    /// The instance to read the attached dependency property's value
    /// from.
    /// </param>
    /// <returns>
    /// The password length for the monitored
    /// <see cref="PasswordBox"/>.
    /// </returns>
    public static int GetPasswordLength(DependencyObject obj)
    {
        return (int)obj.GetValue(PasswordLengthProperty);
    }

    /// <summary>
    /// Sets the value of the attached dependency property
    /// <see cref="PasswordLengthProperty"/> on the specified object.
    /// </summary>
    /// <param name="obj">
    /// The object on which to set the attached dependency property
    /// value.
    /// </param>
    /// <param name="value">The value to set for the attached property.</param>
    public static void SetPasswordLength(DependencyObject obj, int value)
    {
        obj.SetValue(PasswordLengthProperty, value);
    }

    /// <summary>
    /// Gets the value of the attached dependency property
    /// <see cref="PasswordProperty"/>.
    /// </summary>
    /// <param name="obj">
    /// The instance to read the attached dependency property's value
    /// from.
    /// </param>
    /// <returns>
    /// The password set on the monitored <see cref="PasswordBox"/>.
    /// </returns>
    public static string GetPassword(DependencyObject obj)
    {
        return (string)obj.GetValue(PasswordProperty);
    }

    /// <summary>
    /// Sets the value of the attached dependency property
    /// <see cref="PasswordProperty"/> on the specified object.
    /// </summary>
    /// <param name="obj">
    /// The object on which to set the attached dependency property
    /// value.
    /// </param>
    /// <param name="value">The value to set for the attached property.</param>
    public static void SetPassword(DependencyObject obj, string value)
    {
        obj.SetValue(PasswordProperty, value);
    }

    /// <summary>
    /// Gets the value of the attached dependency property
    /// <see cref="SecurePasswordProperty"/>.
    /// </summary>
    /// <param name="obj">
    /// The instance to read the attached dependency property's value
    /// from.
    /// </param>
    /// <returns>
    /// The secure password set on the monitored
    /// <see cref="PasswordBox"/>.
    /// </returns>
    public static SecureString GetSecurePassword(DependencyObject obj)
    {
        return (SecureString)obj.GetValue(SecurePasswordProperty);
    }

    /// <summary>
    /// Sets the value of the attached dependency property
    /// <see cref="SecurePasswordProperty"/> on the specified object.
    /// </summary>
    /// <param name="obj">
    /// The object on which to set the attached dependency property
    /// value.
    /// </param>
    /// <param name="value">The value to set for the attached property.</param>
    public static void SetSecurePassword(DependencyObject obj, SecureString value)
    {
        obj.SetValue(SecurePasswordProperty, value);
    }

    private static void OnMonitoringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PasswordBox pb)
        {
            (MonitorLevel nv, MonitorLevel ov) = ((MonitorLevel)e.NewValue, (MonitorLevel)e.OldValue);
            if (nv == MonitorLevel.None && ov != MonitorLevel.None)
            {
                d.SetValue(IsMonitoringDependencyPropertyKey, false);
                pb.PasswordChanged -= PasswordChanged;
            }
            else if (nv != MonitorLevel.None && ov == MonitorLevel.None)
            {
                d.SetValue(IsMonitoringDependencyPropertyKey, true);
                pb.PasswordChanged += PasswordChanged;
            }
        }
    }

    private static void PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (sender is PasswordBox pb)
        {
            MonitorLevel v = GetMonitoring(pb);
            if (v.HasFlag(MonitorLevel.None)) SetPasswordLength(pb, pb.Password.Length);
            if (v.HasFlag(MonitorLevel.Password)) SetPassword(pb, pb.Password);
            if (v.HasFlag(MonitorLevel.SecurePassword)) SetSecurePassword(pb, pb.SecurePassword);
        }
    }

    private static bool ValidateMonitorValue(object value)
    {
        return value is MonitorLevel l && typeof(MonitorLevel).IsEnumDefined(l);
    }
}
