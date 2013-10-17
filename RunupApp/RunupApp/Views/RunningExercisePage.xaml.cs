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
    /// 
    /// Main logic calls made on viewmodel.
    /// The code here is for setup of things like service(Timer, GPSService) and drawing on view.
    /// This is so the services are properly stopped when page is removed and so viewmodel doesn't need a direct reference back to the page.
    /// </summary>
    public partial class ExercisePage : PhoneApplicationPage
    {
        // Properties
        private static IGPSService _locationService;
        private RunningExerciseViewModel _viewModel;
        private TaskFactory _taskFactory;
        private bool _hasCenteredMap = false;
        private DispatcherTimer _runUpdater;
        private EventHandler _timerTick;

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
            _locationService.GPSLocationChanged += _viewModel.GPSLocationChanged;

            // Turn on GPS
            _locationService.StartService();

            // Set timer for updating running time
            _runUpdater = new DispatcherTimer();
            _runUpdater.Interval = TimeSpan.FromSeconds(1);
            _timerTick = (Object sender, EventArgs args) => { _viewModel.UpdateRunningTime(); };
            _runUpdater.Tick += _timerTick;
            _runUpdater.Start();
        }

        // Events
        // :Navigation
        protected override void OnRemovedFromJournal(System.Windows.Navigation.JournalEntryRemovedEventArgs e)
        {
            // Set GPS service off
            _locationService.StopService();

            // Remove handlers
            _locationService.GPSLocationChanged -= GPSLocationChanged;
            _locationService.GPSLocationChanged -= _viewModel.GPSLocationChanged;
            this.DataContext = null;
            _runUpdater.Tick -= _timerTick;
            _runUpdater.Stop();
        }

        // :GPS
        private void GPSLocationChanged(double latitude, double longitude, DateTime time)
        {
            // Map
            // :Center
            if (!_hasCenteredMap)
            {
                _hasCenteredMap = true;
                _taskFactory.StartNew(() => mapOfRunningRoute.Center = new GeoCoordinate(latitude, longitude));
            }

            // :Draw point
            _taskFactory.StartNew(() => _DrawPoint(latitude, longitude));
        }

        // ::Draw map point
        private void _DrawPoint(double latitude, double longitude)
        {
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
        }
    }
}