using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using System.Threading;

namespace MicroFramework
{
    class WebServer
    {
        private int endpointPort = 80;
        private Microsoft.SPOT.Net.NetworkInformation.NetworkInterface networkInterface;
        private Socket listenerSocket;
        private IPEndPoint listenerEndPoint;

        public WebServer(int port)
        {
            endpointPort = port;
            // Display IP Address
            networkInterface = Microsoft.SPOT.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()[0];
            Debug.Print("IP Address: " + networkInterface.IPAddress.ToString());
            //networkInterface.EnableDhcp();

            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenerEndPoint = new IPEndPoint(IPAddress.Any, endpointPort);

            listenerSocket.Bind(listenerEndPoint);
            listenerSocket.Listen(Int32.MaxValue);
        }

        public Socket WaitForConnection()
        {
            Socket clientSocket = listenerSocket.Accept();
            return clientSocket;
        }

        public void HandleConnection(Socket clientSocket, string response)
        {
            new WebServerClient(clientSocket, response, true);
        }


    }
}
