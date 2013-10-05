using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System.Threading;

namespace MicroFramework
{
    class LM75Driver : IDriver
    {
        private I2C i2c = I2C.Instance;
        private I2CDevice.Configuration config;
        private ManagedQueue queue = new ManagedQueue();

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
                i2c.Write(config, writeBuffer, 100);

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
                if(readBuffer[0] & 0x80) // 8th bit set means negativ
                    temperature = -((~con) + 1)/2;
                else
                    temperature = con/2

                queue.Add(new QueueClass(temperature));

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
