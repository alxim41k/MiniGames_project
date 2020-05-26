using System;
using Network;
using Network.Enums;
using Network.Converter;
using System.Text;
using Network.Extensions;

namespace ServerClient
{
    public class Server
    {
        private ServerConnectionContainer serverConnectionContainer;
        public void Main()
        {
            //1. Start listen on a port
            serverConnectionContainer = ConnectionFactory.CreateServerConnectionContainer(1234, false);

            //2. Apply optional settings.
            #region Optional settings
            serverConnectionContainer.ConnectionLost += (a, b, c) => Console.WriteLine($"{serverConnectionContainer.Count} {b.ToString()} Connection lost {a.IPRemoteEndPoint.Port}. Reason {c.ToString()}");
            serverConnectionContainer.ConnectionEstablished += connectionEstablished;
            serverConnectionContainer.AllowUDPConnections = true;
            serverConnectionContainer.UDPConnectionLimit = 2;
            #endregion Optional settings

            //Call start here, because we had to enable the bluetooth property at first.
            serverConnectionContainer.Start();
        }
        private void connectionEstablished(Connection connection, ConnectionType type)
        {
            Console.WriteLine($"{serverConnectionContainer.Count} {connection.GetType()} connected on port {connection.IPRemoteEndPoint.Port}");

            //3. Register packet listeners.
            connection.RegisterRawDataHandler("HelloWorld", (rawData, con) => Console.WriteLine($"RawDataPacket received. Data: {rawData.ToUTF8String()}"));
            connection.RegisterRawDataHandler("BoolValue", (rawData, con) => Console.WriteLine($"RawDataPacket received. Data: {rawData.ToBoolean()}"));
            connection.RegisterRawDataHandler("DoubleValue", (rawData, con) => Console.WriteLine($"RawDataPacket received. Data: {rawData.ToDouble()}"));
        }

    }
    public class Client
    {
        public void Main()
        {
            ConnectionResult connectionResult = ConnectionResult.TCPConnectionNotAlive;
            //1. Establish a connection to the server.
            TcpConnection tcpConnection = ConnectionFactory.CreateTcpConnection("127.0.0.1", 1234, out connectionResult);
            //2. Register what happens if we get a connection
            if (connectionResult == ConnectionResult.Connected)
            {
                Console.WriteLine($"{tcpConnection.ToString()} Connection established");
                //3. Send a raw data packet request.
                tcpConnection.SendRawData(RawDataConverter.FromUTF8String("HelloWorld", "Hello, this is the RawDataExample!"));
                tcpConnection.SendRawData(RawDataConverter.FromBoolean("BoolValue", true));
                tcpConnection.SendRawData(RawDataConverter.FromBoolean("BoolValue", false));
                tcpConnection.SendRawData(RawDataConverter.FromDouble("DoubleValue", 32.99311325d));
                //4. Send a raw data packet request without any helper class
                tcpConnection.SendRawData("HelloWorld", Encoding.UTF8.GetBytes("Hello, this is the RawDataExample!"));
                tcpConnection.SendRawData("BoolValue", BitConverter.GetBytes(true));
                tcpConnection.SendRawData("BoolValue", BitConverter.GetBytes(false));
                tcpConnection.SendRawData("DoubleValue", BitConverter.GetBytes(32.99311325d));
            }
        }

    }
}
