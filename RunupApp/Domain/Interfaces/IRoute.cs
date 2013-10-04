/// \file IRoute.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    /// <summary>
    /// Route which contains information about a running/walking route.
    /// </summary>
    public interface IRoute
    {
        // Properties
        /// <summary>
        /// List of points that make up the running/walking route.
        /// </summary>
        ICollection<IRoutePoint> Points
        {
            get;
            set;
        }

        /// <summary>
        /// How far was the route.
        /// </summary>
        double DistanceRun
        {
            get; /// \note: Can be implemented as function so doesn't have to keep track variable.
        }

        /// <summary>
        /// Average speed when compared to route run(so far if active).
        /// </summary>
        double AverageSpeed
        {
            get; /// \note: Can be implemented as function so doesn't have to keep track variable.
        }

        /// <summary>
        /// Gets the speed from the newest points in the route.
        /// 
        /// \post If not enought points it will return 0.
        /// </summary>
        double NewestSpeed
        {
            get; /// \note: Can be implemented as function so doesn't have to keep track variable.
        }
    }
}
