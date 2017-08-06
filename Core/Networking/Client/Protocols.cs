//
//  Protocols.cs
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

#region Opciones de compilación

//Preferir excepciones en lugar de continuar con código alternativo
//#define PreferExceptions

#endregion

#if IncludeExampleImplementations

using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MCART.Networking.Client.Protocols
{

    /// <summary>
    /// Implementación predeterminada de la clase <see cref="Client"/> que envía
    /// una solicitud de eco.
    /// </summary>
    /// <remarks>Este protocolo utiliza TCP/IP, no IGMP.</remarks>
    public class EchoClient : Client
    {
        /// <summary>
        /// Ejecuta una prueba de eco con los bytes de datos especificados.
        /// </summary>
        /// <returns>
        /// La respuesta del servidor. Debe ser un arreglo de
        /// <see cref="byte"/> compuesto de ceros.
        /// </returns>
        /// <param name="size">
        /// Tamaño del paquete de eco. Si se omite, se utiliza un tamaño de 32
        /// bytes.
        /// </param>
        /// <remarks>Este protocolo utiliza TCP/IP, no IGMP.</remarks>
        public byte[] TestEcho(ushort size = 32)
        {
            byte[] test = new byte[size];
            return TalkToServer(test);
        }
    }
}
#endif