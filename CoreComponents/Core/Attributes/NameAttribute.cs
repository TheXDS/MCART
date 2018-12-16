using System;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc />
    /// <summary>
    ///     Establece un nombre personalizado para describir este elemento.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    [Serializable]
    public sealed class NameAttribute : TextAttribute
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="NameAttribute" />.
        /// </summary>
        /// <param name="name">Valor del atributo.</param>
        public NameAttribute(string name) : base(name)
        {
        }
    }
}