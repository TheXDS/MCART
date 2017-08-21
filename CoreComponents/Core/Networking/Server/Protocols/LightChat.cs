//
//  LightChat.cs
// 
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

#if IncludeExampleImplementations

using System.Collections.Generic;
using System.IO;
using MCART.Attributes;
using static System.Text.Encoding;

namespace MCART.Networking.Server.Protocols
{
    /// <summary>
    /// Protocolo simple de chat.
    /// </summary>
    [Beta]
    [Unsecure]
    public class LightChat : Protocol<Client<string>>
    {
        /// <summary>
        /// Describe a un usuario registrado del protocolo 
        /// <see cref="LightChat"/>.
        /// </summary>
        public class UserRegistry
        {
            byte[] pwd;
            /// <summary>
            /// Establece el hash de contraseña del usuario.
            /// </summary>
            public byte[] Password { set => pwd = value; }
            /// <summary>
            /// Indica si este usuario ha sido baneado.
            /// </summary>
            public bool Banned;
            /// <summary>
            /// Comprueba la contraseña.
            /// </summary>
            /// <param name="pw">contraseña a comprobar.</param>
            /// <returns>
            /// <c>true</c> si la contraseña coincide, <c>false</c> en caso
            /// contrario.
            /// </returns>
            public bool CheckPw(byte[] pw) => pwd == pw;
        }

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

        /// <summary>
        /// Lista de usuarios registrados para este protocolo
        /// </summary>
        public Dictionary<string, UserRegistry> Users = new Dictionary<string, UserRegistry>();

        /// <summary>
        /// Atiende al cliente
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        public override void ClientAttendant(Client<string> client, Server<Client<string>> server, byte[] data)
        {
            using (var br = new BinaryReader(new MemoryStream(data)))
            {
                switch ((Command)br.ReadByte())
                {
                    case Command.Login:
                        if (!client.userObj.IsEmpty()) client.Send(NewErr(ErrCodes.InvalidCommand));
                        else
                        {
                            string usr = br.ReadString();
                            if (Users.ContainsKey(usr) && Users[usr].CheckPw(br.ReadBytes(64)))
                            {
                                if (!Users[usr].Banned)
                                {
                                    client.userObj = usr;
                                    server.Broadcast(NewMsg($"{client.userObj} ha iniciado sesión."), client);
                                    client.Send(OkMsg());
                                    client.Send(NewMsg("Has iniciado sesión."));
                                }
                                else client.Send(NewErr(ErrCodes.Banned));
                            }
                            else client.Send(NewErr(ErrCodes.InvalidLogin));
                        }
                        break;
                    case Command.Logout:
                        if (client.userObj.IsEmpty()) client.Send(NewErr(ErrCodes.NoLogin));
                        else
                        {
                            server.Broadcast(NewMsg($"{client.userObj} ha cerrado sesión."), client);
                            client.Send(OkMsg());
                            client.Send(NewMsg("Has cerrado sesión."));
                            client.userObj = null;
                            client.Disconnect();
                        }
                        break;
                    case Command.List:
                        using (var os = new MemoryStream())
                        {
                            using (var bw = new BinaryWriter(os))
                            {
                                bw.Write((byte)RetVal.Ok);
                                bw.Write(server.clients.Count);
                                foreach (var j in server.clients)
                                    if (!j.userObj.IsEmpty() && j.IsNot(client))
                                        bw.Write(j.userObj);
                                client.Send(os.ToArray());
                            }
                        }
                        break;
                    case Command.Say:
                        if (client.userObj.IsEmpty()) client.Send(NewErr(ErrCodes.NoLogin));
                        else
                        {
                            string msg = br.ReadString();
                            server.Broadcast(NewMsg($"{client.userObj} dice al grupo: {msg}"), client);
                            client.Send(OkMsg());
                            client.Send(NewMsg($"Dijiste: {msg}"));
                        }
                        break;
                    case Command.SayTo:
                        if (client.userObj.IsEmpty()) client.Send(NewErr(ErrCodes.NoLogin));
                        else
                        {
                            string dest = br.ReadString();
                            string msg = br.ReadString();

                            foreach (var j in server.clients)
                            {
                                if (j.userObj == dest)
                                {
                                    j.Send(NewMsg($"{client.userObj} te dice: {msg}"));
                                    client.Send(OkMsg());
                                    client.Send(NewMsg($"Dijiste a {dest}: {msg}"));
                                    break;
                                }
                                else client.Send(NewErr(ErrCodes.InvalidInfo));
                            }
                        }
                        break;
                    default:
                        // Comando desconocido. Devolver error.
                        client.Send(NewErr(ErrCodes.InvalidCommand));
                        break;
                }
            }
        }

