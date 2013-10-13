using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.SPOT;
using System.Threading;


namespace MicroFramework
{
    /// <summary>
    /// Class to run a webserver through sockets.
    /// </summary>
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
        
        /// <summary>
        /// Constructer given port, controller and whether to run async and then starts webserver after subscribing to network info
        /// </summary>
        /// <param name="port">Port number to listen to</param>
        /// <param name="control">Controller to be called to handle requests and responses</param>
        /// <param name="asyncronious">Whether the webserver should run requests sync or async</param>
        public WebServer(int port, IController control, bool asyncronious)
        {
            //Set the given port, controller and whether its running async
            endpointPort = port;
            controller = control;
            async = asyncronious;

            //Subscribe for network availability changed event
            Microsoft.SPOT.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;

            // Initial, not being used, but then thread wont be null used throughout to check status.
            thread = new Thread(Run);

            //Start WebServer
            Start();
        }

        /// <summary>
        /// Starts the webserver
        /// </summary>
        public void Start()
        {
            //Stop server before starting
            Stop();
            //If the thread is alive, that its not been aborted.
            if (!thread.IsAlive)
            {
                Debug.Print("Starting WebServer");
                //Reset whole setup, has to since its been forced out of its accept hold, so socket is closed. Else thread would be stuck
                Setup();
                //Set control variable
                running = true;
                //Initiate new thread and start it
                thread = new Thread(Run);
                thread.Start();
            }
        }

        /// <summary>
        /// Stops the webserver thread
        /// </summary>
        public void Stop()
        {
            //If thread hasnt been aborted
            if (thread.IsAlive)
            {
                Debug.Print("Stopping WebServer");
                //Set volatile variable to while loop in thread stops
                running = false;
                // Release from socket accept hold.
                listenerSocket.Close();
                //Wait for it to stop
                while (thread.IsAlive)
                {
                    Thread.Sleep(500);
                }
            }
        }

        /// <summary>
        /// Run function for thread
        /// </summary>
        private void Run()
        {
            //Run for as long as variable aint going false
            while (running)
            {
                //Catch exception, when we force release its accept loop and connection issues and stop webserver
                try
                {
                    //Wait for socket request
                    Socket clientSocket = listenerSocket.Accept();
                    //Create new client to handle incomming request, given controller and clientsocket
                    new WebServerClient(clientSocket, controller, async);
                }
                catch
                {
                    //Stop thread
                    running = false;
                    // Decouple.
                    listenerSocket.Close();
                    Debug.Print("Stopping WebServer - Socket Error");
                }
           }
        }

        /// <summary>
        /// Handling network availability events from system
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Network Availability Event Arguments</param>
        void NetworkChange_NetworkAvailabilityChanged(object sender, Microsoft.SPOT.Net.NetworkInformation.NetworkAvailabilityEventArgs e)
        {
            //If its available, start webserver, if not, stop server
            if (e.IsAvailable)
            {
                // Start incase its stopped
                Start();
            }
            else
            {
                //Stop Server
                Stop();
            }
        }

        /// <summary>
        /// Sets up the webserver socket 
        /// </summary>
        private void Setup()
        {
            //Print IP
            networkInterface = Microsoft.SPOT.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()[0];
            Debug.Print(networkInterface.IPAddress);

            //Set Socket to be TCP
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Set socket to listen on port 80 from any ip
            listenerEndPoint = new IPEndPoint(IPAddress.Any, endpointPort);

            //Bind socket to port 80, any ip
            listenerSocket.Bind(listenerEndPoint);
            //Start listening
            listenerSocket.Listen(Int32.MaxValue);
        }
    }
}
