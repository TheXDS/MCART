//
//  Enums.cs
// 
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2017 César Andrés Morgan
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace LightChat
{
    /// <summary>
    /// Protocolo simple de chat.
    /// </summary>
    public partial class LightChat
    {
        /// <summary>
        /// Comandos para el protocolo <see cref="LightChat"/>.
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
            Err,
            /// <summary>
            /// Control de cliente.
            /// </summary>
            CC
        }

        /// <summary>
        /// Valores de código de error.
        /// </summary>
        public enum ErrCodes : byte
        {
            /// <summary>
            /// No hay error, o error desconocido.
            /// </summary>
            Unknown,
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
    }
}