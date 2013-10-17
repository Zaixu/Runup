using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Implementations;

namespace RunupApp.ViewModels
{
    class ExerciseDetailedViewModel
    {
        // Members
        IExercise _exercise;

        // Properties
        /// <summary>
        /// Information about when the exercise was run.
        /// </summary>
        public DateTime DateInfo
        {
            get
            {
                return (_exercise.ExerciseStart);
            }
        }

        /// <summary>
        /// How long the exercise was.
        /// </summary>
        public string TotalTime
        {
            get
            {
                TimeSpan runningTime = _exercise.ExerciseEnd.Subtract(_exercise.ExerciseStart);
                return (string.Format("{0:hh\\:mm\\:ss}", runningTime));
            }
        }

        /// <summary>
        /// The average speed around the route.
        /// </summary>
        public string AverageSpeed
        {
            get
            {
                string speed = string.Format("{0:0.00}", _exercise.AverageSpeed);
                return (speed);
            }
        }

        /// <summary>
        /// Distance run/walked.
        /// </summary>
        public string Distance
        {
            get
            {
                string speed = string.Format("{0:0.00}", _exercise.DistanceRun);
                return (speed);
            }
        }

        /// <summary>
        /// Approx how many calories burnt from how long the exercise was.
        /// </summary>
        public string BurntCalories
        {
            get
            {
                TimeSpan runningTime = _exercise.ExerciseEnd.Subtract(_exercise.ExerciseStart);
                double caloriesDouble = ((double)872 / 3600) * runningTime.TotalSeconds;
                string calories = string.Format("{0:0.00}", caloriesDouble);
                return (calories);
            }
        }
        
        // Functions
        // :Constructors
        /// <summary>
        /// Created new viewmodel.
        /// </summary>
        /// <param name="exercise">Exercise to create the viewmodel around.</param>
        public ExerciseDetailedViewModel(IExercise exercise)
        {
            // Setup
            _exercise = exercise;
        }
    }
}
