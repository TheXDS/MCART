/*
PasswordBoxMonitor.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

using System;
using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace TheXDS.MCART.Wpf.Component
{
    /// <summary>
    /// Contiene una serie de propiedades de dependencia adjuntas para
    /// monitorear y controlar un <see cref="PasswordBox"/>.
    /// </summary>
    public class PasswordBoxMonitor : DependencyObject
    {
        /// <summary>
        /// Enumera los distintos niveles de monitoreo de contraseña.
        /// </summary>
        [Flags]
        public enum MonitorLevel : byte
        {
            /// <summary>
            /// Ninguno. El <see cref="PasswordBox"/> no será monitoreado.
            /// </summary>
            None,

            /// <summary>
            /// Longitud de contraseña. Únicamente se expondrá la propiedad
            /// <see cref="PasswordLengthProperty"/>.
            /// </summary>
            Length,

            /// <summary>
            /// Contraseña de texto plano. Monitorea el contenido de la
            /// contraseña de texto plano del <see cref="PasswordBox"/>.
            /// </summary>
            Password,

            /// <summary>
            /// Contraseña de texto plano y longitud de cotraseña.
            /// </summary>
            PasswordAndLength,

            /// <summary>
            /// Contraseña segura. Monitorea el contenido de la contraseña
            /// segura del <see cref="PasswordBox"/>.
            /// </summary>
            SecurePassword,

            /// <summary>
            /// Contraseña segura y longitud de cotraseña.
            /// </summary>
            SecurePasswordAndLength,

            /// <summary>
            /// Contraseña segura y contraseña de texto plano.
            /// </summary>
            SecurePasswordAndPassword,

            /// <summary>
            /// Contraseña segura, contraseña de texto plano y longitud de cotraseña.
            /// </summary>
            All
        }

        private static readonly DependencyPropertyKey IsMonitoringDependencyPropertyKey = DependencyProperty.RegisterAttachedReadOnly(
            "IsMonitoring", typeof(bool), typeof(PasswordBoxMonitor), new PropertyMetadata(false));

        /// <summary>
        /// Permite monitorear un <see cref="PasswordBox"/>.
        /// </summary>
        public static readonly DependencyProperty MonitoringProperty = DependencyProperty.RegisterAttached(
            "Monitoring", typeof(MonitorLevel), typeof(PasswordBoxMonitor), new PropertyMetadata(MonitorLevel.None, OnMonitoringChanged),ValidateMonitorValue);

        /// <summary>
        /// Obtiene la longitud de la contraseña presentada en el
        /// <see cref="PasswordBox"/>.
        /// </summary>
        public static readonly DependencyProperty PasswordLengthProperty = DependencyProperty.RegisterAttached(
            "PasswordLength", typeof(int), typeof(PasswordBoxMonitor), new PropertyMetadata(0));

        /// <summary>
        /// Emula una propiedad <see cref="PasswordBox.Password"/> bindeable.
        /// </summary>
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached(
            "Password", typeof(string), typeof(PasswordBoxMonitor), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// Emula una propiedad <see cref="PasswordBox.SecurePassword"/> bindeable.
        /// </summary>
        public static readonly DependencyProperty SecurePasswordProperty = DependencyProperty.RegisterAttached(
            "SecurePassword", typeof(SecureString), typeof(PasswordBoxMonitor), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// Obtiene una propiedad que indica si el <see cref="PasswordBox"/>
        /// está siendo monitoreado.
        /// </summary>
        public static readonly DependencyProperty IsMonitoringDependencyProperty = IsMonitoringDependencyPropertyKey.DependencyProperty;

        /// <summary>
        /// Obtiene un valor que indica si el <see cref="PasswordBox"/> está
        /// siendo monitoreado.
        /// </summary>
        /// <param name="obj">
        /// Instancia para la cual obtener el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si esta instancia monitorea al
        /// <see cref="PasswordBox"/> especificado, <see langword="false"/> en
        /// caso contrario.
        /// </returns>
        public static bool GetIsMonitoring(DependencyObject obj)
        {
            return GetMonitoring(obj) != MonitorLevel.None;
        }

        /// <summary>
        /// Obtiene el valor de la propiedad de dependencia adjunta
        /// <see cref="MonitoringProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Instancia para la cual obtener el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <returns>
        /// El valor de monitoreo activo para el <see cref="PasswordBox"/>.
        /// </returns>
        public static MonitorLevel GetMonitoring(DependencyObject obj)
        {
            return (MonitorLevel)obj.GetValue(MonitoringProperty);
        }

        /// <summary>
        /// Establece el valor de la propiedad de dependencia adjunta
        /// <see cref="MonitoringProperty"/> para el objeto especificado.
        /// </summary>
        /// <param name="obj">
        /// Objeto para el cual establecer el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <param name="value">Valor de la propiedad de dependencia adjunta.</param>
        public static void SetMonitoring(DependencyObject obj, MonitorLevel value)
        {
            obj.SetValue(MonitoringProperty, value);
        }

        /// <summary>
        /// Obtiene el valor de la propiedad de dependencia adjunta
        /// <see cref="PasswordLengthProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Instancia para la cual obtener el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <returns>
        /// La longitud de la contraseña para el <see cref="PasswordBox"/>
        /// monitoreado.
        /// </returns>
        public static int GetPasswordLength(DependencyObject obj)
        {
            return (int)obj.GetValue(PasswordLengthProperty);
        }

        /// <summary>
        /// Establece el valor de la propiedad de dependencia adjunta
        /// <see cref="PasswordLengthProperty"/> para el objeto especificado.
        /// </summary>
        /// <param name="obj">
        /// Objeto para el cual establecer el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <param name="value">Valor de la propiedad de dependencia adjunta.</param>
        public static void SetPasswordLength(DependencyObject obj, int value)
        {
            obj.SetValue(PasswordLengthProperty, value);
        }

        /// <summary>
        /// Obtiene el valor de la propiedad de dependencia adjunta
        /// <see cref="PasswordProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Instancia para la cual obtener el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <returns>
        /// La contraseña establecida en el <see cref="PasswordBox"/>
        /// monitoreado.
        /// </returns>
        public static string GetPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        /// <summary>
        /// Establece el valor de la propiedad de dependencia adjunta
        /// <see cref="PasswordProperty"/> para el objeto especificado.
        /// </summary>
        /// <param name="obj">
        /// Objeto para el cual establecer el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <param name="value">Valor de la propiedad de dependencia adjunta.</param>
        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        /// <summary>
        /// Obtiene el valor de la propiedad de dependencia adjunta
        /// <see cref="SecurePasswordProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Instancia para la cual obtener el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <returns>
        /// La contraseña establecida en el <see cref="PasswordBox"/>
        /// monitoreado.
        /// </returns>
        public static SecureString GetSecurePassword(DependencyObject obj)
        {
            return (SecureString)obj.GetValue(SecurePasswordProperty);
        }

        /// <summary>
        /// Establece el valor de la propiedad de dependencia adjunta
        /// <see cref="SecurePasswordProperty"/> para el objeto especificado.
        /// </summary>
        /// <param name="obj">
        /// Objeto para el cual establecer el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <param name="value">Valor de la propiedad de dependencia adjunta.</param>
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
}