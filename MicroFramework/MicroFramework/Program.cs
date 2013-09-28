using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace MicroFramework
{
    public class Program
    {
        
        public static void Main()
        {
            // Let network get setup.
            Thread.Sleep(5000);

            // Setup Netduino to listen on port 80 with sockets.
            WebServer webServer = new WebServer(80);
            // Setup I2C for temperature measuring.
            TemperatureI2CDriver tempDriver = new TemperatureI2CDriver(72, 100);

            string response;

            while (true)
            {
                if (webServer.ListenForRequest())
                {
                    response = webServer.ReceiveData();
                    Debug.Print(response);
                }
                Debug.Print("Check1");
                //webServer.SendData(tempDriver.GetData());
                webServer.SendData("HTTP/1.1 200 OK\r\n" + "Content-Type: text/html; charset=utf-8\r\n\r\n" + "<html><head><title>Netduino Plus LED Sample</title></head>" + "<body>" + "Mooo" + "</body></html>");
                webServer.EndRequest();
            }
        }

    }
}
