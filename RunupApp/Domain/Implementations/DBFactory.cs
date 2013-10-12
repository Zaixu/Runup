using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.CloudService;
using System.Collections.ObjectModel;

namespace Domain.Implementations
{
    class DBFactory : IDBFactory
    {
        public Routes CreateRoute(IRoute route)
        {
            // Setup
            Routes createdRoute = new Routes();

            // Convert
            // :Route
            createdRoute.RoutePoints = new ObservableCollection<RoutePoints>();

            // :Points
            foreach (var point in route.Points)
            {
                var createdPoint = new RoutePoints();
                createdPoint.Latitude = point.Latitude;
                createdPoint.Longitude = point.Longitude;
                createdPoint.Time = point.Time;

                createdRoute.RoutePoints.Add(createdPoint);
            }

            return(createdRoute);
        }
    }
}
