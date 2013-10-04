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
            if (App.Geolocator == null)
            {
                App.Geolocator = new Geolocator();
                App.Geolocator.DesiredAccuracy = PositionAccuracy.High;
                App.Geolocator.MovementThreshold = 100; // The units are meters.
                App.Geolocator.PositionChanged += geolocator_PositionChanged;
            }
        }

        void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {

            if (!App.RunningInBackground)
            {
                Console.WriteLine("Not in background");
            }
            else
            {
                Microsoft.Phone.Shell.ShellToast toast = new Microsoft.Phone.Shell.ShellToast();
                toast.Content = args.Position.Coordinate.Latitude.ToString("0.00");
                toast.Title = "Location: ";
                toast.Show();
            }
        }
        #endregion
    }
}