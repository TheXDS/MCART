using System;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc cref="Attribute"/>
    /// <summary>
    ///     Especifica la versión de un elemento, además de ser la clase base para
    ///     los atributos que describan un valor <see cref="Version" /> para un
    ///     elemento.
    /// </summary>
    [Serializable]
    public abstract class VersionAttributeBase : Attribute, IValueAttribute<Version>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="VersionAttributeBase" />.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        /// <param name="build">Número de compilación.</param>
        /// <param name="rev">Número de revisión.</param>
        protected VersionAttributeBase(int major, int minor, int build, int rev)
        {
            Value = new Version(major, minor, build, rev);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene el valor asociado a este atributo.
        /// </summary>
        /// <value>El valor de este atributo.</value>
        public Version Value { get; }
    }
}