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

namespace RunupApp.Views
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        // Events
        private void btnStartExercise_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/RunningExerciseView.xaml", UriKind.Relative));
        }
    }
}