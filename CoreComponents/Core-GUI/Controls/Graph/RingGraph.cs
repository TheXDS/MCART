/*
RingGraph.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace TheXDS.MCART.Controls
{
    public partial class RingGraph : ISliceGraph
    {
        IGraphColorizer colorizer;
        readonly ObservableCollection<Slice> slices = new ObservableCollection<Slice>();
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="RingGraph"/>.
        /// </summary>
        public RingGraph()
        {
            slices.CollectionChanged += Slices_CollectionChanged;
            Init();
        }
        /// <summary>
        /// Obtiene un listado de los <see cref="Slice"/> que conforman el
        /// set de datos de este <see cref="ISliceGraph"/>.
        /// </summary>
        /// <remarks>
        /// Esta no puede ser una propiedad de dependencia debido a que la
        /// observación de la lista de <see cref="Slice"/> se implementa
        /// mediante eventos.
        /// </remarks>
        public IList<Slice> Slices => slices;
        /// <summary>
        /// Obtiene o establece un <see cref="IGraphColorizer"/> opcional a
        /// utilizar para establecer los colores de las series.
        /// </summary>
        public IGraphColorizer Colorizer
        {
            get => colorizer;
            set
            {
                colorizer = value;
                Redraw();
            }
        }
        /// <summary>
        /// Vuelve a dibujar todo el control.
        /// </summary>
        /// <param name="r">
        /// <see cref="Slice"/> que ha realizado la solicitud.
        /// </param>
        public void DrawMe(Slice r)
        {
            // Si un Slice cambia, cambia todo el gráfico.
            Redraw();
        }
        private void Slices_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Slice j in e.NewItems) j.DrawingParent = this;
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
            Redraw();
        }
    }
}