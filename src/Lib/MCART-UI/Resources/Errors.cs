using System;

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Contiene una serie de funciones que generan excepciones para errores
    /// ocurridos dentro de las funciones de UI de MCART.
    /// </summary>
    public static class UiErrors
    {
        /// <summary>
        /// Genera una excepción cuando un valor es <see langword="null"/>.
        /// </summary>
        /// <param name="v">
        /// Nombre del valor que es <see langword="null"/>.
        /// </param>
        /// <returns>
        /// Una nueva instancia de la clase <see cref="ArgumentException"/> que
        /// describe el error.
        /// </returns>
        public static Exception NullValue(string v)
        {
            return new ArgumentException(string.Format(Strings.UiErrors.NullValueException, v));
        }
    }
}
