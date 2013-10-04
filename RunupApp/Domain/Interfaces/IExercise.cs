/// \file IExercise.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    /// <summary>
    /// Contains meta information about the exercise.
    /// 
    /// This is things like the starttime and stoptime.
    /// </summary>
    public interface IExercise
    {
        // Properties
        /// <summary>
        /// When the exercise started.
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

        IRoute RouteRun
        {
            get;
            set;
        }
    }
}
