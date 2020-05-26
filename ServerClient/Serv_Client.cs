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
        private ServerConnectionContainer serverConnectionContainer;
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
        public void Main()
        {
        }

    }
}
