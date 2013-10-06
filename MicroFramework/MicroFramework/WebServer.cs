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

            thread = new Thread(Run);

            Start();
        }

        public void Start()
        {
            if (!thread.IsAlive)
            {
                Setup();
                running = true;
                thread.Start();
            }
        }

        public void Stop()
        {
            if (thread.IsAlive)
            {
                running = false;
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
                    // Ignore
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

            if(listenerSocket != null)
                listenerSocket.Close();
            
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenerEndPoint = new IPEndPoint(IPAddress.Any, endpointPort);

            listenerSocket.Bind(listenerEndPoint);
            listenerSocket.Listen(Int32.MaxValue);
        }
    }
}
