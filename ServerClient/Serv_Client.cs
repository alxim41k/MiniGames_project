using System;
using Network;
using Network.Enums;
using Network.Converter;
namespace ServerClient
{
    public class Server
    {
        public void Main()
        {
            //1. Create a new server container.
            
            ServerConnectionContainer serverConnectionContainer = ConnectionFactory.CreateServerConnectionContainer("127.0.0.1", 1234, false);
            //2. Apply some settings
            serverConnectionContainer.AllowUDPConnections = true;
            //3. Set a delegate which will be called if we receive a connection
            serverConnectionContainer.ConnectionEstablished += ServerConnectionContainer_ConnectionEstablished;
            //4. Set a delegate which will be called if we lose a connection
            serverConnectionContainer.ConnectionLost += ServerConnectionContainer_ConnectionLost;
            //4. Start listening on port 1234
            serverConnectionContainer.StartTCPListener();
            
            Console.ReadLine();
        }

        private void ServerConnectionContainer_ConnectionLost(Connection connection, ConnectionType connectionType, CloseReason closeReason)
        {
            Console.WriteLine($"Connection {connection.IPRemoteEndPoint} {connectionType} lost. {closeReason}");
        }

        private void ServerConnectionContainer_ConnectionEstablished(Connection connection, ConnectionType connectionType)
        {
            Console.WriteLine($"{connectionType} Connection received {connection.IPRemoteEndPoint}.");
        }
    }
    public class Client
    {
        private ClientConnectionContainer clientConnectionContainer;
        public void Main()
        {
            
            //1. Create a new client connection container.
            clientConnectionContainer = ConnectionFactory.CreateClientConnectionContainer("127.0.0.1", 1234);
            //2. Setup events which will be fired if we receive a connection
            clientConnectionContainer.ConnectionEstablished += ClientConnectionContainer_ConnectionEstablished;
            clientConnectionContainer.ConnectionLost += ClientConnectionContainer_ConnectionLost;
        }
        
        private void ClientConnectionContainer_ConnectionLost(Connection connection, Network.Enums.ConnectionType connectionType, Network.Enums.CloseReason closeReason)
        {
            Console.WriteLine($"Connection {connection.IPRemoteEndPoint} {connectionType} lost. {closeReason}");
        }

        private void ClientConnectionContainer_ConnectionEstablished(Connection connection, Network.Enums.ConnectionType connectionType)
        {
            Console.WriteLine($"{connectionType} Connection received {connection.IPRemoteEndPoint}.");
        }
        private void Send(string Data)
        {
            
        }
    }
}
