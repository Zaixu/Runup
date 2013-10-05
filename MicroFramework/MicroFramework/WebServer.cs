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
        private Microsoft.SPOT.Net.NetworkInformation.NetworkInterface networkInterface = null;
        private Socket listenerSocket = null;
        private IPEndPoint listenerEndPoint = null;
        private IController controller = null;
        // Everything is treated as volatile in micro framework
        private bool running = false;
        Thread thread = null;

        public WebServer(int port, IController control)
        {
            endpointPort = port;
            controller = control;

            Microsoft.SPOT.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;

            thread = new Thread(new ThreadStart(Run));

            Setup();
            
        }

        public void Start()
        {
            if (!thread.IsAlive)
            {
                running = true;
                thread.Start();
            }
        }

        public void Stop()
        {
            if(thread.IsAlive)
            {
                running = false;
                thread.Join();
            }
        }

        public void Run()
        {
            while (running)
            {
                try
                {
                    Socket clientSocket = listenerSocket.Accept();
                    new WebServerClient(clientSocket, controller, true);
                }
                catch
                {
                    running = false;
                }
            }
        }

        void NetworkChange_NetworkAvailabilityChanged(object sender, Microsoft.SPOT.Net.NetworkInformation.NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable)
            {
                // Must have lost connection, reset everything incase of startup without ethernet
                Setup();
                networkInterface = Microsoft.SPOT.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()[0];
                Debug.Print(networkInterface.IPAddress);
            }
            else
            {
                Stop();
                networkInterface = Microsoft.SPOT.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()[0];
                Debug.Print(networkInterface.IPAddress);
            }
        }

        public void Setup()
        {
            Stop();

            networkInterface = Microsoft.SPOT.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()[0];
            Debug.Print(networkInterface.IPAddress);
            
            if(listenerSocket != null)
                listenerSocket.Close();
            
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenerEndPoint = new IPEndPoint(IPAddress.Any, endpointPort);

            listenerSocket.Bind(listenerEndPoint);
            listenerSocket.Listen(Int32.MaxValue);

            Start();
        }
    }
}
