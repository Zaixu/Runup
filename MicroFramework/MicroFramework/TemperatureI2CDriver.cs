using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace MicroFramework
{
    class TemperatureI2CDriver
    {
        private I2CDevice device;
        private I2CDevice.Configuration config;




        public TemperatureI2CDriver(ushort address, int clockratekhz)
        {
            config = new I2CDevice.Configuration(address, clockratekhz);
            device = new I2CDevice(config);
        }

        public string GetData()
        {
            byte[] outBuffer1 = new byte[2] { 0x01, 0xE0 };
            I2CDevice.I2CWriteTransaction writeConfigTransaction = I2CDevice.CreateWriteTransaction(outBuffer1);

            byte[] outBuffer = new byte[1] { 0x00 };
            I2CDevice.I2CWriteTransaction writeTransaction = I2CDevice.CreateWriteTransaction(outBuffer);

            byte[] inBuffer = new byte[2];
            I2CDevice.I2CReadTransaction readTransaction = I2CDevice.CreateReadTransaction(inBuffer);

            I2CDevice.I2CTransaction[] configTransaction = new I2CDevice.I2CTransaction[] { writeConfigTransaction };
            I2CDevice.I2CTransaction[] getTempTransaction = new I2CDevice.I2CTransaction[] { writeTransaction, readTransaction };

            device.Execute(configTransaction, 600);
            device.Execute(getTempTransaction, 100);

            return ((inBuffer[0] << 4) | (inBuffer[1] >> 4)).ToString(); 
        }

    }
}
