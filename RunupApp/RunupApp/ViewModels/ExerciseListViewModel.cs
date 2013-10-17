using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Domain.Interfaces;
using Domain.Implementations;

namespace RunupApp.ViewModels
{
    class ExerciseListViewModel : ViewModelBase
    {
        // Members

        // Properties
        public ObservableCollection<IExercise> Exercises
        {
            get;
            set;
        }

        // Functions
        public ExerciseListViewModel()
        {
            // Setup
            Exercises = new ObservableCollection<IExercise>();
        }
    }
}
