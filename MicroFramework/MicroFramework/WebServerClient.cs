using System;
using Microsoft.SPOT;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace MicroFramework
{
    /// <summary>
    /// Class to be run to handle every socket request
    /// </summary>
    class WebServerClient
    {
        private Socket clientSocket;
        private IController controller;

        /// <summary>
        /// Sets up the client request handling, which client to handle, what controller to respond to and whether it needs to be async or not
        /// </summary>
        /// <param name="cSocket">Client socket to handle</param>
        /// <param name="control">Controller object to respond to</param>
        /// <param name="async">Handle request async or sync</param>
        public WebServerClient(Socket cSocket, IController control, Boolean async)
        {
            //Setup class objects
            controller = control;
            clientSocket = cSocket;

            //Start new thread if handling is async else do it sync
            if (async)
                new Thread(ProcessRequest).Start();
            else
                ProcessRequest();
        }

        /// <summary>
        /// Handle requests
        /// </summary>
        private void ProcessRequest()
        {
            const Int32 microsecondsPerSecond = 1000000;
            //Wait 5 seconds for data having arrived
            bool dataReady = clientSocket.Poll(5 * microsecondsPerSecond, SelectMode.SelectRead);

            //If data is ready and theres data to read
            if (dataReady && clientSocket.Available > 0)
            {
                byte[] buffer = new byte[clientSocket.Available];
                //Receive request data
                int bytesRead = clientSocket.Receive(buffer);
                //Encode the received data
                string request = new string(System.Text.Encoding.UTF8.GetChars(buffer));
                //Send request data to controller and receive what to respond
                String fullResponse = controller.Handler(request);
                //Send the respond back to client
                clientSocket.Send(System.Text.Encoding.UTF8.GetBytes(fullResponse));
            }
            //Close client socket
            clientSocket.Close();
        }
    }
}
