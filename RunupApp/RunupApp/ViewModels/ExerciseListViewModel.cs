using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Domain.Interfaces;
using Domain.Implementations;
using System.Windows.Input;

namespace RunupApp.ViewModels
{
    public class ExerciseListViewModel : ViewModelBase
    {
        // Members

        // Properties
        public ObservableCollection<IExercise> ExercisesSynced
        {
            get;
            set;
        }

        public ICommand ShowExercise
        {
            get
            {
                return new RelayCommand<int>(_ShowExercise);
            }
        }

        // Functions
        // :Constructors
        public ExerciseListViewModel()
        {
            // Setup
            ExercisesSynced = new ObservableCollection<IExercise>();
            // TEST
            ExercisesSynced.Add(new Exercise() { ExerciseStart = DateTime.Now, ID = 1});
            NotifyPropertyChanged("ExercisesSynced");
            // \TEST
        }

        // :Commands
        /// <summary>
        /// 
        /// </summary>
        private void _ShowExercise(int ID)
        {

        }
    }
}
