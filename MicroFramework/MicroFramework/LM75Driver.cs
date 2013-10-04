using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using System.Threading;

namespace MicroFramework
{
    class LM75Driver : IDriver
    {
        private I2C i2c = I2C.Instance;
        private I2CDevice.Configuration config;
        private float temperature = 0;

        public LM75Driver(ushort address, int clockRateKhz)
        {
            config = new I2CDevice.Configuration(address, clockRateKhz);

            new Thread(Pull).Start();
        }

        private void Pull()
        {
            while (true)
            {
                byte[] writeBuffer = new byte[1] { 0x00 };
                i2c.Write(config, writeBuffer, 1000);

                byte[] readBuffer = new byte[2];
                i2c.Read(config, readBuffer, 1000);

                //MSB First
                byte con;
                con = (byte)(((readBuffer[1] & 0x80) >> 7) & 0xFF);
                con = (byte)(con | (byte)((readBuffer[0] << 1) & 0xFF));

                if ((readBuffer[0] & 0x80) > 0)
                {
                    temperature = ((float)con - 512) / 2;
                }
                else
                    temperature = (float)con / 2;

                Thread.Sleep(1000);
            }
        }

        public string GetData()
        {
            return temperature.ToString();
        }

    }
}