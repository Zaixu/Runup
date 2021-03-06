﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RunupApp.ViewModels;
using System.Diagnostics;

namespace RunupApp.Views
{
    public partial class LoginView : PhoneApplicationPage
    {
        /// <summary>
        /// Holds the Views ViewModel
        /// </summary>
        private LoginViewModel viewModel;

        /// <summary>
        /// Holds the current application Instance
        /// </summary>
        private App application;

        /// <summary>
        /// Constructor - Make ViewModel/Application instance available
        /// </summary>
        public LoginView()
        {
            InitializeComponent();
            
            //Get our xaml created ViewModel
            viewModel = ContentStackPanel.DataContext as LoginViewModel;
            //Get our current application instance
            application = Application.Current as App;

            // If view is visible subscribe to CloudService LoginCompleted event
            application.CloudService.LoginCompleted += viewModel.CloudService_LoginCompleted;
        }

        /// <summary>
        /// When page is navigated to, remove back entry if its from RegisterView and subscribe to CloudService LoginCompleted event.
        /// </summary>
        /// <param name="e">Navigation Event Arguments</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.IsNavigationInitiator)
            {
                //Subscribe to login completed event
                application.CloudService.LoginCompleted += viewModel.CloudService_LoginCompleted;

                //If the former page is RegisterView, delete it from stack - Do not wanna have a history of Login/Register/Login etc to go back on
                var formerPage = NavigationService.BackStack.First();
                if (formerPage != null && formerPage.Source.ToString() == "/Views/RegisterView.xaml")
                    NavigationService.RemoveBackEntry();
            }
        }

        /// <summary>
        /// When navigating from view
        /// </summary>
        /// <param name="e">NavigationEvent Arguments</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            //If its being a navigate within the app, remove event subcribtion
            if (e.IsNavigationInitiator)
            {
                // Remove event
                application.CloudService.LoginCompleted -= viewModel.CloudService_LoginCompleted;
            }
        }
    }
}