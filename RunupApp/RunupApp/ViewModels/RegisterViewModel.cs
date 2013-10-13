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
using Microsoft.Phone.Shell;

namespace RunupApp.ViewModels
{
    /// <summary>
    /// ViewModel to handle the RegisterView
    /// </summary>
    public class RegisterViewModel : ViewModelBase
    {
        /// <summary>
        /// Current user made from view email/password fields, new class upon creation
        /// </summary>
        private Users user = new Users();

        /// <summary>
        /// Current app instance for initiating code.
        /// </summary>
        private App application = Application.Current as App;

        /// <summary>
        /// Current email from the user object accessible from binded view
        /// </summary>
        public string Email
        {
            get { return user.Email; }
            set
            {
                user.Email = value;
                //Databinding notify
                NotifyPropertyChanged("Email");
            }
        }

        /// <summary>
        /// Current password from the user object, accessible from binded view
        /// </summary>
        public string Password
        {
            get { return user.Password; }
            set
            {
                user.Password = value;
                //Databinding notify
                NotifyPropertyChanged("Password");
            }
        }

        /// <summary>
        /// Current message being showed on screen
        /// </summary>
        private string message;

        /// <summary>
        /// Get/Set current message being shown on screen, data binded with notifying
        /// </summary>
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                //Databinding notify
                NotifyPropertyChanged("Message");
            }
        }

        /// <summary>
        /// Current status on the progress bar, whether its showing or not
        /// </summary>
        private Visibility progress = Visibility.Collapsed;

        /// <summary>
        /// Get/Set progress bar visibility on view - data binded with notifying
        /// </summary>
        public Visibility Progress
        {
            get
            {
                return progress;
            }
            set
            {
                progress = value;
                //Databinding notify
                NotifyPropertyChanged("Progress");
            }
        }

        /// <summary>
        /// Command from view to handle click on register button
        /// </summary>
        public ICommand RegisterButtonCommand
        {
            get
            {
                //Return DelegateCommand to view, that uses the RegisterButton function
                return new DelegateCommand(RegisterButton);
            }
        }

        /// <summary>
        /// The actual action of the register button command - Show progress bar, reset view message
        /// and start async cloudservice call to register user
        /// </summary>
        private void RegisterButton()
        {
            //Show progress bar
            Progress = Visibility.Visible;
            //Reset message
            Message = "";
            //Start async call to cloudservice to register user
            application.CloudService.RegisterAsync(user);
        }

        /// <summary>
        /// Command from view to handle click on login button
        /// </summary>
        public ICommand LoginButtonCommand
        {
            get
            {
                //Return DelegateCommand to view, that uses the LoginButton function
                return new DelegateCommand(LoginButton);
            }
        }

        /// <summary>
        /// The actual action of the login button command - Navigate to LoginView
        /// </summary>
        private void LoginButton()
        {
            (application.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/LoginView.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Function to handle the callback from cloudservice - hides progress bar, if its been a success, 
        /// navigates to LoginView else output error to view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CloudService_RegisterCompleted(object sender, Domain.CloudService.RegisterCompletedEventArgs e)
        {
            //Hide progress bar
            Progress = Visibility.Collapsed;

            //If success, navigate to LoginView else output error to view
            if (e.Result.ToString() == "Success")
            {
                if(!App.RunningInBackground)
                    (application.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/LoginView.xaml", UriKind.Relative));
            }
            else
            {
                Message = e.Result.ToString();
            }

            if (App.RunningInBackground)
            {
                ShellToast msg = new ShellToast();
                msg.Title = "Login:";
                msg.Content = e.Result.ToString();
                msg.Show();
            }
        }
    }
}
