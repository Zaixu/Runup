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
        public ObservableCollection<IExercise> ExercisesSynced
        {
            get;
            set;
        }

        // Functions
        public ExerciseListViewModel()
        {
            // Setup
            ExercisesSynced = new ObservableCollection<IExercise>();
            // TEST
            ExercisesSynced.Add(new Exercise() { ExerciseStart = DateTime.Now });
            // \TEST
        }
    }
}
