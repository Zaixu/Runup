using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Domain.Interfaces;
using Domain.Implementations;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows;

namespace RunupApp.ViewModels
{
    public class ExerciseListViewModel : ViewModelBase
    {
        // Members
        /// <summary>
        /// Current running app.
        /// </summary>
        private App _application = Application.Current as App;

        // Properties
        /// <summary>
        /// New exercises which have not been synced.
        /// </summary>
        public ObservableCollection<IExercise> ExercisesNew
        {
            get
            {
                return (App.NewExercisesStack);
            }
            private set
            {
                // Nothing
            }
        }

        /// <summary>
        /// Exercises which have been synced. 
        /// </summary>
        public ObservableCollection<IExercise> ExercisesSynced
        {
            get;
            set;
        }

        /// <summary>
        /// Show exercise in detailed view.
        /// </summary>
        public ICommand ShowNonSyncedExercise
        {
            get
            {
                return new RelayCommand<DateTime>(_ShowNonSyncedExercise);
            }
        }

        /// <summary>
        /// Show exercise in detailed view.
        /// </summary>
        public ICommand ShowSyncedExercise
        {
            get
            {
                return new RelayCommand<int>(_ShowSyncedExercise);
            }
        }

        // Functions
        // :Constructors
        public ExerciseListViewModel()
        {
            // Setup
            ExercisesSynced = new ObservableCollection<IExercise>();
        }

        // :Commands
        /// <summary>
        /// Show exercise in detailed vied.
        /// </summary>
        private void _ShowNonSyncedExercise(DateTime startTime)
        {
            // Setup
            IExercise exercise = App.NewExercisesStack.Where(x => x.ExerciseStart == startTime).FirstOrDefault();
            App.SelectedExercise = exercise;

            // Navigate
            (_application.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/ExerciseDetailedView.xaml", UriKind.Relative));
        }

        private void _ShowSyncedExercise(int ID)
        {

        }
    }
}
