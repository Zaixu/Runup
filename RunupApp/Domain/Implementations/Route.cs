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
        // Properties
        // :IRoute
        ICollection<IRoutePoint> IRoute.Points
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        double IRoute.DistanceRun
        {
            get { throw new NotImplementedException(); }
        }

        double IRoute.AverageSpeed
        {
            get { throw new NotImplementedException(); }
        }

        double IRoute.NewestSpeed
        {
            get { throw new NotImplementedException(); }
        }
    }
}
