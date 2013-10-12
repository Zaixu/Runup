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
        private App application;

        public LoginView()
        {
            InitializeComponent();
            viewModel = ContentStackPanel.DataContext as LoginViewModel;
            application = Application.Current as App;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(NavigationService.CanGoBack)
                NavigationService.RemoveBackEntry();

            application.CloudService.LoginCompleted += viewModel.CloudService_LoginCompleted;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            application.CloudService.LoginCompleted -= viewModel.CloudService_LoginCompleted;
        }

    }
}