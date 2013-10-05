using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.SPOT;
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

            Microsoft.SPOT.Net.NetworkInformation.NetworkChange.NetworkAddressChanged += NetworkChange_NetworkAddressChanged;
            Microsoft.SPOT.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;

            Setup();
            
        }

        void NetworkChange_NetworkAvailabilityChanged(object sender, Microsoft.SPOT.Net.NetworkInformation.NetworkAvailabilityEventArgs e)
        {
            Debug.Print("Available");
        }

        void NetworkChange_NetworkAddressChanged(object sender, EventArgs e)
        {
            Debug.Print("Address Change");
        }

        public void Setup()
        {
            networkInterface = Microsoft.SPOT.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()[0];
            Debug.Print("IP Address: " + networkInterface.IPAddress.ToString());
            
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenerEndPoint = new IPEndPoint(IPAddress.Any, endpointPort);

            listenerSocket.Bind(listenerEndPoint);
            listenerSocket.Listen(Int32.MaxValue);
        }

        public Socket WaitForConnection()
        {
            Socket clientSocket;
            try
            {
                clientSocket = listenerSocket.Accept();
            }
            catch(Exception e)
            {
                clientSocket = null;
            }
            return clientSocket;
        }

        public void HandleConnection(Socket clientSocket)
        {
            if(clientSocket != null)
                new WebServerClient(clientSocket, controller, true);
        }


    }
}
