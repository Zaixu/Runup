using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunupApp.ViewModels;
using RunupApp.CloudService;
using System.Windows.Input;
using System.Windows;

namespace RunupApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        
        private Users user = new Users();

        public LoginViewModel()
        {

        }

        public string Email
        {
            get 
            {
                return user.Email; 
            }
            set
            {
                user.Email = value;
                NotifyPropertyChanged("Email");
            }
        }

        public string Password
        {
            get 
            {
                return user.Password; 
            }
            set
            {
                user.Password = value;
                NotifyPropertyChanged("Password");
            }
        }

        private string message;
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                NotifyPropertyChanged("Message");
            }
        }

        private Visibility progress = Visibility.Collapsed;
        public Visibility Progress
        {
            get 
            {
                return progress; 
            }
            set
            {
                progress = value;
                NotifyPropertyChanged("Progress");
            }
        }
        
        public ICommand LoginCommand
        {
            get 
            { 
                return new DelegateCommand(Login); 
            }
        }

        private void Login()
        {
            App thisApp = Application.Current as App;
            thisApp.CloudService.LoginAsync(user);
        }
    }   
}
