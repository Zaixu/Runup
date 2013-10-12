using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunupApp.ViewModels;
using Domain.CloudService;
using System.Windows.Input;
using System.Windows;
using Microsoft.Phone.Controls;

namespace RunupApp.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private Users user = new Users();
        private App application = Application.Current as App;

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


        public ICommand RegisterButtonCommand
        {
            get
            {
                return new DelegateCommand(RegisterButton);
            }
        }

        private void RegisterButton()
        {
            Progress = Visibility.Visible;
            Message = "";
            application.CloudService.RegisterAsync(user);
        }

        public ICommand LoginButtonCommand
        {
            get
            {
                return new DelegateCommand(LoginButton);
            }
        }

        private void LoginButton()
        {
            PhoneApplicationFrame frame = application.RootVisual as PhoneApplicationFrame;
            frame.Navigate(new Uri("/Views/LoginView.xaml", UriKind.Relative));
        }

        public void CloudService_RegisterCompleted(object sender, Domain.CloudService.RegisterCompletedEventArgs e)
        {
            Progress = Visibility.Collapsed;

            if (e.Result.ToString() == "Success")
            {
                PhoneApplicationFrame frame = application.RootVisual as PhoneApplicationFrame;
                frame.Navigate(new Uri("/Views/LoginView.xaml", UriKind.Relative));
            }
            else
            {
                Message = e.Result.ToString();
            }
        }
    }
}
