using System;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc />
    /// <summary>
    /// Establece un formato de protocolo para abrir un vínculo por medio del 
    /// sistema operativo.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class ProtocolFormatAttribute : Attribute
    {
        /// <summary>
        /// Formato de llamada de protocolo.
        /// </summary>
        public string Format { get; }
        /// <inheritdoc />
        /// <summary>
        /// Establece un formato de protocolo para abrir un vínculo por medio del sistema operativo.
        /// </summary>
        /// <param name="format">Máscara a aplicar.</param>
        public ProtocolFormatAttribute(string format)
        {
            Format = format;
        }
        /// <summary>
        ///     Abre un url con este protocolo formateado.
        /// </summary>
        /// <param name="url">
        ///     URL del recurso a abrir por medio del protocolo definido por
        ///     este atributo.
        /// </param>
        public void Open(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return;
            try { System.Diagnostics.Process.Start(string.Format(Format, url)); }
            catch { /* Ignorar excepción */ }
        }
    }
}