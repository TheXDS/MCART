using System;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc cref="Attribute"/>
    /// <summary>
    ///     Clase base para los atributos basados en números enteros.
    /// </summary>
    public abstract class IntAttribute : Attribute, IValueAttribute<int>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Crea una nueva isntancia de la clase
        ///     <see cref="T:TheXDS.MCART.Attributes.IntAttribute" />.
        /// </summary>
        /// <param name="attributeValue">Valor de este atributo.</param>
        protected IntAttribute(int attributeValue)
        {
            Value = attributeValue;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene el valor asociado a este atributo.
        /// </summary>
        public int Value { get; }
    }
}