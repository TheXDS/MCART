/*
PasswordBoxMonitor.cs

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

        private static readonly DependencyPropertyKey IsMonitoringDependencyPropertyKey = DependencyProperty.RegisterAttachedReadOnly("IsMonitoring", typeof(bool), typeof(PasswordBoxMonitor), new PropertyMetadata(false));
        
        /// <summary>
        /// Permite monitorear un <see cref="PasswordBox"/>.
        /// </summary>
        public static readonly DependencyProperty MonitoringProperty = DependencyProperty.RegisterAttached("Monitoring", typeof(MonitorLevel), typeof(PasswordBoxMonitor), new UIPropertyMetadata(MonitorLevel.None, OnMonitoringChanged));

        /// <summary>
        /// Obtiene la longitud de la contraseña presentada en el
        /// <see cref="PasswordBox"/>.
        /// </summary>
        public static readonly DependencyProperty PasswordLengthProperty = DependencyProperty.RegisterAttached("PasswordLength", typeof(int), typeof(PasswordBoxMonitor), new UIPropertyMetadata(0));

        /// <summary>
        /// Emula una propiedad <see cref="PasswordBox.Password"/> bindeable.
        /// </summary>
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordBoxMonitor), new UIPropertyMetadata(string.Empty));

        /// <summary>
        /// Emula una propiedad <see cref="PasswordBox.SecurePassword"/> bindeable.
        /// </summary>
        public static readonly DependencyProperty SecurePasswordProperty = DependencyProperty.RegisterAttached("SecurePassword", typeof(SecureString), typeof(PasswordBoxMonitor), new UIPropertyMetadata(null));

        /// <summary>
        /// Obtiene una propiedad que indica si el <see cref="PasswordBox"/>
        /// está siendo monitoreado.
        /// </summary>
        public static readonly DependencyProperty IsMonitoringDependencyProperty = IsMonitoringDependencyPropertyKey.DependencyProperty;

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
            return (MonitorLevel)obj.GetValue(MonitoringProperty) != MonitorLevel.None;
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
                var (nv, ov) = ((MonitorLevel)e.NewValue, (MonitorLevel)e.OldValue);
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

        static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox pb)
            {
                var v = GetMonitoring(pb);
                if (v.HasFlag(MonitorLevel.None)) SetPasswordLength(pb, pb.Password.Length);
                if (v.HasFlag(MonitorLevel.Password)) SetPassword(pb, pb.Password);
                if (v.HasFlag(MonitorLevel.SecurePassword)) SetSecurePassword(pb, pb.SecurePassword);
            }
        }
    }
}