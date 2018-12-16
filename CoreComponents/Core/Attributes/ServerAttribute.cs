using System;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc />
    /// <summary>
    ///     Attributo que define la ruta de un servidor.
    /// </summary>
    /// <remarks>
    ///     Es posible establecer este atributo más de una vez en un mismo elemento.
    /// </remarks>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Serializable]
    public sealed class ServerAttribute : Attribute
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ServerAttribute" /> estableciendo el servidor y el puerto
        ///     al cual este atributo hará referencia.
        /// </summary>
        /// <param name="server">Nombre del servidor / Dirección IP.</param>
        /// <param name="port">Número de puerto del servidor.</param>
        /// <remarks>
        ///     Si se define un número de puerto en <paramref name="server" />, el
        ///     valor del parámetro <paramref name="port" /> tomará precedencia.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///     Se produce si el servidor es una ruta malformada.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Se produce si <paramref name="port" /> es inferior a 1, o superior
        ///     a 65535.
        /// </exception>
        public ServerAttribute(string server, int port)
        {
            if (server.IsEmpty()) throw new ArgumentNullException(nameof(server));
            if (!port.IsBetween(1, 65535)) throw new ArgumentOutOfRangeException(nameof(port));
            Server = server;
            Port = port;
        }

        /// <summary>
        ///     Obtiene el servidor.
        /// </summary>
        /// <value>
        ///     La ruta del servidor a la cual este atributo apunta.
        /// </value>
        public string Server { get; }

        /// <summary>
        ///     Obtiene o establece el puerto de conexión del servidor.
        /// </summary>
        /// <value>
        ///     Un valor entre 1 y 65535 que establece el número de puerto a
        ///     apuntar.
        /// </value>
        public int Port { get; }

        /// <summary>
        ///     Devuelve una cadena que representa al objeto actual.
        /// </summary>
        /// <returns>Una cadena que representa al objeto actual.</returns>
        public override string ToString()
        {
            return $"{Server}:{Port}";
        }
    }
}