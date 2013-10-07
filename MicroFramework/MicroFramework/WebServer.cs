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
        private bool async = true;
        private Thread thread = null;
        

        public WebServer(int port, IController control, bool asyncronious)
        {
            endpointPort = port;
            controller = control;
            async = asyncronious;

            Microsoft.SPOT.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;

            // Initial, not being used, but then thread wont be null used throughout to check status.
            thread = new Thread(Run);

            Start();
        }

        public void Start()
        {
            Stop();
            if (!thread.IsAlive)
            {
                Debug.Print("Starting WebServer");
                Setup();
                running = true;
                thread = new Thread(Run);
                thread.Start();
            }
        }

        public void Stop()
        {
            if (thread.IsAlive)
            {
                Debug.Print("Stopping WebServer");
                running = false;
                // Release from hold.
                listenerSocket.Close();

                while (thread.IsAlive)
                {
                    Thread.Sleep(500);
                }
            }
        }

        private void Run()
        {
            while (running)
            {
                try
                {
                    Socket clientSocket = listenerSocket.Accept();
                    new WebServerClient(clientSocket, controller, async);
                }
                catch
                {
                    running = false;
                    // Decouple.
                    listenerSocket.Close();
                    Debug.Print("Stopping WebServer - Socket Error");
                }
           }
        }

        void NetworkChange_NetworkAvailabilityChanged(object sender, Microsoft.SPOT.Net.NetworkInformation.NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable)
            {
                // Start incase its stopped
                Start();
            }
            else
            {
                Stop();
            }
        }

        private void Setup()
        {
            networkInterface = Microsoft.SPOT.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()[0];
            Debug.Print(networkInterface.IPAddress);

            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenerEndPoint = new IPEndPoint(IPAddress.Any, endpointPort);

            listenerSocket.Bind(listenerEndPoint);
            listenerSocket.Listen(Int32.MaxValue);
        }
    }
}
