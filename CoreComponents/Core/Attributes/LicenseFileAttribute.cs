using System;
using System.IO;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc />
    /// <summary>
    ///     Establece un archivo de licencia externo a asociar con el elemento.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Module | AttributeTargets.Assembly)]
    [Serializable]
    public sealed class LicenseFileAttribute : TextAttribute
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="LicenseFileAttribute" />.
        /// </summary>
        /// <param name="licenseFile">
        ///     Ruta del archivo de licencia adjunto.
        /// </param>
        public LicenseFileAttribute(string licenseFile) : base(licenseFile)
        {
        }

        /// <summary>
        ///     Lee el archivo de licencia especificado por este atributo.
        /// </summary>
        /// <returns>
        ///     El contenido del archivo de licencia especificado.
        /// </returns>
        public string ReadLicense()
        {
            using (var inp = new StreamReader(Value)) return inp.ReadToEnd();
        }
    }
}