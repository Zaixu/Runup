using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Domain.Implementations;

namespace RunupApp
{
    public partial class ExercisePage : PhoneApplicationPage
    {
        public ExercisePage()
        {
            InitializeComponent();
        }

        // Events
        // :Navigation
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Turn on GPS service... Lingerie or whatever helps.
            if (App.LocationService == null)
            {
                App.LocationService = new GPSService(GPS_ACCURACY.HIGH, 50);
                App.LocationService.GPSLocationChanged += GPSLocationChanged;
            }

            App.LocationService.StartService();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Nothing here
        }

        protected override void OnRemovedFromJournal(System.Windows.Navigation.JournalEntryRemovedEventArgs e)
        {
            // Set GPS service off
            App.LocationService.StopService();
        }

        // :GPS
        private void GPSLocationChanged(double latitude, double longitude)
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
            App.LocationService.StopService();

            // Go to main page
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}