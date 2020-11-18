/*
NameAttribute.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using TheXDS.MCART.Networking.Mrpc;
using Xunit;

#nullable enable

namespace TheXDS.MCART.MrpcTests
{
    public class UnitTest1
    {
        #region Compartidos

        private interface IRemoteContract
        {
            string SayHello(string name);

            DateTime Now();

            decimal Conversion(ConvRequest data);

            double Sum(double a, double b);

            void Foo();

            void Bar(int foe);
        }

        private struct ConvRequest
        {
            public string SourceCurrency { get; set; }
            public string TargetCurrency { get; set; }
            public decimal Input { get; set; }
        }

        #endregion

        #region Servidor

        private class RemoteClass : IRemoteContract
        {
            private readonly Dictionary<string, decimal> _conversions = new Dictionary<string, decimal>();

            public void Bar(int foe)
            {
            }

            public decimal Conversion(ConvRequest data)
            {
                return data.Input * _conversions[data.TargetCurrency] / _conversions[data.SourceCurrency];
            }

            public void Foo() { }

            public DateTime Now() => DateTime.Now;

            public string SayHello(string name) => $"Hello, {name}.";

            public double Sum(double a, double b) => a + b;
        }

        #endregion

        #region Cliente (compilado dinámicamente)

        private class RemoteClient : MrpcCaller, IRemoteContract
        {
            public void Bar(int foe)
            {
                RemoteCall<object?>(foe);
            }

            public decimal Conversion(ConvRequest data)
            {
                return RemoteCall<decimal>(data);
            }

            public void Foo()
            {
                RemoteCall<object?>();
            }

            public DateTime Now()
            {
                return RemoteCall<DateTime>();
            }

            public string SayHello(string name)
            {
                return RemoteCall<string>(name);
            }

            public double Sum(double a, double b)
            {
                return RemoteCall<double>(a,b);
            }
        }

        #endregion

        [Fact]
        public void ServerInstanceTest()
        {
            var svc = new MrpcService<RemoteClass>(new IPEndPoint(IPAddress.Loopback,51200), new RemoteClass());
            
        }

        [Fact]
        public void ClientInstanceTest()
        {
            var cli = new MrpcClient<IRemoteContract>(new IPEndPoint(IPAddress.Loopback, 51200));
            Assert.Equal("Hello, John.", cli.Call.SayHello("John"));
        }
    }
}
