using System;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc />
    /// <summary>
    ///     Establece el texto de licencia a asociar con el elemento.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Module | AttributeTargets.Assembly)]
    [Serializable]
    public sealed class LicenseTextAttribute : TextAttribute
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="LicenseTextAttribute" />.
        /// </summary>
        /// <param name="licenseText">Texto de la licencia.</param>
        public LicenseTextAttribute(string licenseText) : base(licenseText)
        {
        }
    }
}