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

namespace RunupApp.Views
{
    public partial class LoginView : PhoneApplicationPage
    {
        private LoginViewModel viewModel;

        public LoginView()
        {
            InitializeComponent();
            viewModel = (ContentStackPanel.DataContext as LoginViewModel);
        }

        private void GoRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/RegisterView.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //App thisApp = Application.Current as App;
            //thisApp.CloudService.LoginCompleted += CloudService_LoginCompleted;
            CloudService.ServiceClient sc = new CloudService.ServiceClient();
        }

        void CloudService_LoginCompleted(object sender, CloudService.LoginCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            
        }

    }
}