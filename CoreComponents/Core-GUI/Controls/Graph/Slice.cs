//
//  Slice.cs
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

using MCART.Types.Extensions;

namespace MCART.Controls
{
    /// <summary>
    /// Representa una sección de un <see cref="ISliceGraph"/>.
    /// </summary>
    public partial class Slice
    {
        /// <summary>
        /// Objeto sobre el cual este <see cref="Slice"/> se dibuja.
        /// </summary>
        internal ISliceGraph drawingParent;

        private void SubSlices_RemovedItem(List<Slice> sender, RemovedItemEventArgs<Slice> e)
        {
            e.RemovedItem.drawingParent = null;
            drawingParent?.DrawMyChildren(this);
        }
        private void SubSlices_AddedItem(List<Slice> sender, AddedItemEventArgs<Slice> e)
        {
            e.NewItem.drawingParent = drawingParent;
            drawingParent?.DrawMyChildren(this);
        }

        /// <summary>
        /// Convierte un arreglo de datos en una colección de
        /// <see cref="Slice"/>.
        /// </summary>
        /// <param name="values">
        /// Valores a incluir en el listado de <see cref="Slice"/>.
        /// </param>
        /// <returns>
        /// Una lista de <see cref="Slice"/> para utilizar como orígen de
        /// datos para un <see cref="ISliceGraph"/>.
        /// </returns>
        public static System.Collections.Generic.IEnumerable<Slice> FromValues(double[] values)
        {
            foreach (double j in values) yield return new Slice { Value = j };
        }
        /// <summary>
        /// Obtiene un listado de <see cref="Slice"/> que pertenecen a este
        /// <see cref="Slice"/>.
        /// </summary>
        public readonly List<Slice> SubSlices = new List<Slice>();
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Slice"/>.
        /// </summary>
        public Slice()
        {
            SubSlices.AddedItem += SubSlices_AddedItem;
            SubSlices.RemovedItem += SubSlices_RemovedItem;
        }
        /// <summary>
        /// Realiza algunas tareas de limpieza antes de destruir a este objeto.
        /// </summary>
        ~Slice()
        {
            SubSlices.AddedItem -= SubSlices_AddedItem;
            SubSlices.Clear();
        }
    }
}