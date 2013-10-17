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
    public partial class ExerciseList : PhoneApplicationPage
    {
        // Members
        private ExerciseListViewModel _viewModel;

        public ExerciseList()
        {
            // Setup
            InitializeComponent();

            _viewModel = new ExerciseListViewModel();
            this.DataContext = _viewModel;
        }
    }
}