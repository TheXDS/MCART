using System;
namespace TheXDS.MCART.Controls.Graph
{
    /// <summary>
    /// Define una serie de métodos a implementar por controles que permitan
    /// mostrar gráficos, en plataformas distintas a WPF.
    /// </summary>
    public partial interface IGraph
    {
        /// <summary>
        /// Vuelve a dibujar el título del <see cref="IGraph"/>
        /// </summary>
        void DrawTitle();

    }
}
