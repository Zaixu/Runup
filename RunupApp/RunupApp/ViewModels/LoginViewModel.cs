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
    class LoginViewModel : ViewModelBase
    {
        private Users user = new Users();

        public string Email
        {
            get { return user.Email; }
            set
            {
                user.Email = value;
                NotifyPropertyChanged("Email");
            }
        }

        public string Password
        {
            get { return user.Password; }
            set
            {
                user.Password = value;
                NotifyPropertyChanged("Password");
            }
        }

        public ICommand LoginCommand
        {
            get { return new DelegateCommand(Login); }
        }

        private void Login()
        {
            App thisApp = Application.Current as App;
            thisApp.CloudService.LoginAsync(user);
        }
    }   
}
