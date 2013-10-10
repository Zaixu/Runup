/// \file RoutePoint.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Implementations
{
    public class RoutePoint : IRoutePoint
    {
        // Properties
        // :RoutePoint
        public double Longitude
        {
            get;
            set;
        }

        public double Latitude
        {
            get;
            set;
        }

        public DateTime Time
        {
            get;
            set;
        }
    }
}
