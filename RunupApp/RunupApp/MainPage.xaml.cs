using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RunupApp.Resources;
using Windows.Devices.Geolocation;
using Domain.Implementations;

namespace RunupApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        // Events
        #region TEST
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            /*if (App.Geolocator == null)
            {
                App.Geolocator = new Geolocator();
                App.Geolocator.DesiredAccuracy = PositionAccuracy.High;
                App.Geolocator.MovementThreshold = 100; // The units are meters.
                App.Geolocator.PositionChanged += geolocator_PositionChanged;
            }*/
            // Later should only be running when exercise in progress.
            /*if (App.LocationService == null)
            {
                App.LocationService = new GPSService(PositionAccuracy.High, 50);
                App.LocationService.GPSLocationChanged += GPSChange;
            }*/
        }

        void GPSChange(double latitude, double longitude)
        {
            if (!App.RunningInBackground)
            {
                Console.WriteLine("Not in background");
            }
            else
            {
                Microsoft.Phone.Shell.ShellToast toast = new Microsoft.Phone.Shell.ShellToast();
                toast.Content = "Latitude: " + latitude.ToString("0.00") + " Longitude: " + longitude.ToString("0:00");
                toast.Title = "Location: ";
                toast.Show();
            }
        }
        #endregion

        private void btnStartExercise_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ExercisePage.xaml", UriKind.Relative));
        }
    }
}