using System;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc />
    /// <summary>
    /// Establece un valor mínimo al cual se deben limitar los campos y propiedades.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MinimumAttribute : ObjectAttribute
    {
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia del atributo
        /// <see cref="T:TheXDS.MCART.Attributes.MinimumAttribute" /> estableciendo el valor mínimo a
        /// representar.
        /// </summary>
        /// <param name="attributeValue">Valor del atributo.</param>
        public MinimumAttribute(object attributeValue) : base(attributeValue)
        {
        }
    }
}