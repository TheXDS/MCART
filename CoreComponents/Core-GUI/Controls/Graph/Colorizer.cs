//
//  Colorizer.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
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

using System.Collections.Generic;
using System.Linq;

namespace MCART.Controls
{
    /// <summary>
    /// Describe una serie de métodos a implementar por clases que permitan
    /// colorear una colección de <see cref="IGraphData"/>.    
    /// </summary>
    public interface IGraphColorizer
    {
        /// <summary>
        /// Aplica el coloreado a la colección de <see cref="IGraphData"/>.
        /// </summary>
        /// <param name="data"></param>
        void Apply(IEnumerable<IGraphData> data);
    }

    /// <summary>
    /// Coloreado de temperatura.
    /// </summary>
    public class HeatColorizer : IGraphColorizer
    {
        /// <summary>
        /// Aplica el coloreado a la colección de <see cref="IGraphData"/>.
        /// </summary>
        /// <param name="data"></param>
        public void Apply(IEnumerable<IGraphData> data)
        {
            float c = data.Count();
            int i = 0;
            foreach (var j in data)
                j.Color = Types.Color.BlendHeat(++i / c);
        }
    }
    /// <summary>
    /// Coloreado de salud.
    /// </summary>
    public class HealthColorizer : IGraphColorizer
    {
        /// <summary>
        /// Aplica el coloreado a la colección de <see cref="IGraphData"/>.
        /// </summary>
        /// <param name="data"></param>
        public void Apply(IEnumerable<IGraphData> data)
        {
            float c = data.Count();
            int i = 0;
            foreach (var j in data)
                j.Color = Types.Color.BlendHealth(++i / c);
        }
    }
    /// <summary>
    /// Coloreado aleatorio.
    /// </summary>
    public class RandomColorizer : IGraphColorizer
    {
        /// <summary>
        /// Aplica el coloreado a la colección de <see cref="IGraphData"/>.
        /// </summary>
        /// <param name="data"></param>
        public void Apply(IEnumerable<IGraphData> data)
        {
            foreach (var j in data) j.Color = Resources.Colors.Pick();
        }
    }

    /// <summary>
    /// Extensiones especiales para <see cref="Slice"/>.
    /// </summary>
    public static class Extensions
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
            if (!data.Any()) throw new Exceptions.EmptyCollectionException<Slice>(data);
            min = double.MaxValue;
            max = double.MinValue;
            foreach (var j in data)
            {
                if (j.Value < min) min = j.Value;
                if (j.Value > max) max = j.Value;
            }
        }
    }
}