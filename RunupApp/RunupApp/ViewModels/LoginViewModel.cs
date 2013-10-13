using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// ViewModel to handle the LoginView
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        /// <summary>
        /// Currently user from email/password input - New empty one upon creation
        /// </summary>
        private Users user = new Users();

        /// <summary>
        /// Current app instance for initiating code.
        /// </summary>
        private App application = Application.Current as App;

        /// <summary>
        /// Current email from the user object
        /// </summary>
        public string Email
        {
            get 
            {
                return user.Email; 
            }
            set
            {
                user.Email = value;
                //Databind notify
                NotifyPropertyChanged("Email");
            }
        }

        /// <summary>
        /// Current password from the user object
        /// </summary>
        public string Password
        {
            get 
            {
                return user.Password; 
            }
            set
            {
                user.Password = value;
                //Databind notify
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
                //Databind notify
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
                //Databind notify
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
        /// The actual action of the register button command - Navigate to RegisterView
        /// </summary>
        private void RegisterButton()
        {
            (application.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/RegisterView.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Command from view to handle push on login button
        /// </summary>
        public ICommand LoginButtonCommand
        {
            get 
            {
                // Return DelegateCommand to view, that uses the LoginButton function
                return new DelegateCommand(LoginButton); 
            }
        }

        /// <summary>
        /// The actual action of the login button command - Show progress bar, 
        /// reset the message being shown and start call to login on cloud service with current input login data
        /// </summary>
        private void LoginButton()
        {
            //Show progress bar
            Progress = Visibility.Visible;
            //Reset message
            Message = "";
            //External async cloudservice login call
            application.CloudService.LoginAsync(user);
        }

        /// <summary>
        /// Function to handle the callback from cloudservice - hides progress bar, sets logged in user to the
        /// checked one thats been sent to the cloudservice, then navigates to MainPage or if error on the check
        /// show the message on View
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Arguments that the CloudService Login function can return (Results, cancelled etc)</param>
        public void CloudService_LoginCompleted(object sender, Domain.CloudService.LoginCompletedEventArgs e)
        {
            // Remove progress bar visibility
            Progress = Visibility.Collapsed;
            
            // If success, apply new user to whole program and navigate away, else output error to view
            if (e.Result.ToString() == "Success")
            {
                application.User = user;
                if(!App.RunningInBackground)
                    (application.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
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
