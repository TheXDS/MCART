namespace TheXDS.MCART
{
    /// <summary>
    ///     Fórmulas de suavizado.
    /// </summary>
    public static class Tween
    {
        /// <summary>
        ///     Realiza un suavizado lineal de un valor.
        /// </summary>
        /// <returns>
        ///     Un valor correspondiente al suavizado aplicado.
        /// </returns>
        /// <param name="step">Número de paso a suavizar.</param>
        /// <param name="total">Total de pasos.</param>
        public static float Linear(int step, int total)
        {
            return (float) step / total;
        }

        /// <summary>
        ///     Realiza un suavizado cuadrático de un valor.
        /// </summary>
        /// <returns>
        ///     Un valor correspondiente al suavizado aplicado.
        /// </returns>
        /// <param name="step">Número de paso a suavizar.</param>
        /// <param name="total">Total de pasos.</param>
        public static float Quadratic(int step, int total)
        {
            var t = (float) step / total;
            return t * t / (2 * t * t - 2 * t + 1);
        }

        /// <summary>
        ///     Realiza un suavizado cúbico de un valor.
        /// </summary>
        /// <returns>
        ///     Un valor correspondiente al suavizado aplicado.
        /// </returns>
        /// <param name="step">Número de paso a suavizar.</param>
        /// <param name="total">Total de pasos.</param>
        public static float Cubic(int step, int total)
        {
            var t = (float) step / total;
            return t * t * t / (3 * t * t - 3 * t + 1);
        }

        /// <summary>
        ///     Realiza un suavizado cuártico de un valor.
        /// </summary>
        /// <returns>
        ///     Un valor correspondiente al suavizado aplicado.
        /// </returns>
        /// <param name="step">Número de paso a suavizar.</param>
        /// <param name="total">Total de pasos.</param>
        public static float Quartic(int step, int total)
        {
            var t = (float) step / total;
            return -((t - 1) * (t - 1) * (t - 1) * (t - 1)) + 1;
        }
    }
}