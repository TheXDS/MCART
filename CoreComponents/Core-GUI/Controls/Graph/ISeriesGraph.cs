using System;
using System.Collections.Generic;
using System.Text;

namespace TheXDS.MCART.Controls.Graph
{
    /// <summary>
    /// Expone una serie de métodos de redibujado disponibles para un
    /// control que acepte un <see cref="Series"/>.
    /// </summary>
    interface ISeriesGraph
    {
        IList<Series> DataSeries { get; }
    }
}
