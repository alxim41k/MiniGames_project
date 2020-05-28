using System;
using Network;
using Network.Enums;
using Network.Converter;
using System.Text;
using Network.Extensions;

namespace Classes
{
    public class Server
    {
        public ServerConnectionContainer serverConnectionContainer;
        public Server(int port, string ip)
        {
            serverConnectionContainer = ConnectionFactory.CreateServerConnectionContainer(ip, port, false);
            serverConnectionContainer.ConnectionEstablished += connectionEstablished;
        }
        private void connectionEstablished(Connection connection, ConnectionType type)
        {
            
        }

    }
    public class Client
    {
        public ClientConnectionContainer clientConnectionContainer;
        public Client(string ip, int port)
        {
            clientConnectionContainer = ConnectionFactory.CreateClientConnectionContainer(ip, port);
            clientConnectionContainer.ConnectionEstablished += connectionEstablished;
        }
        private void connectionEstablished(Connection connection, ConnectionType type)
        {

        }

    }
}
