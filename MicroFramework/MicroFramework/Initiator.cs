using System;
using Microsoft.SPOT;
using System.Net;
using Microsoft.SPOT.Net;

namespace MicroFramework
{
    class Initiator
    {
        public Initiator()
        {
            Microsoft.SPOT.Net.NetworkInformation.NetworkChange.NetworkAddressChanged += NetworkChange_NetworkAddressChanged;
            Microsoft.SPOT.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
        }

        void NetworkChange_NetworkAvailabilityChanged(object sender, Microsoft.SPOT.Net.NetworkInformation.NetworkAvailabilityEventArgs e)
        {
           
        }

        void NetworkChange_NetworkAddressChanged(object sender, EventArgs e)
        {
            
        }
    }
}
