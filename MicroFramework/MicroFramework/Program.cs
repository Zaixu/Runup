﻿using System;
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

            while (true)
            {
                webServer.HandleConnection(webServer.WaitForConnection(), tempDriver.GetData());

            }
        }

    }
}
