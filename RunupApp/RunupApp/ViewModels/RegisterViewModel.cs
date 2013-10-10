using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunupApp.ViewModels;
using RunupApp.CloudService;
using System.Windows.Input;
using System.Windows;

namespace RunupApp.ViewModels
{
    class RegisterViewModel : ViewModelBase
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

        public ICommand RegisterCommand
        {
            get { return new DelegateCommand(Register); }
        }

        private void Register()
        {
            App thisApp = Application.Current as App;
            thisApp.CloudService.RegisterAsync(user);
        }
    }
}
