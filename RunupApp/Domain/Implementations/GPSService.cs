using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Windows.Devices.Geolocation;

namespace Domain.Implementations
{
    public class GPSService : IGPSService
    {
        // Properties
        private Geolocator GPS;

        // :IGPSService
        public HandleGPSLocationChanged GPSLocationChanged
        {
            get;
            set;
        }

        // Functions
        // :Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="accuracy">How accurate the position should be. High is preferred for accurate route.</param>
        /// <param name="movementThreshold">How many 'meters' there should be from last position to trigger position changed event('GPSLocationChanged').</param>
        public GPSService(PositionAccuracy accuracy = PositionAccuracy.High, double movementThreshold = 50)
        {
            // Setup
            SetupGPS(accuracy, movementThreshold);
        }

        // :Helper functions
        private void SetupGPS(PositionAccuracy accuracy, double movementThreshold)
        {
            if (GPS == null)
            {
                GPS = new Geolocator();
                GPS.DesiredAccuracy = accuracy;
                GPS.MovementThreshold = movementThreshold;
                GPS.PositionChanged += GPSPositionChanged;
            }
        }

        // :GPS events
        void GPSPositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            // Own
            // Nothing here

            // Extern
            if (GPSLocationChanged != null)
                GPSLocationChanged(args.Position.Coordinate.Latitude, args.Position.Coordinate.Longitude);
        }
    }
}
