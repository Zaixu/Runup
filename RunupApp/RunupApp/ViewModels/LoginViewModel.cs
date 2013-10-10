using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunupApp.CloudService;
using RunupApp.ViewModels;
using System.Windows.Input;

namespace RunupApp.ViewModel
{
    class LoginViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Users user = new Users();

        public string Email
        {
            get { return user.Email; }
            set
            {
                user.Email = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Email"));
                }
            }
        }

        public string Password
        {
            get { return user.Password; }
            set
            {
                user.Password = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Password"));
                }
            }
        }

        public ICommand LoginCommand
        {
            get { return new DelegateCommand(Login); }
        }

        private void Login()
        {
            
            
        }
    }   
}
