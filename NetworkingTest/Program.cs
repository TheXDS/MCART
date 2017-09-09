using MCART.Networking.Server;
using MCART.Networking;
using System;

namespace NetworkingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Server srv = new Echo();
            srv.Logging += Srv_Logging;

            srv.Start();
            Console.ReadKey();

            srv.Logging -= Srv_Logging;
            srv.Stop();       
        }

        private static void Srv_Logging(object sender, MCART.Events.LoggingEventArgs e)
        {
            Console.WriteLine(e.Value);
        }
    }
    /// <summary>
    /// Este protocolo responde a un cliente con exactamente la misma
    /// información que éste envía.
    /// </summary>
    [Port(7)] class Echo : Protocol
    {
        /// <summary>
        /// Atiende al cliente.
        /// </summary>
        /// <param name="client">Cliente que ha enviado datos.</param>
        /// <param name="server">
        /// Servidor al cual el cliente se encuentra conectado.
        /// </param>
        /// <param name="data">Datos enviados por el cliente.</param>
        public override void ClientAttendant(Client client, Server<Client> server, byte[] data)
        {            
            client.Send(data);
        }
    }
}