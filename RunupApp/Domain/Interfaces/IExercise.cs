﻿/// \file IRoute.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    /// <summary>
    /// Exercise which contains information about a running/walking route.
    /// </summary>
    public interface IExercise
    {
        // Properties
        /// <summary>
        /// Unique identifier for the exercise.
        /// </summary>
        int ID
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

        /// <summary>
        /// How many points in the route.
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// List of points in the exercise.
        /// </summary>
        List<IRoutePoint> Points
        {
            get;
            set;
        }

        /// <summary>
        /// When the exercise began.
        /// </summary>
        DateTime ExerciseStart
        {
            get;
            set;
        }

        /// <summary>
        /// When the exercise ended.
        /// </summary>
        DateTime ExerciseEnd
        {
            get;
            set;
        }

        // Functions
        void AddPoint(double latitude, double longitude, DateTime time);
    }
}
