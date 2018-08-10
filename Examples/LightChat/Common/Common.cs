using System;
using System.Collections.Generic;
using System.Text;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Networking;

namespace TheXDS.LightChat
{
    /// <summary>
    /// Comandos para el protocolo <see cref="LightChatClient"/>.
    /// </summary>
    public enum Command : byte
    {
        /// <summary>
        /// Iniciar sesión.
        /// </summary>
        /// <remarks>
        /// Descripción de estructura de comando:
        /// Offset | Tamaño | Descripción
        /// -------+--------+------------
        /// 0x0001 | 1 byte | Longitud de campo de nombre de usuario.
        /// 0x0002 | 0x0001 | Bytes Unicode que representan  el nombre de
        ///        |        | usuario.
        /// 0x00nn |64 bytes| Hash de contraseña.
        /// </remarks>
        Login,
        /// <summary>
        /// Cerrar sesión.
        /// </summary>
        Logout,
        /// <summary>
        /// Devolver una lista de los usuarios conectados.
        /// </summary>
        List,
        /// <summary>
        /// Enviar un mensaje.
        /// </summary>
        Say,
        /// <summary>
        /// Enviar un mensaje a un usuario.
        /// </summary>
        SayTo
    }

    /// <summary>
    /// Valores de retorno del servidor.
    /// </summary>
    public enum RetVal : byte
    {
        /// <summary>
        /// Operación finalizada correctamente.
        /// </summary>
        Ok,
        /// <summary>
        /// Mensaje.
        /// </summary>
        Msg,
        /// <summary>
        /// Error en la operación.
        /// </summary>
        [ErrorResponse] Err,
        /// <summary>
        /// Control de cliente.
        /// </summary>
        CC,
        /// <summary>
        /// Comando desconocido.
        /// </summary>
        [UnknownResponse] Unknown,
        /// <summary>
        /// Inicio de sesión inválido
        /// </summary>
        InvalidLogin,
        /// <summary>
        /// Usuario expulsado.
        /// </summary>
        Banned,
        /// <summary>
        /// Información faltante o inválida.
        /// </summary>
        InvalidInfo,
        /// <summary>
        /// Comando inválido.
        /// </summary>
        InvalidCommand,
        /// <summary>
        /// No se ha iniciado sesión.
        /// </summary>
        NoLogin
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    internal sealed class CommandAttribute : Attribute, IValueAttribute<Command>
    {
        public Command Value { get; }

        public CommandAttribute(Command value)
        {
            Value = value;
        }
    }
    
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    internal sealed class ResponseAttribute : Attribute, IValueAttribute<RetVal>
    {
        public RetVal Value { get; }

        public ResponseAttribute(RetVal value)
        {
            Value = value;
        }
    }

}