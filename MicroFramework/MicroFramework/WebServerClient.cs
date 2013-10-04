using System;
using Microsoft.SPOT;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace MicroFramework
{
    class WebServerClient
    {
        private Socket clientSocket;
        private IController controller;

        public WebServerClient(Socket cSocket, IController control, Boolean async)
        {
            controller = control;
            clientSocket = cSocket;

            if (async)
                new Thread(ProcessRequest).Start();
            else
                ProcessRequest();
        }

        private void ProcessRequest()
        {
            const Int32 microsecondsPerSecond = 1000000;
            bool dataReady = clientSocket.Poll(5 * microsecondsPerSecond, SelectMode.SelectRead);

            if (dataReady && clientSocket.Available > 0)
            {
                byte[] buffer = new byte[clientSocket.Available];
                int bytesRead = clientSocket.Receive(buffer);

                string request = new string(System.Text.Encoding.UTF8.GetChars(buffer));
                String fullResponse = controller.Handler(request);
                //String fullResponse = "HTTP/1.1 200 OK\r\nContent-Type: text/html; charset=utf-8\r\n\r\n<html><head><title>WebServer</title></head><body>" + response + "</body></html>";

                clientSocket.Send(System.Text.Encoding.UTF8.GetBytes(fullResponse));
            }

            clientSocket.Close();
        }
    }
}
