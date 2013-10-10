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
        private GPS_ACCURACY _accuracy;
        private double _movementThreshold;

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
        public GPSService(GPS_ACCURACY accuracy = GPS_ACCURACY.HIGH, double movementThreshold = 50)
        {
            // Setup
            _accuracy = accuracy;
            _movementThreshold = movementThreshold;
        }

        // Functions
        // :IGPSService
        public void StopService()
        {
            if(GPS != null)
            {
                GPS.PositionChanged -= GPSPositionChanged;
                GPS = null;
            }
        }

        public void StartService()
        {
            if (GPS == null)
                SetupGPS(_accuracy, _movementThreshold);
        }

        // :Helper functions
        private void SetupGPS(GPS_ACCURACY accuracy, double movementThreshold)
        {
            if (GPS == null)
            {
                GPS = new Geolocator();

                switch (accuracy)
                {
                    case GPS_ACCURACY.HIGH:
                        GPS.DesiredAccuracy = PositionAccuracy.High;
                        break;
                    case GPS_ACCURACY.LOW:
                        GPS.DesiredAccuracy = PositionAccuracy.Default;
                        break;
                    default:
                        GPS.DesiredAccuracy = PositionAccuracy.High;
                        break;
                }
                GPS.MovementThreshold = movementThreshold;
                GPS.PositionChanged += GPSPositionChanged;
            }
        }

        // :GPS events
        void GPSPositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            DateTime currentTime = DateTime.Now;

            // Extern
            if (GPSLocationChanged != null)
                GPSLocationChanged(args.Position.Coordinate.Latitude, args.Position.Coordinate.Longitude, currentTime);
        }
    }

    // Helper types
    public enum GPS_ACCURACY
    {
        HIGH,
        LOW
    }
}
