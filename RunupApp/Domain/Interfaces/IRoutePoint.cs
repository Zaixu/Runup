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
        /// X location.
        /// </summary>
        int X
        {
            get;
            set;
        }

        /// <summary>
        /// Y location.
        /// </summary>
        int Y
        {
            get;
            set;
        }
    }
}
