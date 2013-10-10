using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace RunupApp.Views
{
    public partial class RegisterView : PhoneApplicationPage
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private void GoLoginButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("../ViewModels/LoginView.xaml", UriKind.Relative));
        }

    }
}