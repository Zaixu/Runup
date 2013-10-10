/// \file IRoutePoint.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    /// <summary>
    /// Point used for tracking exercise route.
    /// </summary>
    public interface IRoutePoint
    {
        // Properties
        /// <summary>
        /// Longitude location.
        /// </summary>
        double Longitude
        {
            get;
            set;
        }

        /// <summary>
        /// Latitude location.
        /// </summary>
        double Latitude
        {
            get;
            set;
        }

        /// <summary>
        /// When the point was made.
        /// </summary>
        DateTime Time
        {
            get;
            set;
        }
    }
}
