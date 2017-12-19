//
//  RingGraph.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
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
namespace MCART.Controls
{
    public partial class RingGraph
    {
        /// <summary>
        /// Realiza tareas adicionales de inicialización.
        /// </summary>
        public void Init()
        {

        }



        public void Redraw()
        {

        }


        /// <summary>
        /// Vuelve a dibujar únicamente a los hijos del <see cref="Slice"/>.
        /// </summary>
        /// <param name="r">
        /// <see cref="Slice"/> que ha realizado la solicitud.
        /// </param>
        public void DrawMyChildren(Slice r)
        {
            //HACK: Aún no es posible redibujar selectivamente los hijos de r.
            Redraw();
        }

        public void DrawOnlyMe(Slice r)
        {
            //HACK: Aún no es posible redibujar selectivamente los hijos de r.
            Redraw();
        }
        public void DrawMyLabel(Slice r)
        {
            //HACK: Aún no es posible redibujar selectivamente los hijos de r.
            Redraw();
        }
    }
}