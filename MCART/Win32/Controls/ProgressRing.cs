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
        /// <summary>
        /// Solicita a GDI que este <see cref="Control"/> vuelva a dibujarse.
        /// </summary>
        public void RequestRedraw()
        {
            Height = (int)_radius * 2;
            Width = Height;
            Invalidate();
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
                RequestRedraw();
            }
        }

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
                string m = string.Format(textFormat, _value);
                SizeF s = cr.MeasureString(m, Font);
                cr.DrawString(m, Font, new SolidBrush(ForeColor), rd + th - s.Width / 2, rd + th - s.Height / 2);
            }
        }
    }
}