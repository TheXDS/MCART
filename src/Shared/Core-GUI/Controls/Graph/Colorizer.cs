/*
Colorizer.cs

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

using System.Collections.Generic;
using System.Linq;

namespace TheXDS.MCART.Controls
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

    /// <inheritdoc />
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
            var graphDatas = data as IGraphData[] ?? data.ToArray();
            float c = graphDatas.Count();
            var i = 0;
            foreach (var j in graphDatas)
                j.Color = Types.Color.BlendHeat(++i / c);
        }
    }
    /// <inheritdoc />
    /// <summary>
    /// Coloreado de salud.
    /// </summary>
    public class HealthColorizer : IGraphColorizer
    {
        /// <inheritdoc />
        /// <summary>
        /// Aplica el coloreado a la colección de <see cref="T:TheXDS.MCART.Controls.IGraphData" />.
        /// </summary>
        /// <param name="data"></param>
        public void Apply(IEnumerable<IGraphData> data)
        {
            var graphDatas = data as IGraphData[] ?? data.ToArray();
            float c = graphDatas.Count();
            var i = 0;
            foreach (var j in graphDatas)
                j.Color = Types.Color.BlendHealth(++i / c);
        }
    }
    /// <inheritdoc />
    /// <summary>
    /// Coloreado aleatorio.
    /// </summary>
    public class RandomColorizer : IGraphColorizer
    {
        /// <inheritdoc />
        /// <summary>
        /// Aplica el coloreado a la colección de <see cref="T:TheXDS.MCART.Controls.IGraphData" />.
        /// </summary>
        /// <param name="data"></param>
        public void Apply(IEnumerable<IGraphData> data)
        {
            foreach (var j in data) j.Color = Resources.Colors.Pick();
        }
    }
}