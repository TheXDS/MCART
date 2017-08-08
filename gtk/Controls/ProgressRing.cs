//
//  DrawingArea.cs
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
using Gtk;
using Gdk;
using Cairo;
using static MCART.Math;

namespace MCART.Controls
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class ProgressRing : DrawingArea
    {
        #region Campos privados
        float _thickness = 4.0f;
        float _radius = 24.0f;
        float _value = 0.33f;
        float _min;
        float _max = 100.0f;
        float _angle;
        float _fontFactor = 0.85f;
        Types.Color _ringColor;
        Types.Color _fill;
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
                if (!value.IsBetween(0, _radius))
                    throw new ArgumentOutOfRangeException(nameof(value));
                _thickness = value;
                QueueDraw();
            }
        }
        /// <summary>
        /// Obtiene o establece la proporción con respecto al radio a utilizar
        /// como tamaño de la fuente para la etiqueta central de este
        /// <see cref="ProgressRing"/>. 
        /// </summary>
        public float FontFactor
        {
            get => _fontFactor;
            set
            {
                if (!value.IsBetween(0.0f, 1.0f))
                    throw new ArgumentOutOfRangeException(nameof(value));
                _fontFactor = value;

                QueueDraw();
            }
        }
        /// <summary>
        /// Obtiene o establece el radio del anillo de este
        /// <see cref="ProgressRing"/>.
        /// </summary>
        public float Radius
        {
            get => _radius;
            set
            {

                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
                _radius = value;

                QueueDraw();
            }
        }
        /// <summary>
        /// Obtiene o establece el valor a representar en este
        /// <see cref="ProgressRing"/>.
        /// </summary>
        public float Value
        {
            get => _value;
            set
            {
                // Si value es NaN, el anillo mostrará progreso indeterminado.
                if (!value.IsBetween(_min, _max) || !float.IsNaN(value))
                    throw new ArgumentOutOfRangeException(nameof(value));
                _value = value;

                QueueDraw();
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

                QueueDraw();
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

                QueueDraw();
            }
        }
        /// <summary>
        /// Obtiene o establece un valor que indica si se mostrará un estado
        /// indeterminado en este <see cref="ProgressRing"/>.
        /// </summary>
        public bool IsIndeterminate
        {
            get => float.IsNaN(_value);
            set
            {
                _value = value ? float.NaN : 0.0f;

                QueueDraw();
            }
        }
        /// <summary>
        /// Obtiene o establece el <see cref="Types.Color"/> a utilizar para dibujar
        /// el fondo del anillo de este <see cref="ProgressRing"/>.
        /// </summary>
        public Types.Color RingColor
        {
            get => _ringColor;
            set
            {
                _ringColor = value;

                QueueDraw();
            }
        }
        /// <summary>
        /// Obtiene o establece el <see cref="Types.Color"/> a utilizar para dibujar
        /// el relleno del anillo de este <see cref="ProgressRing"/>.
        /// </summary>
        public Types.Color Fill
        {
            get => _fill;
            set
            {
                _fill = value;

                QueueDraw();
            }
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

                QueueDraw();

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

                QueueDraw();
            }
        }
        /// <summary>
        /// Obtiene o establece el formato de texto a aplicar al centro de este
        /// <see cref="ProgressRing"/>.
        /// </summary>
        public string TextFormat { get => textFormat; set => textFormat = value; }
        #endregion

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ProgressRing"/>.
        /// </summary>
        public ProgressRing()
        {
            SetSizeRequest((int)_radius, (int)_radius);
        }
        /// <summary>
        /// Controla el dibujado del control al generarse el evento
        /// <see cref="EventExpose"/> 
        /// </summary>
        /// <returns>Esta función siempre devuelve <c>true</c>.</returns>
        /// <param name="evnt">Argumentos del evento.</param>
        protected override bool OnExposeEvent(EventExpose evnt)
        {
            //base.OnExposeEvent(evnt);

            // Este objeto dibuja el control.
            using (Context cr = CairoHelper.Create(evnt.Window))
            {
                // Grosor de las líneas.
                cr.LineWidth = _thickness;

                // Preparar propiedades de texto...
                cr.SelectFontFace(
                    "Courier 10 Pitch",
                    FontSlant.Normal,
                    FontWeight.Normal);
                cr.SetFontSize(_radius * _fontFactor);

                // Dibujar el Fondo...
                cr.SetSourceColor(_ringColor);
                cr.Arc(_radius, _radius, _radius, 0.0, 360.0);

                // Dibujar el relleno...
                cr.SetSourceColor(_fill);
                cr.Arc(
                    _radius,
                    _radius,
                    _radius,
                    _angle * Deg_Rad,
                    System.Math.PI * (_value / (_max - _min))
                );
                //TODO: dibujar texto...
                return true;
            }
        }

        //protected override void OnSizeAllocated(Gdk.Rectangle allocation)
        //{
        //    base.OnSizeAllocated(allocation);
        //    // Insert layout code here.
        //}

        /// <summary>
        /// Informa a Gtk del tamaño requerido por este control.
        /// </summary>
        /// <param name="requisition">Requisition.</param>
        protected override void OnSizeRequested(ref Requisition requisition)
        {
            requisition.Height = (int)_radius * 2;
            requisition.Width = (int)_radius * 2;
        }
    }
}