﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Domain.Implementations;
using Domain.Interfaces;
using RunupApp.ViewModels;

namespace RunupApp
{
    /// <summary>
    /// For information and operation for currently running exercise.
    /// </summary>
    public partial class ExercisePage : PhoneApplicationPage
    {
        // Properties
        private static IGPSService _locationService;
        private RunningExerciseViewModel _viewModel;

        // Functions
        public ExercisePage()
        {
            // Setup
            InitializeComponent();
            _viewModel = new RunningExerciseViewModel();
            this.DataContext = _viewModel;
        }

        // Events
        // :Navigation
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Turn on GPS service... Lingerie or whatever helps.
            if (_locationService == null)
            {
                _locationService = new GPSService(GPS_ACCURACY.HIGH, 2);
                _locationService.GPSLocationChanged += GPSLocationChanged;
                _locationService.GPSLocationChanged += _viewModel.GPSLocationChanged;
            }

            _locationService.StartService();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Nothing here
        }

        protected override void OnRemovedFromJournal(System.Windows.Navigation.JournalEntryRemovedEventArgs e)
        {
            // Set GPS service off
            _locationService.StopService();
        }

        // :GPS
        private void GPSLocationChanged(double latitude, double longitude, DateTime time)
        {
            // Update exercise info(route)
            
            if (App.RunningInBackground == true)
            {
                // Update UI
            }
        }

        private void btnStopExercise_Click(object sender, RoutedEventArgs e)
        {
            /* Save exercise info.
             Any last things.
             No Uploading.
             */

            // Stop
            _locationService.StopService();

            // Go to main page
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}