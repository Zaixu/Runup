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
                string speed = string.Format("{0:0.00}", _exercise.NewestSpeed);
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
                string speed = string.Format("{0:0.00}", _exercise.AverageSpeed);
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
                string speed = string.Format("{0:0.00}", _exercise.DistanceRun);
                return (speed);
            }
        }

        /// <summary>
        /// Current calories burnt
        /// </summary>
        public string CurrentCalories
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
        // :Constructors and deconstructors
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
        public void GPSLocationChanged(double latitude, double longitude, DateTime time)
        {
            // Update route info
            _exercise.AddPoint(latitude, longitude, time);
            _exercise.ExerciseEnd = time;

            // Notify properties
            if (!App.RunningInBackground)
            {
                _taskFactory.StartNew(() =>
                    {
                        NotifyPropertyChanged("CurrentSpeed");
                        NotifyPropertyChanged("AverageSpeed");
                        NotifyPropertyChanged("CurrentDistance");
                        NotifyPropertyChanged("RunningTime");
                        NotifyPropertyChanged("CurrentCalories");
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
                _exercise.ExerciseEnd = DateTime.Now;
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

        private void _StopExercise()
        {
            App.NewExercisesStack.Add(_exercise);
            var app = Application.Current as App;

            (app.RootVisual as PhoneApplicationFrame).GoBack();
        }

        // Functions
        /// <summary>
        /// Gets the underlying route.
        /// </summary>
        public IExercise GetRoute()
        {
            return _exercise;
        }
    }
}
