using System;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc cref="Attribute"/>
    /// <summary>
    ///     Agrega un elemento de tipo a un elemento, además de ser la
    ///     clase base para los atributos que describan un valor representable como
    ///     <see cref="Type" /> para un elemento.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    [Serializable]
    public class TypeAttribute : Attribute, IValueAttribute<Type>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="TypeAttribute" />.
        /// </summary>
        /// <param name="type">Valor de este atributo.</param>
        protected TypeAttribute(Type type)
        {
            Value = type;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene el valor asociado a este atributo.
        /// </summary>
        /// <value>El valor de este atributo.</value>
        public Type Value { get; }
    }
}