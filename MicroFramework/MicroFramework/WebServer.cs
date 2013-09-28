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
        private Socket clientSocket;

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
            listenerSocket.Listen(1);
        }

        public bool ListenForRequest()
        {
            // Wait for client to connect.
            clientSocket = listenerSocket.Accept();
            // Wait for data to arrive
            bool dataReady = clientSocket.Poll(5000000, SelectMode.SelectRead);

            return dataReady;
        }

        public string ReceiveData()
        {
            if (clientSocket.Available > 0)
            {
                byte[] buffer = new byte[clientSocket.Available];
                int bytesRead = clientSocket.Receive(buffer);

                string request = new string(System.Text.Encoding.UTF8.GetChars(buffer));

                return request;
            }

            return null;
        }

        public void SendData(string response)
        {
            clientSocket.Send(System.Text.Encoding.UTF8.GetBytes(response));
        }

        public void EndRequest()
        {
            clientSocket.Close();
        }

    }
}
