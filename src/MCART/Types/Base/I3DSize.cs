namespace TheXDS.MCART.Types.Base
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que represente
    /// un tamaño en tres dimensiones.
    /// </summary>
    public interface I3DSize : ISize
    {
        /// <summary>
        /// Obtiene el componente de profundidad del tamaño.
        /// </summary>
        double Depth { get; set; }
    }
}