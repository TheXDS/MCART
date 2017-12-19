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
using Gtk;
using Cairo;
using static MCART.Math;
using static System.Math;

namespace MCART.Controls
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class ProgressRing : DrawingArea
    {
        float _fontFactor = 0.4f;
        Types.Color _textColor = Resources.Colors.Gray;

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
                if (!value.IsValid()) throw new ArgumentException(nameof(value));
                if (!value.IsBetween(0.0f, 1.0f)) throw new ArgumentOutOfRangeException(nameof(value));
                _fontFactor = value;
                QueueDraw();
            }
        }

        /// <summary>
        /// Obtiene o establece el <see cref="Types.Color"/> a utilizar para dibujar
        /// el relleno del anillo de este <see cref="ProgressRing"/>.
        /// </summary>
        public Types.Color TextColor
        {
            get => _textColor;
            set { _textColor = value; QueueDraw(); }
        }

        /// <summary>
        /// Solicita a Gtk que este <see cref="Widget"/> vuelva a dibujarse.
        /// </summary>
        public void RequestRedraw() => QueueDraw();

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
        /// <see cref="OnDrawn"/> 
        /// </summary>
        /// <returns>Esta función siempre devuelve <c>true</c>.</returns>
        /// <param name="cr">Contexto gráfico a utilizar.</param>
        protected override bool OnDrawn(Context cr)
        {
            // Radio interno del anillo.
            float rd = _radius - (Thickness / 2);

            // Preparar propiedades de dibujo...
            cr.LineWidth = _thickness;

            // Dibujar el Fondo...                
            cr.SetSourceColor(_ringColor);
            cr.Arc(_radius, _radius, rd, 0.0, 360.0);
            cr.Stroke();

            // Dibujar el relleno...
            double strt = (_angle - 90) * Deg_Rad;
            double nd = (PI * 2.0 * ((_value - _min) * (sbyte)Sweep / (_max - _min))) - (PI / 2);
            cr.SetSourceColor(_fill);
            if (!IsIndeterminate)
            {
                if (Sweep == SweepDirection.Clockwise)
                    cr.Arc(_radius, _radius, rd, strt, nd);
                else
                    cr.Arc(_radius, _radius, rd, nd, strt);
                cr.Stroke();
            }
            else
            {
                //TODO: Animación de movimiento
            }

            // dibujar texto... 
            cr.LineWidth = 1.0;
            cr.SetFontSize(_radius * _fontFactor);
            cr.SetSourceColor(_textColor);
            TextExtents centertxt = cr.TextExtents(string.Format(textFormat, _value));
            cr.MoveTo(_radius - centertxt.Width / 2, _radius + _radius * (_fontFactor / 3));
            cr.TextPath(string.Format(textFormat, _value));
            cr.Fill();
            return true;
        }

        /// <summary>
        /// Informa a Gtk del tamaño requerido por este control.
        /// </summary>
        /// <param name="allocation">
        /// Variable de referencia a utilizar para devolver la información.
        /// </param>
        protected override void OnSizeAllocated(Gdk.Rectangle allocation)
        {
            //base.OnSizeAllocated(allocation);
            allocation.Height = (int)_radius * 2;
            allocation.Width = allocation.Height;
        }
    }
}