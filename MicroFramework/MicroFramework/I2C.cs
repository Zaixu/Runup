using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace MicroFramework
{
    /// <summary>
    /// Singleton class to handle I2C connections: As there can only be one to handle all devices
    /// </summary>
    public class I2C
    {
        private I2CDevice device;
        private static I2C instance = null;
        private static readonly object sync = new Object();

        /// <summary>
        /// Singleton, constructor not available to the outside
        /// </summary>
        private I2C()
        {
            //Set up default configuration
            I2CDevice.Configuration config = new I2CDevice.Configuration(0,0);
            device = new I2CDevice(config);
        }

        /// <summary>
        /// Public method to get the singleton instance of I2C
        /// Double check locking
        /// </summary>
        public static I2C Instance
        {
            get
            {
                //Check if its not been made else return instance
                if (instance == null)
                {
                    //Lock object so multiple threads dont try to create instance
                    lock(sync)
                    {
                        //Make sure its not been created since first check
                        if(instance == null)
                        {
                            //Create instance
                            instance = new I2C();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Function for writing to a specific configuration
        /// </summary>
        /// <param name="configuration">Configuration to write to</param>
        /// <param name="buffer">Data to be written</param>
        /// <param name="transactionTimeout">Timeout on execution</param>
        /// <returns></returns>
        public int Write(I2CDevice.Configuration configuration, byte[] buffer, int transactionTimeout)
        {
            //Create writetranscaction to i2c
            I2CDevice.I2CTransaction[] writeTransaction = new I2CDevice.I2CTransaction[] { I2CDevice.CreateWriteTransaction(buffer) };

            //Locking object, so no other can occupy the lines
            lock (sync)
            {
                //Set configuration
                device.Config = configuration;
                //Execute write
                int transferred = device.Execute(writeTransaction, transactionTimeout);
                //If the transferred isnt equal to data length, error on write
                if (transferred != buffer.Length)
                    Debug.Print("Error writing to device.");
                //Return bytes written
                return transferred;
            }
        }

        /// <summary>
        /// Function for reading from a specific configuration
        /// </summary>
        /// <param name="configuration">Configuration to read from</param>
        /// <param name="buffer">Buffer to be read to</param>
        /// <param name="transactionTimeout">Timeout on read execution</param>
        /// <returns>Bytes read</returns>
        public int Read(I2CDevice.Configuration configuration, byte[] buffer, int transactionTimeout)
        {
            //Create readtransaction to i2c
            I2CDevice.I2CTransaction[] readTransaction = new I2CDevice.I2CTransaction[] { I2CDevice.CreateReadTransaction(buffer) };

            //Locking object, so no other can occupy the lines
            lock (sync)
            {
                //Set configuration
                device.Config = configuration;
                //Execute read
                int transferred = device.Execute(readTransaction, transactionTimeout);
                //If the transferred isnt equal to data length, error on read
                if (transferred != buffer.Length)
                    Debug.Print("Error reading from device.");
                //Return bytes read
                return transferred;
            }
        }
    }
}