        /// <summary>
        /// Crea un nuevo mensaje de error.
        /// </summary>
        /// <param name="errNum">Número de error.</param>
        /// <param name="msg">Opcional. Mensaje.</param>
        /// <returns>
        /// Un mensaje compuesto por un arreglo de <see cref="byte"/>.
        /// </returns>
        byte[] NewErr(ErrCodes errNum, string msg = null)
        {
            List<byte> outp = new List<byte>
                {
                    (byte)RetVal.Err,
                    (byte)errNum
                };
            if (!msg.IsEmpty()) outp.AddRange(Unicode.GetBytes(msg));
            return outp.ToArray();
        }

        /// <summary>
        /// Crea un nuevo mensaje indicando que el servidor ha atendido la
        /// solicitud exitosamente, devolviendo datos.
        /// </summary>
        /// <returns>
        /// Un mensaje compuesto por un arreglo de <see cref="byte"/>.
        /// </returns>
        byte[] OkMsg(byte[] data)
        {
            using (var os = new MemoryStream())
            {
                using (var bw = new BinaryWriter(os))
                {
                    bw.Write((byte)RetVal.Ok);
                    bw.Write(data);
                    return os.ToArray();
                }
            }
        }

        /// <summary>
        /// Crea un nuevo mensaje indicando que el servidor ha atendido la
        /// solicitud exitosamente.
        /// </summary>
        /// <returns>
        /// Un mensaje compuesto por un arreglo de <see cref="byte"/>.
        /// </returns>
        byte[] OkMsg() => new byte[] { (byte)RetVal.Ok };

        /// <summary>
        /// Crea un nuevo mensaje para el cliente.
        /// </summary>
        /// <param name="msg">Texto del mensaje.</param>
        /// <returns>
        /// Un mensaje compuesto por un arreglo de <see cref="byte"/>.
        /// </returns>
        byte[] NewMsg(string msg)
        {
            using (var os = new MemoryStream())
            {
                using (var bw = new BinaryWriter(os))
                {
                    bw.Write((byte)RetVal.Msg);
                    bw.Write(msg);
                    return os.ToArray();
                }
            }
        }

        /// <summary>
        /// Realiza funciones de bienvenida al cliente que se acaba de
        /// conectar a este servidor.
        /// </summary>
        /// <param name="client">Cliente que acaba de conectarse.</param>
        /// <param name="server">
        /// Instancia del servidor al cual el cliente se ha conectado.
        /// </param>
        /// <returns>
        /// <c>true</c> para indicar que el cliente ha sido aceptado por el
        /// protocolo, <c>false</c> para indicar lo contrario.
        /// </returns>
        public override bool ClientWelcome(Client<string> client, Server<Client<string>> server)
        {
            // Bonito lugar para verificar si la IP no se encuentra baneada, o
            // para obtener la clave pública RSA del cliente y enviar la del 
            // servidor en caso de utilizar un canal cifrado para las 
            // comunicaciones.

            // Actualmente, la implementación de LightChat procura ser simple,
            // aunque insegura. Se acepta al nuevo cliente sin protesto.
            // TODO: Implementar cifrado RSA para las comunicaciones.
            return true;
        }
    }
}
#endif