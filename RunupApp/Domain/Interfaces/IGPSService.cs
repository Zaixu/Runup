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
    }

    // Helper types
    public delegate void HandleGPSLocationChanged(double latitude, double longitude);
}
