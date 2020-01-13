/*
BusyContainer.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     "Surfin Bird" (Original implementation) <https://stackoverflow.com/users/4267982/surfin-bird>
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

#nullable enable

using System.Windows;
using System.Windows.Controls;
using TheXDS.MCART.Misc;
using System.Windows.Media.Effects;

namespace TheXDS.MCART.Controls
{
    /// <summary>
    /// Contenedor que permite bloquear el contenido mientras la aplicación
    /// se encuentre ocupada.
    /// </summary>
    public class BusyContainer : ContentControl
    {
        private static readonly DependencyPropertyKey _currentBusyEffectPropertyKey = DependencyProperty.RegisterReadOnly(nameof(CurrentBusyEffect), typeof(Effect), typeof(BusyContainer), new PropertyMetadata(null, null, OnBusyEffectChanged));

        private static void OnChangeCurrentBusyEffect(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(CurrentBusyEffectProperty);
        }

        private static object? OnBusyEffectChanged(DependencyObject d, object baseValue)
        {
            var o = (BusyContainer)d;
            return o.IsBusy ? d.GetValue(BusyEffectProperty): null;
        }

        /// <summary>
        /// Inicializa la clase <see cref="BusyContainer"/>.
        /// </summary>
        static BusyContainer()
        {
            var d = new ResourceDictionary { Source = WpfInternal.MkTemplateUri<BusyContainer>() };
            Application.Current?.Resources.MergedDictionaries.Add(d);
        }

        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="IsBusy"/>.
        /// </summary>
        public static DependencyProperty IsBusyProperty = DependencyProperty.Register(nameof(IsBusy), typeof(bool), typeof(BusyContainer), new PropertyMetadata(false, OnChangeCurrentBusyEffect));

        /// <summary>
        /// Identifica a la propiedad de dependencia
        /// <see cref="BusyContent"/>.
        /// </summary>
        public static DependencyProperty BusyContentProperty = DependencyProperty.Register(nameof(BusyContent), typeof(object), typeof(BusyContainer), new PropertyMetadata(null));

        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="BusyContentStringFormat"/>.
        /// </summary>
        public static DependencyProperty BusyContentStringFormatProperty = DependencyProperty.Register(nameof(BusyContentStringFormat), typeof(string), typeof(BusyContainer), new PropertyMetadata(null));

        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="BusyEffect"/>.
        /// </summary>
        public static DependencyProperty BusyEffectProperty = DependencyProperty.Register(nameof(BusyEffect), typeof(Effect), typeof(BusyContainer), new PropertyMetadata(new BlurEffect() { Radius = 5.0 }, OnChangeCurrentBusyEffect));
        
        /// <summary>
        /// Identifica a la propiedad de dependencia de solo lectura <see cref="CurrentBusyEffect"/>.
        /// </summary>
        public static DependencyProperty CurrentBusyEffectProperty = _currentBusyEffectPropertyKey.DependencyProperty;

        /// <summary>
        /// Obtiene o establece el efecto a aplicar al contenido cuando
        /// este control se encuentre ocupado.
        /// </summary>
        public Effect BusyEffect
        {
            get => (Effect)GetValue(BusyEffectProperty);
            set => SetValue(BusyEffectProperty, value);
        }

        /// <summary>
        /// Obtiene o establece el formato a utilizar para mostrar el
        /// contenido ocupado de este control.
        /// </summary>
        public string BusyContentStringFormat
        {
            get => (string)GetValue(BusyContentStringFormatProperty);
            set => SetValue(BusyContentStringFormatProperty, value);
        }

        /// <summary>
        /// Obtiene o establece el contenido a mostrar cuando el control se
        /// encuente ocupado.
        /// </summary>
        public object BusyContent
        {
            get => GetValue(BusyContentProperty);
            set => SetValue(BusyContentProperty, value);
        }

        /// <summary>
        /// Obtiene o establece un valor que coloca este contenedor en
        /// estado de ocupado.
        /// </summary>
        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set => SetValue(IsBusyProperty, value);
        }

        /// <summary>
        /// Obtiene el efecto actualmente aplicado al estado de ocupado del control.
        /// </summary>
        public Effect CurrentBusyEffect => (Effect)GetValue(CurrentBusyEffectProperty);

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="BusyContainer"/>.
        /// </summary>
        public BusyContainer()
        {
            BusyContent = new BusyIndicator();
        }
    }
}