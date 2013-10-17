using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RunupApp.ViewModels
{
    /// <summary>
    /// For delegating model calls to functions without parameters.
    /// 
    /// Source: http://www.markwithall.com/programming/2013/03/01/worlds-simplest-csharp-wpf-mvvm-example.html
    /// </summary>
    public class DelegateCommand : ICommand
    {
        // Members
        private readonly Action _action;
        #pragma warning disable 67
        public event EventHandler CanExecuteChanged;
        #pragma warning restore 67

        // Functions
        // :Constructors
        /// <summary>
        /// Sets up the command with the function it should execute.
        /// </summary>
        /// <param name="action">Action to execute.</param>
        public DelegateCommand(Action action)
        {
            _action = action;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
