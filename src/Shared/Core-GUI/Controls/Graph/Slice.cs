/*
Slice.cs

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

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;

namespace TheXDS.MCART.Controls
{
    /// <summary>
    /// Representa una sección de un <see cref="ISliceGraph"/>.
    /// </summary>
    public partial class Slice : IGraphData
    {
        private ISliceGraph drawingParent;
        /// <summary>
        /// Objeto sobre el cual este <see cref="Slice"/> se dibuja.
        /// </summary>
        internal ISliceGraph DrawingParent
        {
            get => drawingParent; set
            {
                drawingParent = value;
                foreach (var j in SubSlices) j.DrawingParent = value;
            }
        }
        /// <summary>
        /// Obtiene un listado de <see cref="Slice"/> que pertenecen a este
        /// <see cref="Slice"/>.
        /// </summary>
        public readonly ObservableCollection<Slice> SubSlices = new ObservableCollection<Slice>();
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
        public static IEnumerable<Slice> FromValues(double[] values)
        {
            foreach (var j in values) yield return new Slice { Value = j };
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Slice"/>.
        /// </summary>
        public Slice()
        {
            SubSlices.CollectionChanged += SubSlices_CollectionChanged;
            Color = Resources.Colors.Pick();
        }
        /// <summary>
        /// Realiza algunas tareas de limpieza antes de destruir a este objeto.
        /// </summary>
        ~Slice()
        {
            SubSlices.Clear();
        }
        private void SubSlices_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Slice j in e.NewItems) j.DrawingParent = DrawingParent;
                    break;
                case NotifyCollectionChangedAction.Replace:
                    foreach (Slice j in e.OldItems) j.DrawingParent = null;
                    goto case NotifyCollectionChangedAction.Add;
                case NotifyCollectionChangedAction.Remove:
                    foreach (Slice j in e.OldItems) j.DrawingParent = null;
                    break;
                case NotifyCollectionChangedAction.Reset: return;
                default: break;
            }
            DrawingParent?.DrawMyChildren(this);
        }
    }

    /// <summary>
    /// Extensiones especiales para <see cref="Slice"/>.
    /// </summary>
    public static class SliceExtensions
    {
        /// <summary>
        /// Obtiene los límites mínimo y máximo de una colección de
        /// <see cref="Slice"/>.
        /// </summary>
        /// <param name="data">
        /// Colección de <see cref="Slice"/> a analizar.
        /// </param>
        /// <param name="min">
        /// Parámetro de salida. Valor mínimo de la colección.
        /// </param>
        /// <param name="max">
        /// Parámetro de salida. Valor máximo de la colección.
        /// </param>
        public static void GetBounds(this IEnumerable<Slice> data, out double min, out double max)
        {
            if (!data.Any()) throw new Exceptions.EmptyCollectionException(data);
            min = double.MaxValue;
            max = double.MinValue;
            foreach (var j in data)
            {
                if (j.Value < min) min = j.Value;
                if (j.Value > max) max = j.Value;
            }
        }
        /// <summary>
        /// Obtiene la suma de todos los valores de una colección de
        /// <see cref="Slice"/>.
        /// </summary>
        /// <param name="c">
        /// Colección de <see cref="Slice"/> para la cual obtener la suma.
        /// </param>
        /// <returns>
        /// Un <see cref="double"/> con la suma de los valores de una colección
        /// de <see cref="Slice"/>.
        /// </returns>
        public static double GetTotal(this IEnumerable<Slice> c)
        {
            var tot = 0.0;
            foreach (var k in c) tot += k.Value;
            return tot;
        }
    }
}