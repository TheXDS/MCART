using System;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc cref="Attribute"/>
    /// <summary>
    ///     Agrega un elemento textual genérico a un elemento, además de ser la
    ///     clase base para los atributos que describan un valor representable como
    ///     <see cref="String" /> para un elemento.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    [Serializable]
    public class TextAttribute : Attribute, IValueAttribute<string>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="TextAttribute" />.
        /// </summary>
        /// <param name="text">Valor de este atributo.</param>
        protected TextAttribute(string text)
        {
            Value = text;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene el valor asociado a este atributo.
        /// </summary>
        /// <value>El valor de este atributo.</value>
        public string Value { get; }
    }
}