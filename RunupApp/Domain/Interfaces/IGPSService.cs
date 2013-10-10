using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    /// <summary>
    /// For handling GPS related functionality.
    /// </summary>
    public interface IGPSService
    {
        // Properties
        /// <summary>
        /// So able to get notified when the GPS location changed.
        /// </summary>
        HandleGPSLocationChanged GPSLocationChanged
        {
            get;
            set;
        }

        /// <summary>
        /// Should be called to properly stop the GPS.
        /// 
        /// It also removes event handlers attached to the service.
        /// </summary>
        void StopService();

        /// <summary>
        /// Begins GPS tracking and 'readies' the service.
        /// </summary>
        void StartService();
    }

    // Helper types
    public delegate void HandleGPSLocationChanged(double latitude, double longitude, DateTime time);
}
