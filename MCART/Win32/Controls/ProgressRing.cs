//
//  ProgressRing.cs
//
//  This file is part of MCART
//
//  Author:
//       César Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Morgan
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

using System;
using System.Drawing;
using System.Windows.Forms;

namespace MCART.Controls
{
    public partial class ProgressRing : Control
    {
        #region Campos privados
        float _thickness = 8.0f;
        float _radius = 48.0f;
        float _value;
        float _min;
        float _max = 100.0f;
        float _angle;
        string _textFormat = "{0:0.0}%";
        Color _ringColor = SystemColors.ControlDark;
        Color _fill = SystemColors.Highlight;
        SweepDirection _sweep = SweepDirection.Clockwise;
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
                Invalidate();
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
                OnResize(this, EventArgs.Empty);
                Invalidate();
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
                Invalidate();
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
                Invalidate();
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
                Invalidate();
            }
        }
        /// <summary>
        /// Obtiene o establece un valor que indica si se mostrará un estado
        /// indeterminado en este <see cref="ProgressRing"/>.
        /// </summary>
        /// <value>
        /// <c>true</c> indica progreso indeterminado, causando que la propiedad
        /// <see cref="Value"/> sea <see cref="float.NaN"/>; <c>false</c> indica
        /// un progreso normal, y causará que <see cref="Value"/> sea igual a 0. 
        /// </value>
        public bool IsIndeterminate
        {
            get => float.IsNaN(_value);
            set
            {
                _value = value ? float.NaN : 0.0f;
                Invalidate();
            }
        }
        /// <summary>
        /// Obtiene o establece el <see cref="Types.Color"/> a utilizar para dibujar
        /// el fondo del anillo de este <see cref="ProgressRing"/>.
        /// </summary>
        public Color RingColor
        {
            get => _ringColor;
            set { _ringColor = value; Invalidate(); }
        }
        /// <summary>
        /// Obtiene o establece el <see cref="Types.Color"/> a utilizar para dibujar
        /// el relleno del anillo de este <see cref="ProgressRing"/>.
        /// </summary>
        public Color Fill
        {
            get => _fill;
            set { _fill = value; Invalidate(); }
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
                Invalidate();
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
                Invalidate();
            }
        }
        /// <summary>
        /// Obtiene o establece un formato de texto a aplicar al control.
        /// </summary>
        public string TextFormat
        {
            get => _textFormat;
            set => _textFormat = value;
        }
        /// <summary>
        /// Obtiene o establece la fuente del texto que muestra el control.
        /// </summary>
        public new Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
                Invalidate();
            }
        }
        #endregion
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ProgressRing"/>.
        /// </summary>
        public ProgressRing()
        {
            InitializeComponent();
            Height = (int)_radius * 2;
            Width = Height;
        }
        /// <summary>
        /// Controla el dibujado del control al generarse el evento
        /// <see cref="Control.Paint"/> 
        /// </summary>
        /// <param name="pe">Argumentos del evento.</param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            using (Graphics cr = pe.Graphics)
            {
                cr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                float th = Thickness / 2; // Grosor del anillo.
                float rd = _radius - th;  // Radio interno del anillo.
                float dm = rd * 2;        // Diámetro del anillo.               
                float nd = 360 * ((_value - _min) / (_max - _min)); //Ángulo final.

                // Dibujar el Fondo...
                cr.DrawEllipse(
                    new Pen(_ringColor, _thickness),
                    th, th, dm, dm);

                // Dibujar el relleno...
                if (!IsIndeterminate)
                {
                    if (Sweep == SweepDirection.Clockwise)
                        cr.DrawArc(new Pen(_fill, _thickness), th, th, dm, dm, _angle - 90, nd);
                    else
                        cr.DrawArc(new Pen(_fill, _thickness), th, th, dm, dm, 270 - nd, nd - _angle);
                }
                else
                {
                    //TODO: Animación de movimiento
                }

                // dibujar texto... 
                string m = string.Format(_textFormat, _value);
                SizeF s = cr.MeasureString(m, Font);
                cr.DrawString(m, Font, new SolidBrush(ForeColor), rd + th - s.Width / 2, rd + th - s.Height / 2);
            }
        }
        private void OnResize(object sender, EventArgs e)
        {
            Height = (int)_radius * 2;
            Width = Height;
        }
    }
}