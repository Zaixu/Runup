using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RunupApp.ViewModels
{
    /// <summary>
    /// For commands which need arguments for execution.
    /// </summary>
    /// <typeparam name="T">Type of parameter for the function called.</typeparam>
    public class RelayCommand<T> : ICommand
    {
        // Fields
        private Action<T> _action;
        #pragma warning disable 67
        public event EventHandler CanExecuteChanged;
        #pragma warning restore 67

        // Functions
        // :Constructors
        /// <summary>
        /// Sets up the command with the function it should execute.
        /// </summary>
        /// <param name="action">Action to execute.</param>
        public RelayCommand(Action<T> action)
        {
            _action = action;
        }

        // :Other
        /// <summary>
        /// Says if the command can run or not.
        /// </summary>
        /// <param name="parameter">Parameter to test.</param>
        /// <returns>Always true.</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">Parameter for the function.</param>
        public void Execute(object parameter)
        {
            _action((T)parameter);
        }
    }
}