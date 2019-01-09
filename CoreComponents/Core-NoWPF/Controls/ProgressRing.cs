/*
ProgressRing.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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

using TheXDS.MCART.Types;
using System;

namespace TheXDS.MCART.Controls
{
    public partial class ProgressRing : IRedrawable
    {
        #region Campos privados
        float _thickness = 8.0f;
        float _radius = 48.0f;
        float _value;
        float _min;
        float _max = 100.0f;
        float _angle;
        Color _ringColor = Resources.Colors.Gray;
        Color _fill = Resources.Colors.SkyBlue;
        SweepDirection _sweep = SweepDirection.Clockwise;
        string textFormat = "{0:0.0}%";
        #endregion
        #region Propiedades
        /// <summary>
        /// Obtiene o establece el grosor del anillo de este
        /// <see cref="ProgressRing"/>.
        /// </summary>
        public float Thickness
        {
            get => _thickness;
            set
            {
                if (!value.IsValid()) throw new ArgumentException(nameof(value));
                if (!value.IsBetween(0, _radius)) throw new ArgumentOutOfRangeException(nameof(value));
                _thickness = value;
                RequestRedraw();
            }
        }
        /// <summary>
        /// Obtiene o establece la proporción con respecto al radio a utilizar
        /// como tamaño de la fuente para la etiqueta central de este
        /// <see cref="ProgressRing"/>. 
        /// </summary>
        public float Radius
        {
            get => _radius;
            set
            {
                if (!value.IsValid()) throw new ArgumentException(nameof(value));
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                _radius = value;
                RequestRedraw();
            }
        }
        /// <summary>
        /// Obtiene o establece el valor a representar en este
        /// <see cref="ProgressRing"/>.
        /// </summary>
        /// <value>
        /// Cualquier valor comprendido entre <see cref="Minimum"/> y
        /// <see cref="Maximum"/>, o <see cref="float.NaN"/>. Si se establece en
        /// <see cref="float.NaN"/>, se mostrará una animación de progreso
        /// indeterminado.
        /// </value>
        public float Value
        {
            get => _value;
            set
            {
                if (!value.IsBetween(_min, _max) && !float.IsNaN(value))
                    throw new ArgumentOutOfRangeException(nameof(value));
                _value = value;
                RequestRedraw();
            }
        }
        /// <summary>
        /// Obtiene o establece el valor mínimo de este
        /// <see cref="ProgressRing"/>.
        /// </summary>
        public float Minimum
        {
            get => _min;
            set
            {
                if (!value.IsValid()) throw new ArgumentException(nameof(value));
                if (value >= _max) throw new ArgumentOutOfRangeException(nameof(value));
                _min = value;
                RequestRedraw();
            }
        }
        /// <summary>
        /// Obtiene o establece el valor máximo de este
        /// <see cref="ProgressRing"/>.
        /// </summary>
        public float Maximum
        {
            get => _max;
            set
            {
                if (!value.IsValid()) throw new ArgumentException(nameof(value));
                if (value <= _min) throw new ArgumentOutOfRangeException(nameof(value));
                _max = value;
                RequestRedraw();
            }
        }
        /// <summary>
        /// Obtiene o establece un valor que indica si se mostrará un estado
        /// indeterminado en este <see cref="ProgressRing"/>.
        /// </summary>
        /// <value>
        /// <see langword="true"/> indica progreso indeterminado, causando que la propiedad
        /// <see cref="Value"/> sea <see cref="float.NaN"/>; <see langword="false"/> indica
        /// un progreso normal, y causará que <see cref="Value"/> sea igual a 0. 
        /// </value>
        public bool IsIndeterminate
        {
            get => float.IsNaN(_value);
            set
            {
                _value = value ? float.NaN : 0.0f;
                RequestRedraw();
            }
        }
        /// <summary>
        /// Obtiene o establece el <see cref="Types.Color"/> a utilizar para dibujar
        /// el fondo del anillo de este <see cref="ProgressRing"/>.
        /// </summary>
        public Color RingColor
        {
            get => _ringColor;
            set { _ringColor = value; RequestRedraw(); }
        }
        /// <summary>
        /// Obtiene o establece el <see cref="Types.Color"/> a utilizar para dibujar
        /// el relleno del anillo de este <see cref="ProgressRing"/>.
        /// </summary>
        public Color Fill
        {
            get => _fill;
            set { _fill = value; RequestRedraw(); }
        }
        /// <summary>
        /// Obtiene o establece el ángulo desde el que se empezará a dibujar el
        /// relleno de este <see cref="ProgressRing"/>. 
        /// </summary>
        public float Angle
        {
            get => _angle;
            set
            {
                if (!value.IsBetween(0.0f, 360.0f)) throw new ArgumentOutOfRangeException(nameof(value));
                _angle = value;
                RequestRedraw();
            }
        }
        /// <summary>
        /// Obtiene o establece la dirección en la cual se rellenará este
        /// <see cref="ProgressRing"/>. 
        /// </summary>
        public SweepDirection Sweep
        {
            get => _sweep;
            set
            {
                if (!typeof(SweepDirection).IsEnumDefined(value))
                    throw new ArgumentException(nameof(value));
                _sweep = value;
                RequestRedraw();
            }
        }
        /// <summary>
        /// Obtiene o establece un formato de texto a aplicar al control.
        /// </summary>
        public string TextFormat
        {
            get => textFormat;
            set
            {
                textFormat = value;
                RequestRedraw();
            }
        }
        #endregion
    }
}