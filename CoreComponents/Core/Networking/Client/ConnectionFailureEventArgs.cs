/*
ConnectionFailureEventArgs.cs

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
using System.Net;

namespace TheXDS.MCART.Networking.Client
{
    public class ConnectionFailureEventArgs : EventArgs
    {
        public ConnectionFailureEventArgs(Exception exception, string host, int port)
        {
            Exception = exception;
            Host = host;
            Port = port;

            if (IPAddress.TryParse(host, out var ip))
            {
                Address = new IPEndPoint(ip, port);
            }
        }

        public ConnectionFailureEventArgs(Exception exception, IPEndPoint address)
        {
            Exception = exception;
            Address = address;
            Host = address.Address.ToString();
            Port = address.Port;
        }

        public ConnectionFailureEventArgs(Exception exception, IPAddress host, int port)
        {
            Exception = exception;
            Host = host.ToString();
            Port = port;
            Address = new IPEndPoint(host, port);
        }

        public Exception Exception { get; }
        public string Host { get; }
        public int Port { get; }
        public IPEndPoint Address { get; }
    }
}