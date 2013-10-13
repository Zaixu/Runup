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

namespace RunupApp.Views
{
    public partial class RegisterView : PhoneApplicationPage
    {
        /// <summary>
        /// Holds the Views ViewModel
        /// </summary>
        private RegisterViewModel viewModel;

        /// <summary>
        /// Holds the current application Instance
        /// </summary>
        private App application;

        /// <summary>
        /// Constructor - Make ViewModel/Application instance available
        /// </summary>
        public RegisterView()
        {
            InitializeComponent();

            //Get our xaml created ViewModel
            viewModel = ContentStackPanel.DataContext as RegisterViewModel;
            //Get our current application instance
            application = Application.Current as App;

            // Subscribe to CloudService RegisterCompleted event
            application.CloudService.RegisterCompleted += viewModel.CloudService_RegisterCompleted;
        }

        /// <summary>
        /// When page is navigated to, remove back entry if its from LoginView and subscribe to CloudService RegisterCompleted event.
        /// </summary>
        /// <param name="e">Navigation Event Arguments</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //If the former page is LoginView, delete it from stack - Do not wanna have a history of Login/Register/Login etc to go back on
            var formerPage = NavigationService.BackStack.First();
            if (formerPage != null && formerPage.Source.ToString() == "/Views/LoginView.xaml")
                NavigationService.RemoveBackEntry();
        }

        /// <summary>
        /// When page is navigated from, unsubscribe CloudService RegisterCompleted event
        /// </summary>
        /// <param name="e">Journal info.</param>
        protected override void OnRemovedFromJournal(JournalEntryRemovedEventArgs e)
        {
            base.OnRemovedFromJournal(e);

            // Remove 
            application.CloudService.RegisterCompleted -= viewModel.CloudService_RegisterCompleted;
        }
    }
}