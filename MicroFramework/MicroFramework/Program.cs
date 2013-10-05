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
            // Turn on onboard LED, its ready to go
            OutputPort onboardLED = new OutputPort(Pins.ONBOARD_LED, false);
           
            // Let network get setup.
            Thread.Sleep(5000);

            // Run time service to get correct date
            NTP ntp = new NTP("pool.ntp.org", "time.windows.com");

            // Setup I2C for temperature measuring.
            LM75Driver tempDriver = new LM75Driver(72, 400);

            // Setup controller for webserver
            IController control = (IController) new Controller(tempDriver);

            // Setup Netduino to listen on port 80 with sockets.
            WebServer webServer = new WebServer(80, control);

            // Device ready
            onboardLED.Write(true);
            
            while (true)
            {
                webServer.HandleConnection(webServer.WaitForConnection());
            }
        }

    }
}
