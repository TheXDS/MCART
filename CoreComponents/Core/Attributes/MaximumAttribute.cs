using System;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc />
    /// <summary>
    /// Establece un valor máximo al cual se deben limitar los campos y propiedades.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MaximumAttribute : ObjectAttribute
    {
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia del atributo
        /// <see cref="T:TheXDS.MCART.Attributes.MinimumAttribute" /> estableciendo el valor máximo a
        /// representar.
        /// </summary>
        /// <param name="attributeValue">Valor del atributo.</param>
        public MaximumAttribute(object attributeValue) : base(attributeValue)
        {
        }
    }
}