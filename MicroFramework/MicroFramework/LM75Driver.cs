using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System.Threading;

namespace MicroFramework
{
    /// <summary>
    /// LM75 Temperature sensor driver
    /// </summary>
    public class LM75Driver : IDriver
    {
        private I2C i2c = I2C.Instance;
        private I2CDevice.Configuration config;
        private ManagedQueue queue = new ManagedQueue();
        private Thread thread = null;
        private bool running = false;

        /// <summary>
        /// Constructor - Sets up configuration and starts thread
        /// </summary>
        /// <param name="address">I2C Address</param>
        /// <param name="clockRateKhz">Clockrate</param>
        public LM75Driver(ushort address, int clockRateKhz)
        {
            //Setup configuration
            config = new I2CDevice.Configuration(address, clockRateKhz);
            // Initial, not being used, but then thread wont be null used throughout to check status.
            thread = new Thread(Pull);
            //Start driver
            Start();
        }

        /// <summary>
        /// Start driver class
        /// </summary>
        public void Start()
        {
            //Stop if running
            Stop();
            //If thread is not running
            if (!thread.IsAlive)
            {
                Debug.Print("Starting LM75Driver");
                //Set volatile variable true so thread while loops
                running = true;
                //Create thread
                thread = new Thread(Pull);
                //Start thread
                thread.Start();
            }
        }

        /// <summary>
        /// Stop driver class
        /// </summary>
        public void Stop()
        {
            //If thread is running
            if (thread.IsAlive)
            {
                Debug.Print("Stopping LM75Driver");
                //Set thread variable to false, so thread loop stops
                running = false;
                //Wait for thread to finish
                while (thread.IsAlive)
                {
                    Thread.Sleep(500);
                }
            }
        }

        /// <summary>
        /// Function that thread runs - Pulls data every 5 secs and adds em to a managed number queue
        /// </summary>
        private void Pull()
        {
            //Run as long till stopped
            while (running)
            {
                //Create write command
                byte[] writeBuffer = new byte[1] { 0x00 };
                i2c.Write(config, writeBuffer, 100);

                //Create read buffer for temperature
                byte[] readBuffer = new byte[2];
                i2c.Read(config, readBuffer, 100);

                //MSB First
                /*
                    A reading:
                        readBuffer[0] readBuffer[1]
                        XX XX XX XX    Y0 00 00 00

                        con(8bit):
                     (X)XX XX XX XY
                */
                byte con;
                con = (byte)(((readBuffer[1] & 0x80) >> 7) & 0xFF);
                con = (byte)(con | (byte)((readBuffer[0] << 1) & 0xFF));

                float temperature = 0;

                /*if ((readBuffer[0] & 0x80) > 0)
                    temperature = ((float)con - 512) / 2;
                else
                    temperature = (float)con / 2;*/

                // 2's complement conversion
                if ((readBuffer[0] & 0x80) > 0) // 8th bit set means negativ
                    temperature = -((float)(~con) + 1) / 2;
                else
                    temperature = (float)con / 2;
                
                //Add data to queue
                queue.Add(new QueueClass(temperature));
                //Wait 5 seconds
                Thread.Sleep(5000);
            }
        }

        public object GetData()
        {
            return queue;
        }


        public class QueueClass
        {
            public float temperature = 0;
            public DateTime datetime = DateTime.Now;

            public QueueClass(float temp)
            {
                temperature = temp;
            }
        }

    }
}
