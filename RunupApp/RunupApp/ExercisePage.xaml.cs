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
using Domain.Interfaces;
using RunupApp.ViewModels;
using System.Windows.Threading;
using System.Windows.Shapes;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Media;
using System.Device.Location;
using System.Threading.Tasks;

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
        private DispatcherTimer _runUpdater;
        private TaskFactory _taskFactory;

        // Functions
        public ExercisePage()
        {
            // Setup
            InitializeComponent();
            _taskFactory = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());
            _viewModel = new RunningExerciseViewModel(_taskFactory);
            this.DataContext = _viewModel;

            // Add GPS service if is first use
            if (_locationService == null)
            {
                _locationService = new GPSService(GPS_ACCURACY.HIGH, 20);
            }

            // Add handlers
            _locationService.GPSLocationChanged += GPSLocationChanged;

            // Turn on GPS
            _locationService.StartService();

            // Set timer for updating running time
            _runUpdater = new DispatcherTimer();
            _runUpdater.Interval = TimeSpan.FromSeconds(1);
            _runUpdater.Tick += TimerTick;
            _runUpdater.Start();
        }

        // Events
        // :Navigation
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Nothing
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // We don't want the page saved
            NavigationService.RemoveBackEntry();
        }

        protected override void OnRemovedFromJournal(System.Windows.Navigation.JournalEntryRemovedEventArgs e)
        {
            // Set GPS service off
            _locationService.StopService();

            // Remove handlers
            _locationService.GPSLocationChanged -= GPSLocationChanged;
            this.DataContext = null;
            _runUpdater.Tick -= TimerTick;
        }

        // :GPS
        private void GPSLocationChanged(double latitude, double longitude, DateTime time)
        {
            if (App.RunningInBackground == true)
            {
                _viewModel.GPSLocationChanged(latitude, longitude, time, false);
            }
            else
            {
                _viewModel.GPSLocationChanged(latitude, longitude, time, true);

                // TEST
                _taskFactory.StartNew(() => _DrawPoint(latitude, longitude));
                // \TEST
            }
        }

        // ::Draw map point
        private void _DrawPoint(double latitude, double longitude)
        {
            // TEST
            Ellipse myCircle = new Ellipse();
            myCircle.Fill = new SolidColorBrush(Colors.Blue);
            myCircle.Height = 20;
            myCircle.Width = 20;
            myCircle.Opacity = 50;

            MapOverlay myLocationOverlay = new MapOverlay();
            myLocationOverlay.Content = myCircle;
            myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
            myLocationOverlay.GeoCoordinate = new GeoCoordinate(latitude, longitude);

            // Create a MapLayer to contain the MapOverlay.
            MapLayer myLocationLayer = new MapLayer();
            myLocationLayer.Add(myLocationOverlay);

            mapOfRunningRoute.Layers.Add(myLocationLayer);
            // \TEST
        }

        // :Timer
        void TimerTick(Object sender, EventArgs args)
        {
            // Update running time
            if(!App.RunningInBackground)
                _viewModel.UpdateRunningTime();
        }

        // :Other
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