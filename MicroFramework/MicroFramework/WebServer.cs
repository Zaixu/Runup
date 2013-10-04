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
        private Int32 endpointPort = 80;
        private Microsoft.SPOT.Net.NetworkInformation.NetworkInterface networkInterface;
        private Socket listenerSocket;
        private IPEndPoint listenerEndPoint;
        private IController controller;

        public WebServer(int port, IController control)
        {
            endpointPort = port;
            controller = control;

            // Display IP Address
            networkInterface = Microsoft.SPOT.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()[0];
            Debug.Print("IP Address: " + networkInterface.IPAddress.ToString());

            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenerEndPoint = new IPEndPoint(IPAddress.Any, endpointPort);

            listenerSocket.Bind(listenerEndPoint);
            listenerSocket.Listen(Int32.MaxValue);
        }

        public Socket WaitForConnection()
        { 
            Socket clientSocket = listenerSocket.Accept();
            Debug.Print(clientSocket.RemoteEndPoint.ToString());
            Thread.Sleep(500);
            return clientSocket;
        }

        public void HandleConnection(Socket clientSocket)
        {
            new WebServerClient(clientSocket, controller, true);
        }


    }
}
