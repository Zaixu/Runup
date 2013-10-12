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
    public partial class RegisterView : PhoneApplicationPage
    {
        private RegisterViewModel viewModel;
        private App application;

        public RegisterView()
        {
            InitializeComponent();
            viewModel = ContentStackPanel.DataContext as RegisterViewModel;
            application = Application.Current as App;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            application.CloudService.RegisterCompleted += viewModel.CloudService_RegisterCompleted;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            application.CloudService.RegisterCompleted -= viewModel.CloudService_RegisterCompleted;
        }

    }
}