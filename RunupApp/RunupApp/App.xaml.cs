using System;
using System.Diagnostics;
using System.Resources;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RunupApp.Resources;
using Windows.Devices.Geolocation;
using Domain.Interfaces;
using Domain.Implementations;
using Domain.CloudService;
using System.IO.IsolatedStorage;
using System.ServiceModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RunupApp
{
    public partial class App : Application
    {
        private Users user = null;
        
        /// <summary>
        /// Gets/Sets user of application, if its set, update the global app bar text to either login or logout
        /// </summary>
        public Users User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                //Get global app bar from xaml resources
                var appBar = App.Current.Resources["AppBar"] as ApplicationBar;
                //If user is logged in, set app bar button to Logout else login
                if (value == null)
                {
                    ((ApplicationBarIconButton)appBar.Buttons[2]).Text = "Login";
                }
                else
                {
                    ((ApplicationBarIconButton)appBar.Buttons[2]).Text = "Logout";
                }
            }
        }

        /// <summary>
        /// Connection to cloud
        /// </summary>
        public ServiceClient CloudService { get; private set; }

        /// <summary>
        /// To indicate if running in back so can shutoff unnecessary features like UI updating.
        /// </summary>
        public static bool RunningInBackground { get; set; }

        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public static PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Contains all exercises not synced.
        /// </summary>
        public static ObservableCollection<IExercise> NewExercisesStack { get; set; }

        /// <summary>
        /// Contains all exercises synced.
        /// </summary>
        public static ObservableCollection<IExercise> ExercisesSynced { get; set; }

        /// <summary>
        /// Exercise selected in Exercise list.
        /// </summary>
        public static IExercise SelectedExercise { get; set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Couple on cloud
            bool Debug = false;
            if (!Debug)
            {
                CloudService = new ServiceClient();
            }

            // Global handler for uncaught exceptions.
            UnhandledException += Application_UnhandledException;

            // Standard XAML initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Language display initialization
            InitializeLanguage();

            // Show graphics profiling information while debugging.
            if (Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode,
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Prevent the screen from turning off while under the debugger by disabling
                // the application's idle detection.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

            // Set up exercise lists
            NewExercisesStack = new ObservableCollection<IExercise>();
            ExercisesSynced = new ObservableCollection<IExercise>();
        }

        /// Event handlers
        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            LoadUserFromIsolatedStorage();
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            RunningInBackground = false;

            // If its being activated and no instance, its been tombstoned
            if (!e.IsApplicationInstancePreserved)
            {
                // Been tombstoned, reload.
                LoadUserFromStateObject();
            }
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            //Save current user to isolated storage incase of program shutdown
            SaveUserToIsolatedStorage();
            //Save current user to stateobject incase of tombstoning
            SaveUserToStateObject();
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            //General close, save user to isolated storage
            SaveUserToIsolatedStorage();
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If async exception of communication is thrown, handle it and show connection error box
            if (e.ExceptionObject is CommunicationException)
            {
                //Show popup
                MessageBox.Show("Communication problem, please check connection");
                //Make exception handled, so program doesnt shut down
                e.Handled = true;
                return;
            }
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        /// <summary>
        /// To indicate the application has been suspended.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Application_RunningInBackground(object sender, RunningInBackgroundEventArgs args)
        {
            RunningInBackground = true;
            // Suspend all unnecessary processing such as UI updates
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Handle reset requests for clearing the backstack
            RootFrame.Navigated += CheckForResetNavigation;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        private void CheckForResetNavigation(object sender, NavigationEventArgs e)
        {
            // If the app has received a 'reset' navigation, then we need to check
            // on the next navigation to see if the page stack should be reset
            if (e.NavigationMode == NavigationMode.Reset)
                RootFrame.Navigated += ClearBackStackAfterReset;
        }

        private void ClearBackStackAfterReset(object sender, NavigationEventArgs e)
        {
            // Unregister the event so it doesn't get called again
            RootFrame.Navigated -= ClearBackStackAfterReset;

            // Only clear the stack for 'new' (forward) and 'refresh' navigations
            if (e.NavigationMode != NavigationMode.New && e.NavigationMode != NavigationMode.Refresh)
                return;

            // For UI consistency, clear the entire page stack
            while (RootFrame.RemoveBackEntry() != null)
            {
                ; // do nothing
            }
        }

        #endregion

        // Initialize the app's font and flow direction as defined in its localized resource strings.
        //
        // To ensure that the font of your application is aligned with its supported languages and that the
        // FlowDirection for each of those languages follows its traditional direction, ResourceLanguage
        // and ResourceFlowDirection should be initialized in each resx file to match these values with that
        // file's culture. For example:
        //
        // AppResources.es-ES.resx
        //    ResourceLanguage's value should be "es-ES"
        //    ResourceFlowDirection's value should be "LeftToRight"
        //
        // AppResources.ar-SA.resx
        //     ResourceLanguage's value should be "ar-SA"
        //     ResourceFlowDirection's value should be "RightToLeft"
        //
        // For more info on localizing Windows Phone apps see http://go.microsoft.com/fwlink/?LinkId=262072.
        //
        private void InitializeLanguage()
        {
            try
            {
                // Set the font to match the display language defined by the
                // ResourceLanguage resource string for each supported language.
                //
                // Fall back to the font of the neutral language if the Display
                // language of the phone is not supported.
                //
                // If a compiler error is hit then ResourceLanguage is missing from
                // the resource file.
                RootFrame.Language = XmlLanguage.GetLanguage(AppResources.ResourceLanguage);

                // Set the FlowDirection of all elements under the root frame based
                // on the ResourceFlowDirection resource string for each
                // supported language.
                //
                // If a compiler error is hit then ResourceFlowDirection is missing from
                // the resource file.
                FlowDirection flow = (FlowDirection)Enum.Parse(typeof(FlowDirection), AppResources.ResourceFlowDirection);
                RootFrame.FlowDirection = flow;
            }
            catch
            {
                // If an exception is caught here it is most likely due to either
                // ResourceLangauge not being correctly set to a supported language
                // code or ResourceFlowDirection is set to a value other than LeftToRight
                // or RightToLeft.

                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                throw;
            }
        }

        /// <summary>
        /// Click event from the global application bar on the Sync button.
        /// </summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event Arguments.</param>
        private void SyncAppBarButton_Click(object sender, EventArgs e)
        {
            // Setup
            ISyncService syncservice = new SyncService();

            // Save all new exercises
            if (User != null)
            {
                // :Go through each one
                foreach (var exercise in NewExercisesStack)
                {
                    syncservice.SaveExercise(User, exercise, syncCallbackSave);
                }

                // :Remove entries
                NewExercisesStack.Clear();
            }
            else
            {
                // Can later be changed to data binding
                MessageBox.Show("Not logged in");
            }

            // Get exercises synced
            if (User != null)
            {
                // :Call service
                syncservice.GetExercisesLight(User, syncCallbackGet);
            }
            else
            {
                // Can later be changed to data binding
                MessageBox.Show("Not logged in");
            }
        }

        // Description: Callback when sync save part is completed.
        private void syncCallbackSave(string status)
        {
            if(!App.RunningInBackground)
            {
                MessageBox.Show("Sync: " + status);
            }
            else // Gone away from app
            {
                ShellToast msg = new ShellToast();
                msg.Title = "Sync:";
                msg.Content = status;
                msg.Show();
            }
        }

        // Description: Callback when sync getting exercise list is completed.
        private void syncCallbackGet(ICollection<IExercise> exercises)
        {
            if (!App.RunningInBackground)
            {
                ExercisesSynced.Clear();
                foreach (var exercise in exercises)
                    ExercisesSynced.Add(exercise);
                MessageBox.Show("Sync: Retrieved list");
            }
            else // Gone away from app
            {
                ShellToast msg = new ShellToast();
                msg.Title = "Sync:";
                msg.Content = "Retrieved list";
                msg.Show();
            }
        }

        /// <summary>
        /// Go to exercise listing page.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void ExerciseListingAppBarButton_Click(object sender, EventArgs e)
        {
            // Navigate to ExeriseList
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/ExerciseListView.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Click event from the global application bar on the Login/Logout button.
        /// </summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event Arguments.</param>
        private void AuthAppBarButton_Click(object sender, EventArgs e)
        {
            // If user aint null, show popup if he is sure that he wants to logout, if he is, reset logged in use. If user is null, just navigate to LoginView
            if (User != null)
            {
                // Show "Are you sure" box to user
                MessageBoxResult res = MessageBox.Show(User.Email + ", your about to logout.\n\rDo you want to continue?", "Logout", MessageBoxButton.OKCancel);
                // If ok, reset logged in user
                if (res == MessageBoxResult.OK)
                {
                    User = null;
                    return;
                }
            }
            else
            {
                // Navigate to LoginView
                (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/LoginView.xaml", UriKind.Relative));
            }
        }

        /// <summary>
        /// Saves the user to IsolatedStorage, if the credentials are different. Else removes it if not logged in and is existing in IsolatedStorage
        /// </summary>
        private void SaveUserToIsolatedStorage()
        {
            //Get isolatedstorage
            IsolatedStorageSettings isolatedStore = IsolatedStorageSettings.ApplicationSettings;
            //If user isnt null, there might be something to change, else check if there is an existing in isolated storage, incase, delete as we are currently logged out.
            if (User != null)
            {
                //If isolatedstorage have a User saved check it, else just add it
                if (isolatedStore.Contains("User"))
                {
                    Users tempUser;
                    tempUser = (Users)isolatedStore["User"];

                    //Check if the user saved is different, if it is, remove the saved one and add the new one. If its not different, just return
                    if (tempUser.Email == User.Email && tempUser.Password == User.Password)
                        return;
                    else
                    {
                        isolatedStore.Remove("User");
                        isolatedStore.Add("User", User);
                    }
                }
                else
                {
                    isolatedStore.Add("User", User);
                }
            }
            else
            {
                if (isolatedStore.Contains("User"))
                {
                    isolatedStore.Remove("User");
                }
            }
        }

        /// <summary>
        /// Loads a user from IsolatedStorage and puts him as logged in with the application
        /// </summary>
        private void LoadUserFromIsolatedStorage()
        {
            //Get isolatedstorage
            IsolatedStorageSettings isolatedStore = IsolatedStorageSettings.ApplicationSettings;

            Users tempUser;
            //If isolatedstorage contains saved user, get it and apply it to application as logged in
            if (isolatedStore.Contains("User"))
            {
                tempUser = (Users)isolatedStore["User"];
                User = tempUser;
            }
        }

        /// <summary>
        /// Saves a user to state object
        /// </summary>
        private void SaveUserToStateObject()
        {
            //Get stateobject
            IDictionary<string, object> stateStore = PhoneApplicationService.Current.State;
            //Remove existing state if any
            stateStore.Remove("User");
            //Add new user state
            stateStore.Add("User", User);
        }

        /// <summary>
        /// Loads a user from state object
        /// </summary>
        private void LoadUserFromStateObject()
        {
            //Get stateobject
            IDictionary<string, object> stateStore = PhoneApplicationService.Current.State;
            //If existing user in state, set application user to that
            if (stateStore.ContainsKey("User"))
                User = (Users)stateStore["User"];
        }
    }
}