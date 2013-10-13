using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Implementations;
using System.Device.Location;
using System.Windows;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using System.Windows.Threading;

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
        private IRoute _route;
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
                return (_route.ExerciseStart.ToString("H:mm:ss"));
            }
        }

        /// <summary>
        /// How long the exercise has been running.
        /// </summary>
        public string RunningTime
        {
            get
            {
                TimeSpan runningTime = _route.ExerciseEnd.Subtract(_route.ExerciseStart);
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
                string speed = string.Format("{0:0.00}", _route.NewestSpeed);
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
                string speed = string.Format("{0:0.00}", _route.AverageSpeed);
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
                string speed = string.Format("{0:0.00}", _route.DistanceRun);
                return (speed);
            }
        }

        // Functions
        // :Constructors and deconstructors
        public RunningExerciseViewModel(TaskFactory taskFactory)
        {
            // Setup
            _route = new Route();
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
        public void GPSLocationChanged(double latitude, double longitude, DateTime time)
        {
            // Update route info
            _route.AddPoint(latitude, longitude, time);
            _route.ExerciseEnd = time;

            // Notify properties
            if (!App.RunningInBackground)
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
            if (!App.RunningInBackground)
            {
                _route.ExerciseEnd = DateTime.Now;
                _taskFactory.StartNew(() =>
                {
                    NotifyPropertyChanged("RunningTime");
                }
                );
            }
        }

        public ICommand StopExercise
        {
            get { return new DelegateCommand(_StopExercise); }
        }

        public void _StopExercise()
        {
            App.NewRoutesStack.Add(_route);
            var app = Application.Current as App;

            (app.RootVisual as PhoneApplicationFrame).GoBack();
        }

        // Functions
        /// <summary>
        /// Gets the underlying route.
        /// </summary>
        public IRoute GetRoute()
        {
            return _route;
        }
    }
}
