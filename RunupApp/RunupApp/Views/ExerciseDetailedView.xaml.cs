using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RunupApp.ViewModels;
using System.Windows.Shapes;
using System.Windows.Media;
using Microsoft.Phone.Maps.Controls;
using System.Device.Location;

namespace RunupApp.Views
{
    public partial class ExerciseDetailedPage : PhoneApplicationPage
    {
        // Members
        private ExerciseDetailedViewModel _viewModel;

        // Functions
        // :Constructors
        public ExerciseDetailedPage()
        {
            // Setup
            InitializeComponent();
            _viewModel = new ExerciseDetailedViewModel(App.SelectedExercise);
            this.DataContext = _viewModel;

            // Map
            var points = App.SelectedExercise.Points;

            // :Draw
            foreach (var point in points)
                _DrawPoint(point.Latitude, point.Longitude);

            // :Center
            var firstPoint = points[0];
            MapOfRunningRoute.Center = new GeoCoordinate(firstPoint.Latitude, firstPoint.Longitude);
        }

        // :Helper functions
        // :Draw map point
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

            MapOfRunningRoute.Layers.Add(myLocationLayer);
        }
    }
}