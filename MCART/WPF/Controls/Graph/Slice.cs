//
//  Slice.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using MCART.Resources;
using MCART.Types;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MCART.Controls
{
    public partial class Slice : DependencyObject
    {
        /// <summary>
        /// Etiqueta de este <see cref="Slice"/>.
        /// </summary>
        internal CheckBox label;
        /// <summary>
        /// Figura que representa a este valor al ser dibujado.
        /// </summary>
        internal UIElement shape;
        /// <summary>
        /// Padre visual de este <see cref="Slice"/>.
        /// </summary>
        internal Panel parent;

        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Name"/>.
        /// </summary>
        public static DependencyProperty NameProperty = DependencyProperty.Register(
                nameof(Name), typeof(string), typeof(Slice),
                new PropertyMetadata("Series"));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="SliceColor"/>.
        /// </summary>
        public static DependencyProperty SliceColorProperty = DependencyProperty.Register(
                nameof(SliceColor), typeof(Color), typeof(Slice),
                new PropertyMetadata(Colors.Pick()));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Value"/>.
        /// </summary>
        public static DependencyProperty ValueProperty = DependencyProperty.Register(
                nameof(Value), typeof(double), typeof(Slice),
                new PropertyMetadata(1.0));
        static DependencyPropertyKey SliceBrushPropertyKey = DependencyProperty.RegisterReadOnly(
                    nameof(SliceBrush), typeof(System.Windows.Media.Brush), typeof(Slice),
                    new PropertyMetadata(null));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="SliceBrush"/>.
        /// </summary>
        public static DependencyProperty SliceBrushProperty = SliceBrushPropertyKey.DependencyProperty;
        /// <summary>
        /// Obtiene un <see cref="System.Windows.Media.Brush"/> que puede
        /// utilizarse para colorear este <see cref="Slice"/>.
        /// </summary>
        public System.Windows.Media.Brush SliceBrush => (System.Windows.Media.Brush)GetValue(SliceBrushProperty);

        /// <summary>
        /// Obtiene o establece el nombre de este <see cref="Slice"/>.
        /// </summary>
        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }
        /// <summary>
        /// Obtiene o establece <see cref="Color"/> a utilizar para dibujar
        /// este <see cref="Slice"/>.
        /// </summary>
        public Color SliceColor
        {
            get
            {
                Color c = (Color)GetValue(SliceColorProperty);
                if (SliceBrush.IsNull()) SetValue(SliceBrushPropertyKey, (System.Windows.Media.Brush)c);
                return c;
            }
            set
            {
                SetValue(SliceBrushPropertyKey, (System.Windows.Media.Brush)value);
                SetValue(SliceColorProperty, value);
            }
        }
        /// <summary>
        /// Obtiene o establece el valor de este <see cref="Slice"/>.
        /// </summary>
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
    }
}