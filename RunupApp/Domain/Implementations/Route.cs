/// \file Route.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Implementations
{
    public class Route : IRoute
    {
        // Members
        private double _latestDistance;
        private int _latestIndex = 0; // For not updating _latestDistance twice

        // Properties
        // :IRoute
        public List<IRoutePoint> Points
        {
            get;
            set;
        }

        public double DistanceRun
        {
            get 
            {
                if (Points.Count < 2)
                    return (0);
                else
                {
                    if ((Points.Count - 1) > _latestIndex)
                    {
                        for (int i = _latestIndex; i < (Points.Count - 1); i++)
                        {
                            // Get newest distance and add to total
                            IRoutePoint latestPoint = Points[i];
                            IRoutePoint secondPoint = Points[i + 1];
                            double distance = DistancePointToPoint(secondPoint, latestPoint);
                            _latestDistance += distance;
                        }
                        
                        _latestIndex = Points.Count - 1;
                    }

                    return (_latestDistance);
                }
            }
        }

        public double AverageSpeed
        {
            get
            {
                if (Points.Count < 2)
                    return (0);
                else
                {
                    TimeSpan timeDiffStartEnd = Points[Points.Count - 1].Time.Subtract(Points[0].Time);
                    double speed = DistanceRun / timeDiffStartEnd.TotalSeconds; // km/s
                    speed = speed * 3600; // km/h

                    return (speed);
                }
            }
        }

        public double NewestSpeed
        {
            get
            {
                if (Points.Count < 2)
                    return (0);
                else
                {
                    // Get from 2 newest points
                    IRoutePoint latestPoint = Points[Points.Count - 1];
                    IRoutePoint secondPoint = Points[Points.Count - 2];
                    double distance = DistancePointToPoint(secondPoint, latestPoint);

                    TimeSpan timeDiff = latestPoint.Time - secondPoint.Time;

                    double speed = distance / timeDiff.TotalSeconds; // km/s
                    speed = speed * 3600; // km/h

                    return (speed);
                }
            }
        }

        public int Count
        {
            get
            {
                return (Points.Count);
            }
        }

        public DateTime ExerciseStart
        {
            get;
            set;
        }

        public DateTime ExerciseEnd
        {
            get;
            set;
        }

        // Functions
        // :Constructors
        public Route()
        {
            // Setup
            Points = new List<IRoutePoint>();
            _latestDistance = 0;
            ExerciseStart = DateTime.Now;
            ExerciseEnd = DateTime.Now;
        }

        // :IRoute
        public void AddPoint(double latitude, double longitude, DateTime time)
        {
            IRoutePoint point = new RoutePoint();
            point.Latitude = latitude;
            point.Longitude = longitude;
            point.Time = time;

            Points.Add(point);
        }

        // :Helper functions
        // Calculate distance between 2 points.
        // Returns in unit 'km'.
        // Source for Haversine formula used: http://stackoverflow.com/questions/27928/how-do-i-calculate-distance-between-two-latitude-longitude-points/27943#27943
        private double DistancePointToPoint(IRoutePoint start, IRoutePoint end)
        {
            var R = 6371; // Radius of the earth in km
            var dLat = DegreesToRadian(end.Latitude - start.Latitude);
            var dLon = DegreesToRadian(end.Longitude - start.Longitude);
            var a =
              Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(DegreesToRadian(start.Latitude)) * Math.Sin(DegreesToRadian(end.Latitude)) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
              ;
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; // Distance in km
            return d;
        }

        private double DegreesToRadian(double degress)
        {
            return degress * (Math.PI/180);
        }
    }
}
