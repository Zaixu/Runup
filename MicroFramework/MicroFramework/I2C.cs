using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace MicroFramework
{
    class I2C
    {
        private I2CDevice device;

        private static I2C instance = null;
        private static readonly object sync = new Object();

        private I2C()
        {
            I2CDevice.Configuration config = new I2CDevice.Configuration(0,0);
            device = new I2CDevice(config);
        }

        // Double check locking
        public static I2C Instance
        {
            get
            {
                if (instance == null)
                {
                    lock(sync)
                    {
                        if(instance == null)
                        {
                            instance = new I2C();
                        }
                    }
                }
                return instance;
            }
        }

        public int Write(I2CDevice.Configuration configuration, byte[] buffer, int transactionTimeout)
        {
            I2CDevice.I2CTransaction[] writeTransaction = new I2CDevice.I2CTransaction[] { I2CDevice.CreateWriteTransaction(buffer) };

            lock (sync)
            {
                device.Config = configuration;

                int transferred = device.Execute(writeTransaction, transactionTimeout);

                if (transferred != buffer.Length)
                    Debug.Print("Error writing to device.");
                
                return transferred;
            }
        }

        public int Read(I2CDevice.Configuration configuration, byte[] buffer, int transactionTimeout)
        {
            I2CDevice.I2CTransaction[] readTransaction = new I2CDevice.I2CTransaction[] { I2CDevice.CreateReadTransaction(buffer) };

            lock (sync)
            {
                device.Config = configuration;

                int transferred = device.Execute(readTransaction, transactionTimeout);

                if (transferred != buffer.Length)
                    Debug.Print("Error reading from device.");

                return transferred;
            }
        }
    }
}
