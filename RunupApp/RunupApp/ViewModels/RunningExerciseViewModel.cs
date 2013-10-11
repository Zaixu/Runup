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
        public DateTime ExerciseStartTime
        {
            get
            {
                return (_exercise.ExerciseStart);
            }
            set
            {
                _exercise.ExerciseStart = value;
            }
        }

        public DateTime ExerciseEndTime
        {
            get
            {
                return (_exercise.ExerciseEnd);
            }
            set
            {
                _exercise.ExerciseEnd = value;
            }
        }

        // Meta info
        public string StartTime
        {
            get
            {
                return (_exercise.ExerciseStart.ToString("H:mm:ss"));
            }
        }

        public string RunningTime
        {
            get
            {
                TimeSpan runningTime = _exercise.ExerciseEnd.Subtract(_exercise.ExerciseStart);
                return (string.Format("{0:hh\\:mm\\:ss}", runningTime));
            }
        }

        // Statistics
        public string CurrentSpeed
        {
            get
            {
                // Format
                string speed = string.Format("{0:0.00}", _exercise.RouteRun.NewestSpeed);
                return(speed);
            }
        }

        public string AverageSpeed
        {
            get
            {
                // Format
                string speed = string.Format("{0:0.00}", _exercise.RouteRun.AverageSpeed);
                return (speed);
            }
        }

        public string CurrentDistance
        {
            get
            {
                // Format
                string speed = string.Format("{0:0.00}", _exercise.RouteRun.DistanceRun);
                return (speed);
            }
        }

        // Map
        // TEST
        public GeoCoordinate CenterPoint
        {
            get
            {
                if (_exercise.RouteRun.Points.Count > 0)
                {
                    double latitude = _exercise.RouteRun.Points[_exercise.RouteRun.Points.Count - 1].Latitude;
                    double longitude = _exercise.RouteRun.Points[_exercise.RouteRun.Points.Count - 1].Longitude;
                    return new GeoCoordinate(latitude, longitude);
                }
                else
                {
                    return new GeoCoordinate(56.14, 9.98);
                }
                    
            }
        }
        // \TEST

        // Functions
        public RunningExerciseViewModel()
        {
            // Setup
            _exercise = new Exercise();
            _taskFactory = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());
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
                        // TEST
                        NotifyPropertyChanged("CenterPoint");
                        // \TEST
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
