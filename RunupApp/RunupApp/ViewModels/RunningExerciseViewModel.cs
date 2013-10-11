using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Implementations;
using System.Device.Location;

namespace RunupApp.ViewModels
{
    /// <summary>
    /// ViewModel for exercise information and interaction.
    /// 
    /// Contains access to running information like speed and distance.
    /// </summary>
    class RunningExerciseViewModel : ViewModelBase
    {
        // Members
        private IExercise _exercise;
        private TaskFactory _taskFactory;

        // Properties
        // :Meta info
        /// <summary>
        /// When the exercise was started.
        /// </summary>
        public string StartTime
        {
            get
            {
                return (_exercise.ExerciseStart.ToString("H:mm:ss"));
            }
        }

        /// <summary>
        /// How long the exercise has been running.
        /// </summary>
        public string RunningTime
        {
            get
            {
                TimeSpan runningTime = _exercise.ExerciseEnd.Subtract(_exercise.ExerciseStart);
                return (string.Format("{0:hh\\:mm\\:ss}", runningTime));
            }
        }

        // :Statistics
        /// <summary>
        /// Current speed on the route.
        /// 
        /// Unit: km/h
        /// </summary>
        public string CurrentSpeed
        {
            get
            {
                // Format
                string speed = string.Format("{0:0.00}", _exercise.RouteRun.NewestSpeed);
                return(speed);
            }
        }

        /// <summary>
        /// Average speed over the route so far.
        /// 
        /// Unit: km/h
        /// </summary>
        public string AverageSpeed
        {
            get
            {
                // Format
                string speed = string.Format("{0:0.00}", _exercise.RouteRun.AverageSpeed);
                return (speed);
            }
        }

        /// <summary>
        /// Current length of the route.
        /// 
        /// Unit: km.
        /// </summary>
        public string CurrentDistance
        {
            get
            {
                // Format
                string speed = string.Format("{0:0.00}", _exercise.RouteRun.DistanceRun);
                return (speed);
            }
        }

        // Functions
        public RunningExerciseViewModel(TaskFactory taskFactory)
        {
            // Setup
            _exercise = new Exercise();
            _taskFactory = taskFactory;
        }

        // Events
        /// <summary>
        /// Call function to set new point and update values.
        /// 
        /// Note: Notifies binded objects about update.
        /// </summary>
        /// <param name="latitude">Latitude of current location.</param>
        /// <param name="longitude">Longitude of current location.</param>
        /// <param name="time">Current time.</param>
        /// <param name="notify">Set true if want binded objects to get events.</param>
        public void GPSLocationChanged(double latitude, double longitude, DateTime time, bool notify=true)
        {
            // Update route info
            _exercise.RouteRun.AddPoint(latitude, longitude, time);
            _exercise.ExerciseEnd = time;

            // Notify properties
            if (notify)
            {
                _taskFactory.StartNew(() =>
                    {
                        NotifyPropertyChanged("CurrentSpeed");
                        NotifyPropertyChanged("AverageSpeed");
                        NotifyPropertyChanged("CurrentDistance");
                        NotifyPropertyChanged("RunningTime");
                    }
                    );
            }
        }

        /// <summary>
        /// Call to force update running time value.
        /// </summary>
        public void UpdateRunningTime()
        {
            _exercise.ExerciseEnd = DateTime.Now;
            _taskFactory.StartNew(() =>
            {
                NotifyPropertyChanged("RunningTime");
            }
            );
        }
    }
}
