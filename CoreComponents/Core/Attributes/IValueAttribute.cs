namespace TheXDS.MCART.Attributes
{
    /// <summary>
    ///     Define una interfaz para los atributos que expongan valores por
    ///     medio de la propiedad <see cref="Value"/>.
    /// </summary>
    /// <typeparam name="T">
    ///     Tipo del valor espuesto por este atributo.
    /// </typeparam>
    public interface IValueAttribute<out T>
    {
        /// <summary>
        /// Obtiene el valor de este atributo.
        /// </summary>
        T Value { get; }
    }
}