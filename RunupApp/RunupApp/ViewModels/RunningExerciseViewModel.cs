﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Implementations;

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


        // Functions
        public RunningExerciseViewModel()
        {
            // Setup
            _exercise = new Exercise();
            _taskFactory = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());
        }

        // Events
        public void GPSLocationChanged(double latitude, double longitude, DateTime time)
        {
            // Add to route
            IRoutePoint point = new RoutePoint();
            point.Latitude = latitude;
            point.Longitude = longitude;
            point.Time = time;
            _exercise.RouteRun.Points.Add(point);

            // Notify properties
            _taskFactory.StartNew(() => 
                {
                    NotifyPropertyChanged("CurrentSpeed");
                    NotifyPropertyChanged("AverageSpeed");
                    NotifyPropertyChanged("CurrentDistance");
                }
                );
        }
    }
}