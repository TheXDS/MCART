using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TheXDS.MCART.Networking.Server;
using TheXDS.MCART.Networking.Server.Protocols;

namespace EchoServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var Srv = new Server(new Echo(), 51200);

            Srv.Start();

            while (Srv.IsAlive) { }
        }
    }
}
