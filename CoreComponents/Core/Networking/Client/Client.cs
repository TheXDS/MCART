﻿/*
Client.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace TheXDS.MCART.Networking.Client
{
    /// <summary>
    /// Permite enviar y recibir información desde y hacia un servidor. Además,
    /// es una clase base para los controladores de protocolo del cliente.
    /// </summary>
    public class Client
    {
        private string _host;
        private int _port;
        private IPEndPoint _endPoint;

        /// <summary>
        /// Conexión al servidor
        /// </summary>
        protected readonly TcpClient connection = new TcpClient();
        /// <summary>
        /// Puerto predeterminado para las conexiones entrantes.
        /// </summary>
        public const int DefaultPort = 51220;
        /// <summary>
        /// Gets a value indicating whether this <see cref="Client"/> is alive.
        /// </summary>
        /// <value><see langword="true"/> if is alive; otherwise, <see langword="false"/>.</value>
        public bool IsAlive => (bool)connection?.Connected;
        /// <summary>
        /// Establece una conexión con el servidor de forma asíncrona.
        /// </summary>
        /// <returns>Un <see cref="Task"/> que representa la tarea.</returns>
        /// <param name="server">Servidor al cual conectarse.</param>
        public async Task ConnectAsync(string server) => await ConnectAsync(server, DefaultPort);
        /// <summary>
        /// Establece una conexión con el servidor de forma asíncrona.
        /// </summary>
        /// <returns>Un <see cref="Task"/> que representa la tarea.</returns>
        /// <param name="server">Servidor al cual conectarse.</param>
        /// <param name="port">
        /// Opcional. Puerto del servidor. Si se omite, se conectará al puerto
        /// predeterminado.
        /// </param>
        public async Task ConnectAsync(string server, int port)
        {
            if (!port.IsBetween(1, 65535)) throw new ArgumentOutOfRangeException(nameof(port));
            try
            {
                await connection.ConnectAsync(server, port);
                AtConnect();
            }
#if PreferExceptions
            catch { throw; }
#else
            catch (Exception ex) { ConnChk(); AtFail(ex); }
#endif
        }
        /// <summary>
        /// Establece una conexión con el servidor de forma asíncrona.
        /// </summary>
        /// <returns>Un <see cref="Task"/> que representa la tarea.</returns>
        /// <param name="server">Servidor al cual conectarse.</param>
        public async Task ConnectAsync(IPEndPoint server)
        {
            try
            {
                await connection.ConnectAsync(server.Address, server.Port);
                AtConnect();
            }
#if PreferExceptions
            catch { throw; }
#else
            catch (Exception ex) { ConnChk(); AtFail(ex); }
#endif
        }
        /// <summary>
        /// Establece una conexión con el servidor de forma asíncrona.
        /// </summary>
        /// <returns>Un <see cref="Task"/> que representa la tarea.</returns>
        /// <param name="server">Servidor al cual conectarse.</param>
        public async Task ConnectAsync(IPAddress server) => await ConnectAsync(server, DefaultPort);
        /// <summary>
        /// Establece una conexión con el servidor de forma asíncrona.
        /// </summary>
        /// <returns>Un <see cref="Task"/> que representa la tarea.</returns>
        /// <param name="server">Servidor al cual conectarse.</param>
        /// <param name="port">
        /// Opcional. Puerto del servidor. Si se omite, se conectará al puerto
        /// predeterminado.
        /// </param>
        public async Task ConnectAsync(IPAddress server, int port)
        {
            if (!port.IsBetween(1, 65535)) throw new ArgumentOutOfRangeException(nameof(port));
            try
            {
                await connection.ConnectAsync(server, port);
                AtConnect();
            }
#if PreferExceptions
            catch { throw; }
#else
            catch (Exception ex) { ConnChk(); AtFail(ex); }
#endif
        }
        /// <summary>
        /// Establece una conexión con el servidor.
        /// </summary>
        /// <param name="server">Servidor al cual conectarse.</param>
        public void Connect(string server) => Connect(server, DefaultPort);
        /// <summary>
        /// Establece una conexión con el servidor.
        /// </summary>
        /// <param name="server">Servidor al cual conectarse.</param>
        /// <param name="port">
        /// Opcional. Puerto del servidor. Si se omite, se conectará al puerto
        /// predeterminado.
        /// </param>
        public void Connect(string server, int port)
        {
            if (!port.IsBetween(1, 65535)) throw new ArgumentOutOfRangeException(nameof(port));
            try
            {
                connection.Connect(server, port);
                AtConnect();
            }
#if PreferExceptions
            catch { throw; }
#else
            catch (Exception ex) { ConnChk(); AtFail(ex); }
#endif
        }
        /// <summary>
        /// Establece una conexión con el servidor.
        /// </summary>
        /// <param name="server">Servidor al cual conectarse.</param>
        public void Connect(IPEndPoint server)
        {
            try
            {
                connection.Connect(server);
                AtConnect();
            }
#if PreferExceptions
            catch { throw; }
#else
            catch (Exception ex) { ConnChk(); AtFail(ex); }
#endif
        }
        /// <summary>
        /// Establece una conexión con el servidor.
        /// </summary>
        /// <param name="server">Servidor al cual conectarse.</param>
        public void Connect(IPAddress server) => Connect(server, DefaultPort);
        /// <summary>
        /// Establece una conexión con el servidor.
        /// </summary>
        /// <param name="server">Servidor al cual conectarse.</param>
        /// <param name="port">
        /// Opcional. Puerto del servidor. Si se omite, se conectará al puerto
        /// predeterminado.
        /// </param>
        public void Connect(IPAddress server, int port)
        {
            if (!port.IsBetween(1, 65535)) throw new ArgumentOutOfRangeException(nameof(port));
            try
            {
                connection.Connect(server, port);
                AtConnect();
            }
#if PreferExceptions
            catch { throw; }
#else
            catch (Exception ex) { ConnChk(); AtFail(ex); }
#endif
        }

        /// <summary>
        /// Desconecta correctamente a este cliente del servidor.
        /// </summary>
        public void Disconnect()
        {
            try { AtDisconnect(); }
#if PreferExceptions
            catch { throw; }
#else
            catch (Exception ex) { AtFail(ex); }
#endif
            finally { ConnChk(); }
        }


        /// <summary>
        /// Envía un mensaje, y espera a que el servidor responda.
        /// </summary>
        /// <returns>Un mensaje enviado por el servidor.</returns>
        /// <param name="data">Mensaje a enviar al servidor.</param>
        public byte[] TalkToServer(byte[] data)
        {
            if (!(data?.Length > 0))
#if PreferExceptions
                throw new ArgumentNullException();
#else
                return new byte[] { };
#endif
            var ns = connection?.GetStream();
            
            if (ns is null)
#if PreferExceptions
                throw new ArgumentNullException();
#else
                return new byte[] { };
#endif                
            ns.Write(data, 0, data.Length);

            var outp = new List<byte>();
            do
            {
                var buff = new byte[connection.ReceiveBufferSize];
                var sze = ns.Read(buff, 0, buff.Length);
                if (sze < connection.ReceiveBufferSize) Array.Resize(ref buff, sze);
                outp.AddRange(buff);
            } while (ns.DataAvailable);

            return outp.ToArray();
        }

        /// <summary>
        /// Envía un mensaje, y espera a que el servidor responda de forma asíncrona.
        /// </summary>
        /// <returns>Un mensaje enviado por el servidor.</returns>
        /// <param name="data">Mensaje a enviar al servidor.</param>
        public async Task<byte[]> TalkToServerAsync(byte[] data)
        {
            if (!(data?.Length > 0))
#if PreferExceptions
                throw new ArgumentNullException();
#else
                return new byte[] { };
#endif
            NetworkStream ns = connection?.GetStream();
            if (ns is null)
#if PreferExceptions
                throw new ArgumentNullException();
#else
                return new byte[] { };
#endif
            using (var ms = new MemoryStream(data))
                await (ms.CopyToAsync(ns));
            using (var ms = new MemoryStream())
            {
                await (ns.CopyToAsync(ms));
                return ms.ToArray();
            }
        }
        /// <summary>
        /// Método invalidable que indica una serie de acciones a realizar al
        /// conectarse exitosamente con el servidor.
        /// </summary>
        /// <remarks>
        /// De forma predeterminada, no se realiza ninguna acción.
        /// </remarks>
        public virtual void AtConnect() { }
        /// <summary>
        /// Método invalidable que indica una serie de acciones a realizar al
        /// desconectarse del servidor.
        /// </summary>
        /// <remarks>
        /// De forma predeterminada, no se realiza ninguna acción.
        /// </remarks>
        public virtual void AtDisconnect() { }
        /// <summary>
        /// Indica una serie de acciones a realizar al no poder establecerse
        /// una conexión con el servidor.
        /// </summary>
        public void AtFail() => AtFail(null);
        /// <summary>
        /// Método invalidable que indica una serie de acciones a realizar al no
        /// poder establecerse una conexión con el servidor.
        /// </summary>
        /// <param name="ex">
        /// Excepción producida en la falla.
        /// </param>
        /// <remarks>
        /// De forma predeterminada, no se realiza ninguna acción.
        /// </remarks>
        public virtual void AtFail(Exception ex) { }

        /// <summary>
        /// Se asegura de cerrar la conexión.
        /// </summary>
        private void ConnChk()
        {
            if (!(connection?.Connected ?? false)) return;
            try
            {
                connection?.GetStream().Dispose();
                connection.Close();
            }
            catch { /* suprimir cualquier excepción */ }
        }
        /// <summary>
        /// Realiza alguans tareas de limpieza antes de finalizar esta
        /// instancia de la clase <see cref="Client"/>.
        /// </summary>
        ~Client() { ConnChk(); }
    }
}