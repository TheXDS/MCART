/*
SelectorPanel.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     "Surfin Bird" (Original implementation) <https://stackoverflow.com/users/4267982/surfin-bird>
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

using System.Windows;
using System.Windows.Controls;

namespace TheXDS.MCART.Controls
{
    /// <summary>
    /// Panel selector simple que utiliza una propiedad de índice para especificar el contenido a mostrar.
    /// </summary>
    public class SelectorPanel : Grid
    {
        /// <summary>
        /// Identifica a la propiedad de dependencia
        /// <see cref="SelectedContent"/>.
        /// </summary>
        public static readonly DependencyProperty SelectedContentProperty =
            DependencyProperty.Register("SelectedContent", typeof(UIElement), typeof(SelectorPanel), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange));

        /// <summary>
        /// Identifica a la propiedad de dependencia
        /// <see cref="SelectedContentIndex"/>.
        /// </summary>
        public static readonly DependencyProperty SelectedContentIndexProperty =
            DependencyProperty.Register("SelectedContentIndex", typeof(int), typeof(SelectorPanel), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsArrange));

        /// <summary>
        /// Obtiene o establece el contenido seleccionado a mostrar en este 
        /// <see cref="SelectorPanel"/>.
        /// </summary>
        public UIElement? SelectedContent
        {
            get => (UIElement?)GetValue(SelectedContentProperty);
            set
            {
                SetValue(SelectedContentProperty, value);
                if (value is UIElement v)
                {
                    SetValue(SelectedContentIndexProperty, InternalChildren.IndexOf(v));
                }
                else
                {
                    SetValue(SelectedContentIndexProperty, -1);
                }
            }
        }

        /// <summary>
        /// Obtiene o establece el índice del contenido seleccionado a mostrar
        /// en este <see cref="SelectorPanel"/>.
        /// </summary>
        public int SelectedContentIndex
        {
            get => (int)GetValue(SelectedContentIndexProperty);
            set
            {
                SetValue(SelectedContentIndexProperty, value);
                if (value >= 0 && InternalChildren.Count > value)
                {
                    SetValue(SelectedContentProperty, InternalChildren[value]);
                }
                else
                {
                    SetValue(SelectedContentProperty, null);
                }
            }
        }

        /// <inheritdoc/>
        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (UIElement? c in InternalChildren)
            {
                if (c is null) continue;
                c.Arrange(SelectedContent == c || InternalChildren.IndexOf(c) == SelectedContentIndex ? new Rect(0, 0, finalSize.Width, finalSize.Height) : new Rect(0, 0, 0, 0));
            }
            return finalSize;
        }
    }
}